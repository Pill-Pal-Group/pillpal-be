namespace PillPal.Application.Features.Brands;

public record UpdateBrandDto
{
    /// <example>https://monke.com/brand</example>
    public string? BrandUrl { get; init; }

    /// <example>https://monke.com/brandlogo.jpg</example>
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
