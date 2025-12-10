using System.ComponentModel.DataAnnotations;

namespace BlazorTry1.Models;

public class User {
    public int Id { get; set; }

    [Required, MaxLength(32)]
    public string Name { get; set; }

    [MaxLength(32)]
    public string? DiscordId { get; set; }

    [MaxLength(17)]
    public string? SteamId { get; set; }

    public DateTime DateJoined { get; set; }

    [MaxLength(1000)]
    public string? Note { get; set; }

    public bool Active { get; set; }

    public int? RankId { get; set; }
    public Rank? Rank { get; set; }

    public ICollection<RankLog> RankLogs { get; set; } = new List<RankLog>();
}