namespace TodoApi.DTOs;

public class TodoItemReadDto {
  public Guid Id { get; set;}
  public string? Name { get; set; }
  public bool IsComplete { get; set; } 
}