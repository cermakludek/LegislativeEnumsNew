using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities;

[Table("api_keys")]
[Index(nameof(Key), IsUnique = true)]
public class ApiKey
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("api_key")]
    public string Key { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public bool Enabled { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    [Required]
    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; } = null!;

    public ICollection<ApiUsage> UsageRecords { get; set; } = new List<ApiUsage>();

    public bool IsExpired()
    {
        return ExpiresAt != null && DateTime.UtcNow > ExpiresAt;
    }

    public bool IsValid()
    {
        return Enabled && !IsExpired();
    }
}
