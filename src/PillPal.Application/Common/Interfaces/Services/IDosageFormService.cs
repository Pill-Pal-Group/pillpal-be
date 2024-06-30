using PillPal.Application.Features.DosageForms;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IDosageFormService
{
    /// <summary>
    /// Retrieves all dosage forms.
    /// </summary>
    /// <returns>
    /// The task result contains a collection of <see cref="DosageFormDto"/> objects.
    /// </returns>
    Task<IEnumerable<DosageFormDto>> GetDosageFormsAsync();

    /// <summary>
    /// Retrieves a dosage form by its unique identifier.
    /// </summary>
    /// <param name="dosageFormId">The unique identifier for the dosage form.</param>
    /// <returns>
    /// The task result contains the <see cref="DosageFormDto"/> representing the found dosage form.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<DosageFormDto> GetDosageFormByIdAsync(Guid dosageFormId);

    /// <summary>
    /// Creates a new dosage form.
    /// </summary>
    /// <param name="createDosageFormDto">The DTO containing the creation data for the dosage form.</param>
    /// <returns>
    /// The task result contains the created <see cref="DosageFormDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<DosageFormDto> CreateDosageFormAsync(CreateDosageFormDto createDosageFormDto);

    /// <summary>
    /// Creates multiple dosage forms.
    /// </summary>
    /// <param name="createDosageFormDtos">The DTOs containing the creation data for the dosage forms.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="DosageFormDto"/> objects.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<IEnumerable<DosageFormDto>> CreateBulkDosageFormsAsync(IEnumerable<CreateDosageFormDto> createDosageFormDtos);

    /// <summary>
    /// Updates an existing dosage form.
    /// </summary>
    /// <param name="dosageFormId">The unique identifier for the dosage form to update.</param>
    /// <param name="updateDosageFormDto">The DTO containing update information for the dosage form.</param>
    /// <returns>
    /// The task result contains the updated <see cref="DosageFormDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<DosageFormDto> UpdateDosageFormAsync(Guid dosageFormId, UpdateDosageFormDto updateDosageFormDto);

    /// <summary>
    /// Deletes a dosage form, this deletion is permanent and cannot be undone.
    /// </summary>
    /// <param name="dosageFormId">The unique identifier for the dosage form to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteDosageFormAsync(Guid dosageFormId);
}
