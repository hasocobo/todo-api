using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TodoApi.Models;

public class TodoContext(DbContextOptions<TodoContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}