using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.ActiveIngredients;

namespace PillPal.Application.Repositories;

public class ActiveIngredientRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider) 
    : BaseRepository(context, mapper, serviceProvider), IActiveIngredientService
{
    public async Task<ActiveIngredientDto> CreateActiveIngredientAsync(CreateActiveIngredientDto createActiveIngredientDto)
    {
        var validator = _serviceProvider.GetRequiredService<CreateActiveIngredientValidator>();

        var validationResult = await validator.ValidateAsync(createActiveIngredientDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var activeIngredient = _mapper.Map<ActiveIngredient>(createActiveIngredientDto);

        await _context.ActiveIngredients.AddAsync(activeIngredient);

        await _context.SaveChangesAsync();

        return _mapper.Map<ActiveIngredientDto>(activeIngredient);
    }

    public async Task DeleteActiveIngredientAsync(Guid activeIngredientId)
    {
        var activeIngredient = await _context.ActiveIngredients
            .Where(ai => ai.Id == activeIngredientId && !ai.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(ActiveIngredient), activeIngredientId);

        activeIngredient.IsDeleted = true;

        _context.ActiveIngredients.Update(activeIngredient);

        await _context.SaveChangesAsync();
    }

    public async Task<ActiveIngredientDto> GetActiveIngredientByIdAsync(Guid activeIngredientId)
    {
        var activeIngredient = await _context.ActiveIngredients
            .Where(ai => ai.Id == activeIngredientId && !ai.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(ActiveIngredient), activeIngredientId);

        return _mapper.Map<ActiveIngredientDto>(activeIngredient);
    }

    public async Task<IEnumerable<ActiveIngredientDto>> GetActiveIngredientsAsync()
    {
        var activeIngredients = await _context.ActiveIngredients
            .Where(ai => !ai.IsDeleted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ActiveIngredientDto>>(activeIngredients);
    }

    public async Task<ActiveIngredientDto> UpdateActiveIngredientAsync(Guid ingredientId, UpdateActiveIngredientDto updateActiveIngredientDto)
    {
        var validator = _serviceProvider.GetRequiredService<UpdateActiveIngredientValidator>();

        var validationResult = await validator.ValidateAsync(updateActiveIngredientDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var activeIngredient = await _context.ActiveIngredients
            .Where(ai => ai.Id == ingredientId && !ai.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(ActiveIngredient), ingredientId);

        activeIngredient = _mapper.Map(updateActiveIngredientDto, activeIngredient);

        _context.ActiveIngredients.Update(activeIngredient);

        await _context.SaveChangesAsync();

        return _mapper.Map<ActiveIngredientDto>(activeIngredient);
    }
}
