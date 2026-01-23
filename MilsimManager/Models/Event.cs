using System.ComponentModel.DataAnnotations;

namespace MilsimManager.Models;

public class Event {
    public int Id { get; set; }

    [Timestamp]
    public uint Version { get; set; }

    [Required, MaxLength(128)]
    public string Name { get; set; } = null!;

    [MaxLength(2000)]
    public string? Description { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public ICollection<UserAttendance> UserAttendances { get; set; } = new List<UserAttendance>();
}