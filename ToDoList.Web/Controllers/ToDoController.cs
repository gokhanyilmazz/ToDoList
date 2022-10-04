using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Web.Controllers
{
    public class ToDoController : Controller
    {
        private readonly ToDoListContext db;

        public ToDoController(ToDoListContext _db)
        {
            db = _db;
        }

        [Authorize]
        public IActionResult Index(int userId)
        {
            List<ToDo> list = db.ToDos.Where(t=>t.StatusId==1).Join(db.ToDoUserRels.Where(r=>r.UserId==userId), t => t.Id, r => r.ToDoId,
                (todo, rel) => new ToDo
                {
                    Description = todo.Description,
                    DateCreated = todo.DateCreated,
                    EndDate = todo.EndDate,
                    Id = todo.Id,
                    IsActive = todo.IsActive,
                    IsDeleted = todo.IsDeleted,
                    Status = todo.Status,
                    StatusId = todo.StatusId,

                }

                ).ToList<ToDo>();

            return View(list);
        }

        [Authorize]
        public IActionResult Complete(int toDoId)
        {
            //gelen id'yi kullanarak, veritabanında, ilgili todo kaydının StatusID'sini 2 (tamamlandı) yapalım.
            ToDo toDo = db.ToDos.Find(toDoId);
            toDo.StatusId = 2;
            db.ToDos.Update(toDo);
            db.SaveChanges();

            return RedirectToAction("Index", new { userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value });


        }

        [HttpPost]
        public string CompleteAjax(ToDo todo)
        {
            ToDo old= db.ToDos.AsNoTracking().First(t=>t.Id==todo.Id);
            //gelen id'yi kullanarak, veritabanında, ilgili todo kaydının StatusID'sini 2 (tamamlandı) yapalım.

            todo.StatusId = 2;
            todo.Description = old.Description;
            todo.DateCreated = old.DateCreated;
            todo.IsDeleted = old.IsDeleted;
            todo.EndDate = old.EndDate;
            todo.IsActive = old.IsActive;
            db.ToDos.Update(todo);
            db.SaveChanges();

            return "Başarılı";



        }



        [HttpPost]
        public string Add(ToDo todo)
        {
            try
            {
                db.ToDos.Add(todo);
                db.SaveChanges();
                //son kaydolan Todo nesnesinin db deki Id sine ulaşmak için bunu yapıyoruz
                todo = db.ToDos.OrderBy(t => t.Id).Last();
                db.ToDoUserRels.Add(new ToDoUserRel { ToDoId = todo.Id, UserId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value) });
                db.SaveChanges();

                return "İşlem Başarılı";
            }
            catch (Exception e)
            {
                return e.Message;
            }


        }


        [HttpPost]
        public IActionResult GetAllByUserId(int userId)
        {
            List<ToDo> list = db.ToDos.Where(t => t.StatusId == 1).Join(db.ToDoUserRels.Where(r => r.UserId == userId), t => t.Id, r => r.ToDoId,
                (todo, rel) => new ToDo
                {
                    Description = todo.Description,
                    DateCreated = todo.DateCreated,
                    EndDate = todo.EndDate,
                    Id = todo.Id,
                    IsActive = todo.IsActive,
                    IsDeleted = todo.IsDeleted,
                    Status = todo.Status,
                    StatusId = todo.StatusId,

                }

                ).ToList<ToDo>();

            return Json(list);
        }



    }
}
