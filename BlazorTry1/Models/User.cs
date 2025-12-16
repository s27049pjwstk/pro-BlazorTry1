using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorTry1.Models;

public class User {
    public int Id { get; set; }

    [Required, MaxLength(32)]
    public string Name { get; set; } = null!;

    [MaxLength(32)]
    public string? DiscordId { get; set; }

    [MaxLength(17)]
    public string? SteamId { get; set; }

    public DateTime DateJoined { get; set; }

    [MaxLength(1000)]
    public string? Note { get; set; }

    public bool Active { get; set; }

    public int? RankId { get; set; }
    public Rank? Rank { get; set; }

    public ICollection<RankLog> RankLogs { get; set; } = new List<RankLog>();
    public ICollection<LeaveOfAbsence> LeaveOfAbsences { get; set; } = new List<LeaveOfAbsence>();
    public ICollection<StatusLog> StatusLogs { get; set; } = new List<StatusLog>();
    public ICollection<UserCertification> UserCertifications { get; set; } = new List<UserCertification>();
    public ICollection<UserAward> UserAwards { get; set; } = new List<UserAward>();

    [NotMapped]
    public bool Status =>
        Active &&
        !isOnLeaveOfAbsence();

    private bool isOnLeaveOfAbsence() {
        var now = DateTime.UtcNow;
        return LeaveOfAbsences.Any(l => l.DateStart <= now && l.DateEnd >= now);
    }
}