using System.ComponentModel.DataAnnotations;

namespace BlazorTry1.Models;

public class UserCertification {
    [Required]
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [Required]
    public int CertificationId { get; set; }
    public Certification Certification { get; set; } = null!;

    public DateTime Date { get; set; }

    [MaxLength(1000)]
    public string? Comment { get; set; }

    [Required, MaxLength(32)]
    public string ApprovedByName { get; set; } = "System";
    public int? ApprovedById { get; set; }
    public User? ApprovedBy { get; set; }
}