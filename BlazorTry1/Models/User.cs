namespace BlazorTry1.Models;

using System.ComponentModel.DataAnnotations;

public class User {
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Discord User Id is required")]
    public string DiscordId { get; set; }
}