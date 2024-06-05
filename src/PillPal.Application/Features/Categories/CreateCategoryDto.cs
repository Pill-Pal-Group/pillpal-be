namespace PillPal.Application.Features.Categories;

public record CreateCategoryDto
{
    public string? CategoryName { get; init; }
}

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.CategoryName).NotEmpty().MaximumLength(50);
    }
}