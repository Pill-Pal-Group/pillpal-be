using PillPal.Application.Features.Brands;

namespace PillPal.Application.Features.MedicinesInBrands;

public record MedicineInBrandsDto
{
    public BrandDto? Brand { get; init; }

    /// <example>100.000</example>
    public decimal Price { get; init; }

    /// <example>https://monke.com/paracetamol</example>
    public string? MedicineUrl { get; init; }
}
