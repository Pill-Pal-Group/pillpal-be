namespace PillPal.Application.Features.PharmaceuticalCompanies;

public record UpdatePharmaceuticalCompanyDto
{
    /// <example>Pfizer</example>
    public string? CompanyName { get; init; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid NationId { get; init; }
}

public class UpdatePharmaceuticalCompanyValidator : AbstractValidator<UpdatePharmaceuticalCompanyDto>
{
    public UpdatePharmaceuticalCompanyValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NationId)
            .NotEmpty();
    }
}
