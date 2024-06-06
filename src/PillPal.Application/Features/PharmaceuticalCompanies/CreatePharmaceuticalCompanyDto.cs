namespace PillPal.Application.Features.PharmaceuticalCompanies;

public record CreatePharmaceuticalCompanyDto
{
    public string? CompanyName { get; init; }

    public Guid NationId { get; init; }
}

public class CreatePharmaceuticalCompanyValidator : AbstractValidator<CreatePharmaceuticalCompanyDto>
{
    public CreatePharmaceuticalCompanyValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NationId)
            .NotEmpty();
    }
}
