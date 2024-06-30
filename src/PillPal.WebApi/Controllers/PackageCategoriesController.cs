using PillPal.Application.Features.PackageCategories;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PackageCategoriesController(IPackageCategoryService packageCategoryService)
    : ControllerBase
{
    /// <summary>
    /// Get all package categories
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <remarks>If IsDeleted is not provided, the default value is false</remarks>
    /// <response code="200">Returns a list of package categories</response>
    [HttpGet(Name = "GetPackageCategories")]
    [ProducesResponseType(typeof(IEnumerable<PackageCategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPackageCategoriesAsync(
        [FromQuery] PackageCategoryQueryParameter queryParameter)
    {
        var packageCategories = await packageCategoryService.GetPackagesAsync(queryParameter);

        return Ok(packageCategories);
    }

    /// <summary>
    /// Get a package category by id
    /// </summary>
    /// <param name="packageCategoryId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="queryParameter"></param>
    /// <remarks>If IsDeleted is not provided, the default value is false</remarks>
    /// <response code="200">Returns a package category</response>
    /// <response code="404">If the package category is not found</response>
    [HttpGet("{packageCategoryId:guid}", Name = "GetPackageCategoryById")]
    [ProducesResponseType(typeof(PackageCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPackageCategoryByIdAsync(
        Guid packageCategoryId,
        [FromQuery] PackageCategoryQueryParameter queryParameter)
    {
        var packageCategory = await packageCategoryService.GetPackageByIdAsync(packageCategoryId, queryParameter);

        return Ok(packageCategory);
    }

    /// <summary>
    /// Create a package category
    /// </summary>
    /// <param name="createPackageCategoryDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/package-categories
    ///     {
    ///         "packageName": "Premium 1 month",
    ///         "packageDescription": "Package for premium feature in 1 month",
    ///         "packageDuration": 30,
    ///         "price": 100000
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created package category</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost(Name = "CreatePackageCategory")]
    [ProducesResponseType(typeof(PackageCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePackageCategoryAsync(CreatePackageCategoryDto createPackageCategoryDto)
    {
        var packageCategory = await packageCategoryService.CreatePackageAsync(createPackageCategoryDto);

        return CreatedAtRoute("GetPackageCategoryById", new { packageCategoryId = packageCategory.Id }, packageCategory);
    }

    /// <summary>
    /// Create multiple package categories
    /// </summary>
    /// <param name="createPackageCategoryDtos"></param>
    /// <remarks>
    /// Requires admin policy
    /// 
    /// Sample request:
    /// 
    ///     POST /api/package-categories/bulk
    ///     [
    ///         {
    ///             "packageName": "Premium 1 month",
    ///             "packageDescription": "Package for premium feature in 1 month",
    ///             "packageDuration": 30,
    ///             "price": 100000
    ///         },
    ///         {
    ///             "packageName": "Premium 3 months",
    ///             "packageDescription": "Package for premium feature in 3 months",
    ///             "packageDuration": 90,
    ///             "price": 250000
    ///         }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="201">Returns the created package categories</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Admin)]
    [HttpPost("bulk", Name = "CreateBulkPackageCategories")]
    [ProducesResponseType(typeof(IEnumerable<PackageCategoryDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBulkPackageCategoriesAsync(IEnumerable<CreatePackageCategoryDto> createPackageCategoryDtos)
    {
        var packageCategories = await packageCategoryService.CreateBulkPackagesAsync(createPackageCategoryDtos);

        return CreatedAtRoute("GetPackageCategories", packageCategories);
    }

    /// <summary>
    /// Update a package category
    /// </summary>
    /// <param name="packageCategoryId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updatePackageCategoryDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     PUT /api/package-categories/{packageCategoryId}
    ///     {
    ///         "packageName": "Premium 1 month",
    ///         "packageDescription": "Package for premium feature in 1 month",
    ///         "packageDuration": 30,
    ///         "price": 100000
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated package category</response>
    /// <response code="404">If the package category is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPut("{packageCategoryId:guid}", Name = "UpdatePackageCategory")]
    [ProducesResponseType(typeof(PackageCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdatePackageCategoryAsync(
        Guid packageCategoryId,
        UpdatePackageCategoryDto updatePackageCategoryDto)
    {
        var packageCategory = await packageCategoryService.UpdatePackageAsync(packageCategoryId, updatePackageCategoryDto);

        return Ok(packageCategory);
    }

    /// <summary>
    /// Delete a package category
    /// </summary>
    /// <param name="packageCategoryId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// <response code="204">No content</response>
    /// <response code="404">If the package category is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{packageCategoryId:guid}", Name = "DeletePackageCategory")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePackageCategoryAsync(Guid packageCategoryId)
    {
        await packageCategoryService.DeletePackageAsync(packageCategoryId);

        return NoContent();
    }
}
