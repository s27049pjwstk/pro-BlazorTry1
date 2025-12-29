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
    public DbSet<UserCertification> UserCertifications => Set<UserCertification>();
    public DbSet<Award> Awards => Set<Award>();
    public DbSet<UserAward> UserAwards => Set<UserAward>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<UnitAssignmentLog> UnitAssignmentLogs => Set<UnitAssignmentLog>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseSqlite("data source=database.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>(e => {
            e.HasKey(u => u.Id);
            e.HasIndex(u => u.Name).IsUnique();
            e.HasIndex(u => u.DiscordId).IsUnique();
            e.HasIndex(u => u.SteamId).IsUnique();
            e.HasIndex(u => u.RankId);
            e.HasIndex(u => u.UnitId);
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
        modelBuilder.Entity<UserCertification>(e => {
            e.HasKey(uc => new { uc.UserId, uc.CertificationId });
            e.HasIndex(uc => uc.UserId);
            e.HasIndex(uc => uc.CertificationId);
            e.Property(uc => uc.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(uc => uc.User)
                .WithMany(u => u.UserCertifications)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(uc => uc.Certification)
                .WithMany(c => c.UserCertifications)
                .HasForeignKey(uc => uc.CertificationId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(uc => uc.ApprovedBy)
                .WithMany()
                .HasForeignKey(uc => uc.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<Award>(e => {
            e.HasKey(a => a.Id);
            e.HasIndex(a => a.Name).IsUnique();
        });
        modelBuilder.Entity<UserAward>(e => {
            e.HasKey(ua => ua.Id);
            e.HasIndex(ua => ua.UserId);
            e.HasIndex(ua => ua.AwardId);
            e.Property(ua => ua.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(ua => ua.User)
                .WithMany(u => u.UserAwards)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ua => ua.Award)
                .WithMany(a => a.UserAwards)
                .HasForeignKey(ua => ua.AwardId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ua => ua.ApprovedBy)
                .WithMany()
                .HasForeignKey(ua => ua.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<Unit>(e => {
            e.HasKey(u => u.Id);
            e.HasIndex(u => u.Name).IsUnique();
            e.HasIndex(u => u.Abbreviation).IsUnique();
            e.HasMany(u => u.Users)
                .WithOne(u => u.Unit)
                .HasForeignKey(u => u.UnitId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<UnitAssignmentLog>(e => {
            e.HasKey(l => l.Id);
            e.HasIndex(l => l.UserId);
            e.HasIndex(l => l.UnitId);
            e.Property(l => l.Date)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(l => l.User)
                .WithMany(u => u.UnitAssignmentLogs)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(l => l.Unit)
                .WithMany()
                .HasForeignKey(l => l.UnitId)
                .OnDelete(DeleteBehavior.SetNull);
            e.HasOne(l => l.ApprovedBy)
                .WithMany()
                .HasForeignKey(l => l.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}