using System.ComponentModel.DataAnnotations;

namespace MilsimManager.Models;

public class UnitAssignmentLog {
    public int Id { get; set; }

    [Timestamp]
    public uint Version { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int? UnitId { get; set; }
    public Unit? Unit { get; set; }

    [Required, MaxLength(64)]
    public string UnitName { get; set; } = string.Empty;

    [Required, MaxLength(16)]
    public string UnitAbbreviation { get; set; } = string.Empty;

    [Required, MaxLength(64)]
    public string Role { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public int? ApprovedById { get; set; }
    public User? ApprovedBy { get; set; }

    [MaxLength(32)]
    public string ApprovedByName { get; set; } = "System";
}