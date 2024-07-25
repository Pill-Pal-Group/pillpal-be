using PillPal.Application.Features.Payments;
using PillPal.Core.Enums;
using PillPal.Infrastructure.PaymentService.VnPay;
using PillPal.Infrastructure.PaymentService.ZaloPay;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PaymentsController(IPaymentService paymentService) 
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

    [HttpGet("vnpay/callback", Name = "VnPayCallback")]
    public async Task<IActionResult> VnPayCallbackAsync([FromQuery] VnPayResponse vnPayResponse)
    {
        PaymentStatusEnums paymentStatus = vnPayResponse.Vnp_ResponseCode == "00" ? PaymentStatusEnums.PAID : PaymentStatusEnums.UNPAID;
        
        await paymentService.UpdatePaymentStatusAsync(vnPayResponse.Vnp_TxnRef!, paymentStatus);

        return Ok();
    }

    [HttpPost("zalopay/callback", Name = "ZaloPayCallback")]
    public async Task<IActionResult> ZaloPayCallbackAsync([FromBody] ZaloPayResponse zaloPayResponse)
    {
        //await paymentService.UpdatePaymentStatusAsync(zaloPayResponse.zpTransToken!);

        return Ok();
    }
}
