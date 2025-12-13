using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities;

public enum ChangeType
{
    Create,
    Update,
    Delete
}

[Table("audit_log")]
[Index(nameof(EntityType))]
[Index(nameof(EntityCode))]
[Index(nameof(ChangedAt))]
[Index(nameof(ChangedBy))]
[Index(nameof(ChangeType))]
public class AuditLog
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

    [Column("changed_at")]
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    [Column("old_values", TypeName = "nvarchar(max)")]
    public string? OldValues { get; set; }

    [Column("new_values", TypeName = "nvarchar(max)")]
    public string? NewValues { get; set; }
}
