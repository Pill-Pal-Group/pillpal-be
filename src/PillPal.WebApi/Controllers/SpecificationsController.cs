using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.Specifications;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class SpecificationsController(ISpecificationService specificationService)
    : ControllerBase
{
    /// <summary>
    /// Get all specifications
    /// </summary>
    /// <response code="200">Returns a list of specifications</response>
    [HttpGet(Name = "GetSpecifications")]
    [ProducesResponseType(typeof(IEnumerable<SpecificationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSpecificationsAsync()
    {
        var specifications = await specificationService.GetAllSpecificationsAsync();

        return Ok(specifications);
    }

    /// <summary>
    /// Get a specification by id
    /// </summary>
    /// <param name="specificationId"></param>
    /// <response code="200">Returns a specification</response>
    /// <response code="404">If the specification is not found</response>
    [HttpGet("{specificationId:guid}", Name = "GetSpecificationById")]
    [ProducesResponseType(typeof(SpecificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSpecificationByIdAsync(Guid specificationId)
    {
        var specification = await specificationService.GetSpecificationByIdAsync(specificationId);

        return Ok(specification);
    }

    /// <summary>
    /// Create a specification
    /// </summary>
    /// <param name="createSpecificationDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/specifications
    ///     {
    ///         "typeName": "Specification Type",
    ///         "detail": "Specification Detail"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created specification</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPost(Name = "CreateSpecification")]
    [ProducesResponseType(typeof(SpecificationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateSpecificationAsync(CreateSpecificationDto createSpecificationDto)
    {
        var specification = await specificationService.CreateSpecificationAsync(createSpecificationDto);

        return CreatedAtRoute("GetSpecificationById", new { specificationId = specification.Id }, specification);
    }

    /// <summary>
    /// Update a specification
    /// </summary>
    /// <param name="specificationId"></param>
    /// <param name="updateSpecificationDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/specifications/{specificationId}
    ///     {
    ///         "typeName": "Specification Type",
    ///         "detail": "Specification Detail"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated specification</response>
    /// <response code="404">If the specification is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPut("{specificationId:guid}", Name = "UpdateSpecification")]
    [ProducesResponseType(typeof(SpecificationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateSpecificationAsync(Guid specificationId, UpdateSpecificationDto updateSpecificationDto)
    {
        var specification = await specificationService.UpdateSpecificationAsync(specificationId, updateSpecificationDto);

        return Ok(specification);
    }

    /// <summary>
    /// Delete a specification
    /// </summary>
    /// <param name="specificationId"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the specification is not found</response>
    [HttpDelete("{specificationId:guid}", Name = "DeleteSpecification")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSpecificationAsync(Guid specificationId)
    {
        await specificationService.DeleteSpecificationAsync(specificationId);

        return NoContent();
    }
}
