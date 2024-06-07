using PillPal.Application.Features.Brands;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IBrandService
{
    /// <summary>
    /// Retrieves all brands.
    /// </summary>
    /// <param name="queryParameter">The query parameters for filtering.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="BrandDto"/> objects.
    /// </returns>
    Task<IEnumerable<BrandDto>> GetBrandsAsync(BrandQueryParameter queryParameter);

    /// <summary>
    /// Retrieves a brand by its unique identifier.
    /// </summary>
    /// <param name="brandId">The unique identifier for the brand.</param>
    /// <returns>
    /// The task result contains the <see cref="BrandDto"/> representing the found brand.
    /// </returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown if the entity is not found.</exception>
    Task<BrandDto> GetBrandByIdAsync(Guid brandId);

    /// <summary>
    /// Creates a new brand.
    /// </summary>
    /// <param name="createBrandDto">The DTO containing the creation data for the brand.</param>
    /// <returns>
    /// The task result contains the created <see cref="BrandDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<BrandDto> CreateBrandAsync(CreateBrandDto createBrandDto);

    /// <summary>
    /// Updates an existing brand.
    /// </summary>
    /// <param name="brandId">The unique identifier for the brand to update.</param>
    /// <param name="updateBrandDto">The DTO containing update information for the brand.</param>
    /// <returns>
    /// The task result contains the updated <see cref="BrandDto"/>.
    /// </returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<BrandDto> UpdateBrandAsync(Guid brandId, UpdateBrandDto updateBrandDto);

    /// <summary>
    /// Deletes a brand by performing a soft delete.
    /// </summary>
    /// <param name="brandId">The unique identifier for the brand to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="Exceptions.NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteBrandAsync(Guid brandId);
}
