using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

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
            .Where(c => c.Id == pharmacyStoreId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmacyStore), pharmacyStoreId);

        pharmacyStore.IsDeleted = true;

        Context.PharmacyStores.Update(pharmacyStore);

        await Context.SaveChangesAsync();
    }

    public async Task<PharmacyStoreDto> GetPharmacyStoreByIdAsync(Guid pharmacyStoreId)
    {
        var pharmacyStore = await Context.PharmacyStores
            .Include(c => c.Brand)
            .Where(c => c.Id == pharmacyStoreId && !c.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmacyStore), pharmacyStoreId);

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
            .Where(c => c.Id == pharmacyStoreId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmacyStore), pharmacyStoreId);

        pharmacyStore = Mapper.Map(updatePharmacyStoreDto, pharmacyStore);

        Context.PharmacyStores.Update(pharmacyStore);

        await Context.SaveChangesAsync();

        return Mapper.Map<PharmacyStoreDto>(pharmacyStore);
    }
}
