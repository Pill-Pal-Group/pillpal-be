using PillPal.Application.Features.Specifications;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var specifications = await specificationService.GetSpecificationsAsync();

        return Ok(specifications);
    }

    /// <summary>
    /// Get a specification by id
    /// </summary>
    /// <param name="specificationId" example="00000000-0000-0000-0000-000000000000"></param>
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
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/specifications
    ///     {
    ///         "typeName": "Specification Type"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created specification</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost(Name = "CreateSpecification")]
    [ProducesResponseType(typeof(SpecificationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateSpecificationAsync(CreateSpecificationDto createSpecificationDto)
    {
        var specification = await specificationService.CreateSpecificationAsync(createSpecificationDto);

        return CreatedAtRoute("GetSpecificationById", new { specificationId = specification.Id }, specification);
    }

    /// <summary>
    /// Create multiple specifications
    /// </summary>
    /// <param name="createSpecificationDtos"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/specifications/bulk
    ///     [
    ///         {
    ///             "typeName": "Specification Type 1"
    ///         },
    ///         {
    ///             "typeName": "Specification Type 2"
    ///         }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="201">Returns the created specifications</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost("bulk", Name = "CreateBulkSpecifications")]
    [ProducesResponseType(typeof(IEnumerable<SpecificationDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBulkSpecificationsAsync(IEnumerable<CreateSpecificationDto> createSpecificationDtos)
    {
        var specifications = await specificationService.CreateBulkSpecificationsAsync(createSpecificationDtos);

        return CreatedAtRoute("GetSpecifications", specifications);
    }

    /// <summary>
    /// Update a specification
    /// </summary>
    /// <param name="specificationId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateSpecificationDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     PUT /api/specifications/{specificationId}
    ///     {
    ///         "typeName": "Specification Type"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated specification</response>
    /// <response code="404">If the specification is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
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
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="specificationId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the specification is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{specificationId:guid}", Name = "DeleteSpecification")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSpecificationAsync(Guid specificationId)
    {
        await specificationService.DeleteSpecificationAsync(specificationId);

        return NoContent();
    }
}
