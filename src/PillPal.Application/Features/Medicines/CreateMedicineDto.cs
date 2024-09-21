namespace PillPal.Application.Features.Medicines;

public record CreateMedicineDto : MedicineRelationDto
{
    /// <example>Sedanxio</example>
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }

    /// <example>https://monke.com/sedanxio.jpg</example>
    public string? Image { get; init; }

    /// <example>VN-17384-13</example>
    public string? RegistrationNumber { get; init; }
}

public class CreateMedicineValidator : AbstractValidator<CreateMedicineDto>
{
    public CreateMedicineValidator(IApplicationDbContext context)
    {
        Include(new MedicineRelationValidator(context));

        RuleFor(x => x.MedicineName)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.RequirePrescript)
            .NotNull();

        RuleFor(x => x.Image)
            .MaximumLength(500);

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .MustAsync(async (registrationNumber, cancellationToken)
                => !await context.Medicines.AnyAsync(x => x.RegistrationNumber == registrationNumber, cancellationToken))
            .WithMessage("Registration number already existed.");
    }
}