namespace PillPal.Application.Features.Medicines;

public record UpdateMedicineDto
{
    /// <example>Paracetamol</example>
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }

    /// <example>https://monke.com/paracetamol.jpg</example>
    public string? Image { get; init; }

    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid SpecificationId { get; init; }
    public IEnumerable<Guid> PharmaceuticalCompanys { get; init; } = default!;
    public IEnumerable<Guid> DosageForms { get; init; } = default!;
    public IEnumerable<Guid> ActiveIngredients { get; init; } = default!;
    public IEnumerable<Guid> Brands { get; init; } = default!;
}

public class UpdateMedicineValidator : AbstractValidator<UpdateMedicineDto>
{
    public UpdateMedicineValidator()
    {
        RuleFor(x => x.MedicineName)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Medicine name must not exceed 100 characters.");

        RuleFor(x => x.RequirePrescript)
            .NotNull()
            .WithMessage("Require prescript is required.");

        RuleFor(x => x.Image)
            .MaximumLength(500)
            .WithMessage("Image must not exceed 100 characters.");

        RuleFor(x => x.SpecificationId)
            .NotEmpty()
            .WithMessage("Specification is required.");

        RuleFor(x => x.PharmaceuticalCompanys)
            .NotEmpty()
            .WithMessage("Pharmaceutical companies are required.");

        RuleFor(x => x.DosageForms)
            .NotEmpty()
            .WithMessage("Dosage forms are required.");

        RuleFor(x => x.ActiveIngredients)
            .NotEmpty()
            .WithMessage("Active ingredients are required.");

        RuleFor(x => x.Brands)
            .NotEmpty()
            .WithMessage("Brands are required.");
    }
}
