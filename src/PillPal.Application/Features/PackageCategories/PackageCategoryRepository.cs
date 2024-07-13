namespace PillPal.Application.Features.PackageCategories;

public class PackageCategoryRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IPackageCategoryService
{
    public async Task<IEnumerable<PackageCategoryDto>> CreateBulkPackagesAsync(
        IEnumerable<CreatePackageCategoryDto> createPackageCategoryDtos)
    {
        await ValidateListAsync(createPackageCategoryDtos);

        var packageCategories = Mapper.Map<IEnumerable<PackageCategory>>(createPackageCategoryDtos);

        await Context.PackageCategories.AddRangeAsync(packageCategories);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<PackageCategoryDto>>(packageCategories);
    }

    public async Task<PackageCategoryDto> CreatePackageAsync(CreatePackageCategoryDto createPackageCategoryDto)
    {
        await ValidateAsync(createPackageCategoryDto);

        var packageCategory = Mapper.Map<PackageCategory>(createPackageCategoryDto);

        await Context.PackageCategories.AddAsync(packageCategory);

        await Context.SaveChangesAsync();

        return Mapper.Map<PackageCategoryDto>(packageCategory);
    }

    public async Task DeletePackageAsync(Guid id)
    {
        var packageCategory = await Context.PackageCategories
            .Where(p => !p.IsDeleted)
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException(nameof(PackageCategory), id);

        Context.PackageCategories.Remove(packageCategory);

        await Context.SaveChangesAsync();
    }

    public async Task<PackageCategoryDto> GetPackageByIdAsync(
        Guid id,
        PackageCategoryQueryParameter queryParameter)
    {
        var packageCategory = await Context.PackageCategories
            .Filter(queryParameter)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException(nameof(PackageCategory), id);

        return Mapper.Map<PackageCategoryDto>(packageCategory);
    }

    public async Task<IEnumerable<PackageCategoryDto>> GetPackagesAsync(PackageCategoryQueryParameter queryParameter)
    {
        var packageCategories = await Context.PackageCategories
            .Filter(queryParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<PackageCategoryDto>>(packageCategories);
    }

    public async Task<PackageCategoryDto> UpdatePackageAsync(
        Guid id,
        UpdatePackageCategoryDto updatePackageCategoryDto)
    {
        await ValidateAsync(updatePackageCategoryDto);

        var packageCategory = await Context.PackageCategories
            .Where(p => !p.IsDeleted)
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new NotFoundException(nameof(PackageCategory), id);

        Mapper.Map(updatePackageCategoryDto, packageCategory);

        await Context.SaveChangesAsync();

        return Mapper.Map<PackageCategoryDto>(packageCategory);
    }
}
