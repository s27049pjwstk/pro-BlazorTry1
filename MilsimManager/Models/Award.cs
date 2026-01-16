using System.ComponentModel.DataAnnotations;

namespace MilsimManager.Models;

public class Award {
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }
    
    public ICollection<UserAward> UserAwards { get; set; } = new List<UserAward>();
}