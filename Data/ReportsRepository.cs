using System.Data;
using Microsoft.Data.SqlClient;
using Champs.Api.Models;

namespace Champs.Api.Data
{
    public interface IReportsRepository
    {
        Task<List<SiteDto>> GetSitesAsync();
        Task<List<YearDto>> GetYearsBySiteAsync(int siteId);
        Task<List<HouseholdTrendRow>> GetHouseholdTrendAsync(int siteId);
        Task<List<TotalPopulationRow>> GetTotalPopulationAsync(int siteId);
        Task<List<PopulationPyramidRow>> GetPopulationPyramidAsync(int siteId, int year);
        Task<List<ChildPyramidRow>> GetChildPyramidAsync(int siteId, int year);
        Task<List<HouseholdSizeRow>> GetHouseholdSizeTrendAsync(int siteId);
        Task<List<MigrationTrendRow>> GetMigrationTrendAsync(int siteId);
        Task<List<PopulationSummaryTrendRow>> GetPopulationSummaryTrendAsync(int siteId);
        Task<List<MaritalStatusChangeTrendRow>> GetMaritalStatusChangeTrendAsync(int siteId);
        Task<List<MarriageAgeDistributionTrendRow>> GetMarriageAgeDistributionTrendAsync(int siteId);
        Task<List<BirthOutcomePregnancyTrendRow>> GetBirthOutcomePregnancyTrendAsync(int siteId);
        Task<List<BirthsByMotherAgeTrendRow>> GetBirthsByMotherAgeTrendAsync(int siteId);
        Task<List<BirthPlaceOutcomeByYearRow>> GetBirthPlaceOutcomeByYearAsync(int siteId);
        Task<List<BirthDeathTrendRow>> GetBirthDeathTrendAsync(int siteId);
        Task<List<ChildDeathsAndStillbirthsTrendRow>> GetChildDeathsAndStillbirthsTrendAsync(int siteId);
        Task<List<Under5DeathAndStillbirthByPlaceRow>> GetUnder5DeathAndStillbirthByPlaceTrendAsync(int siteId);
        Task<List<MortalityRatesTrendRow>> GetMortalityRatesTrendAsync(int siteId);
        Task<List<PopulationPyramidAllYearsRow>> GetPopulationPyramidsAllYearsAsync(int siteId);
    }

    public class ReportsRepository : IReportsRepository
    {
        private readonly string _connectionString;

        public ReportsRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ChampsDb")!;
        }

