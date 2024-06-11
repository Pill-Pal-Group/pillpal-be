using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.ActiveIngredients;

public class ActiveIngredientRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), IActiveIngredientService
{
    public async Task<ActiveIngredientDto> CreateActiveIngredientAsync(CreateActiveIngredientDto createActiveIngredientDto)
    {
        await ValidateAsync(createActiveIngredientDto);

        var activeIngredient = Mapper.Map<ActiveIngredient>(createActiveIngredientDto);

        await Context.ActiveIngredients.AddAsync(activeIngredient);

        await Context.SaveChangesAsync();

        return Mapper.Map<ActiveIngredientDto>(activeIngredient);
    }

    public async Task DeleteActiveIngredientAsync(Guid ingredientId)
    {
        var activeIngredient = await Context.ActiveIngredients
            .Where(ai => ai.Id == ingredientId && !ai.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(ActiveIngredient), ingredientId);

        activeIngredient.IsDeleted = true;

        Context.ActiveIngredients.Update(activeIngredient);

        await Context.SaveChangesAsync();
    }

    public async Task<ActiveIngredientDto> GetActiveIngredientByIdAsync(Guid ingredientId)
    {
        var activeIngredient = await Context.ActiveIngredients
            .Where(ai => ai.Id == ingredientId && !ai.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(ActiveIngredient), ingredientId);

        return Mapper.Map<ActiveIngredientDto>(activeIngredient);
    }

    public async Task<IEnumerable<ActiveIngredientDto>> GetActiveIngredientsAsync(ActiveIngredientQueryParameter queryParameter)
    {
        var activeIngredients = await Context.ActiveIngredients
            .Where(ai => !ai.IsDeleted)
            .Filter(queryParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<ActiveIngredientDto>>(activeIngredients);
    }

    public async Task<ActiveIngredientDto> UpdateActiveIngredientAsync(Guid ingredientId, UpdateActiveIngredientDto updateActiveIngredientDto)
    {
        await ValidateAsync(updateActiveIngredientDto);

        var activeIngredient = await Context.ActiveIngredients
            .Where(ai => ai.Id == ingredientId && !ai.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(ActiveIngredient), ingredientId);

        activeIngredient = Mapper.Map(updateActiveIngredientDto, activeIngredient);

        Context.ActiveIngredients.Update(activeIngredient);

        await Context.SaveChangesAsync();

        return Mapper.Map<ActiveIngredientDto>(activeIngredient);
    }
}
