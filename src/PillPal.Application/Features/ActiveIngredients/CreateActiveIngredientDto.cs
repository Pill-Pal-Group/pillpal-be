namespace PillPal.Application.Features.ActiveIngredients;

public record CreateActiveIngredientDto
{
    /// <example>Paracetamol</example>
    public string? IngredientName { get; init; }
}

public class CreateActiveIngredientValidator : AbstractValidator<CreateActiveIngredientDto>
{
    public CreateActiveIngredientValidator()
    {
        RuleFor(x => x.IngredientName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
