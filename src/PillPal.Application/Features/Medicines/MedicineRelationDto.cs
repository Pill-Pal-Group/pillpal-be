namespace PillPal.Application.Features.Medicines;

public record MedicineRelationDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid SpecificationId { get; init; }
    public IEnumerable<Guid> Categories { get; init; } = default!;
    public IEnumerable<Guid> PharmaceuticalCompanies { get; init; } = default!;
    public IEnumerable<Guid> DosageForms { get; init; } = default!;
    public IEnumerable<Guid> ActiveIngredients { get; init; } = default!;
}

public class MedicineRelationValidator : AbstractValidator<MedicineRelationDto>
{
    private readonly IApplicationDbContext _context;

    public MedicineRelationValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.Categories)
            .NotEmpty()
            .WithMessage("Categories are required.")
            .ForEach(rule => rule
                .MustAsync((id, cancellationToken) => _context.Categories.AnyAsync(c => c.Id == id && !c.IsDeleted, cancellationToken))
                .WithMessage("Category with Id {PropertyValue} does not exist."));

        RuleFor(x => x.SpecificationId)
            .NotEmpty()
            .WithMessage("Specification is required.")
            .MustAsync((id, cancellationToken) => _context.Specifications.AnyAsync(s => s.Id == id, cancellationToken))
            .WithMessage("Specification does not exist.");

        RuleFor(x => x.PharmaceuticalCompanies)
            .NotEmpty()
            .WithMessage("Pharmaceutical companies are required.")
            .ForEach(rule => rule
                .MustAsync((id, cancellationToken) => _context.PharmaceuticalCompanies.AnyAsync(pc => pc.Id == id && !pc.IsDeleted, cancellationToken))
                .WithMessage("Pharmaceutical company with Id {PropertyValue} does not exist."));

        RuleFor(x => x.DosageForms)
            .NotEmpty()
            .WithMessage("Dosage forms are required.")
            .ForEach(rule => rule
                .MustAsync((id, cancellationToken) => _context.DosageForms.AnyAsync(df => df.Id == id, cancellationToken))
                .WithMessage("Dosage form with Id {PropertyValue} does not exist."));

        RuleFor(x => x.ActiveIngredients)
            .NotEmpty()
            .WithMessage("Active ingredients are required.")
            .ForEach(rule => rule
                .MustAsync((id, cancellationToken) => _context.ActiveIngredients.AnyAsync(ai => ai.Id == id && !ai.IsDeleted, cancellationToken))
                .WithMessage("Active ingredient with Id {PropertyValue} does not exist."));
    }
}
