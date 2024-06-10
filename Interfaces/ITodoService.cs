using TodoApi.Models;
using TodoApi.DTOs;

namespace TodoApi.Interfaces;

public interface ITodoService {
  public Task<IEnumerable<TodoItemReadDto>> GetTodoItems();
  public Task<TodoItem?> GetTodoItemById(Guid id);
  public Task CreateTodoItem(TodoItem todoItem);
  public Task UpdateTodoItem(TodoItem todoItem);
  public Task<bool> DeleteTodoItem(Guid id);

  public Task SaveChangesAsync();

  public bool TodoItemExists(Guid id);

}