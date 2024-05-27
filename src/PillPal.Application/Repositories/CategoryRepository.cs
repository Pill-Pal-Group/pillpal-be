using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Categories;

namespace PillPal.Application.Repositories;

public class CategoryRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ICategoryService
{
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var validator = _serviceProvider.GetRequiredService<CreateCategoryValidator>();

        var validationResult = await validator.ValidateAsync(createCategoryDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var category = _mapper.Map<Category>(createCategoryDto);

        await _context.Categories.AddAsync(category);

        await _context.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteCategoryAsync(Guid categoryId)
    {
        var category = await _context.Categories
            .Where(c => c.Id == categoryId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Category), categoryId);

        category.IsDeleted = true;

        _context.Categories.Update(category);

        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await _context.Categories
            .Where(c => !c.IsDeleted)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await _context.Categories
            .Where(c => c.Id == categoryId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Category), categoryId);

        return _mapper.Map<CategoryDto>(category); 
    }

    public async Task<CategoryDto> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto)
    {
        var validator = _serviceProvider.GetRequiredService<UpdateCategoryValidator>();

        var validationResult = await validator.ValidateAsync(updateCategoryDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var category = await _context.Categories
            .Where(c => c.Id == categoryId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Category), categoryId);

        _mapper.Map(updateCategoryDto, category);

        _context.Categories.Update(category);

        await _context.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(category); 
    }
}
