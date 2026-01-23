using System.ComponentModel.DataAnnotations;

namespace MilsimManager.Models;

public class LeaveOfAbsence {
    public int Id { get; set; }

    [Timestamp]
    public uint Version { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime DateStart { get; set; }

    [Required]
    public DateTime DateEnd { get; set; }
}