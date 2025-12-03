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


    }
}
