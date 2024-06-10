using Microsoft.EntityFrameworkCore;
using TodoApi.DTOs;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly TodoContext _context;

    public TodoService(TodoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreateTodoItem(TodoItem todoItem)
    {
        if (todoItem == null)
        {
            throw new ArgumentNullException(nameof(todoItem));
        }

        await _context.TodoItems.AddAsync(todoItem);
        await SaveChangesAsync();
    }

    public async Task<bool> DeleteTodoItem(Guid id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return false;
        }

        _context.TodoItems.Remove(todoItem);
        await SaveChangesAsync();
        return true;
    }

    public async Task<TodoItem?> GetTodoItemById(Guid id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task<IEnumerable<TodoItemReadDto>> GetTodoItems()
    {
        return await _context.TodoItems
            .Select(item => new TodoItemReadDto
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            }).ToListAsync();
    }

    public async Task UpdateTodoItem(TodoItem todoItem)
    {
        _context.Entry(todoItem).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public bool TodoItemExists(Guid id)
    {
        return _context.TodoItems.Any(e => e.Id == id);
    }
}
