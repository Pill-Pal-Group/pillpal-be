using PillPal.Application.Common.Paginations;
using PillPal.Application.Features.Specifications;

namespace PillPal.Application.Common.Interfaces.Services;

public interface ISpecificationService
{
    /// <summary>
    /// Retrieves all specifications.
    /// </summary>
    /// <param name="queryParameter">The query parameter for filtering.</param>
    /// <returns>
    /// The task result contains a pagination collection of <see cref="SpecificationDto"/> objects.
    /// </returns>
    Task<PaginationResponse<SpecificationDto>> GetSpecificationsAsync(SpecificationQueryParameter queryParameter);

    /// <summary>
    /// Retrieves a specification by its unique identifier.
    /// </summary>
    /// <param name="specificationId">The unique identifier for the specification.</param>
    /// <returns>
    /// The task result contains the <see cref="SpecificationDto"/> representing the found specification.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<SpecificationDto> GetSpecificationByIdAsync(Guid specificationId);

    /// <summary>
    /// Creates a new specification.
    /// </summary>
    /// <param name="createSpecificationDto">The DTO containing the creation data for the specification.</param>
    /// <returns>
    /// The task result contains the created <see cref="SpecificationDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<SpecificationDto> CreateSpecificationAsync(CreateSpecificationDto createSpecificationDto);

    /// <summary>
    /// Creates multiple specifications.
    /// </summary>
    /// <param name="createSpecificationDtos">The DTOs containing the creation data for the specifications.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="SpecificationDto"/> objects.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<IEnumerable<SpecificationDto>> CreateBulkSpecificationsAsync(IEnumerable<CreateSpecificationDto> createSpecificationDtos);

    /// <summary>
    /// Updates an existing specification.
    /// </summary>
    /// <param name="specificationId">The unique identifier for the specification to update.</param>
    /// <param name="updateSpecificationDto">The DTO containing update information for the specification.</param>
    /// <returns>
    /// The task result contains the updated <see cref="SpecificationDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<SpecificationDto> UpdateSpecificationAsync(Guid specificationId, UpdateSpecificationDto updateSpecificationDto);

    /// <summary>
    /// Deletes a specification by performing a soft delete.
    /// </summary>
    /// <param name="specificationId">The unique identifier for the specification to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteSpecificationAsync(Guid specificationId);
}
