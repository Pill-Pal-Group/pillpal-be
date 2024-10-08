﻿using PillPal.Application.Features.ActiveIngredients;
using PillPal.Application.Features.Categories;
using PillPal.Application.Features.DosageForms;
using PillPal.Application.Features.MedicineInBrands;
using PillPal.Application.Features.PharmaceuticalCompanies;
using PillPal.Application.Features.Specifications;

namespace PillPal.Application.Features.Medicines;

public record MedicineDto
{
    /// <example>00000000-0000-0000-0000-000000000000</example>
    public Guid Id { get; init; }

    /// <example>MED6060-555555</example>
    public string? MedicineCode { get; init; }

    /// <example>Sedanxio</example>
    public string? MedicineName { get; init; }
    public bool RequirePrescript { get; init; }

    /// <example>https://monke.com/sedanxio.jpg</example>
    public string? Image { get; init; }

    /// <example>VN-17384-13</example>
    public string? RegistrationNumber { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public SpecificationDto? Specification { get; init; }

    public IEnumerable<CategoryDto> Categories { get; init; } = default!;

    public IEnumerable<PharmaceuticalCompanyDto> PharmaceuticalCompanies { get; init; } = default!;

    public IEnumerable<DosageFormDto> DosageForms { get; init; } = default!;

    public IEnumerable<ActiveIngredientDto> ActiveIngredients { get; init; } = default!;

    public IEnumerable<MedicineInBrandsDto> MedicineInBrands { get; init; } = default!;
}
