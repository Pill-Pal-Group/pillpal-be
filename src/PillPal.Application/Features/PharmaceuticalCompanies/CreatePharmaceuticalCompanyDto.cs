namespace PillPal.Application.Features.PharmaceuticalCompanies;

public record CreatePharmaceuticalCompanyDto
{
    /// <example>Pfizer</example>
    public string? CompanyName { get; init; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid NationId { get; init; }
}

public class CreatePharmaceuticalCompanyValidator : AbstractValidator<CreatePharmaceuticalCompanyDto>
{
    public CreatePharmaceuticalCompanyValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.NationId)
            .NotEmpty()
            .MustAsync(async (id, cancellationToken)
                => await context.Nations.AnyAsync(x => x.Id == id, cancellationToken))
            .WithMessage("Nation not found.");
    }
}
