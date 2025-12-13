using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities.Cuzk;

/// <summary>
/// Entity representing land type classification (Druh pozemku).
/// Based on ČÚZK codelist SC_D_POZEMKU.
/// </summary>
[Table("cuzk_land_types")]
[Index(nameof(Code), IsUnique = true)]
public class LandType : BaseEntity
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

    [Column("agricultural_land")]
    public bool? AgriculturalLand { get; set; }

    [MaxLength(10)]
    [Column("land_parcel_type_code")]
    public string? LandParcelTypeCode { get; set; }

    [Column("building_parcel")]
    public bool? BuildingParcel { get; set; }

    [Column("mandatory_land_protection")]
    public bool? MandatoryLandProtection { get; set; }

    [Column("mandatory_land_use")]
    public bool? MandatoryLandUse { get; set; }

    public string GetName(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;

    public string? GetDescription(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? DescriptionCs : DescriptionEn;
}
