using PillPal.Application.Dtos.ActiveIngredients;
using PillPal.Application.Dtos.Brands;
using PillPal.Application.Dtos.DosageForms;
using PillPal.Application.Dtos.PharmaceuticalCompanies;
using PillPal.Application.Dtos.Specifications;
using System.Text.Json.Serialization;

namespace PillPal.Application.Dtos.Medicines;

public record MedicineDto
{
    public Guid Id { get; init; }
    public string? MedicineCode { get; init; }
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }
    public string? Image { get; init; }
    public bool IsDeleted { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SpecificationDto? Specification { get; init; }

    public IEnumerable<PharmaceuticalCompanyDto> PharmaceuticalCompanies { get; init; } = default!;

    public IEnumerable<DosageFormDto> DosageForms { get; init; } = default!;

    public IEnumerable<ActiveIngredientDto> ActiveIngredients { get; init; } = default!;

    public IEnumerable<BrandDto> Brands { get; init; } = default!;
}
