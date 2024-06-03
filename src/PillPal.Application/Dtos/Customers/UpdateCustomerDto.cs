namespace PillPal.Application.Dtos.Customers;

public record UpdateCustomerDto
{
    public DateTimeOffset? Dob { get; init; }
    public string? Address { get; init; }
    public Guid IdentityUserId { get; init; }
}

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerDto>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.Dob)
            .LessThan(DateTimeOffset.Now)
            .WithMessage("Dob must be less than current date.");

        RuleFor(x => x.Address)
            .MaximumLength(255);

        RuleFor(x => x.IdentityUserId)
            .NotEmpty();
    }
}
