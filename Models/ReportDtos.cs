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

    public record MaritalStatusChangeTrendRow(
    int DataYear,
    decimal TotalMSChanges,
    decimal Divorces,
    decimal Separations,
    decimal WidowsWidowers,
    decimal Reunions
);

    public record MarriageAgeDistributionTrendRow(
    int DataYear,
    decimal MaleUnder18,
    decimal Male18To25,
    decimal Male25To35,
    decimal Male35Plus,
    decimal FemaleUnder18,
    decimal Female18To25,
    decimal Female25To35,
    decimal Female35Plus
);
    public record BirthOutcomePregnancyTrendRow(
        int DataYear,
        decimal LiveBirths,
        decimal StillBirths,
        decimal AbortionsMiscarriages,
        decimal CurrentlyPregnant,
        decimal TotalOutcome
    );


}
