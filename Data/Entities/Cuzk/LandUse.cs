using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities.Cuzk;

/// <summary>
/// Entity representing land use classification (Způsob využití pozemku).
/// Based on ČÚZK codelist SC_ZP_VYUZITI_POZ.
/// </summary>
[Table("cuzk_land_uses")]
[Index(nameof(Code), IsUnique = true)]
public class LandUse : BaseEntity
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

    [MaxLength(10)]
    [Column("land_parcel_type_code")]
    public string? LandParcelTypeCode { get; set; }

    public string GetName(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;

    public string? GetDescription(string lang) =>
        lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? DescriptionCs : DescriptionEn;
}
