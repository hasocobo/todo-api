using TodoApi.Models;
using TodoApi.DTOs;

namespace TodoApi.Interfaces;

public interface ITodoService {
  public Task<IEnumerable<TodoItemReadDto>> GetTodoItems();
  public Task<TodoItemReadDto> GetTodoItemById(Guid id);
  public Task<TodoItemCreateDto> CreateTodoItem(TodoItemCreateDto todoItemCreateDto);
  public Task<TodoItemCreateDto> UpdateTodoItem(Guid id, TodoItemCreateDto todoItemCreateDto);
  public Task<bool> DeleteTodoItem(Guid id);

}