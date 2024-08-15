using PillPal.Application.Features.Payments;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PaymentsController(IPaymentService paymentService, ICustomerPackageService customerPackageService)
    : ControllerBase
{
    /// <summary>
    /// Create payment request
    /// </summary>
    /// <param name="packagePaymentInfo"></param>
    /// <response code="200">Returns the payment response</response>
    /// <response code="400">If the payment method is invalid or unsupported</response>
    /// <response code="422">If the request is invalid</response>
    [Authorize(Policy.Customer)]
    [HttpGet("packages", Name = "CreatePayment")]
    [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePaymentAsync([FromQuery] CustomerPackagePaymentInformation packagePaymentInfo)
    {
        var paymentResponse = await paymentService.CreatePaymentRequestAsync(packagePaymentInfo);

        return Ok(paymentResponse);
    }

    /// <summary>
    /// Confirm payment
    /// </summary>
    /// <param name="customerPackageId"></param>
    /// <response code="204">If the operation success</response>
    /// <response code="404">If the customer package is not found</response>
    [Authorize(Policy.Customer)]
    [HttpGet("packages/payment", Name = "ConfirmPayment")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ConfirmPackagePayment([FromQuery] Guid customerPackageId)
    {
        await customerPackageService.UpdateConfirmPackagePayment(customerPackageId);

        return NoContent();
    }
}
