namespace PillPal.Application.Features.Brands;

public record CreateBrandDto
{
    /// <example>Pharmacity</example>
    public string? BrandName { get; set; }

    /// <example>https://monke.com/brand</example>
    public string? BrandUrl { get; init; }

    /// <example>https://monke.com/brandlogo.jpg</example>
    public string? BrandLogo { get; init; }
}

public class CreateBrandValidator : AbstractValidator<CreateBrandDto>
{
    public CreateBrandValidator()
    {
        RuleFor(x => x.BrandName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.BrandUrl)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.BrandLogo)
            .NotEmpty()
            .MaximumLength(500);
    }
}
