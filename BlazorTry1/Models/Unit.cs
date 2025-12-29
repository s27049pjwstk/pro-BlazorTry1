using System.ComponentModel.DataAnnotations;

namespace BlazorTry1.Models;

public class Unit {
    public int Id { get; set; }

    [Required, MaxLength(64)]
    public string Name { get; set; } = null!;

    [MaxLength(16)]
    public string? Abbreviation { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}