using PillPal.Core.Dtos.Ingredients.Commands;
using PillPal.Core.Dtos.Ingredients.Queries;

namespace PillPal.Service.Applications.Ingredients;

public interface IIngredientService
{
    Task<IEnumerable<GetIngredientQuery>> GetIngredientsAsync();
    Task CreateIngredientAsync(CreateIngredientCommand ingredient);
}
