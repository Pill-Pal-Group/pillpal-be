using PillPal.Application.Features.MedicationTakes;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IMedicationTakeService
{
    /// <summary>
    /// Create Medication Take from given Prescript
    /// </summary>
    /// <param name="prescriptId"></param>
    /// <returns>
    /// The task result contains a collection of <see cref="MedicationTakesDto"/> objects.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown when the prescript is not found.</exception>
    /// <exception cref="BadRequestException">Thrown when the Medication Take is already created.</exception>
    Task<IEnumerable<MedicationTakesDto>> CreateMedicationTakeAsync(Guid prescriptId);

    /// <summary>
    /// Get Medication Takes from given Prescript
    /// </summary>
    /// <param name="prescriptId"></param>
    /// <param name="dateTake"></param>
    /// <returns>
    /// The task result contains a collection of <see cref="MedicationTakesListDto"/> objects.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown when the prescript is not found.</exception>
    Task<IEnumerable<MedicationTakesListDto>> GetMedicationTakesAsync(Guid prescriptId, DateTimeOffset? dateTake);

    /// <summary>
    /// Removes a Medication Take by performing a soft delete.
    /// </summary>
    /// <param name="medicationTakeId">The unique identifier for the Medication Take to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteMedicationTakeAsync(Guid medicationTakeId);
}
