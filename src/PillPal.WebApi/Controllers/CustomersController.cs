using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Customers;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class CustomersController(ICustomerService customerService)
    : ControllerBase
{
    /// <summary>
    /// Get all customers
    /// </summary>
    /// <response code="200">Returns a list of customers</response>
    [HttpGet(Name = "GetCustomers")]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomersAsync()
    {
        var customers = await customerService.GetCustomersAsync();

        return Ok(customers);
    }

    /// <summary>
    /// Get a customer by id
    /// </summary>
    /// <param name="customerId"></param>
    /// <response code="200">Returns a customer</response>
    /// <response code="404">If the customer is not found</response>
    [HttpGet("{customerId:guid}", Name = "GetCustomerById")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await customerService.GetCustomerByIdAsync(customerId);

        return Ok(customer);
    }

    /// <summary>
    /// Create a customer
    /// </summary>
    /// <param name="createCustomerDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/customers
    ///     {
    ///         "dob": "2000-01-01T00:00:00+00:00",
    ///         "address": "123 Main St",
    ///         "identityUserId": "00000000-0000-0000-0000-000000000000"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created customer</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPost(Name = "CreateCustomer")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
    {
        var customer = await customerService.CreateCustomerAsync(createCustomerDto);

        return CreatedAtRoute("GetCustomerById", new { customerId = customer.Id }, customer);
    }

    /// <summary>
    /// Update a customer
    /// </summary>
    /// <param name="customerId"></param>
    /// <param name="updateCustomerDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/customers/{customerId}
    ///     {
    ///         "dob": "2000-01-01T00:00:00+00:00",
    ///         "address": "123 Main St",
    ///         "identityUserId": "00000000-0000-0000-0000-000000000000"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated customer</response>
    /// <response code="404">If the customer is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPut("{customerId:guid}", Name = "UpdateCustomer")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateCustomerAsync(Guid customerId, UpdateCustomerDto updateCustomerDto)
    {
        var customer = await customerService.UpdateCustomerAsync(customerId, updateCustomerDto);

        return Ok(customer);
    }
}
