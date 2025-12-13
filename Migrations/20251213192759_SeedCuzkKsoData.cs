using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegislativeEnumsNew.Migrations
{
    /// <inheritdoc />
    public partial class SeedCuzkKsoData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Land Types
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_land_types ON;
INSERT INTO cuzk_land_types (Id, Code, name_cs, name_en, abbreviation, agricultural_land, land_parcel_type_code, building_parcel, mandatory_land_protection, mandatory_land_use, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '2', N'orná půda', N'arable land', N'orná půda', 1, NULL, 0, 1, 0, '1993-01-01', NULL, 1, GETDATE(), 'system'),
(2, '3', N'chmelnice', N'hop field', N'chmelnice', 1, '302', 0, 1, 0, '1993-01-01', NULL, 2, GETDATE(), 'system'),
(3, '4', N'vinice', N'vineyard', N'vinice', 1, '303', 0, 1, 0, '1993-01-01', NULL, 3, GETDATE(), 'system'),
(4, '5', N'zahrada', N'garden', N'zahrada', 1, '304', 0, 1, 0, '1993-01-01', NULL, 4, GETDATE(), 'system'),
(5, '6', N'ovocný sad', N'orchard', N'ovoc. sad', 1, '305', 0, 1, 0, '1993-01-01', NULL, 5, GETDATE(), 'system'),
(6, '7', N'trvalý travní porost', N'permanent grassland', N'travní p.', 1, '306', 0, 1, 0, '1993-01-01', NULL, 6, GETDATE(), 'system'),
(7, '8', N'trvalý travní porost', N'permanent grassland (historical)', N'travní p.', 1, '307', 0, 1, 0, '1993-01-01', '2000-09-01', 7, GETDATE(), 'system'),
(8, '10', N'lesní pozemek', N'forest land', N'lesní poz', 0, '308', 0, 1, 0, '1993-01-01', NULL, 8, GETDATE(), 'system'),
(9, '11', N'vodní plocha', N'water area', N'vodní pl.', 0, NULL, 0, 0, 1, '1993-01-01', NULL, 9, GETDATE(), 'system'),
(10, '13', N'zastavěná plocha a nádvoří', N'built-up area and courtyard', N'zast. pl.', 0, NULL, 1, 0, 0, '1993-01-01', NULL, 10, GETDATE(), 'system'),
(11, '14', N'ostatní plocha', N'other area', N'ostat.pl.', 0, NULL, 0, 0, 1, '1993-01-01', NULL, 11, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_land_types OFF;
");

            // Area Determinations
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_area_determinations ON;
INSERT INTO cuzk_area_determinations (Id, Code, name_cs, name_en, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '0', N'Graficky nebo v digitalizované mapě', N'Graphically or in digitized map', '1993-01-01', NULL, 1, GETDATE(), 'system'),
(2, '1', N'Jiným číselným způsobem', N'By other numerical method', '1993-01-01', NULL, 2, GETDATE(), 'system'),
(3, '2', N'Ze souřadnic v S-JTSK', N'From S-JTSK coordinates', '1993-01-01', NULL, 3, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_area_determinations OFF;
");

            // Land Uses
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_land_uses ON;
INSERT INTO cuzk_land_uses (Id, Code, name_cs, name_en, abbreviation, land_parcel_type_code, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '1', N'skleník, pařeniště', N'greenhouse, hotbed', N'skleník-pařeniš.', NULL, '1993-01-01', NULL, 1, GETDATE(), 'system'),
(2, '2', N'školka', N'nursery', N'školka', NULL, '1993-01-01', NULL, 2, GETDATE(), 'system'),
(3, '3', N'plantáž dřevin', N'tree plantation', N'plantáž dřevin', NULL, '1998-10-15', NULL, 3, GETDATE(), 'system'),
(4, '4', N'les jiný než hospodářský', N'non-commercial forest', N'les(ne hospodář)', NULL, '1993-01-01', NULL, 4, GETDATE(), 'system'),
(5, '5', N'lesní pozemek, na kterém je budova', N'forest land with building', N'les s budovou', NULL, '1993-01-01', NULL, 5, GETDATE(), 'system'),
(6, '6', N'rybník', N'pond', N'rybník', '803', '1993-01-01', NULL, 6, GETDATE(), 'system'),
(7, '7', N'koryto vodního toku přirozené nebo upravené', N'watercourse bed', N'tok přirozený', '802', '1993-01-01', NULL, 7, GETDATE(), 'system'),
(8, '8', N'koryto vodního toku umělé', N'artificial watercourse', N'tok umělý', '802', '1993-01-01', NULL, 8, GETDATE(), 'system'),
(9, '9', N'vodní nádrž přírodní', N'natural reservoir', N'nádrž přírodní', '803', '1993-01-01', NULL, 9, GETDATE(), 'system'),
(10, '10', N'vodní nádrž umělá', N'artificial reservoir', N'nádrž umělá', '803', '1993-01-01', NULL, 10, GETDATE(), 'system'),
(11, '11', N'zamokřená plocha', N'wetland', N'zamokřená pl.', '804', '1993-01-01', NULL, 11, GETDATE(), 'system'),
(12, '12', N'společný dvůr', N'common courtyard', N'společný dvůr', '319', '1993-01-01', NULL, 12, GETDATE(), 'system'),
(13, '13', N'zbořeniště', N'demolition site', N'zbořeniště', '319', '1993-01-01', NULL, 13, GETDATE(), 'system'),
(14, '14', N'dráha', N'railway', N'dráha', NULL, '1993-01-01', NULL, 14, GETDATE(), 'system'),
(15, '15', N'dálnice', N'motorway', N'dálnice', NULL, '1993-01-01', NULL, 15, GETDATE(), 'system'),
(16, '16', N'silnice', N'road', N'silnice', NULL, '1993-01-01', NULL, 16, GETDATE(), 'system'),
(17, '17', N'ostatní komunikace', N'other road', N'ostat.komunikace', NULL, '1993-01-01', NULL, 17, GETDATE(), 'system'),
(18, '18', N'ostatní dopravní plocha', N'other transport area', N'ost.dopravní pl.', NULL, '1993-01-01', NULL, 18, GETDATE(), 'system'),
(19, '19', N'zeleň', N'greenery', N'zeleň', '314', '1993-01-01', NULL, 19, GETDATE(), 'system'),
(20, '20', N'sportoviště a rekreační plocha', N'sports area', N'sport.a rekr.pl.', NULL, '1993-01-01', NULL, 20, GETDATE(), 'system'),
(21, '21', N'pohřebiště', N'cemetery', N'pohřeb.', '315', '1993-01-01', NULL, 21, GETDATE(), 'system'),
(22, '22', N'kulturní a osvětová plocha', N'cultural area', N'kult.a osvět.pl.', NULL, '1993-01-01', NULL, 22, GETDATE(), 'system'),
(23, '23', N'manipulační plocha', N'handling area', N'manipulační pl.', NULL, '1993-01-01', NULL, 23, GETDATE(), 'system'),
(24, '24', N'dobývací prostor', N'mining area', N'dobývací prost.', '701', '1993-01-01', NULL, 24, GETDATE(), 'system'),
(25, '25', N'skládka', N'dump', N'skládka', NULL, '1993-01-01', NULL, 25, GETDATE(), 'system'),
(26, '26', N'jiná plocha', N'other area', N'jiná plocha', NULL, '1993-01-01', NULL, 26, GETDATE(), 'system'),
(27, '27', N'neplodná půda', N'barren land', N'neplodná půda', '316', '1993-01-01', NULL, 27, GETDATE(), 'system'),
(28, '28', N'vodní plocha, na které je budova', N'water area with building', N'vod.pl.s budovou', NULL, '2007-03-01', NULL, 28, GETDATE(), 'system'),
(29, '29', N'fotovoltaická elektrárna', N'photovoltaic power plant', N'foto. elektrárna', NULL, '2014-01-01', NULL, 29, GETDATE(), 'system'),
(30, '30', N'mez, stráň', N'balk, slope', N'mez, stráň', NULL, '2017-04-01', NULL, 30, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_land_uses OFF;
");

            // Building Types
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_building_types ON;
INSERT INTO cuzk_building_types (Id, Code, name_cs, name_en, abbreviation, entry_code, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '1', N'budova s číslem popisným', N'building with descriptive number', N'č.p.', 1, '1993-01-01', NULL, 1, GETDATE(), 'system'),
(2, '2', N'budova s číslem evidenčním', N'building with registration number', N'č.e.', 1, '1993-01-01', NULL, 2, GETDATE(), 'system'),
(3, '3', N'budova bez čísla popisného nebo evidenčního', N'building without number', N'bez čp/če', 0, '1993-01-01', NULL, 3, GETDATE(), 'system'),
(4, '4', N'rozestavěná budova', N'building under construction', N'rozestav.', 0, '1993-01-01', NULL, 4, GETDATE(), 'system'),
(5, '5', N'poschoďová garáž', N'multi-storey garage', N'posch.gar', 0, '1993-01-01', '1999-12-31', 5, GETDATE(), 'system'),
(6, '6', N'vodní dílo', N'water structure', N'vod.dílo', 0, '2007-01-01', NULL, 6, GETDATE(), 'system'),
(7, '7', N'budova s rozestavěnými jednotkami', N'building with units under construction', N's roz.jed', 0, '2015-08-01', NULL, 7, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_building_types OFF;
");

            // Building Uses
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_building_uses ON;
INSERT INTO cuzk_building_uses (Id, Code, name_cs, name_en, abbreviation, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '1', N'průmyslový objekt', N'industrial building', N'prům.obj', '1993-01-01', NULL, 1, GETDATE(), 'system'),
(2, '2', N'zemědělská usedlost', N'agricultural homestead', N'zem.used', '1993-01-01', NULL, 2, GETDATE(), 'system'),
(3, '3', N'objekt k bydlení', N'residential building', N'bydlení', '1993-01-01', NULL, 3, GETDATE(), 'system'),
(4, '4', N'objekt lesního hospodářství', N'forestry building', N'les.hosp', '1993-01-01', NULL, 4, GETDATE(), 'system'),
(5, '5', N'objekt občanské vybavenosti', N'civic amenities building', N'obč.vyb', '1993-01-01', NULL, 5, GETDATE(), 'system'),
(6, '6', N'bytový dům', N'apartment building', N'byt.dům', '1993-01-01', NULL, 6, GETDATE(), 'system'),
(7, '7', N'rodinný dům', N'family house', N'rod.dům', '1993-01-01', NULL, 7, GETDATE(), 'system'),
(8, '8', N'stavba pro rodinnou rekreaci', N'recreational building', N'rod.rekr', '1993-01-01', NULL, 8, GETDATE(), 'system'),
(9, '9', N'stavba pro shromažďování většího počtu osob', N'assembly building', N'shromaž.', '1993-01-01', NULL, 9, GETDATE(), 'system'),
(10, '10', N'stavba pro obchod', N'commercial building', N'obchod', '1993-01-01', NULL, 10, GETDATE(), 'system'),
(11, '11', N'stavba ubytovacího zařízení', N'accommodation building', N'ubyt.zař', '1993-01-01', NULL, 11, GETDATE(), 'system'),
(12, '12', N'stavba pro výrobu a skladování', N'production and storage building', N'výroba', '1993-01-01', NULL, 12, GETDATE(), 'system'),
(13, '13', N'zemědělská stavba', N'agricultural building', N'zem.stav', '1993-01-01', NULL, 13, GETDATE(), 'system'),
(14, '14', N'stavba pro administrativu', N'administrative building', N'adminis.', '1993-01-01', NULL, 14, GETDATE(), 'system'),
(15, '15', N'stavba občanského vybavení', N'civic equipment building', N'obč.vyb.', '1993-01-01', NULL, 15, GETDATE(), 'system'),
(16, '16', N'stavba technického vybavení', N'technical equipment building', N'tech.vyb', '1993-01-01', NULL, 16, GETDATE(), 'system'),
(17, '17', N'stavba pro dopravu', N'transport building', N'doprava', '1993-01-01', NULL, 17, GETDATE(), 'system'),
(18, '18', N'garáž', N'garage', N'garáž', '1993-01-01', NULL, 18, GETDATE(), 'system'),
(19, '19', N'jiná stavba', N'other building', N'jiná st.', '1993-01-01', NULL, 19, GETDATE(), 'system'),
(20, '20', N'víceúčelová stavba', N'multi-purpose building', N'víceúčel', '2007-03-01', NULL, 20, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_building_uses OFF;
");

            // Unit Types
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_unit_types ON;
INSERT INTO cuzk_unit_types (Id, Code, name_cs, name_en, abbreviation, civil_code, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '1', N'byt nebo nebytový prostor', N'flat or non-residential space', NULL, 0, '1993-01-01', '2013-12-31', 1, GETDATE(), 'system'),
(2, '2', N'rozestavěný byt nebo nebytový prostor', N'flat or space under construction', N'rozest.', 0, '1993-01-01', '2013-12-31', 2, GETDATE(), 'system'),
(3, '3', N'jednotka vymezená podle zákona o vlastnictví bytů', N'unit defined by Ownership of Flats Act', N'byt.z.', 0, '2014-01-01', NULL, 3, GETDATE(), 'system'),
(4, '4', N'jednotka vymezená podle občanského zákoníku', N'unit defined by Civil Code', N'obč.z.', 1, '2014-01-01', NULL, 4, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_unit_types OFF;
");

            // Unit Uses
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_unit_uses ON;
INSERT INTO cuzk_unit_uses (Id, Code, name_cs, name_en, abbreviation, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '1', N'byt', N'flat', N'byt', '1993-01-01', NULL, 1, GETDATE(), 'system'),
(2, '2', N'ateliér', N'studio', N'ateliér', '1993-01-01', NULL, 2, GETDATE(), 'system'),
(3, '3', N'garáž', N'garage', N'garáž', '1993-01-01', NULL, 3, GETDATE(), 'system'),
(4, '4', N'dílna nebo provozovna', N'workshop or business premises', N'dílna', '1993-01-01', NULL, 4, GETDATE(), 'system'),
(5, '5', N'jiný nebytový prostor', N'other non-residential space', N'j.nebyt', '1993-01-01', NULL, 5, GETDATE(), 'system'),
(6, '6', N'rozestavěná jednotka', N'unit under construction', N'rozest.', '2013-12-18', NULL, 6, GETDATE(), 'system'),
(7, '7', N'skupina bytů', N'group of flats', N'sk.byt', '2014-01-01', NULL, 7, GETDATE(), 'system'),
(8, '8', N'skupina nebytových prostorů', N'group of non-residential spaces', N'sk.neb', '2014-01-01', NULL, 8, GETDATE(), 'system'),
(9, '9', N'skupina bytů a nebytových prostorů', N'group of flats and non-residential spaces', N'sk.bneb', '2014-01-01', NULL, 9, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_unit_uses OFF;
");

            // Property Protection Types
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_property_protection_types ON;
INSERT INTO cuzk_property_protection_types (Id, Code, name_cs, name_en, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '0', N'neurčen', N'unspecified', '1993-01-01', NULL, 1, GETDATE(), 'system'),
(2, '1', N'ochrana přírody a krajiny', N'nature and landscape protection', '1999-06-01', NULL, 2, GETDATE(), 'system'),
(3, '2', N'památková ochrana', N'monument protection', '1999-06-01', NULL, 3, GETDATE(), 'system'),
(4, '3', N'ochrana přírodních léčivých zdrojů', N'spa and mineral water protection', '1999-06-01', NULL, 4, GETDATE(), 'system'),
(5, '4', N'ochrana nerostného bohatství', N'mineral wealth protection', '1999-06-01', NULL, 5, GETDATE(), 'system'),
(6, '5', N'ochrana značky geodetického bodu', N'geodetic point protection', '1999-06-01', NULL, 6, GETDATE(), 'system'),
(7, '6', N'jiná ochrana pozemku', N'other land protection', '1999-06-01', NULL, 7, GETDATE(), 'system'),
(8, '7', N'ochrana vodního díla', N'water structure protection', '2002-01-01', NULL, 8, GETDATE(), 'system'),
(9, '8', N'ochrana vodního zdroje', N'water source protection', '2002-01-01', NULL, 9, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_property_protection_types OFF;
");

            // Property Protections
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_property_protections ON;
INSERT INTO cuzk_property_protections (Id, Code, name_cs, name_en, protection_type_code, applies_to_land, applies_to_building, applies_to_unit, applies_to_building_right, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '3', N'vnitřní lázeňské území', N'inner spa area', '0', 1, 1, 1, 1, '1993-01-01', NULL, 3, GETDATE(), 'system'),
(2, '4', N'památkově chráněné území', N'monument protected area', '0', 1, 1, 1, 1, '1993-01-01', NULL, 4, GETDATE(), 'system'),
(3, '9', N'ochranné pásmo národního parku', N'national park buffer zone', '1', 1, 0, 1, 1, '1993-01-01', NULL, 9, GETDATE(), 'system'),
(4, '10', N'chráněná krajinná oblast - I.zóna', N'protected landscape area - zone I', '1', 1, 0, 1, 1, '1993-01-01', NULL, 10, GETDATE(), 'system'),
(5, '15', N'nemovitá národní kulturní památka', N'national cultural monument', '2', 1, 1, 1, 1, '1998-10-15', NULL, 15, GETDATE(), 'system'),
(6, '18', N'nemovitá kulturní památka', N'cultural monument', '2', 1, 1, 1, 1, '1998-10-15', NULL, 18, GETDATE(), 'system'),
(7, '26', N'pozemek určený k plnění funkcí lesa', N'forest functions land', '6', 1, 0, 0, 1, '1998-10-15', NULL, 26, GETDATE(), 'system'),
(8, '27', N'zemědělský půdní fond', N'agricultural land fund', '6', 1, 0, 0, 1, '1998-10-15', NULL, 27, GETDATE(), 'system'),
(9, '28', N'ochranné pásmo vodního díla', N'water structure buffer zone', '7', 1, 1, 1, 1, '2002-01-01', NULL, 28, GETDATE(), 'system'),
(10, '38', N'národní park', N'national park', '1', 1, 0, 1, 1, '2019-01-03', NULL, 38, GETDATE(), 'system'),
(11, '39', N'chráněná krajinná oblast', N'protected landscape area', '1', 1, 0, 1, 1, '2020-02-14', NULL, 39, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_property_protections OFF;
");

            // Simplified Parcel Sources
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_simplified_parcel_sources ON;
INSERT INTO cuzk_simplified_parcel_sources (Id, Code, name_cs, name_en, abbreviation, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '3', N'Evidence nemovitostí', N'Real Estate Register', N'EN', '1993-01-01', NULL, 1, GETDATE(), 'system'),
(2, '4', N'Pozemkový katastr', N'Land Cadastre', N'PK', '1993-01-01', NULL, 2, GETDATE(), 'system'),
(3, '6', N'Přídělový plán nebo jiný podklad', N'Allotment plan', N'GP', '1993-01-01', NULL, 3, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_simplified_parcel_sources OFF;
");

            // Building Right Purposes
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_building_right_purposes ON;
INSERT INTO cuzk_building_right_purposes (Id, Code, name_cs, name_en, valid_from, valid_to, sort_order, created_at, created_by) VALUES
(1, '1', N'bytový dům', N'apartment building', '2014-01-01', NULL, 1, GETDATE(), 'system'),
(2, '2', N'rodinný dům', N'family house', '2014-01-01', NULL, 2, GETDATE(), 'system'),
(3, '3', N'stavba pro sport a rekreaci', N'sports and recreation building', '2014-01-01', NULL, 3, GETDATE(), 'system'),
(4, '6', N'stavba pro administrativu', N'administrative building', '2014-01-01', NULL, 6, GETDATE(), 'system'),
(5, '7', N'stavba vodního díla', N'water structure', '2014-01-01', NULL, 7, GETDATE(), 'system'),
(6, '10', N'garáž', N'garage', '2014-01-01', NULL, 10, GETDATE(), 'system'),
(7, '99', N'neuveden', N'not specified', '2018-01-23', NULL, 99, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_building_right_purposes OFF;
");

            // Soil Ecological Units (sample)
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT cuzk_soil_ecological_units ON;
INSERT INTO cuzk_soil_ecological_units (Id, Code, name_cs, name_en, description_cs, price, valid_from, sort_order, created_at, created_by) VALUES
(1, '00100', N'BPEJ 00100', N'BPEJ 00100', N'Klimatický region 0, HPJ 01', 17.23, '2014-01-01', 1, GETDATE(), 'system'),
(2, '10100', N'BPEJ 10100', N'BPEJ 10100', N'Klimatický region 1, HPJ 01', 15.88, '2014-01-01', 2, GETDATE(), 'system'),
(3, '20100', N'BPEJ 20100', N'BPEJ 20100', N'Klimatický region 2, HPJ 01', 14.55, '2014-01-01', 3, GETDATE(), 'system'),
(4, '30100', N'BPEJ 30100', N'BPEJ 30100', N'Klimatický region 3, HPJ 01', 13.25, '2014-01-01', 4, GETDATE(), 'system'),
(5, '40100', N'BPEJ 40100', N'BPEJ 40100', N'Klimatický region 4, HPJ 01', 12.11, '2014-01-01', 5, GETDATE(), 'system'),
(6, '50100', N'BPEJ 50100', N'BPEJ 50100', N'Klimatický region 5, HPJ 01', 11.00, '2014-01-01', 6, GETDATE(), 'system'),
(7, '60100', N'BPEJ 60100', N'BPEJ 60100', N'Klimatický region 6, HPJ 01', 9.22, '2014-01-01', 7, GETDATE(), 'system'),
(8, '70100', N'BPEJ 70100', N'BPEJ 70100', N'Klimatický region 7, HPJ 01', 7.60, '2014-01-01', 8, GETDATE(), 'system'),
(9, '80100', N'BPEJ 80100', N'BPEJ 80100', N'Klimatický region 8, HPJ 01', 6.07, '2014-01-01', 9, GETDATE(), 'system'),
(10, '90100', N'BPEJ 90100', N'BPEJ 90100', N'Klimatický region 9, HPJ 01', 4.65, '2014-01-01', 10, GETDATE(), 'system');
SET IDENTITY_INSERT cuzk_soil_ecological_units OFF;
");

            // KSO Building Classifications - Level 1
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT building_classifications ON;
INSERT INTO building_classifications (Id, Code, name_cs, name_en, Level, parent_id, sort_order, created_at) VALUES
(1, '801', N'Budovy občanské výstavby', N'Civil Buildings', 1, NULL, 1, GETDATE()),
(2, '802', N'Haly občanské výstavby', N'Civil Halls', 1, NULL, 2, GETDATE()),
(3, '803', N'Budovy pro bydlení', N'Residential Buildings', 1, NULL, 3, GETDATE()),
(4, '811', N'Haly pro výrobu a služby', N'Production Halls', 1, NULL, 4, GETDATE()),
(5, '812', N'Budovy pro výrobu a služby', N'Production Buildings', 1, NULL, 5, GETDATE()),
(6, '813', N'Věže, stožáry, komíny', N'Towers, Masts', 1, NULL, 6, GETDATE()),
(7, '821', N'Mosty', N'Bridges', 1, NULL, 7, GETDATE()),
(8, '822', N'Komunikace pozemní a letiště', N'Roads and Airports', 1, NULL, 8, GETDATE()),
(9, '823', N'Plochy a úpravy území', N'Land Areas', 1, NULL, 9, GETDATE()),
(10, '824', N'Dráhy kolejové', N'Railways', 1, NULL, 10, GETDATE()),
(11, '827', N'Vedení trubní', N'Pipelines', 1, NULL, 11, GETDATE()),
(12, '828', N'Vedení elektrická', N'Electrical Lines', 1, NULL, 12, GETDATE()),
(13, '831', N'Hydromeliorace', N'Hydromelioration', 1, NULL, 13, GETDATE()),
(14, '832', N'Hráze a objekty na tocích', N'Dams', 1, NULL, 14, GETDATE()),
(15, '833', N'Nádrže na tocích', N'Reservoirs', 1, NULL, 15, GETDATE());
");

            // KSO Building Classifications - Level 2 for 801
            migrationBuilder.Sql(@"
INSERT INTO building_classifications (Id, Code, name_cs, name_en, Level, parent_id, sort_order, created_at) VALUES
(16, '801.1', N'Budovy pro zdravotní péči', N'Healthcare Buildings', 2, 1, 1, GETDATE()),
(17, '801.2', N'Budovy pro komunální služby', N'Municipal Services', 2, 1, 2, GETDATE()),
(18, '801.3', N'Budovy pro výuku a výchovu', N'Education Buildings', 2, 1, 3, GETDATE()),
(19, '801.4', N'Budovy pro vědu a kulturu', N'Science and Culture', 2, 1, 4, GETDATE()),
(20, '801.5', N'Budovy pro tělovýchovu', N'Sports Buildings', 2, 1, 5, GETDATE()),
(21, '801.6', N'Budovy pro správu', N'Administration', 2, 1, 6, GETDATE()),
(22, '801.7', N'Budovy pro ubytování', N'Accommodation', 2, 1, 7, GETDATE()),
(23, '801.8', N'Budovy pro obchod', N'Retail Buildings', 2, 1, 8, GETDATE()),
(24, '801.9', N'Budovy pro sociální péči', N'Social Care', 2, 1, 9, GETDATE());
");

            // KSO Building Classifications - Level 2 for 803
            migrationBuilder.Sql(@"
INSERT INTO building_classifications (Id, Code, name_cs, name_en, Level, parent_id, sort_order, created_at) VALUES
(25, '803.1', N'Budovy jednobytové', N'Single-family houses', 2, 3, 1, GETDATE()),
(26, '803.2', N'Budovy dvojbytové', N'Two-family houses', 2, 3, 2, GETDATE()),
(27, '803.3', N'Domy bytové typové', N'Standard apartments', 2, 3, 3, GETDATE()),
(28, '803.4', N'Domy bytové netypové', N'Non-standard apartments', 2, 3, 4, GETDATE()),
(29, '803.5', N'Domovy důchodců', N'Retirement homes', 2, 3, 5, GETDATE()),
(30, '803.6', N'Budovy pro přechodné ubytování', N'Temporary accommodation', 2, 3, 6, GETDATE());
");

            // KSO Building Classifications - Level 3 for 801.1
            migrationBuilder.Sql(@"
INSERT INTO building_classifications (Id, Code, name_cs, name_en, Level, parent_id, sort_order, created_at) VALUES
(31, '801.11', N'budovy nemocnic', N'hospital buildings', 3, 16, 1, GETDATE()),
(32, '801.12', N'polikliniky', N'polyclinics', 3, 16, 2, GETDATE()),
(33, '801.13', N'zdravotnická střediska', N'medical centers', 3, 16, 3, GETDATE()),
(34, '801.14', N'ambulatoria', N'ambulatories', 3, 16, 4, GETDATE()),
(35, '801.15', N'budovy lázeňské péče', N'spa care buildings', 3, 16, 5, GETDATE()),
(36, '801.16', N'budovy veterinární péče', N'veterinary buildings', 3, 16, 6, GETDATE());
SET IDENTITY_INSERT building_classifications OFF;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM building_classifications");
            migrationBuilder.Sql("DELETE FROM cuzk_soil_ecological_units");
            migrationBuilder.Sql("DELETE FROM cuzk_building_right_purposes");
            migrationBuilder.Sql("DELETE FROM cuzk_simplified_parcel_sources");
            migrationBuilder.Sql("DELETE FROM cuzk_property_protections");
            migrationBuilder.Sql("DELETE FROM cuzk_property_protection_types");
            migrationBuilder.Sql("DELETE FROM cuzk_unit_uses");
            migrationBuilder.Sql("DELETE FROM cuzk_unit_types");
            migrationBuilder.Sql("DELETE FROM cuzk_building_uses");
            migrationBuilder.Sql("DELETE FROM cuzk_building_types");
            migrationBuilder.Sql("DELETE FROM cuzk_land_uses");
            migrationBuilder.Sql("DELETE FROM cuzk_area_determinations");
            migrationBuilder.Sql("DELETE FROM cuzk_land_types");
        }
    }
}
