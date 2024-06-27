using PillPal.Application.Features.PharmaceuticalCompanies;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IPharmaceuticalCompanyService
{
    /// <summary>
    /// Retrieves all pharmaceutical companies.
    /// </summary>
    /// <returns>
    /// The task result contains a collection of <see cref="PharmaceuticalCompanyDto"/> objects.
    /// </returns>
    Task<IEnumerable<PharmaceuticalCompanyDto>> GetPharmaceuticalCompaniesAsync();

    /// <summary>
    /// Retrieves a pharmaceutical company by its unique identifier.
    /// </summary>
    /// <param name="companyId">The unique identifier for the pharmaceutical company.</param>
    /// <returns>
    /// The task result contains the <see cref="PharmaceuticalCompanyDto"/> representing the found pharmaceutical company.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<PharmaceuticalCompanyDto> GetPharmaceuticalCompanyByIdAsync(Guid companyId);

    /// <summary>
    /// Creates a new pharmaceutical company.
    /// </summary>
    /// <param name="createPharmaceuticalCompanyDto">The DTO containing the creation data for the pharmaceutical company.</param>
    /// <returns>
    /// The task result contains the created <see cref="PharmaceuticalCompanyDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<PharmaceuticalCompanyDto> CreatePharmaceuticalCompanyAsync(CreatePharmaceuticalCompanyDto createPharmaceuticalCompanyDto);

    /// <summary>
    /// Updates an existing pharmaceutical company.
    /// </summary>
    /// <param name="companyId">The unique identifier for the pharmaceutical company to update.</param>
    /// <param name="updatePharmaceuticalCompanyDto">The DTO containing update information for the pharmaceutical company.</param>
    /// <returns>
    /// The task result contains the updated <see cref="PharmaceuticalCompanyDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<PharmaceuticalCompanyDto> UpdatePharmaceuticalCompanyAsync(Guid companyId, UpdatePharmaceuticalCompanyDto updatePharmaceuticalCompanyDto);

    /// <summary>
    /// Deletes a pharmaceutical company by performing a soft delete.
    /// </summary>
    /// <param name="companyId">The unique identifier for the pharmaceutical company to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeletePharmaceuticalCompanyAsync(Guid companyId);
}
