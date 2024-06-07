using PillPal.Application.Features.PharmacyStores;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IPharmacyStoreService
{
    /// <summary>
    /// Retrieves all pharmacy stores.
    /// </summary>
    /// <returns>
    /// The task result contains a collection of <see cref="PharmacyStoreDto"/> objects.
    /// </returns>
    Task<IEnumerable<PharmacyStoreDto>> GetPharmacyStoresAsync();

    /// <summary>
    /// Retrieves a pharmacy store by its unique identifier.
    /// </summary>
    /// <param name="pharmacyStoreId">The unique identifier for the pharmacy store.</param>
    /// <returns>
    /// The task result contains the <see cref="PharmacyStoreDto"/> representing the found pharmacy store.
    /// </returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown if the entity is not found.</exception>
    Task<PharmacyStoreDto> GetPharmacyStoreByIdAsync(Guid pharmacyStoreId);

    /// <summary>
    /// Creates a new pharmacy store.
    /// </summary>
    /// <param name="createPharmacyStoreDto">The DTO containing the creation data for the pharmacy store.</param>
    /// <returns>
    /// The task result contains the created <see cref="PharmacyStoreDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<PharmacyStoreDto> CreatePharmacyStoreAsync(CreatePharmacyStoreDto createPharmacyStoreDto);

    /// <summary>
    /// Updates an existing pharmacy store.
    /// </summary>
    /// <param name="pharmacyStoreId">The unique identifier for the pharmacy store to update.</param>
    /// <param name="updatePharmacyStoreDto">The DTO containing update information for the pharmacy store.</param>
    /// <returns>
    /// The task result contains the updated <see cref="PharmacyStoreDto"/>.
    /// </returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<PharmacyStoreDto> UpdatePharmacyStoreAsync(Guid pharmacyStoreId, UpdatePharmacyStoreDto updatePharmacyStoreDto);

    /// <summary>
    /// Deletes a pharmacy store by performing a soft delete.
    /// </summary>
    /// <param name="pharmacyStoreId">The unique identifier for the pharmacy store to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown if the entity is not found.</exception>
    Task DeletePharmacyStoreAsync(Guid pharmacyStoreId);
}
