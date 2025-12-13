using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities.Cuzk;

/// <summary>
/// Entity representing the relationship between building type and building use (Vazba typ stavby a využití stavby).
/// Based on ČÚZK codelist SC_TYPB_ZPVYB.
/// </summary>
[Table("cuzk_building_type_uses")]
[Index(nameof(BuildingTypeCode), nameof(BuildingUseCode), IsUnique = true)]
public class BuildingTypeUse : BaseEntity
{
    [Required]
    [MaxLength(10)]
    [Column("building_type_code")]
    public string BuildingTypeCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    [Column("building_use_code")]
    public string BuildingUseCode { get; set; } = string.Empty;
}
