namespace PillPal.Application.Dtos.Customers;

public record CreateCustomerDto
{
    public DateTimeOffset? Dob { get; init; }
    public string? Address { get; init; }
    public Guid IdentityUserId { get; init; }
}

public class CreateCustomerValidator : AbstractValidator<CreateCustomerDto>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Dob)
            .Empty()
            .LessThan(DateTimeOffset.Now)
            .WithMessage("Dob must be less than current date.");

        RuleFor(x => x.Address)
            .Empty()
            .MaximumLength(255);

        RuleFor(x => x.IdentityUserId)
            .NotEmpty();
    }
}
