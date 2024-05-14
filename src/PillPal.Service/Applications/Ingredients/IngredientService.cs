using AutoMapper;
using PillPal.Core.Dtos.Ingredients.Commands;
using PillPal.Core.Dtos.Ingredients.Queries;
using PillPal.Core.Models;
using PillPal.Infrastructure.Repository;

namespace PillPal.Service.Applications.Ingredients;

public class IngredientService : BaseService, IIngredientService
{
    public IngredientService(UnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper)
    {
    }

    public async Task CreateIngredientAsync(CreateIngredientCommand ingredient)
    {
        var validator = new CreateIngredientCommandValidator();
        var result = validator.Validate(ingredient);

        //todo

        Ingredient newIngredient = _mapper.Map<Ingredient>(ingredient);
        await _unitOfWork.Ingredient.Insert(newIngredient);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<GetIngredientQuery>> GetIngredientsAsync()
    {
        var list = await _unitOfWork.Ingredient.Get();
        return _mapper.Map<IEnumerable<GetIngredientQuery>>(list.ToList());
    }
}
