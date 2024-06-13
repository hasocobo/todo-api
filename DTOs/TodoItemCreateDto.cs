namespace TodoApi.DTOs;

public class TodoItemCreateDto {
  public string? Name { get; set; }
  public bool IsComplete { get; set; } 

  public required string CategoryName { get; set; }
}