namespace BlazorTry1.Models;

using Microsoft.EntityFrameworkCore;

public class Context : DbContext {
    public Context() {}
    public Context(DbContextOptions options) : base(options) {}
    public DbSet<User> Users => Set<User>();
    public DbSet<Rank> Ranks => Set<Rank>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite("data source=database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Rank>(opt => {
            opt.HasKey(r => r.Id);
            opt.Property(r => r.Name).IsRequired();
        });
    }
}