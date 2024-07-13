namespace PillPal.Application.Features.PharmacyStores;

public class PharmacyStoreRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IPharmacyStoreService
{
    public async Task<PharmacyStoreDto> CreatePharmacyStoreAsync(CreatePharmacyStoreDto createPharmacyStoreDto)
    {
        await ValidateAsync(createPharmacyStoreDto);

        var pharmacyStore = Mapper.Map<PharmacyStore>(createPharmacyStoreDto);

        await Context.PharmacyStores.AddAsync(pharmacyStore);

        await Context.SaveChangesAsync();

        return Mapper.Map<PharmacyStoreDto>(pharmacyStore);
    }

    public async Task DeletePharmacyStoreAsync(Guid pharmacyStoreId)
    {
        var pharmacyStore = await Context.PharmacyStores
            .Where(c => !c.IsDeleted)
            .FirstOrDefaultAsync(c => c.Id == pharmacyStoreId)
            ?? throw new NotFoundException(nameof(PharmacyStore), pharmacyStoreId);

        Context.PharmacyStores.Remove(pharmacyStore);

        await Context.SaveChangesAsync();
    }

    public async Task<PharmacyStoreDto> GetPharmacyStoreByIdAsync(Guid pharmacyStoreId)
    {
        var pharmacyStore = await Context.PharmacyStores
            .Include(c => c.Brand)
            .Where(c => !c.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == pharmacyStoreId)
            ?? throw new NotFoundException(nameof(PharmacyStore), pharmacyStoreId);

        return Mapper.Map<PharmacyStoreDto>(pharmacyStore);
    }

    public async Task<IEnumerable<PharmacyStoreDto>> GetPharmacyStoresAsync()
    {
        var pharmacyStores = await Context.PharmacyStores
            .Include(c => c.Brand)
            .Where(c => !c.IsDeleted)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<PharmacyStoreDto>>(pharmacyStores);
    }

    public async Task<PharmacyStoreDto> UpdatePharmacyStoreAsync(Guid pharmacyStoreId, UpdatePharmacyStoreDto updatePharmacyStoreDto)
    {
        await ValidateAsync(updatePharmacyStoreDto);

        var pharmacyStore = await Context.PharmacyStores
            .Where(c => !c.IsDeleted)
            .FirstOrDefaultAsync(c => c.Id == pharmacyStoreId)
            ?? throw new NotFoundException(nameof(PharmacyStore), pharmacyStoreId);

        pharmacyStore = Mapper.Map(updatePharmacyStoreDto, pharmacyStore);

        Context.PharmacyStores.Update(pharmacyStore);

        await Context.SaveChangesAsync();

        return Mapper.Map<PharmacyStoreDto>(pharmacyStore);
    }
}
