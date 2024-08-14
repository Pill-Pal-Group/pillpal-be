using PillPal.Application.Features.Payments;
using PillPal.Core.Enums;
using PillPal.Infrastructure.PaymentService.ZaloPay;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PaymentsController(IPaymentService paymentService, ICustomerPackageService customerPackageService)
    : ControllerBase
{
    [Authorize(Policy.Customer)]
    [HttpGet("packages", Name = "CreatePayment")]
    [ProducesResponseType(typeof(PaymentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePaymentAsync([FromQuery] CustomerPackagePaymentInformation packagePaymentInfo)
    {
        var paymentResponse = await paymentService.CreatePaymentRequestAsync(packagePaymentInfo);

        return Ok(paymentResponse);
    }

    [HttpPost("zalopay/callback", Name = "ZaloPayCallback")]
    public async Task<IActionResult> ZaloPayCallbackAsync([FromBody] ZaloPayResponse zaloPayResponse)
    {
        //await paymentService.UpdatePaymentStatusAsync(zaloPayResponse.zpTransToken!);

        return Ok();
    }

    /// <summary>
    /// Confirm payment
    /// </summary>
    /// <param name="customerPackageId"></param>
    /// <returns></returns>
    [HttpGet("packages/payment", Name = "ConfirmPayment")]
    public async Task<IActionResult> ConfirmPackagePayment([FromQuery] Guid customerPackageId)
    {
        await customerPackageService.UpdateConfirmPackagePayment(customerPackageId);

        return Ok();
    }
}
