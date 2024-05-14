namespace PillPal.Core.Dtos.Ingredients.Commands;

public record CreateIngredientCommand(
    string IngredientCode,
    string IngredientName,
    string IngredientDescription,
    string IngredientType,
    string ImageUrl);

public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
{
    public CreateIngredientCommandValidator()
    {
        RuleFor(x => x.IngredientCode).NotEmpty();
        RuleFor(x => x.IngredientName).NotEmpty();
        RuleFor(x => x.IngredientDescription).NotEmpty();
        RuleFor(x => x.IngredientType).NotEmpty();
        RuleFor(x => x.ImageUrl).NotEmpty();
    }
}
