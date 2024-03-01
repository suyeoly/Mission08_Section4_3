
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mission08_Section4_3.Models
{
    public class EFTodoRepository : ITodoRepository
    {
        private TodosContext _context;
        public EFTodoRepository(TodosContext temp) 
        {
            _context = temp;
        }

        public List<Todo> Todos => _context.Todos.ToList();

        public void AddTodo(Todo todo)
        {
            _context.Add(todo);
            _context.SaveChanges();
        }
        public void RemoveTodo(Todo todo) 
        {
            _context.Remove(todo);
            _context.SaveChanges();
        }
        public void UpdateTodo(Todo todo)
        {
            _context.Update(todo);
            _context.SaveChanges();
        }

        // Make up Classes ***I am not sure about these below
        public void GetIncompleteTodosWithCategory()   
        {
            var tasks = _context.Todos.Include(t => t.Category)
                .Where(t => t.Completed == 0)
                .ToList();
        }
        public Todo GetTodoById(Todo taskId)
        {
            return _context.Todos
                .FirstOrDefault(t => t.TaskId == taskId);
        }
        public void ToggleCompletionStatus(int taskId)
        {
            var task = _context.Todos.FirstOrDefault(t => t.TaskId == taskId);
            if (task != null)
            {
                task.Completed = task.Completed == 0 ? 1 : 0;
                _context.SaveChanges();
                return RedirectToAction("Quadrants");
            }
        }
    }
}
