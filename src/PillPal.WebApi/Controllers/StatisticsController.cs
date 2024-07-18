using PillPal.Application.Features.Statistics;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class StatisticsController(IStatisticService statisticService)
    : ControllerBase
{
    [HttpGet("package-percent")]
    [ProducesResponseType(typeof(CustomerPackagePercent), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerPackagePercentAsync([FromQuery] ReportTime reportTime)
    {
        var result = await statisticService.GetCustomerPackagePercentAsync(reportTime);
        return Ok(result);
    }

    [HttpGet("package-report")]
    [ProducesResponseType(typeof(CustomerPackageReport), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerPackageReportAsync([FromQuery] ReportTime reportTime)
    {
        var result = await statisticService.GetCustomerPackageReportAsync(reportTime);
        return Ok(result);
    }

    [HttpGet("reports")]
    [ProducesResponseType(typeof(ReportByTime), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReportsAsync([FromQuery] ReportTime reportTime)
    {
        var result = await statisticService.GetReportsAsync(reportTime);
        return Ok(result);
    }

    [HttpGet("top-package")]
    [ProducesResponseType(typeof(TopCustomerPackage), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTopCustomerPackageAsync([FromQuery] ReportTime reportTime)
    {
        var result = await statisticService.GetTopCustomerPackageAsync(reportTime);
        return Ok(result);
    }

    [HttpGet("customer-registration")]
    [ProducesResponseType(typeof(CustomerRegistration), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTotalCustomerRegistrationAsync([FromQuery] ReportTime reportTime)
    {
        var result = await statisticService.GetTotalCustomerRegistrationAsync(reportTime);
        return Ok(result);
    }
}
