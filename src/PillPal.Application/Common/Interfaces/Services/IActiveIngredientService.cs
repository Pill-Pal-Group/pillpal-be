using PillPal.Application.Features.ActiveIngredients;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IActiveIngredientService
{
    /// <summary>
    /// Retrieves all active ingredients.
    /// </summary>
    /// <param name="queryParameter">The query parameters for filtering.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="ActiveIngredientDto"/> objects.
    /// </returns>
    Task<IEnumerable<ActiveIngredientDto>> GetActiveIngredientsAsync(ActiveIngredientQueryParameter queryParameter);

    /// <summary>
    /// Retrieves an active ingredient by its unique identifier.
    /// </summary>
    /// <param name="ingredientId">The unique identifier for the active ingredient.</param>
    /// <returns>
    /// The task result contains the <see cref="ActiveIngredientDto"/> representing the found active ingredient.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task<ActiveIngredientDto> GetActiveIngredientByIdAsync(Guid ingredientId);

    /// <summary>
    /// Creates a new active ingredient.
    /// </summary>
    /// <param name="createActiveIngredientDto">The DTO containing the creation data for the active ingredient.</param>
    /// <returns>
    /// The task result contains the created <see cref="ActiveIngredientDto"/>.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<ActiveIngredientDto> CreateActiveIngredientAsync(CreateActiveIngredientDto createActiveIngredientDto);

    /// <summary>
    /// Creates multiple active ingredients.
    /// </summary>
    /// <param name="createActiveIngredientDtos">The DTOs containing the creation data for the active ingredients.</param>
    /// <returns>
    /// The task result contains a collection of <see cref="ActiveIngredientDto"/> objects.
    /// </returns>
    /// <exception cref="ValidationException">Thrown when validation fails for the creation data.</exception>
    Task<IEnumerable<ActiveIngredientDto>> CreateBulkActiveIngredientsAsync(IEnumerable<CreateActiveIngredientDto> createActiveIngredientDtos);

    /// <summary>
    /// Updates an existing active ingredient.
    /// </summary>
    /// <param name="ingredientId">The unique identifier for the ingredient to update.</param>
    /// <param name="updateActiveIngredientDto">The DTO containing update information for the active ingredient.</param>
    /// <returns>
    /// The task result contains the updated <see cref="ActiveIngredientDto"/>.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    /// <exception cref="ValidationException">Thrown when validation fails for the update information.</exception>
    Task<ActiveIngredientDto> UpdateActiveIngredientAsync(Guid ingredientId, UpdateActiveIngredientDto updateActiveIngredientDto);

    /// <summary>
    /// Deletes an active ingredient by performing a soft delete.
    /// </summary>
    /// <param name="ingredientId">The unique identifier for the active ingredient to delete.</param>
    /// <returns>
    /// A task that represents the asynchronous delete operation.
    /// </returns>
    /// <exception cref="NotFoundException">Thrown if the entity is not found.</exception>
    Task DeleteActiveIngredientAsync(Guid ingredientId);
}
