using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities.Cuzk;

/// <summary>
/// Entity representing building type classification (Typ stavby).
/// Based on ČÚZK codelist SC_T_BUDOV.
/// </summary>
[Table("cuzk_building_types")]
[Index(nameof(Code), IsUnique = true)]
public class BuildingType : BaseEntity
{
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [Column("name_cs")]
    public string NameCs { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    [Column("name_en")]
    public string NameEn { get; set; } = string.Empty;

    [MaxLength(500)]
    [Column("description_cs")]
    public string? DescriptionCs { get; set; }

    [MaxLength(500)]
    [Column("description_en")]
    public string? DescriptionEn { get; set; }

    [MaxLength(20)]
    [Column("abbreviation")]
    public string? Abbreviation { get; set; }

    [Column("entry_code")]
    public bool? EntryCode { get; set; }

    public string GetName(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;

    public string? GetDescription(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? DescriptionCs : DescriptionEn;
}
