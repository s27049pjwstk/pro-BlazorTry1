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
    public DbSet<StatusLog> StatusLogs => Set<StatusLog>();
    public DbSet<Certification> Certifications => Set<Certification>();

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
            e.HasKey(r => r.Id);
            e.HasIndex(r => r.UserId);
            e.Property(r => r.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(r => r.User)
                .WithMany(u => u.RankLogs)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(r => r.Rank)
                .WithMany()
                .HasForeignKey(r => r.RankId)
                .OnDelete(DeleteBehavior.SetNull);
            e.HasOne(r => r.ApprovedBy)
                .WithMany()
                .HasForeignKey(r => r.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<LeaveOfAbsence>(e => {
            e.HasKey(l => l.Id);
            e.HasIndex(l => l.UserId);
            e.Property(l => l.DateStart)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(l => l.User)
                .WithMany(u => u.LeaveOfAbsences)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<StatusLog>(e => {
            e.HasKey(s => s.Id);
            e.HasIndex(s => s.UserId);
            e.Property(s => s.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(s => s.User)
                .WithMany(u => u.StatusLogs)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(s => s.ApprovedBy)
                .WithMany()
                .HasForeignKey(s => s.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<Certification>(e => {
            e.HasKey(c => c.Id);
            e.HasIndex(c => c.Name).IsUnique();
        });
    }
}