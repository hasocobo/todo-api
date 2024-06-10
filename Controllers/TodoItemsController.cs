using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.DTOs;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*[Authorize]*/
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemReadDto>>> GetTodoItems()
        {
            var todoItems = await _context.TodoItems
                .Select(item => new TodoItemReadDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsComplete = item.IsComplete
                }).ToListAsync();
            
            return todoItems;
        }


        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemReadDto>> GetTodoItem(Guid id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            var todoItemReadDto = new TodoItemReadDto
            {
                Id = todoItem.Id,
                IsComplete = todoItem.IsComplete,
                Name = todoItem.Name
            };

            return todoItemReadDto;
        }

        [HttpPost]
        public async Task<ActionResult> PostTodoItem(TodoItemCreateDto todoItemCreateDto)
        {
            var todoItem = new TodoItem
            {
                Name = todoItemCreateDto.Name,
                IsComplete = todoItemCreateDto.IsComplete
            };

            await _context.TodoItems.AddAsync(todoItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItemCreateDto todoItemCreateDto)
        {
            var existingTodoItem = await _context.TodoItems.FindAsync(id);

            if (existingTodoItem == null)
            {
                return NotFound();
            }

            existingTodoItem.Name = todoItemCreateDto.Name;
            existingTodoItem.IsComplete = todoItemCreateDto.IsComplete;

            _context.Entry(existingTodoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754


        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return Ok($"{todoItem.Name} with Id: {todoItem.Id} deleted successfully");
        }
        

        private bool TodoItemExists(Guid id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
