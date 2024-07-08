using PillPal.Application.Features.Nations;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class NationsController(INationService nationService)
    : ControllerBase
{
    /// <summary>
    /// Get all nations
    /// </summary>
    /// <response code="200">Returns a list of nations</response>
    [HttpGet(Name = "GetNations")]
    [ProducesResponseType(typeof(IEnumerable<NationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNationsAsync()
    {
        var nations = await nationService.GetNationsAsync();

        return Ok(nations);
    }

    /// <summary>
    /// Get a nation by id
    /// </summary>
    /// <param name="nationId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a nation</response>
    /// <response code="404">If the nation is not found</response>
    [HttpGet("{nationId:guid}", Name = "GetNationById")]
    [ProducesResponseType(typeof(NationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNationByIdAsync(Guid nationId)
    {
        var nation = await nationService.GetNationByIdAsync(nationId);

        return Ok(nation);
    }

    /// <summary>
    /// Create a nation
    /// </summary>
    /// <param name="createNationDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/nations
    ///     {
    ///         "nationName": "Nation Name"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created nation</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost(Name = "CreateNation")]
    [ProducesResponseType(typeof(NationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateNationAsync(CreateNationDto createNationDto)
    {
        var nation = await nationService.CreateNationAsync(createNationDto);

        return CreatedAtRoute("GetNationById", new { nationId = nation.Id }, nation);
    }

    /// <summary>
    /// Create multiple nations
    /// </summary>
    /// <param name="createNationDtos"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/nations/bulk
    ///     [
    ///         {
    ///             "nationName": "Nation Name 1"
    ///         },
    ///         {
    ///             "nationName": "Nation Name 2"
    ///         }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="201">Returns the created nations</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost("bulk", Name = "CreateBulkNations")]
    [ProducesResponseType(typeof(IEnumerable<NationDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBulkNationsAsync(IEnumerable<CreateNationDto> createNationDtos)
    {
        var nations = await nationService.CreateBulkNationsAsync(createNationDtos);

        return CreatedAtRoute("GetNations", nations);
    }

    /// <summary>
    /// Update a nation
    /// </summary>
    /// <param name="nationId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateNationDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    ///     
    ///     PUT /api/nations/{nationId}
    ///     {
    ///         "nationName": "Nation Name"
    ///      }
    ///      
    /// </remarks>
    /// <response code="200">Returns the updated nation</response>
    /// <response code="404">If the nation is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPut("{nationId:guid}", Name = "UpdateNation")]
    [ProducesResponseType(typeof(NationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateNationAsync(Guid nationId, UpdateNationDto updateNationDto)
    {
        var nation = await nationService.UpdateNationAsync(nationId, updateNationDto);

        return Ok(nation);
    }

    /// <summary>
    /// Delete a nation (soft delete)
    /// </summary>
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="nationId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the nation is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{nationId:guid}", Name = "DeleteNation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNationAsync(Guid nationId)
    {
        await nationService.DeleteNationAsync(nationId);

        return NoContent();
    }
}
