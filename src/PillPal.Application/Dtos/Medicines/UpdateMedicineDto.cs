namespace PillPal.Application.Dtos.Medicines;

public record UpdateMedicineDto
{
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }
    public string? Image { get; init; }
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
            .MaximumLength(100)
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
