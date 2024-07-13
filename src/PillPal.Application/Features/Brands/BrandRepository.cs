namespace PillPal.Application.Features.Brands;

public class BrandRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IBrandService
{
    public async Task<BrandDto> CreateBrandAsync(CreateBrandDto createBrandDto)
    {
        await ValidateAsync(createBrandDto);

        var brand = Mapper.Map<Brand>(createBrandDto);

        await Context.Brands.AddAsync(brand);

        await Context.SaveChangesAsync();

        return Mapper.Map<BrandDto>(brand);
    }

    public async Task<IEnumerable<BrandDto>> CreateBulkBrandsAsync(IEnumerable<CreateBrandDto> createBrandDtos)
    {
        await ValidateListAsync(createBrandDtos);

        var brands = Mapper.Map<IEnumerable<Brand>>(createBrandDtos);

        await Context.Brands.AddRangeAsync(brands);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task DeleteBrandAsync(Guid brandId)
    {
        var brand = await Context.Brands
            .Where(b => !b.IsDeleted)
            .FirstOrDefaultAsync(b => b.Id == brandId)
            ?? throw new NotFoundException(nameof(Brand), brandId);

        Context.Brands.Remove(brand);

        await Context.SaveChangesAsync();
    }

    public async Task<BrandDto> GetBrandByIdAsync(Guid brandId)
    {
        var brand = await Context.Brands
            .Where(b => !b.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == brandId)
            ?? throw new NotFoundException(nameof(Brand), brandId);

        return Mapper.Map<BrandDto>(brand);
    }

    public async Task<IEnumerable<BrandDto>> GetBrandsAsync(BrandQueryParameter queryParameter)
    {
        var brands = await Context.Brands
            .Where(b => !b.IsDeleted)
            .Filter(queryParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<BrandDto>>(brands);
    }

    public async Task<BrandDto> UpdateBrandAsync(Guid brandId, UpdateBrandDto updateBrandDto)
    {
        await ValidateAsync(updateBrandDto);

        var brand = await Context.Brands
            .Where(b => !b.IsDeleted)
            .FirstOrDefaultAsync(b => b.Id == brandId)
            ?? throw new NotFoundException(nameof(Brand), brandId);

        Mapper.Map(updateBrandDto, brand);

        Context.Brands.Update(brand);

        await Context.SaveChangesAsync();

        return Mapper.Map<BrandDto>(brand);
    }
}
