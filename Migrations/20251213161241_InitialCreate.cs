using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LegislativeEnumsNew.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    role = table.Column<int>(type: "int", nullable: false),
                    usage_plan = table.Column<int>(type: "int", nullable: true),
                    trial_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    enabled = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "audit_log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entity_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    entity_id = table.Column<long>(type: "bigint", nullable: false),
                    entity_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    change_type = table.Column<int>(type: "int", nullable: false),
                    changed_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    changed_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    old_values = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    new_values = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_audit_log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "building_classifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    parent_id = table.Column<long>(type: "bigint", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_building_classifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_building_classifications_building_classifications_parent_id",
                        column: x => x.parent_id,
                        principalTable: "building_classifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "codelist_registry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    web_url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    api_url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    icon_class = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_codelist_registry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_area_determinations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_area_determinations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_building_right_purposes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_building_right_purposes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_building_type_uses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    building_type_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    building_use_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_building_type_uses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_building_types",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    entry_code = table.Column<bool>(type: "bit", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_building_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_building_uses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_building_uses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_land_type_uses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    land_type_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    land_use_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_land_type_uses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_land_types",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    agricultural_land = table.Column<bool>(type: "bit", nullable: true),
                    land_parcel_type_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    building_parcel = table.Column<bool>(type: "bit", nullable: true),
                    mandatory_land_protection = table.Column<bool>(type: "bit", nullable: true),
                    mandatory_land_use = table.Column<bool>(type: "bit", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_land_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_land_uses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    land_parcel_type_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_land_uses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_property_protection_types",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_property_protection_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_property_protections",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    protection_type_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    applies_to_land = table.Column<bool>(type: "bit", nullable: true),
                    applies_to_building = table.Column<bool>(type: "bit", nullable: true),
                    applies_to_unit = table.Column<bool>(type: "bit", nullable: true),
                    applies_to_building_right = table.Column<bool>(type: "bit", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_property_protections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_simplified_parcel_sources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_simplified_parcel_sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_soil_ecological_units",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    detailed_description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_soil_ecological_units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_unit_types",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    civil_code = table.Column<bool>(type: "bit", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_unit_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cuzk_unit_uses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    abbreviation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cuzk_unit_uses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "flags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: true),
                    icon_class = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "network_types",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description_cs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description_en = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_network_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "voltage_levels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    name_cs = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    name_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    voltage_range_cs = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    voltage_range_en = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    valid_from = table.Column<DateOnly>(type: "date", nullable: true),
                    valid_to = table.Column<DateOnly>(type: "date", nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voltage_levels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "api_keys",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    api_key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_keys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_api_keys_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodelistFlags",
                columns: table => new
                {
                    CodelistId = table.Column<long>(type: "bigint", nullable: false),
                    FlagId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodelistFlags", x => new { x.CodelistId, x.FlagId });
                    table.ForeignKey(
                        name: "FK_CodelistFlags_codelist_registry_CodelistId",
                        column: x => x.CodelistId,
                        principalTable: "codelist_registry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CodelistFlags_flags_FlagId",
                        column: x => x.FlagId,
                        principalTable: "flags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "api_usage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    api_key_id = table.Column<long>(type: "bigint", nullable: false),
                    Endpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    request_count = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ip_address = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    user_agent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    response_status = table.Column<int>(type: "int", nullable: true),
                    response_time_ms = table.Column<long>(type: "bigint", nullable: true),
                    response_format = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_usage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_api_usage_api_keys_api_key_id",
                        column: x => x.api_key_id,
                        principalTable: "api_keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "codelist_registry",
                columns: new[] { "Id", "api_url", "Code", "created_at", "created_by", "description_cs", "description_en", "icon_class", "name_cs", "name_en", "sort_order", "updated_at", "updated_by", "valid_from", "valid_to", "web_url" },
                values: new object[,]
                {
                    { 1L, "/api/voltage-levels", "VOLTAGE_LEVEL", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Rozdělení napětí dle velikosti", "Voltage classification by magnitude", "bi-lightning", "Napěťové hladiny", "Voltage Levels", 1, null, null, null, null, "/voltage-levels" },
                    { 2L, "/api/network-types", "NETWORK_TYPE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Rozdělení sítí z hlediska vedení", "Network classification by transmission", "bi-diagram-3", "Typy sítí", "Network Types", 2, null, null, null, null, "/network-types" },
                    { 3L, "/api/cuzk/building-types", "BUILDING_TYPE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník typů staveb ČÚZK", "CUZK building types codelist", "bi-building", "Typy staveb", "Building Types", 3, null, null, null, null, "/cuzk/building-types" },
                    { 4L, "/api/cuzk/building-uses", "BUILDING_USE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník způsobů využití staveb ČÚZK", "CUZK building uses codelist", "bi-house-door", "Využití staveb", "Building Uses", 4, null, null, null, null, "/cuzk/building-uses" },
                    { 5L, "/api/cuzk/land-types", "LAND_TYPE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník druhů pozemků ČÚZK", "CUZK land types codelist", "bi-map", "Druhy pozemků", "Land Types", 5, null, null, null, null, "/cuzk/land-types" },
                    { 6L, "/api/cuzk/land-uses", "LAND_USE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník způsobů využití pozemků ČÚZK", "CUZK land uses codelist", "bi-tree", "Využití pozemků", "Land Uses", 6, null, null, null, null, "/cuzk/land-uses" },
                    { 7L, "/api/cuzk/unit-types", "UNIT_TYPE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník typů jednotek ČÚZK", "CUZK unit types codelist", "bi-door-open", "Typy jednotek", "Unit Types", 7, null, null, null, null, "/cuzk/unit-types" },
                    { 8L, "/api/cuzk/unit-uses", "UNIT_USE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník způsobů využití jednotek ČÚZK", "CUZK unit uses codelist", "bi-key", "Využití jednotek", "Unit Uses", 8, null, null, null, null, "/cuzk/unit-uses" },
                    { 9L, "/api/cuzk/property-protections", "PROPERTY_PROTECTION", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník způsobů ochrany nemovitostí ČÚZK", "CUZK property protections codelist", "bi-shield", "Ochrana nemovitostí", "Property Protections", 9, null, null, null, null, "/cuzk/property-protections" },
                    { 10L, "/api/cuzk/property-protection-types", "PROPERTY_PROTECTION_TYPE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník typů ochrany nemovitostí ČÚZK", "CUZK property protection types codelist", "bi-shield-check", "Typy ochrany nemovitostí", "Property Protection Types", 10, null, null, null, null, "/cuzk/property-protection-types" },
                    { 11L, "/api/cuzk/area-determinations", "AREA_DETERMINATION", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník způsobů určení výměry ČÚZK", "CUZK area determinations codelist", "bi-rulers", "Určení výměry", "Area Determinations", 11, null, null, null, null, "/cuzk/area-determinations" },
                    { 12L, "/api/cuzk/soil-ecological-units", "SOIL_ECOLOGICAL_UNIT", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Bonitované půdně ekologické jednotky", "Soil ecological units codelist", "bi-flower1", "BPEJ", "Soil Ecological Units", 12, null, null, null, null, "/cuzk/soil-ecological-units" },
                    { 13L, "/api/cuzk/simplified-parcel-sources", "SIMPLIFIED_PARCEL_SOURCE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Zdroje parcel zjednodušené evidence", "Simplified parcel sources codelist", "bi-file-earmark", "Zdroje parcel ZE", "Simplified Parcel Sources", 13, null, null, null, null, "/cuzk/simplified-parcel-sources" },
                    { 14L, "/api/cuzk/building-right-purposes", "BUILDING_RIGHT_PURPOSE", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselník účelů práva stavby ČÚZK", "CUZK building right purposes codelist", "bi-hammer", "Účely práva stavby", "Building Right Purposes", 14, null, null, null, null, "/cuzk/building-right-purposes" },
                    { 15L, "/api/kso", "BUILDING_CLASSIFICATION", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Klasifikace stavebních objektů (KSO/JKSO)", "Building object classification (KSO/JKSO)", "bi-diagram-3", "KSO", "Building Classifications", 15, null, null, null, null, "/kso" }
                });

            migrationBuilder.InsertData(
                table: "flags",
                columns: new[] { "Id", "Active", "Code", "Color", "created_at", "created_by", "description_cs", "description_en", "icon_class", "name_cs", "name_en", "sort_order", "updated_at", "updated_by" },
                values: new object[,]
                {
                    { 1L, true, "CUZK", "#007bff", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Číselníky ČÚZK", "CUZK Codelists", "bi-building", "ČÚZK", "CUZK", 1, null, null },
                    { 2L, true, "ENERGY", "#ffc107", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Energetické číselníky", "Energy Codelists", "bi-lightning", "Energie", "Energy", 2, null, null },
                    { 3L, true, "KSO", "#28a745", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Klasifikace stavebních objektů", "Building Classification", "bi-house", "KSO", "KSO", 3, null, null }
                });

            migrationBuilder.InsertData(
                table: "network_types",
                columns: new[] { "Id", "Code", "created_at", "created_by", "description_cs", "description_en", "name_cs", "name_en", "sort_order", "updated_at", "updated_by", "valid_from", "valid_to" },
                values: new object[,]
                {
                    { 1L, "DS", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Vzájemně propojené vedení a zařízení 110 kV a nižších napěťových úrovní", "Interconnected lines and equipment of 110 kV and lower voltage levels", "Distribuční soustava", "Distribution System", 1, null, null, null, null },
                    { 2L, "PS", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Vzájemně propojená vedení a zařízení 400 kV, 220 kV a vybraných vedení 110 kV", "Interconnected lines and equipment of 400kV, 220kV and selected 110kV lines", "Přenosová soustava", "Transmission System", 2, null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "voltage_levels",
                columns: new[] { "Id", "Code", "created_at", "created_by", "name_cs", "name_en", "sort_order", "updated_at", "updated_by", "valid_from", "valid_to", "voltage_range_cs", "voltage_range_en" },
                values: new object[,]
                {
                    { 1L, "MN", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Malé napětí", "Extra Low Voltage", 1, null, null, null, null, "do 50 V AC / 120 V DC", "up to 50V AC / 120V DC" },
                    { 2L, "NN", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Nízké napětí", "Low Voltage", 2, null, null, null, null, "50 V až 1 kV AC / 120 V až 1,5 kV DC", "50V to 1kV AC / 120V to 1.5kV DC" },
                    { 3L, "VN", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Vysoké napětí", "High Voltage", 3, null, null, null, null, "1 kV až 52 kV AC", "1kV to 52kV AC" },
                    { 4L, "VVN", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Velmi vysoké napětí", "Very High Voltage", 4, null, null, null, null, "52 kV až 300 kV AC", "52kV to 300kV AC" },
                    { 5L, "ZVN", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Zvláště vysoké napětí", "Extra High Voltage", 5, null, null, null, null, "300 kV až 800 kV AC", "300kV to 800kV AC" },
                    { 6L, "UVN", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "system", "Ultra vysoké napětí", "Ultra High Voltage", 6, null, null, null, null, "nad 800 kV AC", "above 800kV AC" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_api_keys_api_key",
                table: "api_keys",
                column: "api_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_api_keys_user_id",
                table: "api_keys",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_api_usage_api_key_id_Timestamp",
                table: "api_usage",
                columns: new[] { "api_key_id", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_api_usage_Timestamp",
                table: "api_usage",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_entity_code",
                table: "audit_log",
                column: "entity_code");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_entity_type",
                table: "audit_log",
                column: "entity_type");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_change_type",
                table: "audit_log",
                column: "change_type");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_changed_at",
                table: "audit_log",
                column: "changed_at");

            migrationBuilder.CreateIndex(
                name: "IX_audit_log_changed_by",
                table: "audit_log",
                column: "changed_by");

            migrationBuilder.CreateIndex(
                name: "IX_building_classifications_Code",
                table: "building_classifications",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_building_classifications_parent_id",
                table: "building_classifications",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_codelist_registry_Code",
                table: "codelist_registry",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CodelistFlags_FlagId",
                table: "CodelistFlags",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_area_determinations_Code",
                table: "cuzk_area_determinations",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_building_right_purposes_Code",
                table: "cuzk_building_right_purposes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_building_type_uses_building_type_code_building_use_code",
                table: "cuzk_building_type_uses",
                columns: new[] { "building_type_code", "building_use_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_building_types_Code",
                table: "cuzk_building_types",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_building_uses_Code",
                table: "cuzk_building_uses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_land_type_uses_land_type_code_land_use_code",
                table: "cuzk_land_type_uses",
                columns: new[] { "land_type_code", "land_use_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_land_types_Code",
                table: "cuzk_land_types",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_land_uses_Code",
                table: "cuzk_land_uses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_property_protection_types_Code",
                table: "cuzk_property_protection_types",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_property_protections_Code",
                table: "cuzk_property_protections",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_simplified_parcel_sources_Code",
                table: "cuzk_simplified_parcel_sources",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_soil_ecological_units_Code",
                table: "cuzk_soil_ecological_units",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_unit_types_Code",
                table: "cuzk_unit_types",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cuzk_unit_uses_Code",
                table: "cuzk_unit_uses",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flags_Code",
                table: "flags",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_network_types_Code",
                table: "network_types",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_voltage_levels_Code",
                table: "voltage_levels",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_usage");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "audit_log");

            migrationBuilder.DropTable(
                name: "building_classifications");

            migrationBuilder.DropTable(
                name: "CodelistFlags");

            migrationBuilder.DropTable(
                name: "cuzk_area_determinations");

            migrationBuilder.DropTable(
                name: "cuzk_building_right_purposes");

            migrationBuilder.DropTable(
                name: "cuzk_building_type_uses");

            migrationBuilder.DropTable(
                name: "cuzk_building_types");

            migrationBuilder.DropTable(
                name: "cuzk_building_uses");

            migrationBuilder.DropTable(
                name: "cuzk_land_type_uses");

            migrationBuilder.DropTable(
                name: "cuzk_land_types");

            migrationBuilder.DropTable(
                name: "cuzk_land_uses");

            migrationBuilder.DropTable(
                name: "cuzk_property_protection_types");

            migrationBuilder.DropTable(
                name: "cuzk_property_protections");

            migrationBuilder.DropTable(
                name: "cuzk_simplified_parcel_sources");

            migrationBuilder.DropTable(
                name: "cuzk_soil_ecological_units");

            migrationBuilder.DropTable(
                name: "cuzk_unit_types");

            migrationBuilder.DropTable(
                name: "cuzk_unit_uses");

            migrationBuilder.DropTable(
                name: "network_types");

            migrationBuilder.DropTable(
                name: "voltage_levels");

            migrationBuilder.DropTable(
                name: "api_keys");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "codelist_registry");

            migrationBuilder.DropTable(
                name: "flags");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
