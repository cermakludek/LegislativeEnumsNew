using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LegislativeEnumsNew.Data.Entities;

/// <summary>
/// Base entity class providing common fields for all codelist entities.
/// Contains validity period (validFrom, validTo) and audit fields.
/// </summary>
public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    /// <summary>
    /// Start date of validity period (PLATNOST_OD).
    /// </summary>
    [Column("valid_from")]
    public DateOnly? ValidFrom { get; set; }

    /// <summary>
    /// End date of validity period (PLATNOST_DO).
    /// Null means the record is currently valid.
    /// </summary>
    [Column("valid_to")]
    public DateOnly? ValidTo { get; set; }

    /// <summary>
    /// Sort order for display purposes.
    /// </summary>
    [Column("sort_order")]
    public int? SortOrder { get; set; }

    /// <summary>
    /// Timestamp when this record was created.
    /// </summary>
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Username of the user who created this record.
    /// </summary>
    [Column("created_by")]
    [MaxLength(100)]
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Timestamp when this record was last updated.
    /// </summary>
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Username of the user who last updated this record.
    /// </summary>
    [Column("updated_by")]
    [MaxLength(100)]
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Checks if this record is currently valid based on validFrom and validTo dates.
    /// </summary>
    public bool IsCurrentlyValid()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var afterStart = ValidFrom == null || today >= ValidFrom;
        var beforeEnd = ValidTo == null || today <= ValidTo;
        return afterStart && beforeEnd;
    }
}
