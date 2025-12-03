using Champs.Api.Data;
using Champs.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Champs.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsRepository _repo;

        public ReportsController(IReportsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("sites")]
        public async Task<ActionResult<IEnumerable<SiteDto>>> GetSites()
        {
            return Ok(await _repo.GetSitesAsync());
        }

        [HttpGet("years")]
        public async Task<ActionResult<IEnumerable<YearDto>>> GetYears([FromQuery] int siteId)
        {
            return Ok(await _repo.GetYearsBySiteAsync(siteId));
        }

        [HttpGet("household-trend")]
        public async Task<ActionResult<IEnumerable<HouseholdTrendRow>>> GetHouseholdTrend([FromQuery] int siteId)
        {
            return Ok(await _repo.GetHouseholdTrendAsync(siteId));
        }

        [HttpGet("total-population")]
        public async Task<ActionResult<IEnumerable<TotalPopulationRow>>> GetTotalPopulation([FromQuery] int siteId)
        {
            return Ok(await _repo.GetTotalPopulationAsync(siteId));
        }

        [HttpGet("pyramid")]
        public async Task<ActionResult<IEnumerable<PopulationPyramidRow>>> GetPyramid(
            [FromQuery] int siteId,
            [FromQuery] int year)
        {
            return Ok(await _repo.GetPopulationPyramidAsync(siteId, year));
        }

        [HttpGet("child-pyramid")]
        public async Task<ActionResult<IEnumerable<ChildPyramidRow>>> GetChildPyramid(
            [FromQuery] int siteId,
            [FromQuery] int year)
        {
            return Ok(await _repo.GetChildPyramidAsync(siteId, year));
        }

        [HttpGet("household-size")]
        public async Task<ActionResult<IEnumerable<HouseholdSizeRow>>> GetHouseholdSize([FromQuery] int siteId)
        {
            return Ok(await _repo.GetHouseholdSizeTrendAsync(siteId));
        }

        [HttpGet("migration-trend")]
        public async Task<ActionResult<IEnumerable<MigrationTrendRow>>> GetMigrationTrend([FromQuery] int siteId)
        {
            return Ok(await _repo.GetMigrationTrendAsync(siteId));
        }

        [HttpGet("population-summary-trend")]
        public async Task<ActionResult<IEnumerable<PopulationSummaryTrendRow>>> GetPopulationSummaryTrend([FromQuery] int siteId)
        {
            return Ok(await _repo.GetPopulationSummaryTrendAsync(siteId));
        }

        [HttpGet("marital-status-change-trend")]
        public async Task<ActionResult<IEnumerable<MaritalStatusChangeTrendRow>>> GetMaritalStatusChangeTrend([FromQuery] int siteId)
        {
            return Ok(await _repo.GetMaritalStatusChangeTrendAsync(siteId));
        }
        [HttpGet("marriage-age-distribution-trend")]
        public async Task<ActionResult<IEnumerable<MarriageAgeDistributionTrendRow>>> GetMarriageAgeDistributionTrend([FromQuery] int siteId)
        {
            return Ok(await _repo.GetMarriageAgeDistributionTrendAsync(siteId));
        }

        [HttpGet("birth-outcome-pregnancy-trend")]
        public async Task<ActionResult<IEnumerable<BirthOutcomePregnancyTrendRow>>> GetBirthOutcomePregnancyTrend([FromQuery] int siteId)
        {
            return Ok(await _repo.GetBirthOutcomePregnancyTrendAsync(siteId));
        }


    }
}
