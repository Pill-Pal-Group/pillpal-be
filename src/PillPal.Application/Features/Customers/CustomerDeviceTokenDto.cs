namespace PillPal.Application.Features.Customers;

public record CustomerDeviceTokenDto
{
    public string? DeviceToken { get; init; }
}

public class UpdateCustomerDeviceTokenValidator : AbstractValidator<CustomerDeviceTokenDto>
{
    public UpdateCustomerDeviceTokenValidator()
    {
        RuleFor(x => x.DeviceToken)
            .NotEmpty()
            .WithMessage("Device token is required.");
    }
}
