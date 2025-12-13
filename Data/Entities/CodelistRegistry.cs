using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities;

[Table("codelist_registry")]
[Index(nameof(Code), IsUnique = true)]
public class CodelistRegistry : BaseEntity
{
    [Required]
    [MaxLength(50)]
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

    [Required]
    [MaxLength(100)]
    [Column("web_url")]
    public string WebUrl { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("api_url")]
    public string ApiUrl { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column("icon_class")]
    public string? IconClass { get; set; }

    public ICollection<Flag> Flags { get; set; } = new HashSet<Flag>();

    public string GetName(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;
    }

    public string? GetDescription(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? DescriptionCs : DescriptionEn;
    }
}
