using System.ComponentModel.DataAnnotations;

namespace MilsimManager.Models;

public class UserAward {
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    public int AwardId { get; set; }
    public Award Award { get; set; } = null!;

    public DateTime Date { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }

    [Required, MaxLength(32)]
    public string ApprovedByName { get; set; } = "System";
    public int? ApprovedById { get; set; }
    public User? ApprovedBy { get; set; }
}