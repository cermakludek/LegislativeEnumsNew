using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities;

[Table("flags")]
[Index(nameof(Code), IsUnique = true)]
public class Flag
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("name_cs")]
    public string NameCs { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("name_en")]
    public string NameEn { get; set; } = string.Empty;

    [MaxLength(200)]
    [Column("description_cs")]
    public string? DescriptionCs { get; set; }

    [MaxLength(200)]
    [Column("description_en")]
    public string? DescriptionEn { get; set; }

    [MaxLength(7)]
    public string? Color { get; set; }

    [MaxLength(50)]
    [Column("icon_class")]
    public string? IconClass { get; set; }

    public bool Active { get; set; } = true;

    [Column("sort_order")]
    public int? SortOrder { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [MaxLength(100)]
    [Column("created_by")]
    public string? CreatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [MaxLength(100)]
    [Column("updated_by")]
    public string? UpdatedBy { get; set; }

    public ICollection<CodelistRegistry> Codelists { get; set; } = new HashSet<CodelistRegistry>();

    public string GetName(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;
    }

    public string? GetDescription(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? DescriptionCs : DescriptionEn;
    }
}
