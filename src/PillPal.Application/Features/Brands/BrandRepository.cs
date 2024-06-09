using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.Brands;

public class BrandRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IBrandService
{
    public async Task<BrandDto> CreateBrandAsync(CreateBrandDto createBrandDto)
    {
        var validator = ServiceProvider.GetRequiredService<CreateBrandValidator>();

        var validationResult = await validator.ValidateAsync(createBrandDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var brand = Mapper.Map<Brand>(createBrandDto);

        await Context.Brands.AddAsync(brand);

        await Context.SaveChangesAsync();

        return Mapper.Map<BrandDto>(brand);
    }

    public async Task DeleteBrandAsync(Guid brandId)
    {
        var brand = await Context.Brands
            .Where(b => b.Id == brandId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Brand), brandId);

        brand.IsDeleted = true;

        Context.Brands.Update(brand);

        await Context.SaveChangesAsync();
    }

    public async Task<BrandDto> GetBrandByIdAsync(Guid brandId)
    {
        var brand = await Context.Brands
            .Where(b => b.Id == brandId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Brand), brandId);

        return Mapper.Map<BrandDto>(brand);
    }

    public async Task<IEnumerable<BrandDto>> GetBrandsAsync(BrandQueryParameter queryParameter)
    {
        var brands = Context.Brands
            .Where(b => !b.IsDeleted)
            .Filter(queryParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<BrandDto> UpdateBrandAsync(Guid brandId, UpdateBrandDto updateBrandDto)
    {
        var validator = ServiceProvider.GetRequiredService<UpdateBrandValidator>();

        var validationResult = await validator.ValidateAsync(updateBrandDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var brand = await Context.Brands
            .Where(b => b.Id == brandId && !b.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Brand), brandId);

        Mapper.Map(updateBrandDto, brand);

        Context.Brands.Update(brand);

        await Context.SaveChangesAsync();

        return Mapper.Map<BrandDto>(brand);
    }
}
