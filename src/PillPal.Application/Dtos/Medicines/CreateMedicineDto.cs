using PillPal.Application.Dtos.Brands;

namespace PillPal.Application.Dtos.Medicines;

public record CreateMedicineDto
{
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }
    public string? Image { get; init; }
    public Guid SpecificationId { get; init; }
    // public IEnumerable<Guid> PharmaceuticalCompanyIds { get; init; } = default!;
    // public IEnumerable<Guid> DosageFormIds { get; init; } = default!;
    // public IEnumerable<Guid> ActiveIngredientIds { get; init; } = default!;
    public IEnumerable<Guid> BrandIds { get; init; } = default!;
}

public class CreateMedicineValidator : AbstractValidator<CreateMedicineDto>
{
    public CreateMedicineValidator()
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

        // RuleFor(x => x.PharmaceuticalCompanyIds)
        //     .NotEmpty()
        //     .WithMessage("Pharmaceutical companies are required.");

        // RuleFor(x => x.DosageFormIds)
        //     .NotEmpty()
        //     .WithMessage("Dosage forms are required.");

        // RuleFor(x => x.ActiveIngredientIds)
        //     .NotEmpty()
        //     .WithMessage("Active ingredients are required.");

        RuleFor(x => x.BrandIds)
            .NotEmpty()
            .WithMessage("Brands are required.");
    }
}