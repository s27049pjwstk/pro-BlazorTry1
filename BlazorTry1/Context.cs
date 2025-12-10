using BlazorTry1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorTry1;

public class Context : DbContext {
    public Context() {}
    public Context(DbContextOptions options) : base(options) {}
    public DbSet<User> Users => Set<User>();
    public DbSet<Rank> Ranks => Set<Rank>();

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
        modelBuilder.Entity<Rank>(e => {
            e.HasKey(r => r.Id);
            e.HasIndex(r => r.Name).IsUnique();
            e.HasIndex(r => r.Code).IsUnique();
            e.HasIndex(r => r.SortOrder).IsUnique();
            e.HasMany(r => r.Users)
                .WithOne(u => u.Rank)
                .HasForeignKey(u => u.RankId);
        });
    }
}