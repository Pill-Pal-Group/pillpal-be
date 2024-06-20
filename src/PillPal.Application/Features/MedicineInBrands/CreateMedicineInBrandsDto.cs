using PillPal.Application.Common.Interfaces.Data;

namespace PillPal.Application.Features.MedicineInBrands;

public record CreateMedicineInBrandsDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid BrandId { get; set; }

    /// <example>100.000</example>
    public decimal Price { get; set; }

    /// <example>https://monke.com/paracetamol</example>
    public string? MedicineUrl { get; set; }
}

public class CreateMedicineInBrandsValidator : AbstractValidator<CreateMedicineInBrandsDto>
{
    private readonly IApplicationDbContext _context;

    public CreateMedicineInBrandsValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(x => x.BrandId)
            .NotEmpty()
            .WithMessage("Brand is required.")
            .MustAsync((id, cancellationToken) => _context.Brands.AnyAsync(b => b.Id == id, cancellationToken))
            .WithMessage("Brand does not exist.");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price is required.");

        RuleFor(x => x.MedicineUrl)
            .MaximumLength(500)
            .WithMessage("Medicine url must not exceed 500 characters.");
    }
}
