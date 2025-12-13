using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities;

[Table("network_types")]
[Index(nameof(Code), IsUnique = true)]
public class NetworkType : BaseEntity
{
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("name_cs")]
    public string NameCs { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("name_en")]
    public string NameEn { get; set; } = string.Empty;

    [Column("description_cs", TypeName = "nvarchar(max)")]
    public string? DescriptionCs { get; set; }

    [Column("description_en", TypeName = "nvarchar(max)")]
    public string? DescriptionEn { get; set; }

    public string GetName(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;
    }

    public string? GetDescription(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? DescriptionCs : DescriptionEn;
    }
}
