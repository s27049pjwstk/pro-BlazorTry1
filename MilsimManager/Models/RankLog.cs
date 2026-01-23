using System.ComponentModel.DataAnnotations;

namespace MilsimManager.Models;

public class RankLog {
    public int Id { get; set; }

    [Timestamp]
    public uint Version { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int? RankId { get; set; }
    public Rank? Rank { get; set; }

    [Required, MaxLength(50)]
    public string RankName { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public int? ApprovedById { get; set; }
    public User? ApprovedBy { get; set; }

    [MaxLength(32)]
    public string ApprovedByName { get; set; } = "System";
}