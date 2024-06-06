namespace PillPal.Application.Features.Brands;

public record CreateBrandDto
{
    public string? BrandUrl { get; init; }
    public string? BrandLogo { get; init; }
}

public class CreateBrandValidator : AbstractValidator<CreateBrandDto>
{
    public CreateBrandValidator()
    {
        RuleFor(x => x.BrandUrl)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.BrandLogo)
            .NotEmpty()
            .MaximumLength(500);
    }
}
