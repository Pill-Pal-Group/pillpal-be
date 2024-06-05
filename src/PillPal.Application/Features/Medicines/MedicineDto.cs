using PillPal.Application.Features.ActiveIngredients;
using PillPal.Application.Features.Brands;
using PillPal.Application.Features.DosageForms;
using PillPal.Application.Features.PharmaceuticalCompanies;
using PillPal.Application.Features.Specifications;
using System.Text.Json.Serialization;

namespace PillPal.Application.Features.Medicines;

public record MedicineDto
{
    public Guid Id { get; init; }
    public string? MedicineCode { get; init; }
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }
    public string? Image { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SpecificationDto? Specification { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<PharmaceuticalCompanyDto>? PharmaceuticalCompanies { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<DosageFormDto>? DosageForms { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<ActiveIngredientDto>? ActiveIngredients { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<BrandDto>? Brands { get; init; }
}
