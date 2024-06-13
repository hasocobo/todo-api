using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TodoApi.Models;

public class TodoContext(DbContextOptions<TodoContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<TodoItem>()
            .HasOne<Category>()
            .WithMany()
            .HasForeignKey(e => e.CategoryId);
    }

}