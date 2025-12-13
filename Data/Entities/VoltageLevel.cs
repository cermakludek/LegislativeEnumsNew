using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities;

[Table("voltage_levels")]
[Index(nameof(Code), IsUnique = true)]
public class VoltageLevel : BaseEntity
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

    [Required]
    [MaxLength(100)]
    [Column("voltage_range_cs")]
    public string VoltageRangeCs { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("voltage_range_en")]
    public string VoltageRangeEn { get; set; } = string.Empty;

    public string GetName(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? NameCs : NameEn;
    }

    public string GetVoltageRange(string lang)
    {
        return lang.Equals("cs", StringComparison.OrdinalIgnoreCase) ? VoltageRangeCs : VoltageRangeEn;
    }
}
