﻿using PillPal.Application.Dtos.Brands;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IBrandService
{
    Task<IEnumerable<BrandDto>> GetBrandsAsync();
    Task<BrandDto> GetBrandByIdAsync(Guid brandId);
    Task<BrandDto> CreateBrandAsync(CreateBrandDto createBrandDto);
    Task<BrandDto> UpdateBrandAsync(Guid brandId, UpdateBrandDto updateBrandDto);
    Task DeleteBrandAsync(Guid brandId);
}
