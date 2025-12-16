using System.ComponentModel.DataAnnotations;

namespace BlazorTry1.Models;

public class Certification {
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }
}