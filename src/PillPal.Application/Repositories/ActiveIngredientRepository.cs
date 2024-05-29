﻿using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.ActiveIngredients;

namespace PillPal.Application.Repositories;

public class ActiveIngredientRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider) 
    : BaseRepository(context, mapper, serviceProvider), IActiveIngredientService
{
    public async Task<ActiveIngredientDto> CreateActiveIngredientAsync(CreateActiveIngredientDto createActiveIngredientDto)
    {
        var validator = ServiceProvider.GetRequiredService<CreateActiveIngredientValidator>();

        var validationResult = await validator.ValidateAsync(createActiveIngredientDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var activeIngredient = Mapper.Map<ActiveIngredient>(createActiveIngredientDto);

        await Context.ActiveIngredients.AddAsync(activeIngredient);

        await Context.SaveChangesAsync();

        return Mapper.Map<ActiveIngredientDto>(activeIngredient);
    }

    public async Task DeleteActiveIngredientAsync(Guid activeIngredientId)
    {
        var activeIngredient = await Context.ActiveIngredients
            .Where(ai => ai.Id == activeIngredientId && !ai.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(ActiveIngredient), activeIngredientId);

        activeIngredient.IsDeleted = true;

        Context.ActiveIngredients.Update(activeIngredient);

        await Context.SaveChangesAsync();
    }

    public async Task<ActiveIngredientDto> GetActiveIngredientByIdAsync(Guid activeIngredientId)
    {
        var activeIngredient = await Context.ActiveIngredients
            .Where(ai => ai.Id == activeIngredientId && !ai.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(ActiveIngredient), activeIngredientId);

        return Mapper.Map<ActiveIngredientDto>(activeIngredient);
    }

    public async Task<IEnumerable<ActiveIngredientDto>> GetActiveIngredientsAsync()
    {
        var activeIngredients = await Context.ActiveIngredients
            .Where(ai => !ai.IsDeleted)
            .ToListAsync();

        return Mapper.Map<IEnumerable<ActiveIngredientDto>>(activeIngredients);
    }

    public async Task<ActiveIngredientDto> UpdateActiveIngredientAsync(Guid ingredientId, UpdateActiveIngredientDto updateActiveIngredientDto)
    {
        var validator = ServiceProvider.GetRequiredService<UpdateActiveIngredientValidator>();

        var validationResult = await validator.ValidateAsync(updateActiveIngredientDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var activeIngredient = await Context.ActiveIngredients
            .Where(ai => ai.Id == ingredientId && !ai.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(ActiveIngredient), ingredientId);

        activeIngredient = Mapper.Map(updateActiveIngredientDto, activeIngredient);

        Context.ActiveIngredients.Update(activeIngredient);

        await Context.SaveChangesAsync();

        return Mapper.Map<ActiveIngredientDto>(activeIngredient);
    }
}
