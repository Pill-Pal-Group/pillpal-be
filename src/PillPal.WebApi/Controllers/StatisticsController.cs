using PillPal.Application.Features.Statistics;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class StatisticsController(IStatisticService statisticService)
    : ControllerBase
{
    /// <summary>
    /// Get customer package percent report from time period
    /// </summary>
    /// <param name="reportTime"></param>
    /// <response code="200">Return customer package percent report</response>
    /// <response code="422">If input date is invalid</response>
    [Authorize(Policy.Admin)]
    [HttpGet("package-percent")]
    [ProducesResponseType(typeof(CustomerPackagePercentReport), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetCustomerPackagePercentAsync([FromQuery] ReportTimeRequest reportTime)
    {
        var result = await statisticService.GetCustomerPackagePercentAsync(reportTime);
        return Ok(result);
    }

    /// <summary>
    /// Get customer package report from time period
    /// </summary>
    /// <param name="reportTime"></param>
    /// <response code="200">Return customer package report</response>
    /// <response code="422">If input date is invalid</response>
    [Authorize(Policy.Admin)]
    [HttpGet("package-report")]
    [ProducesResponseType(typeof(CustomerPackageReport), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetCustomerPackageReportAsync([FromQuery] ReportTimeRequest reportTime)
    {
        var result = await statisticService.GetCustomerPackageReportAsync(reportTime);
        return Ok(result);
    }

    /// <summary>
    /// Get overall report from time period
    /// </summary>
    /// <param name="reportTime"></param>
    /// <response code="200">Return overall report</response>
    /// <response code="422">If input date is invalid</response>
    [Authorize(Policy.Admin)]
    [HttpGet("reports")]
    [ProducesResponseType(typeof(ReportByTime), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetReportsAsync([FromQuery] ReportTimeRequest reportTime)
    {
        var result = await statisticService.GetReportsAsync(reportTime);
        return Ok(result);
    }

    /// <summary>
    /// Get top customer package report from time period
    /// </summary>
    /// <param name="reportTime"></param>
    /// <response code="200">Return top customer package report</response>
    /// <response code="422">If input date is invalid</response>
    [Authorize(Policy.Admin)]
    [HttpGet("top-package")]
    [ProducesResponseType(typeof(TopCustomerPackageReport), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetTopCustomerPackageAsync([FromQuery] ReportTimeRequest reportTime)
    {
        var result = await statisticService.GetTopCustomerPackageAsync(reportTime);
        return Ok(result);
    }

    /// <summary>
    /// Get total customer registration report from time period
    /// </summary>
    /// <param name="reportTime"></param>
    /// <response code="200">Return total customer registration report</response>
    /// <response code="422">If input date is invalid</response>
    [Authorize(Policy.Admin)]
    [HttpGet("customer-registration")]
    [ProducesResponseType(typeof(CustomerRegistrationReport), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetTotalCustomerRegistrationAsync([FromQuery] ReportTimeRequest reportTime)
    {
        var result = await statisticService.GetTotalCustomerRegistrationAsync(reportTime);
        return Ok(result);
    }
}
