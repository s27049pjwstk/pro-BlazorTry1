namespace BlazorTry1.Models;

using Microsoft.EntityFrameworkCore;

public class Context : DbContext {
    public DbSet<User> Users => Set<User>();
    public DbSet<Rank> Ranks => Set<Rank>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite("data source=database.db");
    }
}