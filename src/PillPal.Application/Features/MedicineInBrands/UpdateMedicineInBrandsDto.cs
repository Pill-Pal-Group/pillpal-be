namespace PillPal.Application.Features.MedicineInBrands;

public record UpdateMedicineInBrandsDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid BrandId { get; init; }

    /// <example>8000</example>
    public decimal Price { get; init; }

    /// <example>VND</example>
    public string? PriceUnit { get; init; }

    /// <example>https://monke.com/paracetamol</example>
    public string? MedicineUrl { get; init; }
}

public class UpdateMedicineInBrandsValidator : AbstractValidator<UpdateMedicineInBrandsDto>
{
    private readonly IApplicationDbContext _context;

    public UpdateMedicineInBrandsValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.BrandId)
            .NotEmpty()
            .MustAsync(async (id, cancellationToken) 
                => await _context.Brands.AnyAsync(b => b.Id == id, cancellationToken))
            .WithMessage("Brand does not exist.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PriceUnit)
            .MaximumLength(20);

        RuleFor(x => x.MedicineUrl)
            .MaximumLength(500);
    }
}
