using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities.Cuzk;

/// <summary>
/// Entity representing building right purpose (Účel práva stavby).
/// Based on ČÚZK codelist SC_UCELY_PS.
/// </summary>
[Table("cuzk_building_right_purposes")]
[Index(nameof(Code), IsUnique = true)]
public class BuildingRightPurpose : BaseEntity
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

    public string GetName(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;

    public string? GetDescription(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? DescriptionCs : DescriptionEn;
}
