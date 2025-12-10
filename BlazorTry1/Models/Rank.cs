using System.ComponentModel.DataAnnotations;

namespace BlazorTry1.Models;

public class Rank {
    public int Id { get; set; }
    
    public int SortOrder { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(20)]
    public string? Abbreviation { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    
}