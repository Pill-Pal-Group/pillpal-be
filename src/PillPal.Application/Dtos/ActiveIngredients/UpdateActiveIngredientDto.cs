namespace PillPal.Application.Dtos.ActiveIngredients;

public record UpdateActiveIngredientDto
{
    public string? IngredientName { get; init; }
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
            .NotEmpty()
            .MaximumLength(500);
    }
}
