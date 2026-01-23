using System.ComponentModel.DataAnnotations;

namespace MilsimManager.Models;

public class UserAttendance {
    [Timestamp]
    public uint Version { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;
}