using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;

namespace ToDoList.Web.Controllers
{
    public class UserTypeController : Controller
    {
        //Dependency injection ile contextimizi aldık.
        private readonly ToDoListContext db;
        public UserTypeController(ToDoListContext _db)
        {
            db= _db;
        }


     
        public IActionResult GetAll()
        {

            return Json(db.UserTypes.ToList());
        }
    }
}
