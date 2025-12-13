using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities.Kso;

/// <summary>
/// Entity representing a building object classification (Klasifikace stavebních objektů - KSO/JKSO).
/// This is a hierarchical codelist with 4 levels:
/// - Level 1: Podskupina (3 digits): e.g., 801 - Budovy občanské výstavby
/// - Level 2: Oddíl (3+1 digits): e.g., 801.1 - Budovy pro zdravotní péči
/// - Level 3: Pododdíl (3+2 digits): e.g., 801.11 - budovy nemocnic
/// - Level 4: Konstrukčně materiálová charakteristika (3+2+1 digits): e.g., 801.11.1 - zděná z cihel
/// </summary>
[Table("building_classifications")]
[Index(nameof(Code), IsUnique = true)]
public class BuildingClassification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [MaxLength(15)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    [Column("name_cs")]
    public string NameCs { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    [Column("name_en")]
    public string NameEn { get; set; } = string.Empty;

    [Column("description_cs", TypeName = "nvarchar(max)")]
    public string? DescriptionCs { get; set; }

    [Column("description_en", TypeName = "nvarchar(max)")]
    public string? DescriptionEn { get; set; }

    /// <summary>
    /// Hierarchy level:
    /// 1 = Podskupina (e.g., 801)
    /// 2 = Oddíl (e.g., 801.1)
    /// 3 = Pododdíl (e.g., 801.11)
    /// 4 = Konstrukčně materiálová charakteristika (e.g., 801.11.1)
    /// </summary>
    public int Level { get; set; }

    [Column("parent_id")]
    public long? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public BuildingClassification? Parent { get; set; }

    public ICollection<BuildingClassification> Children { get; set; } = new List<BuildingClassification>();

    [Column("valid_from")]
    public DateOnly? ValidFrom { get; set; }

    [Column("valid_to")]
    public DateOnly? ValidTo { get; set; }

    [Column("sort_order")]
    public int? SortOrder { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    public string GetName(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;
    }

    public string? GetDescription(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? DescriptionCs : DescriptionEn;
    }
}
