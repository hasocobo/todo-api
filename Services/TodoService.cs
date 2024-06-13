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
        ArgumentNullException.ThrowIfNull(todoItem);

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

    public async Task<IEnumerable<TodoItem>> GetTodoItems()
    {
        return await _context.TodoItems
            .Include(c => c.Category)
            .ToListAsync();
    }

    public async Task UpdateTodoItem(TodoItem todoItem)
    {
        _context.Entry(todoItem).State = EntityState.Modified;
        await SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        return await _context.Categories
            .Include(c => c.TodoItems)
            .ToListAsync();
    }

    public async Task CreateCategory(Category category)
    {
        await _context.Categories.AddAsync(category);
        await SaveChangesAsync();
    }

    public async Task<Category?> GetCategoryByName(string categoryName)
    {
        return await _context.Categories
            .Include(c => c.TodoItems)
            .SingleOrDefaultAsync(c => c.Name == categoryName);
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
