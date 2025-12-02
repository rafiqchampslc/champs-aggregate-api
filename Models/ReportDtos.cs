namespace Champs.Api.Models
{
    public record SiteDto(int SiteId);
    public record YearDto(int Year);

    public record HouseholdTrendRow(string IndicatorCode, int DataYear, decimal Value);

    public record TotalPopulationRow(int DataYear, decimal TotalPopulation);

    public record PopulationPyramidRow(string AgeGroupLabel, decimal MaleCount, decimal FemaleCount);

    public record ChildPyramidRow(string AgeGroupLabel, decimal MaleCount, decimal FemaleCount);
    public record HouseholdSizeRow(int DataYear, decimal HouseholdSize);

    public record MigrationTrendRow(int DataYear, decimal InMigration, decimal OutMigration);
    public record PopulationSummaryTrendRow(int DataYear, decimal TotalPopulation, decimal ReproductiveAgeWomen, decimal Under5Children);

}
