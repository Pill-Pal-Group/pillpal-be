namespace PillPal.Application.Features.ActiveIngredients;

public record UpdateActiveIngredientDto
{
    /// <example>Paracetamol</example>
    public string? IngredientName { get; init; }

    /// <example>Paracetamol is a paracetamol-based analgesic</example>
    public string? IngredientInformation { get; init; }
}

public class UpdateActiveIngredientValidator : AbstractValidator<UpdateActiveIngredientDto>
{
    public UpdateActiveIngredientValidator()
    {
        RuleFor(x => x.IngredientName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.IngredientInformation)
            .MaximumLength(2048);
    }
}
