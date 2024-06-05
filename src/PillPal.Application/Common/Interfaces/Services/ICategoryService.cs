using PillPal.Application.Features.Categories;

namespace PillPal.Application.Common.Interfaces.Services;

public interface ICategoryService
{
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
    Task<CategoryDto> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto);
    Task DeleteCategoryAsync(Guid categoryId);
    Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId);
    Task<IEnumerable<CategoryDto>> GetCategoriesAsync(CategoryQueryParameter queryParameter);
}
