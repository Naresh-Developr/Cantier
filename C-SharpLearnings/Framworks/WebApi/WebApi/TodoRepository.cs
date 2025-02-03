namespace WebApi
{
    public class TodoRepository
    {
        private readonly List<TodoItem> _todos = new();
        private int _nextId = 1;

        public IEnumerable<TodoItem> GetAll() => _todos;
        public TodoItem? GetById(int id) => _todos.FirstOrDefault(t => t.Id == id);
        public void Add(TodoItem item) { item.Id = _nextId++; _todos.Add(item); }
        public bool Update(TodoItem item)
        {
            var index = _todos.FindIndex(t => t.Id == item.Id);
            if (index == -1) return false;
            _todos[index] = item;
            return true;
        }
        public bool Delete(int id) => _todos.RemoveAll(t => t.Id == id) > 0;
    }
}