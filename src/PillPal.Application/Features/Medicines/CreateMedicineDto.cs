namespace PillPal.Application.Features.Medicines;

public record CreateMedicineDto : MedicineRelationDto
{
    /// <example>Paracetamol</example>
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }

    /// <example>https://monke.com/paracetamol.jpg</example>
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
            .MaximumLength(100)
            .WithMessage("Medicine name must not exceed 100 characters.");

        RuleFor(x => x.RequirePrescript)
            .NotNull()
            .WithMessage("Require prescript is required.");

        RuleFor(x => x.Image)
            .MaximumLength(500)
            .WithMessage("Image must not exceed 100 characters.");

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .WithMessage("Registration number is required.")
            .MustAsync(async (registrationNumber, cancellationToken)
                => !await context.Medicines.AnyAsync(x => x.RegistrationNumber == registrationNumber, cancellationToken))
            .WithMessage("Registration number already existed.");
    }
}