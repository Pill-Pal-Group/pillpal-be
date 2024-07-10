namespace PillPal.Application.Features.Accounts;

public record UpdateManagerInformationDto
{
    /// <example>094278290</example>
    public string? PhoneNumber { get; set; }
}

public class UpdateManagerInformationDtoValidator : AbstractValidator<UpdateManagerInformationDto>
{
    public UpdateManagerInformationDtoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{9}$");
    }
}
