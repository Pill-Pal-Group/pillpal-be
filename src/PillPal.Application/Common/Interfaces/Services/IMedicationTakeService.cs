using PillPal.Application.Common.Exceptions;
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
    /// The task result contains a collection of <see cref="MedicationTakesDto"/> objects.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown when the prescript is not found.</exception>
    Task<IEnumerable<MedicationTakesDto>> GetMedicationTakesAsync(Guid prescriptId, DateTimeOffset? dateTake);
}
