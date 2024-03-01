using Microsoft.AspNetCore.Mvc;
using Mission08_Section4_3.Models;
using System.Diagnostics;

namespace Mission08_Section4_3.Controllers
{
    public class HomeController : Controller
    {
        private ITodoRepository _repo;
        public HomeController(ITodoRepository temp) 
        {
            _repo = temp;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Todo());
        }
        [HttpPost]
        public IActionResult Create(Todo t) 
        {
            if (ModelState.IsValid)
            {
                _repo.AddTodo(t);
            }
                return View(new Todo());
        }
    }
}
