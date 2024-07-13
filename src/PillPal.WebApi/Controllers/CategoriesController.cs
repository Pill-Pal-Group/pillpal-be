using PillPal.Application.Features.Categories;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    [ProducesResponseType(typeof(PaginationResponse<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoriesAsync([FromQuery] CategoryQueryParameter queryParameter)
    {
        var categories = await categoryService.GetCategoriesAsync(queryParameter);

        return Ok(categories);
    }

    /// <summary>
    /// Get a category by id
    /// </summary>
    /// <param name="categoryId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a category</response>
    /// <response code="404">If the category is not found</response>
    [HttpGet("{categoryId:guid}", Name = "GetCategoryById")]
    [Cache(Key = nameof(Category), IdParameterName = "categoryId")]
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
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
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
    [Authorize(Policy.Administrative)]
    [HttpPost(Name = "CreateCategory")]
    [Cache(Key = nameof(Category), IdParameterName = "categoryId")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        var category = await categoryService.CreateCategoryAsync(createCategoryDto);

        return CreatedAtRoute("GetCategoryById", new { categoryId = category.Id }, category);
    }

    /// <summary>
    /// Create multiple categories
    /// </summary>
    /// <param name="createCategoryDtos"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/categories/bulk
    ///     [
    ///         {
    ///             "categoryName": "Category Name 1"
    ///         },
    ///         {
    ///             "categoryName": "Category Name 2"
    ///         }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="201">Returns the created categories</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost("bulk", Name = "CreateBulkCategories")]
    [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBulkCategoriesAsync(IEnumerable<CreateCategoryDto> createCategoryDtos)
    {
        var categories = await categoryService.CreateBulkCategoriesAsync(createCategoryDtos);

        return CreatedAtRoute("GetCategories", categories);
    }

    /// <summary>
    /// Update a category
    /// </summary>
    /// <param name="categoryId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateCategoryDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
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
    [Authorize(Policy.Administrative)]
    [HttpPut("{categoryId:guid}", Name = "UpdateCategory")]
    [Cache(Key = nameof(Category), IdParameterName = "categoryId")]
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
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="categoryId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the category is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{categoryId:guid}", Name = "DeleteCategory")]
    [Cache(Key = nameof(Category), IdParameterName = "categoryId")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategoryAsync(Guid categoryId)
    {
        await categoryService.DeleteCategoryAsync(categoryId);

        return NoContent();
    }
}
