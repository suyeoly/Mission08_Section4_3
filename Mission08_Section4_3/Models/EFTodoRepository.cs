
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
    }
}
