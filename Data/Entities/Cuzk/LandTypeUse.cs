using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities.Cuzk;

/// <summary>
/// Entity representing the relationship between land type and land use (Vazba druh pozemku a využití).
/// Based on ČÚZK codelist SC_POZEMEK_VYUZITI.
/// </summary>
[Table("cuzk_land_type_uses")]
[Index(nameof(LandTypeCode), nameof(LandUseCode), IsUnique = true)]
public class LandTypeUse : BaseEntity
{
    [Required]
    [MaxLength(10)]
    [Column("land_type_code")]
    public string LandTypeCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    [Column("land_use_code")]
    public string LandUseCode { get; set; } = string.Empty;
}
