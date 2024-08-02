using PillPal.Application.Features.CustomerPackages;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class CustomerPackagesController(ICustomerPackageService customerPackageService)
    : ControllerBase
{
    /// <summary>
    /// Get all customer packages
    /// </summary>
    /// <remarks>Requires administrative policy (e.g. admin, manager)</remarks>
    /// <response code="200">Returns a list of customer packages</response>
    [Authorize(Policy.Administrative)]
    [HttpGet("packages", Name = "GetCustomerPackages")]
    [ProducesResponseType(typeof(IEnumerable<CustomerPackageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerPackagesAsync()
    {
        var customerPackages = await customerPackageService.GetCustomerPackagesAsync();

        return Ok(customerPackages);
    }

    /// <summary>
    /// Get customer package by id
    /// </summary>
    /// <param name="packageId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <remarks>Requires administrative policy (e.g. admin, manager)</remarks>
    /// <response code="200">Returns a customer package</response>
    /// <response code="404">If the customer package is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpGet("packages/{packageId}", Name = "GetCustomerPackage")]
    [ProducesResponseType(typeof(CustomerPackageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerPackageAsync(Guid packageId)
    {
        var customerPackage = await customerPackageService.GetCustomerPackageAsync(packageId);

        return Ok(customerPackage);
    }
}
