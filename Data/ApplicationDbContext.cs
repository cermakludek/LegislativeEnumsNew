using LegislativeEnumsNew.Data.Entities;
using LegislativeEnumsNew.Data.Entities.Cuzk;
using LegislativeEnumsNew.Data.Entities.Kso;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LegislativeEnumsNew.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    // Core entities
    public DbSet<ApiKey> ApiKeys => Set<ApiKey>();
    public DbSet<ApiUsage> ApiUsages => Set<ApiUsage>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Flag> Flags => Set<Flag>();
    public DbSet<CodelistRegistry> CodelistRegistry => Set<CodelistRegistry>();
    public DbSet<VoltageLevel> VoltageLevels => Set<VoltageLevel>();
    public DbSet<NetworkType> NetworkTypes => Set<NetworkType>();

    // KSO entities
    public DbSet<BuildingClassification> BuildingClassifications => Set<BuildingClassification>();

    // CUZK entities
    public DbSet<BuildingType> CuzkBuildingTypes => Set<BuildingType>();
    public DbSet<BuildingUse> CuzkBuildingUses => Set<BuildingUse>();
    public DbSet<BuildingTypeUse> CuzkBuildingTypeUses => Set<BuildingTypeUse>();
    public DbSet<LandType> CuzkLandTypes => Set<LandType>();
    public DbSet<LandUse> CuzkLandUses => Set<LandUse>();
    public DbSet<LandTypeUse> CuzkLandTypeUses => Set<LandTypeUse>();
    public DbSet<UnitType> CuzkUnitTypes => Set<UnitType>();
    public DbSet<UnitUse> CuzkUnitUses => Set<UnitUse>();
    public DbSet<PropertyProtection> CuzkPropertyProtections => Set<PropertyProtection>();
    public DbSet<PropertyProtectionType> CuzkPropertyProtectionTypes => Set<PropertyProtectionType>();
    public DbSet<AreaDetermination> CuzkAreaDeterminations => Set<AreaDetermination>();
    public DbSet<SoilEcologicalUnit> CuzkSoilEcologicalUnits => Set<SoilEcologicalUnit>();
    public DbSet<SimplifiedParcelSource> CuzkSimplifiedParcelSources => Set<SimplifiedParcelSource>();
    public DbSet<BuildingRightPurpose> CuzkBuildingRightPurposes => Set<BuildingRightPurpose>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure CodelistRegistry - Flag many-to-many relationship
        builder.Entity<CodelistRegistry>()
            .HasMany(c => c.Flags)
            .WithMany(f => f.Codelists)
            .UsingEntity<Dictionary<string, object>>(
                "CodelistFlags",
                j => j.HasOne<Flag>().WithMany().HasForeignKey("FlagId"),
                j => j.HasOne<CodelistRegistry>().WithMany().HasForeignKey("CodelistId"));

        // Configure BuildingClassification self-referencing hierarchy
        builder.Entity<BuildingClassification>()
            .HasOne(b => b.Parent)
            .WithMany(b => b.Children)
            .HasForeignKey(b => b.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure ApplicationUser - ApiKey relationship
        builder.Entity<ApplicationUser>()
            .HasMany(u => u.ApiKeys)
            .WithOne(k => k.User)
            .HasForeignKey(k => k.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure ApiKey - ApiUsage relationship
        builder.Entity<ApiKey>()
            .HasMany(k => k.UsageRecords)
            .WithOne(u => u.ApiKey)
            .HasForeignKey(u => u.ApiKeyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed initial admin user roles
        SeedData(builder);
    }

    private static void SeedData(ModelBuilder builder)
    {
        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Seed Flags
        builder.Entity<Flag>().HasData(
            new Flag { Id = 1, Code = "CUZK", NameCs = "ČÚZK", NameEn = "CUZK", DescriptionCs = "Číselníky ČÚZK", DescriptionEn = "CUZK Codelists", Color = "#007bff", IconClass = "bi-building", Active = true, SortOrder = 1, CreatedAt = seedDate, CreatedBy = "system" },
            new Flag { Id = 2, Code = "ENERGY", NameCs = "Energie", NameEn = "Energy", DescriptionCs = "Energetické číselníky", DescriptionEn = "Energy Codelists", Color = "#ffc107", IconClass = "bi-lightning", Active = true, SortOrder = 2, CreatedAt = seedDate, CreatedBy = "system" },
            new Flag { Id = 3, Code = "KSO", NameCs = "KSO", NameEn = "KSO", DescriptionCs = "Klasifikace stavebních objektů", DescriptionEn = "Building Classification", Color = "#28a745", IconClass = "bi-house", Active = true, SortOrder = 3, CreatedAt = seedDate, CreatedBy = "system" }
        );

        // Seed VoltageLevel
        builder.Entity<VoltageLevel>().HasData(
            new VoltageLevel { Id = 1, Code = "MN", NameCs = "Malé napětí", NameEn = "Extra Low Voltage", VoltageRangeCs = "do 50 V AC / 120 V DC", VoltageRangeEn = "up to 50V AC / 120V DC", SortOrder = 1, CreatedAt = seedDate, CreatedBy = "system" },
            new VoltageLevel { Id = 2, Code = "NN", NameCs = "Nízké napětí", NameEn = "Low Voltage", VoltageRangeCs = "50 V až 1 kV AC / 120 V až 1,5 kV DC", VoltageRangeEn = "50V to 1kV AC / 120V to 1.5kV DC", SortOrder = 2, CreatedAt = seedDate, CreatedBy = "system" },
            new VoltageLevel { Id = 3, Code = "VN", NameCs = "Vysoké napětí", NameEn = "High Voltage", VoltageRangeCs = "1 kV až 52 kV AC", VoltageRangeEn = "1kV to 52kV AC", SortOrder = 3, CreatedAt = seedDate, CreatedBy = "system" },
            new VoltageLevel { Id = 4, Code = "VVN", NameCs = "Velmi vysoké napětí", NameEn = "Very High Voltage", VoltageRangeCs = "52 kV až 300 kV AC", VoltageRangeEn = "52kV to 300kV AC", SortOrder = 4, CreatedAt = seedDate, CreatedBy = "system" },
            new VoltageLevel { Id = 5, Code = "ZVN", NameCs = "Zvláště vysoké napětí", NameEn = "Extra High Voltage", VoltageRangeCs = "300 kV až 800 kV AC", VoltageRangeEn = "300kV to 800kV AC", SortOrder = 5, CreatedAt = seedDate, CreatedBy = "system" },
            new VoltageLevel { Id = 6, Code = "UVN", NameCs = "Ultra vysoké napětí", NameEn = "Ultra High Voltage", VoltageRangeCs = "nad 800 kV AC", VoltageRangeEn = "above 800kV AC", SortOrder = 6, CreatedAt = seedDate, CreatedBy = "system" }
        );

        // Seed NetworkType
        builder.Entity<NetworkType>().HasData(
            new NetworkType { Id = 1, Code = "DS", NameCs = "Distribuční soustava", NameEn = "Distribution System", DescriptionCs = "Vzájemně propojené vedení a zařízení 110 kV a nižších napěťových úrovní", DescriptionEn = "Interconnected lines and equipment of 110 kV and lower voltage levels", SortOrder = 1, CreatedAt = seedDate, CreatedBy = "system" },
            new NetworkType { Id = 2, Code = "PS", NameCs = "Přenosová soustava", NameEn = "Transmission System", DescriptionCs = "Vzájemně propojená vedení a zařízení 400 kV, 220 kV a vybraných vedení 110 kV", DescriptionEn = "Interconnected lines and equipment of 400kV, 220kV and selected 110kV lines", SortOrder = 2, CreatedAt = seedDate, CreatedBy = "system" }
        );

        // Seed CodelistRegistry
        builder.Entity<CodelistRegistry>().HasData(
            new CodelistRegistry { Id = 1, Code = "VOLTAGE_LEVEL", NameCs = "Napěťové hladiny", NameEn = "Voltage Levels", DescriptionCs = "Rozdělení napětí dle velikosti", DescriptionEn = "Voltage classification by magnitude", WebUrl = "/voltage-levels", ApiUrl = "/api/voltage-levels", IconClass = "bi-lightning", SortOrder = 1, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 2, Code = "NETWORK_TYPE", NameCs = "Typy sítí", NameEn = "Network Types", DescriptionCs = "Rozdělení sítí z hlediska vedení", DescriptionEn = "Network classification by transmission", WebUrl = "/network-types", ApiUrl = "/api/network-types", IconClass = "bi-diagram-3", SortOrder = 2, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 3, Code = "BUILDING_TYPE", NameCs = "Typy staveb", NameEn = "Building Types", DescriptionCs = "Číselník typů staveb ČÚZK", DescriptionEn = "CUZK building types codelist", WebUrl = "/cuzk/building-types", ApiUrl = "/api/cuzk/building-types", IconClass = "bi-building", SortOrder = 3, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 4, Code = "BUILDING_USE", NameCs = "Využití staveb", NameEn = "Building Uses", DescriptionCs = "Číselník způsobů využití staveb ČÚZK", DescriptionEn = "CUZK building uses codelist", WebUrl = "/cuzk/building-uses", ApiUrl = "/api/cuzk/building-uses", IconClass = "bi-house-door", SortOrder = 4, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 5, Code = "LAND_TYPE", NameCs = "Druhy pozemků", NameEn = "Land Types", DescriptionCs = "Číselník druhů pozemků ČÚZK", DescriptionEn = "CUZK land types codelist", WebUrl = "/cuzk/land-types", ApiUrl = "/api/cuzk/land-types", IconClass = "bi-map", SortOrder = 5, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 6, Code = "LAND_USE", NameCs = "Využití pozemků", NameEn = "Land Uses", DescriptionCs = "Číselník způsobů využití pozemků ČÚZK", DescriptionEn = "CUZK land uses codelist", WebUrl = "/cuzk/land-uses", ApiUrl = "/api/cuzk/land-uses", IconClass = "bi-tree", SortOrder = 6, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 7, Code = "UNIT_TYPE", NameCs = "Typy jednotek", NameEn = "Unit Types", DescriptionCs = "Číselník typů jednotek ČÚZK", DescriptionEn = "CUZK unit types codelist", WebUrl = "/cuzk/unit-types", ApiUrl = "/api/cuzk/unit-types", IconClass = "bi-door-open", SortOrder = 7, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 8, Code = "UNIT_USE", NameCs = "Využití jednotek", NameEn = "Unit Uses", DescriptionCs = "Číselník způsobů využití jednotek ČÚZK", DescriptionEn = "CUZK unit uses codelist", WebUrl = "/cuzk/unit-uses", ApiUrl = "/api/cuzk/unit-uses", IconClass = "bi-key", SortOrder = 8, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 9, Code = "PROPERTY_PROTECTION", NameCs = "Ochrana nemovitostí", NameEn = "Property Protections", DescriptionCs = "Číselník způsobů ochrany nemovitostí ČÚZK", DescriptionEn = "CUZK property protections codelist", WebUrl = "/cuzk/property-protections", ApiUrl = "/api/cuzk/property-protections", IconClass = "bi-shield", SortOrder = 9, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 10, Code = "PROPERTY_PROTECTION_TYPE", NameCs = "Typy ochrany nemovitostí", NameEn = "Property Protection Types", DescriptionCs = "Číselník typů ochrany nemovitostí ČÚZK", DescriptionEn = "CUZK property protection types codelist", WebUrl = "/cuzk/property-protection-types", ApiUrl = "/api/cuzk/property-protection-types", IconClass = "bi-shield-check", SortOrder = 10, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 11, Code = "AREA_DETERMINATION", NameCs = "Určení výměry", NameEn = "Area Determinations", DescriptionCs = "Číselník způsobů určení výměry ČÚZK", DescriptionEn = "CUZK area determinations codelist", WebUrl = "/cuzk/area-determinations", ApiUrl = "/api/cuzk/area-determinations", IconClass = "bi-rulers", SortOrder = 11, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 12, Code = "SOIL_ECOLOGICAL_UNIT", NameCs = "BPEJ", NameEn = "Soil Ecological Units", DescriptionCs = "Bonitované půdně ekologické jednotky", DescriptionEn = "Soil ecological units codelist", WebUrl = "/cuzk/soil-ecological-units", ApiUrl = "/api/cuzk/soil-ecological-units", IconClass = "bi-flower1", SortOrder = 12, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 13, Code = "SIMPLIFIED_PARCEL_SOURCE", NameCs = "Zdroje parcel ZE", NameEn = "Simplified Parcel Sources", DescriptionCs = "Zdroje parcel zjednodušené evidence", DescriptionEn = "Simplified parcel sources codelist", WebUrl = "/cuzk/simplified-parcel-sources", ApiUrl = "/api/cuzk/simplified-parcel-sources", IconClass = "bi-file-earmark", SortOrder = 13, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 14, Code = "BUILDING_RIGHT_PURPOSE", NameCs = "Účely práva stavby", NameEn = "Building Right Purposes", DescriptionCs = "Číselník účelů práva stavby ČÚZK", DescriptionEn = "CUZK building right purposes codelist", WebUrl = "/cuzk/building-right-purposes", ApiUrl = "/api/cuzk/building-right-purposes", IconClass = "bi-hammer", SortOrder = 14, CreatedAt = seedDate, CreatedBy = "system" },
            new CodelistRegistry { Id = 15, Code = "BUILDING_CLASSIFICATION", NameCs = "KSO", NameEn = "Building Classifications", DescriptionCs = "Klasifikace stavebních objektů (KSO/JKSO)", DescriptionEn = "Building object classification (KSO/JKSO)", WebUrl = "/kso", ApiUrl = "/api/kso", IconClass = "bi-diagram-3", SortOrder = 15, CreatedAt = seedDate, CreatedBy = "system" }
        );
    }
}
