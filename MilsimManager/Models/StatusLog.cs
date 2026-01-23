using System.ComponentModel.DataAnnotations;

namespace MilsimManager.Models;

public class StatusLog {
    public int Id { get; set; }

    [Timestamp]
    public uint Version { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime Date { get; set; }

    public int? ApprovedById { get; set; }
    public User? ApprovedBy { get; set; }

    [MaxLength(32)]
    public string ApprovedByName { get; set; } = "System";
}