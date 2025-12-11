using System.ComponentModel.DataAnnotations;

namespace BlazorTry1.Models;

public class LeaveOfAbsence {
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime DateStart { get; set; }

    [Required]
    public DateTime DateEnd { get; set; }
}