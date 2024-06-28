using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.Brands;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class BrandsController(IBrandService brandService)
    : ControllerBase
{
    /// <summary>
    /// Get all brands
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <response code="200">Returns a list of brands</response>
    [HttpGet(Name = "GetBrands")]
    [ProducesResponseType(typeof(IEnumerable<BrandDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBrandsAsync([FromQuery] BrandQueryParameter queryParameter)
    {
        var brands = await brandService.GetBrandsAsync(queryParameter);

        return Ok(brands);
    }

    /// <summary>
    /// Get a brand by id
    /// </summary>
    /// <param name="brandId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a brand</response>
    /// <response code="404">If the brand is not found</response>
    [HttpGet("{brandId:guid}", Name = "GetBrandById")]
    [ProducesResponseType(typeof(BrandDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBrandByIdAsync(Guid brandId)
    {
        var brand = await brandService.GetBrandByIdAsync(brandId);

        return Ok(brand);
    }

    /// <summary>
    /// Create a brand
    /// </summary>
    /// <param name="createBrandDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/brands
    ///     {
    ///         "brandName": "Brand Name",
    ///         "brandUrl": "Brand Url",
    ///         "brandLogo": "Brand Logo"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created brand</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPost(Name = "CreateBrand")]
    [ProducesResponseType(typeof(BrandDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBrandAsync(CreateBrandDto createBrandDto)
    {
        var brand = await brandService.CreateBrandAsync(createBrandDto);

        return CreatedAtAction("GetBrandById", new { brandId = brand.Id }, brand);
    }

    /// <summary>
    /// Update a brand
    /// </summary>
    /// <param name="brandId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateBrandDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/brands/{brandId}
    ///     {
    ///         "brandName": "Brand Name",
    ///         "brandUrl": "Brand Url",
    ///         "brandLogo": "Brand Logo"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated brand</response>
    /// <response code="404">If the brand is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPut("{brandId:guid}", Name = "UpdateBrand")]
    [ProducesResponseType(typeof(BrandDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateBrandAsync(Guid brandId, UpdateBrandDto updateBrandDto)
    {
        var brand = await brandService.UpdateBrandAsync(brandId, updateBrandDto);

        return Ok(brand);
    }

    /// <summary>
    /// Delete a brand (soft delete)
    /// </summary>
    /// <param name="brandId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the brand is not found</response>
    [HttpDelete("{brandId:guid}", Name = "DeleteBrand")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBrandAsync(Guid brandId)
    {
        await brandService.DeleteBrandAsync(brandId);

        return NoContent();
    }
}
