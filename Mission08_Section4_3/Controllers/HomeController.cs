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
            _repo.GetIncompleteTodosWithCategory(); // Not sure about this

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _context.Todos
                .Single(x => x.TaskId == id);
            ViewBag.Category = _context.Category.ToList();
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

            var task = _context.Todos.FirstOrDefault(t => t.TaskId == taskId);
            if (task != null)
            {
                task.Completed = !task.Completed; // Toggle the completion status
                _context.SaveChanges();
                return RedirectToAction("Quadrants");
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _context.Todos
                .Single(x => x.TaskId == id);
            return View(recordToDelete);
        }

        [HttpPost]
        public IActionResult Delete(Todo updatedTask)
        {
            _repo.RemoveTodo(updatedTask); //Good
            return RedirectToAction("Quadrants");
        }
    }
}
