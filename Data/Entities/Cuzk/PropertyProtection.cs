using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities.Cuzk;

/// <summary>
/// Entity representing property protection method (Způsob ochrany nemovitosti).
/// Based on ČÚZK codelist SC_ZP_OCHRANY_NEM.
/// </summary>
[Table("cuzk_property_protections")]
[Index(nameof(Code), IsUnique = true)]
public class PropertyProtection : BaseEntity
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

    [MaxLength(10)]
    [Column("protection_type_code")]
    public string? ProtectionTypeCode { get; set; }

    [Column("applies_to_land")]
    public bool? AppliesToLand { get; set; }

    [Column("applies_to_building")]
    public bool? AppliesToBuilding { get; set; }

    [Column("applies_to_unit")]
    public bool? AppliesToUnit { get; set; }

    [Column("applies_to_building_right")]
    public bool? AppliesToBuildingRight { get; set; }

    public string GetName(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;

    public string? GetDescription(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? DescriptionCs : DescriptionEn;
}
