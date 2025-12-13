using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LegislativeEnumsNew.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace LegislativeEnumsNew.Data;

public enum UserRole { Admin, Contributor, Reader }

public enum UsagePlan
{
    PerMonth,
    PerYear,
    PerRequest
}

public static class UsagePlanExtensions
{
    public static string GetDisplayName(this UsagePlan plan, string lang)
    {
        return (plan, lang.ToLowerInvariant()) switch
        {
            (UsagePlan.PerMonth, "cs") => "Měsíční",
            (UsagePlan.PerMonth, _) => "Per Month",
            (UsagePlan.PerYear, "cs") => "Roční",
            (UsagePlan.PerYear, _) => "Per Year",
            (UsagePlan.PerRequest, "cs") => "Za požadavek",
            (UsagePlan.PerRequest, _) => "Per Request",
            _ => plan.ToString()
        };
    }
}

public class ApplicationUser : IdentityUser
{
    [MaxLength(100)]
    [Column("first_name")]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("role")]
    public UserRole Role { get; set; } = UserRole.Reader;

    [Column("usage_plan")]
    public UsagePlan? UsagePlan { get; set; }

    [Column("trial_end_date")]
    public DateOnly? TrialEndDate { get; set; }

    [Column("enabled")]
    public bool Enabled { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public bool IsInTrialPeriod()
    {
        return TrialEndDate != null && DateOnly.FromDateTime(DateTime.Today) < TrialEndDate;
    }

    public bool IsTrialExpired()
    {
        return TrialEndDate != null && DateOnly.FromDateTime(DateTime.Today) > TrialEndDate;
    }

    public string GetDisplayName()
    {
        if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
            return $"{FirstName} {LastName}";
        if (!string.IsNullOrWhiteSpace(FirstName))
            return FirstName;
        if (!string.IsNullOrWhiteSpace(LastName))
            return LastName;
        return UserName ?? Email ?? "Unknown";
    }
}
