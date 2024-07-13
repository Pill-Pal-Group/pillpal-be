using PillPal.Application.Features.Accounts;
using PillPal.Application.Features.Customers;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class AccountsController(
    IAccountService accountService,
    ICustomerService customerService)
    : ControllerBase
{
    /// <summary>
    /// Get all accounts with given id
    /// </summary>
    /// <remarks>Requires admin policy</remarks>
    /// <param name="accountId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a list of accounts</response>
    /// <response code="404">If the account is not found</response>
    [Authorize(Policy.Admin)]
    [HttpGet("{accountId:guid}", Name = "GetAccountById")]
    [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountByIdAsync(Guid accountId)
    {
        var account = await accountService.GetAccountAsync(accountId);

        return Ok(account);
    }

    /// <summary>
    /// Get all managers
    /// </summary>
    /// <remarks>Requires admin policy</remarks>
    /// <response code="200">Returns a list of managers</response>
    [Authorize(Policy.Admin)]
    [HttpGet("manager", Name = "GetManagers")]
    [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetManagersAsync()
    {
        var managers = await accountService.GetAccountsAsync(Role.Manager);

        return Ok(managers);
    }

    /// <summary>
    /// Assign a manager
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Requires admin policy
    /// 
    /// Sample request:
    /// 
    ///     POST /api/accounts/manager
    ///     {
    ///         "email": "manager@pillpal",
    ///         "password": "password",
    ///         "phoneNumber": "0234567899"
    ///     }
    ///     
    /// </remarks>
    /// <response code="204">Assign the manager successfully</response>
    /// <response code="409">If the email is already exists</response>
    /// <response code="422">If the request is invalid</response>
    [Authorize(Policy.Admin)]
    [HttpPost("manager", Name = "AssignManager")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AssignManagerAsync(AssignManagerRequest request)
    {
        await accountService.AssignManagerAsync(request);

        return NoContent();
    }

    /// <summary>
    /// Update manager information
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Requires manager policy
    /// 
    /// Sample request:
    /// 
    ///     PUT /api/accounts/manager
    ///     {
    ///         "phoneNumber": "0234567896"
    ///     }
    ///     
    /// </remarks>
    /// <response code="204">Update the manager information successfully</response>
    /// <response code="422">If the request is invalid</response>
    [Authorize(Policy.Manager)]
    [HttpPut("manager", Name = "UpdateManagerInformation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateManagerInformationAsync(UpdateManagerInformationDto request)
    {
        await accountService.UpdateManagerInformationAsync(request);

        return NoContent();
    }

    /// <summary>
    /// Get all customers
    /// </summary>
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="queryParameter"></param>
    /// <response code="200">Returns a list of customers</response>
    [Authorize(Policy.Administrative)]
    [HttpGet("customer", Name = "GetCustomers")]
    [ProducesResponseType(typeof(IEnumerable<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomersAsync([FromQuery] CustomerQueryParameter queryParameter)
    {
        var customers = await customerService.GetCustomersAsync(queryParameter);

        return Ok(customers);
    }

    /// <summary>
    /// Get a customer by id
    /// </summary>
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="customerId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a customer</response>
    /// <response code="404">If the customer is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpGet("customer/{customerId:guid}", Name = "GetCustomerById")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerByIdAsync(Guid customerId)
    {
        var customer = await customerService.GetCustomerByIdAsync(customerId);

        return Ok(customer);
    }

    /// <summary>
    /// Lock a customer
    /// </summary>
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="customerId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">Lock the customer</response>
    /// <response code="404">If the customer is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpPut("customer/{customerId:guid}/lock", Name = "LockCustomer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LockCustomerAsync(Guid customerId)
    {
        await accountService.LockAccountAsync(customerId);

        return NoContent();
    }

    /// <summary>
    /// Unlock a customer
    /// </summary>
    /// <remarks>Requires admin policy</remarks>
    /// <param name="customerId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">Unlock the customer</response>
    /// <response code="404">If the customer is not found</response>
    [Authorize(Policy.Admin)]
    [HttpPut("customer/{customerId:guid}/unlock", Name = "UnlockCustomer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnlockCustomerAsync(Guid customerId)
    {
        await accountService.UnlockAccountAsync(customerId);

        return NoContent();
    }
}
