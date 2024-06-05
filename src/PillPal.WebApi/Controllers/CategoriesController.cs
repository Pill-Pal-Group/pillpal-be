using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.Categories;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class CategoriesController(ICategoryService categoryService)
    : ControllerBase
{
    /// <summary>
    /// Get all categories
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <response code="200">Returns a list of categories</response>
    [HttpGet(Name = "GetCategories")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoriesAsync([FromQuery] CategoryQueryParameter queryParameter)
    {
        var categories = await categoryService.GetCategoriesAsync(queryParameter);

        return Ok(categories);
    }

    /// <summary>
    /// Get a category by id
    /// </summary>
    /// <param name="categoryId"></param>
    /// <response code="200">Returns a category</response>
    /// <response code="404">If the category is not found</response>
    [HttpGet("{categoryId:guid}", Name = "GetCategoryById")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await categoryService.GetCategoryByIdAsync(categoryId);

        return Ok(category);
    }

    /// <summary>
    /// Create a category
    /// </summary>
    /// <param name="createCategoryDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/categories
    ///     {
    ///         "categoryName": "Category Name"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created category</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPost(Name = "CreateCategory")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var category = await categoryService.CreateCategoryAsync(createCategoryDto);

        return CreatedAtRoute("GetCategoryById", new { categoryId = category.Id }, category);
    }

    /// <summary>
    /// Update a category
    /// </summary>
    /// <param name="categoryId"></param>
    /// <param name="updateCategoryDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/categories/{categoryId}
    ///     {
    ///         "categoryName": "Category Name"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated category</response>
    /// <response code="404">If the category is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPut("{categoryId:guid}", Name = "UpdateCategory")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDto updateCategoryDto)
    {
        var category = await categoryService.UpdateCategoryAsync(categoryId, updateCategoryDto);

        return Ok(category);
    }

    /// <summary>
    /// Delete a category (soft delete)
    /// </summary>
    /// <param name="categoryId"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the category is not found</response>
    [HttpDelete("{categoryId:guid}", Name = "DeleteCategory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategoryAsync(Guid categoryId)
    {
        await categoryService.DeleteCategoryAsync(categoryId);

        return NoContent();
    }
}
