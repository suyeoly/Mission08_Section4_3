using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                return View("Confirmation");
            }
            else
            {
                return View(new Todo());
            }
        }

        //Quadrants 
        public IActionResult Quadrants()
        {
            var tasks = _repo.GetIncompleteTodosWithCategory() ?? new List<Todo>(); // Ensure it's never null

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _repo.GetTodoById(id);
            if (recordToEdit == null)
            {
                return NotFound();
            }
       
            return View("Create", recordToEdit);

        }

        [HttpPost]
        public IActionResult Edit(Todo updatedTask)
        {
            _repo.UpdateTodo(updatedTask); //Good
            return RedirectToAction("quadrants");
        }

        [HttpPost]
        public IActionResult CompletionStatus(int taskId)
        {
            _repo.ToggleCompletionStatus(taskId);

 
            return RedirectToAction("Quadrants");
   
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _repo.GetTodoById(id);
            if (recordToDelete == null) return NotFound();
            return RedirectToAction("Quadrants");
        }

        [HttpPost]
        public IActionResult Delete(Todo updatedTask)
        {
            _repo.RemoveTodo(updatedTask); //Good
            return RedirectToAction("Quadrants");
        }
    }
}
