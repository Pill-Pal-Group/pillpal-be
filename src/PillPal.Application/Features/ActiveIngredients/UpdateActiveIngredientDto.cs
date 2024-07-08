namespace PillPal.Application.Features.ActiveIngredients;

public record UpdateActiveIngredientDto
{
    /// <example>Paracetamol</example>
    public string? IngredientName { get; init; }
}

public class UpdateActiveIngredientValidator : AbstractValidator<UpdateActiveIngredientDto>
{
    public UpdateActiveIngredientValidator()
    {
        RuleFor(x => x.IngredientName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
