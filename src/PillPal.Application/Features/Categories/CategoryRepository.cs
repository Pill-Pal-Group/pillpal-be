using PillPal.Application.Common.Exceptions;
using PillPal.Application.Common.Interfaces.Data;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Common.Repositories;

namespace PillPal.Application.Features.Categories;

public class CategoryRepository(IApplicationDbContext context, IMapper mapper, IServiceProvider serviceProvider)
    : BaseRepository(context, mapper, serviceProvider), ICategoryService
{
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var validator = ServiceProvider.GetRequiredService<CreateCategoryValidator>();

        var validationResult = await validator.ValidateAsync(createCategoryDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var category = Mapper.Map<Category>(createCategoryDto);

        await Context.Categories.AddAsync(category);

        await Context.SaveChangesAsync();

        return Mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteCategoryAsync(Guid categoryId)
    {
        var category = await Context.Categories
            .Where(c => c.Id == categoryId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Category), categoryId);

        category.IsDeleted = true;

        Context.Categories.Update(category);

        await Context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await Context.Categories
            .Where(c => !c.IsDeleted)
            .ToListAsync();

        return Mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await Context.Categories
            .Where(c => c.Id == categoryId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Category), categoryId);

        return Mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto)
    {
        var validator = ServiceProvider.GetRequiredService<UpdateCategoryValidator>();

        var validationResult = await validator.ValidateAsync(updateCategoryDto);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var category = await Context.Categories
            .Where(c => c.Id == categoryId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Category), categoryId);

        Mapper.Map(updateCategoryDto, category);

        Context.Categories.Update(category);

        await Context.SaveChangesAsync();

        return Mapper.Map<CategoryDto>(category);
    }
}
