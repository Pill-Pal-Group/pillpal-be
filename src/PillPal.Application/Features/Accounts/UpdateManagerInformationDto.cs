namespace PillPal.Application.Features.Accounts;

public record UpdateManagerInformationDto
{
    /// <example>0942782905</example>
    public string? PhoneNumber { get; set; }
}

public class UpdateManagerInformationDtoValidator : AbstractValidator<UpdateManagerInformationDto>
{
    public UpdateManagerInformationDtoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10}$")
            .WithMessage("Phone number must be 10 digits.");
    }
}
