using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Nations;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    /// <param name="nationId"></param>
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
    /// Sample request:
    /// 
    ///     POST /api/nations
    ///     {
    ///         "nationCode": "Nation Code",
    ///         "nationName": "Nation Name"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created nation</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPost(Name = "CreateNation")]
    [ProducesResponseType(typeof(NationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateNationAsync(CreateNationDto createNationDto)
    {
        var nation = await nationService.CreateNationAsync(createNationDto);

        return CreatedAtRoute("GetNationById", new { nationId = nation.Id }, nation);
    }

    /// <summary>
    /// Update a nation
    /// </summary>
    /// <param name="nationId"></param>
    /// <param name="updateNationDto"></param>
    /// <remarks>
    /// Sample request:
    ///     
    ///     PUT /api/nations/{nationId}
    ///     {
    ///         "nationCode": "Nation Code",
    ///         "nationName": "Nation Name"
    ///      }
    ///      
    /// </remarks>
    /// <response code="200">Returns the updated nation</response>
    /// <response code="404">If the nation is not found</response>
    /// <response code="422">If the input data is invalid</response>
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
    /// <param name="nationId"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the nation is not found</response>
    [HttpDelete("{nationId:guid}", Name = "DeleteNation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNationAsync(Guid nationId)
    {
        await nationService.DeleteNationAsync(nationId);

        return NoContent();
    }
}
