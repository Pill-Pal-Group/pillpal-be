using PillPal.Application.Features.TermsOfServices;

namespace PillPal.Application.Common.Interfaces.Services;

public interface ITermsOfService
{
    /// <summary>
    /// Retrieves all terms of services.
    /// </summary>
    /// <param name="queryParameter">The query parameter to filter the terms of services.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="TermsOfServiceDto"/> objects.
    /// </returns>
    Task<IEnumerable<TermsOfServiceDto>> GetTermsOfServicesAsync(TermsOfServiceQueryParameter queryParameter);

    /// <summary>
    /// Retrieves a terms of service by its unique identifier.
    /// </summary>
    /// <param name="termsOfServiceId">The unique identifier for the terms of service.</param>
    /// <returns>
    /// The task result contains the <see cref="TermsOfServiceDto"/> representing the found terms of service.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<TermsOfServiceDto> GetTermsOfServiceByIdAsync(Guid termsOfServiceId);

    /// <summary>
    /// Creates a new terms of service.
    /// </summary>
    /// <param name="createTermsOfServiceDto">The DTO containing the creation data for the terms of service.</param>
    /// <returns>
    /// The task result contains the created <see cref="TermsOfServiceDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<TermsOfServiceDto> CreateTermsOfServiceAsync(CreateTermsOfServiceDto createTermsOfServiceDto);

    /// <summary>
    /// Creates multiple terms of services.
    /// </summary>
    /// <param name="createTermsOfServiceDtos">The DTOs containing the creation data for the terms of services.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="TermsOfServiceDto"/> objects.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<IEnumerable<TermsOfServiceDto>> CreateBulkTermsOfServiceAsync(IEnumerable<CreateTermsOfServiceDto> createTermsOfServiceDtos);

    /// <summary>
    /// Updates an existing terms of service.
    /// </summary>
    /// <param name="termsOfServiceId">The unique identifier for the terms of service to update.</param>
    /// <param name="updateTermsOfServiceDto">The DTO containing update information for the terms of service.</param>
    /// <returns>
    /// The task result contains the updated <see cref="TermsOfServiceDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<TermsOfServiceDto> UpdateTermsOfServiceAsync(Guid termsOfServiceId, UpdateTermsOfServiceDto updateTermsOfServiceDto);

    /// <summary>
    /// Deletes a terms of service by performing a soft delete.
    /// </summary>
    /// <param name="termsOfServiceId">The unique identifier for the terms of service to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteTermsOfServiceAsync(Guid termsOfServiceId);
}
