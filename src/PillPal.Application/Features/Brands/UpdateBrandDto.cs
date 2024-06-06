namespace PillPal.Application.Features.Brands;

public record UpdateBrandDto
{
    public string? BrandUrl { get; init; }
    public string? BrandLogo { get; init; }
}

public class UpdateBrandValidator : AbstractValidator<UpdateBrandDto>
{
    public UpdateBrandValidator()
    {
        RuleFor(x => x.BrandUrl)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.BrandLogo)
            .NotEmpty()
            .MaximumLength(500);
    }
}
