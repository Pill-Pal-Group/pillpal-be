using PillPal.Application.Dtos.PharmacyStores;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IPharmacyStoreService
{
    Task<PharmacyStoreDto> CreatePharmacyStoreAsync(CreatePharmacyStoreDto createPharmacyStoreDto);
    Task<PharmacyStoreDto> UpdatePharmacyStoreAsync(Guid pharmacyStoreId, UpdatePharmacyStoreDto updatePharmacyStoreDto);
    Task DeletePharmacyStoreAsync(Guid pharmacyStoreId);
    Task<PharmacyStoreDto> GetPharmacyStoreByIdAsync(Guid pharmacyStoreId);
    Task<IEnumerable<PharmacyStoreDto>> GetPharmacyStoresAsync();
}
