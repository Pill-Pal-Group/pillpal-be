using PillPal.Application.Features.Nations;

namespace PillPal.Application.Common.Interfaces.Services;

public interface INationService
{
    /// <summary>
    /// Retrieves all nations.
    /// </summary>
    /// <param name="queryParameter">The query parameters for filtering.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="NationDto"/> objects.
    /// </returns>
    Task<IEnumerable<NationDto>> GetNationsAsync(NationQueryParameter queryParameter);

    /// <summary>
    /// Retrieves a nation by its unique identifier.
    /// </summary>
    /// <param name="nationId">The unique identifier for the nation.</param>
    /// <returns>
    /// The task result contains the <see cref="NationDto"/> representing the found nation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<NationDto> GetNationByIdAsync(Guid nationId);

    /// <summary>
    /// Creates a new nation.
    /// </summary>
    /// <param name="createNationDto">The DTO containing the creation data for the nation.</param>
    /// <returns>
    /// The task result contains the created <see cref="NationDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<NationDto> CreateNationAsync(CreateNationDto createNationDto);

    /// <summary>
    /// Creates multiple nations.
    /// </summary>
    /// <param name="createNationDtos">The DTOs containing the creation data for the nations.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="NationDto"/> objects.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<IEnumerable<NationDto>> CreateBulkNationsAsync(IEnumerable<CreateNationDto> createNationDtos);

    /// <summary>
    /// Updates an existing nation.
    /// </summary>
    /// <param name="nationId">The unique identifier for the nation to update.</param>
    /// <param name="updateNationDto">The DTO containing update information for the nation.</param>
    /// <returns>
    /// The task result contains the updated <see cref="NationDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<NationDto> UpdateNationAsync(Guid nationId, UpdateNationDto updateNationDto);

    /// <summary>
    /// Deletes a nation by performing a soft delete.
    /// </summary>
    /// <param name="nationId">The unique identifier for the nation to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteNationAsync(Guid nationId);
}
