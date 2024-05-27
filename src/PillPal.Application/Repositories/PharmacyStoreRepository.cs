using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.PharmacyStores;

namespace PillPal.Application.Repositories;

public class PharmacyStoreRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IPharmacyStoreService
{
    public async Task<PharmacyStoreDto> CreatePharmacyStoreAsync(CreatePharmacyStoreDto createPharmacyStoreDto)
    {
        var validator = _serviceProvider.GetRequiredService<CreatePharmacyStoreValidator>();

        var validationResult = await validator.ValidateAsync(createPharmacyStoreDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var pharmacyStore = _mapper.Map<PharmacyStore>(createPharmacyStoreDto);

        await _context.PharmacyStores.AddAsync(pharmacyStore);

        await _context.SaveChangesAsync();

        return _mapper.Map<PharmacyStoreDto>(pharmacyStore);
    }

    public async Task DeletePharmacyStoreAsync(Guid pharmacyStoreId)
    {
        var pharmacyStore = await _context.PharmacyStores
            .Where(c => c.Id == pharmacyStoreId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmacyStore), pharmacyStoreId);

        pharmacyStore.IsDeleted = true;

        _context.PharmacyStores.Update(pharmacyStore);

        await _context.SaveChangesAsync(); 
    }

    public async Task<PharmacyStoreDto> GetPharmacyStoreByIdAsync(Guid pharmacyStoreId)
    {
        var pharmacyStore = await _context.PharmacyStores
            .Include(c => c.Brand)
            .Where(c => c.Id == pharmacyStoreId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmacyStore), pharmacyStoreId);

        return _mapper.Map<PharmacyStoreDto>(pharmacyStore);
    }

    public async Task<IEnumerable<PharmacyStoreDto>> GetPharmacyStoresAsync()
    {
        var pharmacyStores = await _context.PharmacyStores
            .Include(c => c.Brand)
            .Where(c => !c.IsDeleted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PharmacyStoreDto>>(pharmacyStores);
    }

    public async Task<PharmacyStoreDto> UpdatePharmacyStoreAsync(Guid pharmacyStoreId, UpdatePharmacyStoreDto updatePharmacyStoreDto)
    {
        var validator = _serviceProvider.GetRequiredService<UpdatePharmacyStoreValidator>();

        var validationResult = await validator.ValidateAsync(updatePharmacyStoreDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var pharmacyStore = await _context.PharmacyStores
            .Where(c => c.Id == pharmacyStoreId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(PharmacyStore), pharmacyStoreId);

        pharmacyStore = _mapper.Map(updatePharmacyStoreDto, pharmacyStore);

        _context.PharmacyStores.Update(pharmacyStore);

        await _context.SaveChangesAsync();

        return _mapper.Map<PharmacyStoreDto>(pharmacyStore);
    }
}
