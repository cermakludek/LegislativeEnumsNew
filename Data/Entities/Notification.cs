using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities;

[Table("notifications")]
[Index(nameof(CreatedAt))]
[Index(nameof(EntityType))]
public class Notification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("entity_type")]
    public string EntityType { get; set; } = string.Empty;

    [Column("entity_id")]
    public long EntityId { get; set; }

    [MaxLength(50)]
    [Column("entity_code")]
    public string? EntityCode { get; set; }

    [Column("change_type")]
    public ChangeType ChangeType { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("changed_by")]
    public string ChangedBy { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(500)]
    [Column("message")]
    public string? Message { get; set; }
}
