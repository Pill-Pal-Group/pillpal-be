namespace PillPal.Application.Features.ActiveIngredients;

public record CreateActiveIngredientDto
{
    public string? IngredientName { get; init; }
    public string? IngredientInformation { get; init; }
}

public class CreateActiveIngredientValidator : AbstractValidator<CreateActiveIngredientDto>
{
    public CreateActiveIngredientValidator()
    {
        RuleFor(x => x.IngredientName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.IngredientInformation)
            .MaximumLength(500);
    }
}
