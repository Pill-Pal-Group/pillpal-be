using PillPal.Application.Features.Payments;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PaymentsController(IPaymentService paymentService)
    : ControllerBase
{
    [HttpGet(Name = "GetPayments")]
    [ProducesResponseType(typeof(IEnumerable<PaymentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPaymentsAsync()
    {
        var payments = await paymentService.GetPaymentsAsync();

        return Ok(payments);
    }

    [HttpGet("{paymentId:guid}", Name = "GetPaymentById")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPaymentByIdAsync(Guid paymentId)
    {
        var payment = await paymentService.GetPaymentAsync(paymentId);

        return Ok(payment);
    }

    [Authorize(Policy.Admin)]
    [HttpPost(Name = "CreatePayment")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePaymentAsync(CreatePaymentDto createPaymentDto)
    {
        var payment = await paymentService.CreatePaymentAsync(createPaymentDto);

        return CreatedAtRoute("GetPaymentById", new { paymentId = payment.Id }, payment);
    }

    [Authorize(Policy.Admin)]
    [HttpPost("bulk", Name = "CreateBulkPayments")]
    [ProducesResponseType(typeof(IEnumerable<PaymentDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBulkPaymentsAsync(IEnumerable<CreatePaymentDto> createPaymentDtos)
    {
        var payments = await paymentService.CreateBulkPaymentsAsync(createPaymentDtos);

        return CreatedAtRoute("GetPayments", payments);
    }

    [Authorize(Policy.Admin)]
    [HttpPut("{paymentId:guid}", Name = "UpdatePayment")]
    [ProducesResponseType(typeof(PaymentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdatePaymentAsync(Guid paymentId, UpdatePaymentDto updatePaymentDto)
    {
        var payment = await paymentService.UpdatePaymentAsync(paymentId, updatePaymentDto);

        return Ok(payment);
    }
}
