using PillPal.Application.Features.PackageCategories;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IPackageCategoryService
{   
    /// <summary>
    /// Get all packages
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <returns>
    /// The task result contains a collection of <see cref="PackageCategoryDto"/> objects.
    /// </returns>
    Task<IEnumerable<PackageCategoryDto>> GetPackagesAsync(PackageCategoryQueryParameter queryParameter);

    /// <summary>
    /// Get a package by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the package.</param>
    /// <param name="queryParameter">The query parameters for filtering.</param>
    /// <returns>
    /// The task result contains the <see cref="PackageCategoryDto"/> representing the found package.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<PackageCategoryDto> GetPackageByIdAsync(Guid id, PackageCategoryQueryParameter queryParameter);

    /// <summary>
    /// Creates a new package.
    /// </summary>
    /// <param name="createPackageCategoryDto">The DTO containing the creation data for the package.</param>
    /// <returns>
    /// The task result contains the created <see cref="PackageCategoryDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<PackageCategoryDto> CreatePackageAsync(CreatePackageCategoryDto createPackageCategoryDto);

    /// <summary>
    /// Creates multiple packages.
    /// </summary>
    /// <param name="createPackageCategoryDtos">The DTOs containing the creation data for the packages.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="PackageCategoryDto"/> objects.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<IEnumerable<PackageCategoryDto>> CreateBulkPackagesAsync(IEnumerable<CreatePackageCategoryDto> createPackageCategoryDtos);

    /// <summary>
    /// Updates an existing package.
    /// </summary>
    /// <param name="id">The unique identifier for the package to update.</param>
    /// <param name="updatePackageCategoryDto">The DTO containing update information for the package.</param>
    /// <returns>
    /// The task result contains the updated <see cref="PackageCategoryDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<PackageCategoryDto> UpdatePackageAsync(Guid id, UpdatePackageCategoryDto updatePackageCategoryDto);

    /// <summary>
    /// Deletes a package by performing a soft delete.
    /// </summary>
    /// <param name="id">The unique identifier for the package to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeletePackageAsync(Guid id);
}
