using PillPal.Application.Features.Medicines;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IMedicineService
{
    /// <summary>
    /// Retrieves all medicines.
    /// </summary>
    /// <param name="queryParameter">The query parameters for filtering.</param>
    /// <param name="includeParameter">The include parameters for related entities.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="MedicineDto"/> objects.
    /// </returns>
    Task<IEnumerable<MedicineDto>> GetMedicinesAsync(
        MedicineQueryParameter queryParameter, MedicineIncludeParameter includeParameter);

    /// <summary>
    /// Retrieves a medicine by its unique identifier.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine.</param>
    /// <returns>
    /// The task result contains the <see cref="MedicineDto"/> representing the found medicine.
    /// </returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown if the entity is not found.</exception>
    Task<MedicineDto> GetMedicineByIdAsync(Guid medicineId);

    /// <summary>
    /// Creates a new medicine.
    /// </summary>
    /// <param name="createMedicineDto">The DTO containing the creation data for the medicine.</param>
    /// <returns>
    /// The task result contains the created <see cref="MedicineDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<MedicineDto> CreateMedicineAsync(CreateMedicineDto createMedicineDto);

    /// <summary>
    /// Updates an existing medicine.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine to update.</param>
    /// <param name="updateMedicineDto">The DTO containing update information for the medicine.</param>
    /// <returns>
    /// The task result contains the updated <see cref="MedicineDto"/>.
    /// </returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<MedicineDto> UpdateMedicineAsync(Guid medicineId, UpdateMedicineDto updateMedicineDto);

    /// <summary>
    /// Deletes a medicine by performing a soft delete.
    /// </summary>
    /// <param name="medicineId">The unique identifier for the medicine to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteMedicineAsync(Guid medicineId);
}
