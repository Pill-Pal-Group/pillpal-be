using PillPal.Application.Features.MedicineInBrands;

namespace PillPal.Application.Features.Medicines;

public record UpdateFullMedicineDto : MedicineRelationDto
{
    /// <example>Paracetamol</example>
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }

    /// <example>https://monke.com/paracetamol.jpg</example>
    public string? Image { get; init; }
    
    public IEnumerable<UpdateMedicineInBrandsDto> MedicineInBrands { get; init; } = default!;
}

public class UpdateFullMedicineValidator : AbstractValidator<UpdateFullMedicineDto>
{
    public UpdateFullMedicineValidator(IApplicationDbContext context)
    {
        Include(new MedicineRelationValidator(context));

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

        RuleFor(x => x.MedicineInBrands)
            .NotEmpty()
            .WithMessage("Brands are required.")
            .ForEach(rule => rule
                .SetValidator(new UpdateMedicineInBrandsValidator(context)));
    }
}
