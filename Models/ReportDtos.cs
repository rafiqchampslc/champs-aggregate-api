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
    public record BirthsByMotherAgeTrendRow(
    int DataYear,
    decimal Births10_14,
    decimal Births15_19,
    decimal Births20_24,
    decimal Births25_29,
    decimal Births30_34,
    decimal Births35_39,
    decimal Births40_44,
    decimal Births45_49
);
    public record BirthPlaceOutcomeByYearRow(
    int DataYear,
    decimal FacilityLive,
    decimal FacilityStill,
    decimal HomeCommLive,
    decimal HomeCommStill,
    decimal UnknownLive,
    decimal UnknownStill
);

    public record BirthDeathTrendRow(
        int DataYear,
        decimal TotalLiveBirths,
        decimal TotalDeaths
    );

    public record ChildDeathsAndStillbirthsTrendRow(
        int DataYear,
        decimal NeonatalDeaths,
        decimal InfantDeaths,
        decimal Under5Deaths,
        decimal Stillbirths
    );
    public record Under5DeathAndStillbirthByPlaceRow(
        int DataYear,
        decimal FacilityUnder5,
        decimal FacilityStill,
        decimal HomeUnder5,
        decimal HomeStill,
        decimal UnknownUnder5,
        decimal UnknownStill
    );

    public record MortalityRatesTrendRow(
        int DataYear,
        decimal Under5Rate,
        decimal InfantRate,
        decimal NeonatalRate,
        decimal StillbirthRatio
    );
    public record PopulationPyramidAllYearsRow(
        int DataYear,
        string AgeGroupLabel,
        decimal MaleCount,
        decimal FemaleCount,
        decimal HHSize
    );
    public record Under5ChildPyramidRow(
        int DataYear,
        string AgeGroupLabel,
        decimal MaleCount,
        decimal FemaleCount
    );

    public record MigrationRatesPerThousandTrendRow(
    int DataYear,
    decimal InMigrationRatePerThousand,
    decimal OutMigrationRatePerThousand
);
    public record HouseholdVisitOutcomesTrendRow(
    int DataYear,
    decimal TotalHouseholdsInCatchment,
    decimal HouseholdsVisited,
    decimal HouseholdsInterviewed,
    decimal ActiveHouseholds,
    decimal AbsentHouseholds,
    decimal RefusedHouseholds
);
    public record SiteAggregatedReportRow(
           int SiteId,
           string SiteName,
           string CountryName,
           int DataYear,
           string IndicatorCode,
           string IndicatorName,
           string DataType,
           DateTime? LastEntryDate,
           decimal IndicatorValue
       );
    public record PregnancyOutcomeStackedRow(
    int DataYear,
    decimal LiveBirths,
    decimal Stillbirths,
    decimal AbortionMiscarriage
);

    public record CumulativeUnder5DeathsRow(
    int DataYear,
    decimal DeathsWithinFirst24Hours,
    decimal Deaths1To6Days,
    decimal Deaths7To29Days,
    decimal Deaths29DaysTo11Months,
    decimal Deaths1To2Years,
    decimal Deaths2To3Years,
    decimal Deaths3To4Years,
    decimal Deaths4To5Years
);


}
