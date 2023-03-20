using blazor_app.Pages;

namespace blazor_app.Data;

public class TodoService
{
    private Dictionary<int, Todo> data = new();

    public TodoService()
    {
        data[1] = new Todo() {Id = 1, Title = "Pierwsze", Deadline = DateTime.Now, IsDone = false};
        data[2] = new Todo() {Id = 2, Title = "Drugie", Deadline = DateTime.Now, IsDone = true};
        data[3] = new Todo() {Id = 3, Title = "Trzecie", Deadline = DateTime.Now, IsDone = false};
        data[4] = new Todo() {Id = 4, Title = "Czwartee", Deadline = DateTime.Now, IsDone = false};
    }

    public List<Todo> FindAll()
    {
        return data.Values.ToList();
    }

    public void AddTodo(Todo item)
    {
        data[item.Id] = item;
    }

    public void RemoveTodo(int id)
    {
        data.Remove(id);
    }
}