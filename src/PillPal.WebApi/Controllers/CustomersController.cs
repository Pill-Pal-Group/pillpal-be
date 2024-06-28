using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.Customers;
using PillPal.Core.Constant;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class CustomersController(ICustomerService customerService)
    : ControllerBase
{
    /// <summary>
    /// Get all customers
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <response code="200">Returns a list of customers</response>
    [Authorize(Policy.Administrative)]
    [HttpGet(Name = "GetCustomers")]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomersAsync([FromQuery] CustomerQueryParameter queryParameter)
    {
        var customers = await customerService.GetCustomersAsync(queryParameter);

        return Ok(customers);
    }

    /// <summary>
    /// Get a customer by id
    /// </summary>
    /// <param name="customerId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a customer</response>
    /// <response code="404">If the customer is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpGet("{customerId:guid}", Name = "GetCustomerById")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await customerService.GetCustomerByIdAsync(customerId);

        return Ok(customer);
    }

    /// <summary>
    /// Get a customer informations
    /// </summary>
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
    /// Update a customer
    /// </summary>
    /// <param name="updateCustomerDto"></param>
    /// <remarks>
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
    /// Update a customer meal time
    /// </summary>
    /// <param name="updateCustomerMealTimeDto"></param>
    /// <remarks>
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
