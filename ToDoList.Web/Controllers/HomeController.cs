using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Data;
namespace ToDoList.Web.Controllers
{
    public class HomeController : Controller
    {



        [Authorize(Roles ="0,1")]
        public IActionResult Index()
        {
           
            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

      
    }
}