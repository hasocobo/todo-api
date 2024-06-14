namespace TodoApi.DTOs;

public class TodoItemReadDto {
  public Guid Id { get; set;}
  public string? Name { get; set; }
  public bool IsComplete { get; set; } 

  public string? CategoryName { get; set; }

  public DateTime CreationTime { get; set; }

  public Guid? CategoryId { get; set; }
}