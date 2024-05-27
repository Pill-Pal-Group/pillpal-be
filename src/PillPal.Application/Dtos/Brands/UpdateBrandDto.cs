namespace PillPal.Application.Dtos.Brands;

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
            .MaximumLength(200);

        RuleFor(x => x.BrandLogo)
            .NotEmpty()
            .MaximumLength(200);
    }
}
