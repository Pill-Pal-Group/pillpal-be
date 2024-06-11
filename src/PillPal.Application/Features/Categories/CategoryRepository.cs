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
        await ValidateAsync(createCategoryDto);

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

    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CategoryQueryParameter queryParameter)
    {
        var categories = await Context.Categories
            .Where(c => !c.IsDeleted)
            .Filter(queryParameter)
            .AsNoTracking()
            .ToListAsync();

        return Mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await Context.Categories
            .Where(c => c.Id == categoryId && !c.IsDeleted)
            .AsNoTracking()
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Category), categoryId);

        return Mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto)
    {
        await ValidateAsync(updateCategoryDto);

        var category = await Context.Categories
            .Where(c => c.Id == categoryId && !c.IsDeleted)
            .FirstOrDefaultAsync() ?? throw new NotFoundException(nameof(Category), categoryId);

        Mapper.Map(updateCategoryDto, category);

        Context.Categories.Update(category);

        await Context.SaveChangesAsync();

        return Mapper.Map<CategoryDto>(category);
    }
}
