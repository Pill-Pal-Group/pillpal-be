using PillPal.Application.Features.ActiveIngredients;

namespace PillPal.Application.Common.Interfaces.Services;

public interface IActiveIngredientService
{
    Task<ActiveIngredientDto> CreateActiveIngredientAsync(CreateActiveIngredientDto createActiveIngredientDto);

    Task<ActiveIngredientDto> UpdateActiveIngredientAsync(Guid ingredientId, UpdateActiveIngredientDto updateActiveIngredientDto);
    Task DeleteActiveIngredientAsync(Guid activeIngredientId);

    Task<ActiveIngredientDto> GetActiveIngredientByIdAsync(Guid activeIngredientId);

    Task<IEnumerable<ActiveIngredientDto>> GetActiveIngredientsAsync(ActiveIngredientQueryParameter queryParameter);
}
