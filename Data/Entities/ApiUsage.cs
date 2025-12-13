using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data.Entities;

[Table("api_usage")]
[Index(nameof(ApiKeyId), nameof(Timestamp))]
[Index(nameof(Timestamp))]
public class ApiUsage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [Column("api_key_id")]
    public long ApiKeyId { get; set; }

    [ForeignKey(nameof(ApiKeyId))]
    public ApiKey ApiKey { get; set; } = null!;

    [Required]
    public string Endpoint { get; set; } = string.Empty;

    [Column("request_count")]
    public int RequestCount { get; set; } = 1;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [MaxLength(45)]
    [Column("ip_address")]
    public string? IpAddress { get; set; }

    [Column("user_agent")]
    public string? UserAgent { get; set; }

    [Column("response_status")]
    public int? ResponseStatus { get; set; }

    [Column("response_time_ms")]
    public long? ResponseTimeMs { get; set; }

    [MaxLength(10)]
    [Column("response_format")]
    public string? ResponseFormat { get; set; }
}
