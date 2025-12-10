using BlazorTry1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorTry1;

public class Context : DbContext {
    public Context() {}
    public Context(DbContextOptions options) : base(options) {}
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite("data source=database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>(e => {
            e.HasKey(u => u.Id);
            e.Property(u => u.DateJoined)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(u => u.Active)
                .ValueGeneratedOnAdd()
                .HasDefaultValue(false);
        });
    }
}