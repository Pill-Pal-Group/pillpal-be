namespace PillPal.Application.Features.PrescriptDetails;

public record UpdatePrescriptDetailImageDto
{
    /// <example>https://monke.com/med-image.jpg</example>
    public string? MedicineImage { get; init; }
}

public class UpdatePrescriptDetailImageValidator : AbstractValidator<UpdatePrescriptDetailImageDto>
{
    public UpdatePrescriptDetailImageValidator()
    {
        RuleFor(p => p.MedicineImage)
            .MaximumLength(500);
    }
}
