namespace PillPal.Application.Features.Medicines;

public record UpdateMedicineDto : MedicineRelationDto
{
    /// <example>Paracetamol</example>
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }

    /// <example>https://monke.com/paracetamol.jpg</example>
    public string? Image { get; init; }
}

public class UpdateMedicineValidator : AbstractValidator<UpdateMedicineDto>
{
    public UpdateMedicineValidator(IApplicationDbContext context)
    {
        Include(new MedicineRelationValidator(context));

        RuleFor(x => x.MedicineName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.RequirePrescript)
            .NotNull();

        RuleFor(x => x.Image)
            .MaximumLength(500);
    }
}
