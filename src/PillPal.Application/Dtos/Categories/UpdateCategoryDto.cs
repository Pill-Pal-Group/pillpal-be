namespace PillPal.Application.Dtos.Categories;

public record UpdateCategoryDto
{
    public string? CategoryName { get; init; }
}

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.CategoryName).NotEmpty().MaximumLength(50);
    }
}
