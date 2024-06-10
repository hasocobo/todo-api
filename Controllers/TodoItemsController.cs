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
using TodoApi.Interfaces;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*[Authorize]*/
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoService _todoService;
        public TodoItemsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/TodoItems

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemReadDto>>> GetTodoItems()
        {
            var todoItems = await _todoService.GetTodoItems();

            return Ok(todoItems);
        }


        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemReadDto>> GetTodoItem(Guid id)
        {
            var todoItem = await _todoService.GetTodoItemById(id);

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

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult> PostTodoItem(TodoItemCreateDto todoItemCreateDto)
        {
            var todoItem = new TodoItem
            {
                Name = todoItemCreateDto.Name,
                IsComplete = todoItemCreateDto.IsComplete
            };

            await _todoService.CreateTodoItem(todoItem);

            await _todoService.SaveChangesAsync();

            return CreatedAtAction(nameof(PostTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(Guid id, TodoItemCreateDto todoItemCreateDto)
        {
            var existingTodoItem = await _todoService.GetTodoItemById(id);

            if (existingTodoItem == null)
            {
                return NotFound();
            }

            existingTodoItem.Name = todoItemCreateDto.Name;
            existingTodoItem.IsComplete = todoItemCreateDto.IsComplete;

            try
            {
                await _todoService.UpdateTodoItem(existingTodoItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_todoService.TodoItemExists(id))
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


        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(Guid id)
        {
            var success = await _todoService.DeleteTodoItem(id);

            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
