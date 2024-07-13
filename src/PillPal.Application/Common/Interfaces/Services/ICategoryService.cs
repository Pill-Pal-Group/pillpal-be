using PillPal.Application.Features.Categories;

namespace PillPal.Application.Common.Interfaces.Services;

public interface ICategoryService
{
    /// <summary>
    /// Retrieves all categories.
    /// </summary>
    /// <param name="queryParameter">The query parameters for filtering.</param>
    /// <returns>
    /// The task result contains a pagination collection of <see cref="CategoryDto"/> objects.
    /// </returns>
    Task<PaginationResponse<CategoryDto>> GetCategoriesAsync(CategoryQueryParameter queryParameter);

    /// <summary>
    /// Retrieves a category by its unique identifier.
    /// </summary>
    /// <param name="categoryId">The unique identifier for the category.</param>
    /// <returns>
    /// The task result contains the <see cref="CategoryDto"/> representing the found category.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="createCategoryDto">The DTO containing the creation data for the category.</param>
    /// <returns>
    /// The task result contains the created <see cref="CategoryDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);

    /// <summary>
    /// Creates multiple categories.
    /// </summary>
    /// <param name="createCategoryDtos">The DTOs containing the creation data for the categories.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="CategoryDto"/> objects.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<IEnumerable<CategoryDto>> CreateBulkCategoriesAsync(IEnumerable<CreateCategoryDto> createCategoryDtos);

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="categoryId">The unique identifier for the category to update.</param>
    /// <param name="updateCategoryDto">The DTO containing update information for the category.</param>
    /// <returns>
    /// The task result contains the updated <see cref="CategoryDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<CategoryDto> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto);

    /// <summary>
    /// Deletes a category by performing a soft delete.
    /// </summary>
    /// <param name="categoryId">The unique identifier for the category to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteCategoryAsync(Guid categoryId);
}
