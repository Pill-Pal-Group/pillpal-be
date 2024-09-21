namespace PillPal.Application.Features.Categories;

public record UpdateCategoryDto
{
    /// <example>Vaccines</example>
    public string? CategoryName { get; init; }
}

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty()
            .MaximumLength(50);
    }
}
