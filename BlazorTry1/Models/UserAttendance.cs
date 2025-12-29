using System.ComponentModel.DataAnnotations;

namespace BlazorTry1.Models;

public class UserAttendance {
    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    public int EventId { get; set; }
    public Event Event { get; set; } = null!;
}