using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;
public class TodoItem {
  public Guid Id { get; set; }
  public string? Name { get; set; }
  public DateTime CreationTime { get; set; }
  public bool IsComplete { get; set; }
  public Guid? CategoryId { get; set; }
  public Category? Category { get; set; }

  public TodoItem() 
  {
    this.Id = Guid.NewGuid(); 
    this.CreationTime = DateTime.Now;
  }
}