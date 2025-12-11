using BlazorTry1.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorTry1;

public class Context : DbContext {
    public Context() {}
    public Context(DbContextOptions options) : base(options) {}
    public DbSet<User> Users => Set<User>();
    public DbSet<Rank> Ranks => Set<Rank>();
    public DbSet<RankLog> RankLogs => Set<RankLog>();
    public DbSet<LeaveOfAbsence> LeaveOfAbsences => Set<LeaveOfAbsence>();

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
                .HasForeignKey(u => u.RankId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<RankLog>(e => {
            e.HasKey(rl => rl.Id);
            e.Property(rl => rl.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(rl => rl.User)
                .WithMany(u => u.RankLogs)
                .HasForeignKey(rl => rl.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(rl => rl.Rank)
                .WithMany()
                .HasForeignKey(rl => rl.RankId)
                .OnDelete(DeleteBehavior.SetNull);
            e.HasOne(rl => rl.ApprovedBy)
                .WithMany()
                .HasForeignKey(rl => rl.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<LeaveOfAbsence>(e => {
            e.HasKey(l => l.Id);
            e.Property(l => l.DateStart)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(l => l.User)
                .WithMany(u => u.LeaveOfAbsences)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}