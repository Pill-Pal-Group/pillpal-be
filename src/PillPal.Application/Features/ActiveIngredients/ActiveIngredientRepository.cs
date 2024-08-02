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

    public async Task<IEnumerable<ActiveIngredientDto>> CreateBulkActiveIngredientsAsync(IEnumerable<CreateActiveIngredientDto> createActiveIngredientDtos)
    {
        await ValidateListAsync(createActiveIngredientDtos);

        var activeIngredients = Mapper.Map<IEnumerable<ActiveIngredient>>(createActiveIngredientDtos);

        await Context.ActiveIngredients.AddRangeAsync(activeIngredients);

        await Context.SaveChangesAsync();

        return Mapper.Map<IEnumerable<ActiveIngredientDto>>(activeIngredients);
    }

    public async Task DeleteActiveIngredientAsync(Guid ingredientId)
    {
        var activeIngredient = await Context.ActiveIngredients
            .Where(ai => !ai.IsDeleted)
            .FirstOrDefaultAsync(ai => ai.Id == ingredientId)
            ?? throw new NotFoundException(nameof(ActiveIngredient), ingredientId);

        Context.ActiveIngredients.Remove(activeIngredient);

        await Context.SaveChangesAsync();
    }

    public async Task<ActiveIngredientDto> GetActiveIngredientByIdAsync(Guid ingredientId)
    {
        var activeIngredient = await Context.ActiveIngredients
            .Where(ai => !ai.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync(ai => ai.Id == ingredientId)
            ?? throw new NotFoundException(nameof(ActiveIngredient), ingredientId);

        return Mapper.Map<ActiveIngredientDto>(activeIngredient);
    }

    public async Task<PaginationResponse<ActiveIngredientDto>> GetActiveIngredientsAsync(ActiveIngredientQueryParameter queryParameter)
    {
        await ValidateAsync(queryParameter);
        
        var activeIngredients = await Context.ActiveIngredients
            .AsNoTracking()
            .Where(ai => !ai.IsDeleted)
            .Filter(queryParameter)
            .ToPaginationResponseAsync<ActiveIngredient, ActiveIngredientDto>(queryParameter, Mapper);

        return activeIngredients;
    }

    public async Task<ActiveIngredientDto> UpdateActiveIngredientAsync(Guid ingredientId, UpdateActiveIngredientDto updateActiveIngredientDto)
    {
        await ValidateAsync(updateActiveIngredientDto);

        var activeIngredient = await Context.ActiveIngredients
            .Where(ai => !ai.IsDeleted)
            .FirstOrDefaultAsync(ai => ai.Id == ingredientId)
            ?? throw new NotFoundException(nameof(ActiveIngredient), ingredientId);

        activeIngredient = Mapper.Map(updateActiveIngredientDto, activeIngredient);

        Context.ActiveIngredients.Update(activeIngredient);

        await Context.SaveChangesAsync();

        return Mapper.Map<ActiveIngredientDto>(activeIngredient);
    }
}
