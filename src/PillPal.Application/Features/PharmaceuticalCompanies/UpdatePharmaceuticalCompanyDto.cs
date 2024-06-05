namespace PillPal.Application.Features.PharmaceuticalCompanies;

public record UpdatePharmaceuticalCompanyDto
{
    public string? CompanyName { get; init; }

    public Guid NationId { get; init; }
}

public class UpdatePharmaceuticalCompanyValidator : AbstractValidator<UpdatePharmaceuticalCompanyDto>
{
    public UpdatePharmaceuticalCompanyValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.NationId)
            .NotEmpty();
    }
}