        private SqlConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task<List<SiteDto>> GetSitesAsync()
        {
            var result = new List<SiteDto>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetSites", conn)
            { CommandType = CommandType.StoredProcedure };

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new SiteDto(reader.GetInt32(reader.GetOrdinal("SiteID"))));
            }

            return result;
        }

        public async Task<List<YearDto>> GetYearsBySiteAsync(int siteId)
        {
            var result = new List<YearDto>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetYearsBySite", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new YearDto(reader.GetInt32(reader.GetOrdinal("DataYear"))));
            }

            return result;
        }

        public async Task<List<HouseholdTrendRow>> GetHouseholdTrendAsync(int siteId)
        {
            var result = new List<HouseholdTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetHouseholdTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var indicator = reader.GetString(reader.GetOrdinal("IndicatorCode"));
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var value = reader.GetDecimal(reader.GetOrdinal("Value"));

                result.Add(new HouseholdTrendRow(indicator, year, value));
            }

            return result;
        }

        public async Task<List<TotalPopulationRow>> GetTotalPopulationAsync(int siteId)
        {
            var result = new List<TotalPopulationRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetTotalPopulation", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var total = reader.GetDecimal(reader.GetOrdinal("TotalPopulation"));

                result.Add(new TotalPopulationRow(year, total));
            }

            return result;
        }

        public async Task<List<PopulationPyramidRow>> GetPopulationPyramidAsync(int siteId, int year)
        {
            var result = new List<PopulationPyramidRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetPopulationPyramid", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);
            cmd.Parameters.AddWithValue("@Year", year);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var label = reader.GetString(reader.GetOrdinal("AgeGroupLabel"));
                var male = reader.GetDecimal(reader.GetOrdinal("MaleCount"));
                var female = reader.GetDecimal(reader.GetOrdinal("FemaleCount"));

                result.Add(new PopulationPyramidRow(label, male, female));
            }

            return result;
        }

        public async Task<List<ChildPyramidRow>> GetChildPyramidAsync(int siteId, int year)
        {
            var result = new List<ChildPyramidRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetChildPyramid", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);
            cmd.Parameters.AddWithValue("@Year", year);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var label = reader.GetString(reader.GetOrdinal("AgeGroupLabel"));
                var male = reader.GetDecimal(reader.GetOrdinal("MaleCount"));
                var female = reader.GetDecimal(reader.GetOrdinal("FemaleCount"));

                result.Add(new ChildPyramidRow(label, male, female));
            }

            return result;
        }

        public async Task<List<HouseholdSizeRow>> GetHouseholdSizeTrendAsync(int siteId)
        {
            var result = new List<HouseholdSizeRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetHouseholdSizeTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var size = reader.GetDecimal(reader.GetOrdinal("HouseholdSize"));

                result.Add(new HouseholdSizeRow(year, size));
            }

            return result;
        }

        public async Task<List<MigrationTrendRow>> GetMigrationTrendAsync(int siteId)
        {
            var result = new List<MigrationTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetMigrationTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var inMig = reader.GetDecimal(reader.GetOrdinal("InMigration"));
                var outMig = reader.GetDecimal(reader.GetOrdinal("OutMigration"));

                result.Add(new MigrationTrendRow(year, inMig, outMig));
            }

            return result;
        }

        public async Task<List<PopulationSummaryTrendRow>> GetPopulationSummaryTrendAsync(int siteId)
        {
            var result = new List<PopulationSummaryTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetPopulationSummaryTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var total = reader.GetDecimal(reader.GetOrdinal("TotalPopulation"));
                var raw = reader.GetDecimal(reader.GetOrdinal("ReproductiveAgeWomen"));
                var u5 = reader.GetDecimal(reader.GetOrdinal("Under5Children"));

                result.Add(new PopulationSummaryTrendRow(year, total, raw, u5));
            }

            return result;
        }

        public async Task<List<MaritalStatusChangeTrendRow>> GetMaritalStatusChangeTrendAsync(int siteId)
        {
            var result = new List<MaritalStatusChangeTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetMaritalStatusChangeTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var total = reader.GetDecimal(reader.GetOrdinal("TotalMSChanges"));
                var div = reader.GetDecimal(reader.GetOrdinal("Divorces"));
                var sep = reader.GetDecimal(reader.GetOrdinal("Separations"));
                var wid = reader.GetDecimal(reader.GetOrdinal("WidowsWidowers"));
                var reu = reader.GetDecimal(reader.GetOrdinal("Reunions"));

                result.Add(new MaritalStatusChangeTrendRow(year, total, div, sep, wid, reu));
            }

            return result;
        }

        public async Task<List<MarriageAgeDistributionTrendRow>> GetMarriageAgeDistributionTrendAsync(int siteId)
        {
            var result = new List<MarriageAgeDistributionTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetMarriageAgeDistributionTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var mU18 = reader.GetDecimal(reader.GetOrdinal("MaleUnder18"));
                var m18_25 = reader.GetDecimal(reader.GetOrdinal("Male18To25"));
                var m25_35 = reader.GetDecimal(reader.GetOrdinal("Male25To35"));
                var m35Plus = reader.GetDecimal(reader.GetOrdinal("Male35Plus"));
                var fU18 = reader.GetDecimal(reader.GetOrdinal("FemaleUnder18"));
                var f18_25 = reader.GetDecimal(reader.GetOrdinal("Female18To25"));
                var f25_35 = reader.GetDecimal(reader.GetOrdinal("Female25To35"));
                var f35Plus = reader.GetDecimal(reader.GetOrdinal("Female35Plus"));

                result.Add(new MarriageAgeDistributionTrendRow(
                    year, mU18, m18_25, m25_35, m35Plus, fU18, f18_25, f25_35, f35Plus));
            }

            return result;
        }

        public async Task<List<BirthOutcomePregnancyTrendRow>> GetBirthOutcomePregnancyTrendAsync(int siteId)
        {
            var result = new List<BirthOutcomePregnancyTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetBirthOutcomePregnancyTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var live = reader.GetDecimal(reader.GetOrdinal("LiveBirths"));
                var still = reader.GetDecimal(reader.GetOrdinal("StillBirths"));
                var abort = reader.GetDecimal(reader.GetOrdinal("AbortionsMiscarriages"));
                var preg = reader.GetDecimal(reader.GetOrdinal("CurrentlyPregnant"));
                var total = reader.GetDecimal(reader.GetOrdinal("TotalOutcome"));

                result.Add(new BirthOutcomePregnancyTrendRow(
                    year, live, still, abort, preg, total));
            }

            return result;
        }
        public async Task<List<BirthsByMotherAgeTrendRow>> GetBirthsByMotherAgeTrendAsync(int siteId)
        {
            var result = new List<BirthsByMotherAgeTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetBirthsByMotherAgeTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var b10_14 = reader.GetDecimal(reader.GetOrdinal("Births10_14"));
                var b15_19 = reader.GetDecimal(reader.GetOrdinal("Births15_19"));
                var b20_24 = reader.GetDecimal(reader.GetOrdinal("Births20_24"));
                var b25_29 = reader.GetDecimal(reader.GetOrdinal("Births25_29"));
                var b30_34 = reader.GetDecimal(reader.GetOrdinal("Births30_34"));
                var b35_39 = reader.GetDecimal(reader.GetOrdinal("Births35_39"));
                var b40_44 = reader.GetDecimal(reader.GetOrdinal("Births40_44"));
                var b45_49 = reader.GetDecimal(reader.GetOrdinal("Births45_49"));

                result.Add(new BirthsByMotherAgeTrendRow(
                    year, b10_14, b15_19, b20_24, b25_29, b30_34, b35_39, b40_44, b45_49));
            }

            return result;
        }
        public async Task<List<BirthPlaceOutcomeByYearRow>> GetBirthPlaceOutcomeByYearAsync(int siteId)
        {
            var result = new List<BirthPlaceOutcomeByYearRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetBirthPlaceOutcomeByYear", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var facilityLive = reader.GetDecimal(reader.GetOrdinal("FacilityLiveBirths"));
                var facilityStill = reader.GetDecimal(reader.GetOrdinal("FacilityStillbirths"));
                var homeCommLive = reader.GetDecimal(reader.GetOrdinal("HomeLiveBirths"));
                var homeCommStill = reader.GetDecimal(reader.GetOrdinal("HomeStillbirths"));
                var unknownLive = reader.GetDecimal(reader.GetOrdinal("UnknownLiveBirths"));
                var unknownStill = reader.GetDecimal(reader.GetOrdinal("UnknownStillbirths"));

                result.Add(new BirthPlaceOutcomeByYearRow(
                    year,
                    facilityLive,
                    facilityStill,
                    homeCommLive,
                    homeCommStill,
                    unknownLive,
                    unknownStill
                ));
            }

            return result;
        }
        public async Task<List<BirthDeathTrendRow>> GetBirthDeathTrendAsync(int siteId)
        {
            var result = new List<BirthDeathTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetBirthDeathTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var live = reader.GetDecimal(reader.GetOrdinal("TotalLiveBirths"));
                var deaths = reader.GetDecimal(reader.GetOrdinal("TotalDeaths"));

                result.Add(new BirthDeathTrendRow(year, live, deaths));
            }

            return result;
        }
        public async Task<List<ChildDeathsAndStillbirthsTrendRow>> GetChildDeathsAndStillbirthsTrendAsync(int siteId)
        {
            var result = new List<ChildDeathsAndStillbirthsTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetChildDeathsAndStillbirthsTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var neo = reader.GetDecimal(reader.GetOrdinal("NeonatalDeaths"));
                var infant = reader.GetDecimal(reader.GetOrdinal("InfantDeaths"));
                var u5 = reader.GetDecimal(reader.GetOrdinal("Under5Deaths"));
                var still = reader.GetDecimal(reader.GetOrdinal("Stillbirths"));

                result.Add(new ChildDeathsAndStillbirthsTrendRow(year, neo, infant, u5, still));
            }

            return result;
        }
        public async Task<List<Under5DeathAndStillbirthByPlaceRow>> GetUnder5DeathAndStillbirthByPlaceTrendAsync(int siteId)
        {
            var result = new List<Under5DeathAndStillbirthByPlaceRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetUnder5DeathAndStillbirthByPlaceTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var facilityU5 = reader.GetDecimal(reader.GetOrdinal("FacilityUnder5"));
                var facilityStill = reader.GetDecimal(reader.GetOrdinal("FacilityStill"));
                var homeU5 = reader.GetDecimal(reader.GetOrdinal("HomeUnder5"));
                var homeStill = reader.GetDecimal(reader.GetOrdinal("HomeStill"));
                var unknownU5 = reader.GetDecimal(reader.GetOrdinal("UnknownUnder5"));
                var unknownStill = reader.GetDecimal(reader.GetOrdinal("UnknownStill"));

                result.Add(new Under5DeathAndStillbirthByPlaceRow(
                    year, facilityU5, facilityStill, homeU5, homeStill, unknownU5, unknownStill));
            }

            return result;
        }
        public async Task<List<MortalityRatesTrendRow>> GetMortalityRatesTrendAsync(int siteId)
        {
            var result = new List<MortalityRatesTrendRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetMortalityRatesTrend", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var year = reader.GetInt32(reader.GetOrdinal("DataYear"));
                var under5Rate = reader.GetDecimal(reader.GetOrdinal("Under5Rate"));
                var infantRate = reader.GetDecimal(reader.GetOrdinal("InfantRate"));
                var neonatalRate = reader.GetDecimal(reader.GetOrdinal("NeonatalRate"));
                var stillbirthRate = reader.GetDecimal(reader.GetOrdinal("StillbirthRatio"));

                result.Add(new MortalityRatesTrendRow(
                    year, under5Rate, infantRate, neonatalRate, stillbirthRate));
            }

            return result;
        }
        public async Task<List<PopulationPyramidAllYearsRow>> GetPopulationPyramidsAllYearsAsync(int siteId)
        {
            var result = new List<PopulationPyramidAllYearsRow>();

            using var conn = CreateConnection();
            using var cmd = new SqlCommand("dbo.usp_GetPopulationPyramidsAllYears", conn)
            { CommandType = CommandType.StoredProcedure };

            cmd.Parameters.AddWithValue("@SiteID", siteId);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new PopulationPyramidAllYearsRow(
                    reader.GetInt32(reader.GetOrdinal("DataYear")),
                    reader.GetString(reader.GetOrdinal("AgeGroupLabel")),
                    reader.GetDecimal(reader.GetOrdinal("MaleCount")),
                    reader.GetDecimal(reader.GetOrdinal("FemaleCount"))
                ));
            }

            return result;
        }


    }
}
