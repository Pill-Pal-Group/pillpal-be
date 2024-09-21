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
            .MaximumLength(200);

        RuleFor(x => x.RequirePrescript)
            .NotNull();

        RuleFor(x => x.Image)
            .MaximumLength(500);

        RuleFor(x => x.MedicineInBrands)
            .NotEmpty()
            .ForEach(rule => rule
                .SetValidator(new UpdateMedicineInBrandsValidator(context)));
    }
}
