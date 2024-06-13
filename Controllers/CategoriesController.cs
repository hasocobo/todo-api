using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase 
{
  private readonly ITodoService _todoService;
  public CategoriesController(ITodoService todoService) 
  {
    _todoService = todoService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAllCategories() 
  {
    var categories = await _todoService.GetCategories();
    return Ok(categories);
  }

  [HttpGet("{name}")]
  public async Task<IActionResult> GetCategory(string name) 
  {
    var category = await _todoService.GetCategoryByName(name);

    return Ok(category);
  }
  
  [HttpPost]
  public async Task<IActionResult> PostCategory(CategoryCreateDto categoryCreateDto)
  {
    var category = new Category {
      Name = categoryCreateDto.Name,
    };
    await _todoService.CreateCategory(category);
    return CreatedAtAction(nameof(PostCategory), new { Id = category.Id }, category);
  }
}