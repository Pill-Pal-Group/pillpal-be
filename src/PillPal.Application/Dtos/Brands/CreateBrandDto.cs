namespace PillPal.Application.Dtos.Brands;

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
            .MaximumLength(200);

        RuleFor(x => x.BrandLogo)
            .NotEmpty()
            .MaximumLength(200);
    }
}
