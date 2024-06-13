using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class Category
 {

  public Category()
  {
    this.Id = Guid.NewGuid();
  }

  public Guid Id { get; set; }

  public required string Name { get; set; }

  public ICollection<TodoItem>? TodoItems { get; } = new List<TodoItem>();

}