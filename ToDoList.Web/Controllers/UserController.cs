using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Data;

namespace ToDoList.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ToDoListContext db;
        public UserController(ToDoListContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [Authorize(Roles = "0")]
        public IActionResult Management()
        {
            return View();
        }
        public IActionResult GetAll(bool isActive)
        {
            var list = db.Users.Where(u => u.IsDeleted == false && u.IsActive == isActive)
                .Join(db.UserTypes, u => u.UserTypeId, t => t.Id,
                (users, types) => new
                {
                    fullName = users.FullName,
                    userName = users.UserName,
                    userType = types.Name,
                    id = users.Id
                }

                ).ToList();
            return Json(new { data = list });
        }
        [HttpPost]
        public string Delete(int id)
        {
            try
            {
                Models.User user = db.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
                user.IsDeleted = true;

                db.Users.Update(user);
                db.SaveChanges();

                return "Başarılı";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(Models.User user)
        {

            Models.User isUser = db.Users.FirstOrDefault(u => u.UserName == user.UserName && u.PassWord == user.PassWord && u.IsDeleted == false && u.IsActive == true);
            if (isUser != null)
            {
                List<Claim> userClaims = new List<Claim>();

                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.Id.ToString()));
                userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName));
                userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.FullName));
                
                //admin ise 0
                //user ise 1  gelecek
                userClaims.Add(new Claim(ClaimTypes.Role, isUser.UserTypeId.ToString()));

                var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);


                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = false

                };

                //Eger kullanıcı, loginli oldugu halde, login sayfasına ulaştıysa, karışıklık olmaması için, her loginden evvel, signout (logout) komutunu çalıştırıyoruz ki, temiz bir başlangıç yapalım.
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


                //LOGIN OLMA AŞAMASI
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(user);
            }
        }
        [HttpPost]
        public string Add(Models.User user)
        {
            if (user != null)
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return db.Users.ToList().Last().Id.ToString();
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return "HATA : Nesnenin içi boş";
        }
        public IActionResult GetUserById(int userId)
        {
            return Json(db.Users.Find(userId));
        }

        [HttpPost]
        public string Update(Models.User user)
        {
            Models.User old = db.Users.AsNoTracking().FirstOrDefault(u => u.Id == user.Id);
            user.DateCreated = old.DateCreated;
            user.IsActive = old.IsActive;
            user.IsDeleted = old.IsDeleted;
            user.ProfilePicture = old.ProfilePicture;

            db.Users.Update(user);
            db.SaveChanges();
            return "Başarılı";
        }
    }
}
