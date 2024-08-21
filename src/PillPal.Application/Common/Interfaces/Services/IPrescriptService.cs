using PillPal.Application.Features.PrescriptDetails;
using PillPal.Application.Features.Prescripts;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IPrescriptService
{
    /// <summary>
    /// Retrieves all prescripts.
    /// </summary>
    /// <param name="queryParameter">The query parameters for filtering.</param>
    /// <param name="includeParameter">The include parameters for related entities.</param>
    /// <returns>
    /// The task result contains a pagination collection of <see cref="PrescriptDto"/> objects.
    /// </returns>
    Task<PaginationResponse<PrescriptDto>> GetPrescriptsAsync(
        PrescriptQueryParameter queryParameter, PrescriptIncludeParameter includeParameter);

    /// <summary>
    /// Retrieves a prescript by its unique identifier.
    /// </summary>
    /// <param name="prescriptId">The unique identifier for the prescript.</param>
    /// <returns>
    /// The task result contains the <see cref="PrescriptDto"/> representing the found prescript.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<PrescriptDto> GetPrescriptByIdAsync(Guid prescriptId);

    /// <summary>
    /// Creates a new prescript.
    /// </summary>
    /// <param name="createPrescriptDto">The DTO containing the creation data for the prescript.</param>
    /// <returns>
    /// The task result contains the created <see cref="PrescriptDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    /// <exception cref="NotFoundException">Thrown if the customer (owner of the prescript) is not found.</exception>
    Task<PrescriptDto> CreatePrescriptAsync(CreatePrescriptDto createPrescriptDto);

    /// <summary>
    /// Updates medicine image of a prescript detail.
    /// </summary>
    /// <param name="prescriptDetailId">The unique identifier for the prescript detail.</param>
    /// <param name="updatePrescriptDetailImageDto">The DTO containing the update data for the prescript detail image.</param>
    /// <returns>The task result.</returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update data.</exception>
    Task UpdatePrescriptDetailImageAsync(Guid prescriptDetailId, UpdatePrescriptDetailImageDto updatePrescriptDetailImageDto);

    /// <summary>
    /// Deletes a prescript by its unique identifier. Soft delete.
    /// </summary>
    /// <param name="prescriptId">The unique identifier for the prescript.</param>
    /// <returns>
    /// The task result.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeletePrescriptByIdAsync(Guid prescriptId);
}
