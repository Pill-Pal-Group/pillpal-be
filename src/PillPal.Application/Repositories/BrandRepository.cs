using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Brands;

namespace PillPal.Application.Repositories;

public class BrandRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider) 
    : BaseRepository(context, mapper, serviceProvider), IBrandService
{
    public async Task<BrandDto> CreateBrandAsync(CreateBrandDto createBrandDto)
    {
        var validator = _serviceProvider.GetRequiredService<CreateBrandValidator>();

        var validationResult = await validator.ValidateAsync(createBrandDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var brand = _mapper.Map<Brand>(createBrandDto);

        await _context.Brands.AddAsync(brand);

        await _context.SaveChangesAsync();

        return _mapper.Map<BrandDto>(brand);
    }

    public async Task DeleteBrandAsync(Guid brandId)
    {
        var brand = await _context.Brands
            .Where(b => b.Id == brandId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Brand), brandId);

        brand.IsDeleted = true;

        _context.Brands.Update(brand);

        await _context.SaveChangesAsync();
    }

    public async Task<BrandDto> GetBrandByIdAsync(Guid brandId)
    {
        var brand = await _context.Brands
            .Where(b => b.Id == brandId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Brand), brandId);

        return _mapper.Map<BrandDto>(brand);
    }

    public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
    {
        var brands = await _context.Brands
            .Where(b => !b.IsDeleted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<BrandDto> UpdateBrandAsync(Guid brandId, UpdateBrandDto updateBrandDto)
    {
        var validator = _serviceProvider.GetRequiredService<UpdateBrandValidator>();

        var validationResult = await validator.ValidateAsync(updateBrandDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var brand = await _context.Brands
            .Where(b => b.Id == brandId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Brand), brandId);

        _mapper.Map(updateBrandDto, brand);

        _context.Brands.Update(brand);

        await _context.SaveChangesAsync();

        return _mapper.Map<BrandDto>(brand);
    }
}
