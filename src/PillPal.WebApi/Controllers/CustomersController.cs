using PillPal.Application.Features.CustomerPackages;
using PillPal.Application.Features.Customers;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class CustomersController(ICustomerService customerService, ICustomerPackageService customerPackageService)
    : ControllerBase
{
    /// <summary>
    /// Get customer informations
    /// </summary>
    /// <remarks>Requires customer policy</remarks>
    /// <response code="200">Returns a customer informations</response>
    [Authorize(Policy.Customer)]
    [HttpGet("info", Name = "GetCustomerInfo")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerInfoAsync()
    {
        var customerInfo = await customerService.GetCustomerInfoAsync();

        return Ok(customerInfo);
    }

    /// <summary>
    /// Get customer packages
    /// </summary>
    /// <remarks>Requires customer policy</remarks>
    /// <response code="200">Returns a list of customer packages</response>
    [Authorize(Policy.Customer)]
    [HttpGet("packages", Name = "GetOwnCustomerPackages")]
    [ProducesResponseType(typeof(IEnumerable<CustomerPackageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerPackagesAsync()
    {
        var customerPackages = await customerPackageService.GetCustomerPackagesAsync(isCustomer: true);

        return Ok(customerPackages);
    }

    /// <summary>
    /// Get customer package by id
    /// </summary>
    /// <param name="packageId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <remarks>Requires customer policy</remarks>
    /// <response code="200">Returns a customer package</response>
    /// <response code="404">If the customer package is not found</response>
    [Authorize(Policy.Customer)]
    [HttpGet("packages/{packageId}", Name = "GetOwnCustomerPackage")]
    [ProducesResponseType(typeof(CustomerPackageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerPackageAsync(Guid packageId)
    {
        var customerPackage = await customerPackageService.GetCustomerPackageAsync(packageId, isCustomer: true);

        return Ok(customerPackage);
    }

    /// <summary>
    /// Update customer informations
    /// </summary>
    /// <param name="updateCustomerDto"></param>
    /// <remarks>
    /// Requires customer policy
    /// 
    /// Sample request:
    /// 
    ///     PUT /api/customers
    ///     {
    ///         "dob": "2002-01-01",
    ///         "address": "Q9, HCMC, Vietnam",
    ///         "phoneNumber": "0123456789"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated customer</response>
    /// <response code="404">If the customer is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Customer)]
    [HttpPut(Name = "UpdateCustomer")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateCustomerAsync(UpdateCustomerDto updateCustomerDto)
    {
        var customer = await customerService.UpdateCustomerAsync(updateCustomerDto);

        return Ok(customer);
    }

    /// <summary>
    /// Update customer meal time
    /// </summary>
    /// <param name="updateCustomerMealTimeDto"></param>
    /// <remarks>
    /// Requires customer policy
    /// 
    /// Sample request:
    /// 
    ///     PUT /api/customers/meal-time
    ///     {
    ///         "breakfastTime": "07:00",
    ///         "lunchTime": "12:00",
    ///         "afternoonTime": "16:00",
    ///         "dinnerTime": "18:00",
    ///         "mealTimeOffset": "00:15"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated customer meal time</response>
    /// <response code="404">If the customer is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Customer)]
    [HttpPut("meal-time", Name = "UpdateCustomerMealTime")]
    [ProducesResponseType(typeof(CustomerMealTimeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateCustomerMealTimeAsync(UpdateCustomerMealTimeDto updateCustomerMealTimeDto)
    {
        var customerMealTime = await customerService.UpdateCustomerMealTimeAsync(updateCustomerMealTimeDto);

        return Ok(customerMealTime);
    }
}
