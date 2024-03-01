namespace Mission08_Section4_3.Models
{
    public interface ITodoRepository
    {
        List<Todo> Todos { get; }

        public void AddTodo(Todo todo);
    }
}
