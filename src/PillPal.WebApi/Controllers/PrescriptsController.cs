using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.Prescripts;
using PillPal.Core.Constant;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PrescriptsController(IPrescriptService prescriptService)
    : ControllerBase
{
    /// <summary>
    /// Retrieves all prescripts.
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <param name="includeParameter"></param>
    /// <response code="200">Returns a list of prescripts</response>
    [HttpGet(Name = "GetPrescripts")]
    [ProducesResponseType(typeof(IEnumerable<PrescriptDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPrescriptsAsync(
        [FromQuery] PrescriptQueryParameter queryParameter,
        [FromQuery] PrescriptIncludeParameter includeParameter)
    {
        var prescripts = await prescriptService.GetPrescriptsAsync(queryParameter, includeParameter);

        return Ok(prescripts);
    }

    /// <summary>
    /// Retrieves a prescript by its unique identifier.
    /// </summary>
    /// <param name="prescriptId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a prescript</response>
    /// <response code="404">If the prescript is not found</response>
    [HttpGet("{prescriptId:guid}", Name = "GetPrescriptById")]
    [ProducesResponseType(typeof(PrescriptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPrescriptByIdAsync(Guid prescriptId)
    {
        var prescript = await prescriptService.GetPrescriptByIdAsync(prescriptId);

        return Ok(prescript);
    }

    /// <summary>
    /// Creates a new prescript.
    /// </summary>
    /// <param name="createPrescriptDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/prescripts
    ///     {
    ///         "prescriptImage": "https://monke.com/prescript-image.jpg",
    ///         "receptionDate": "2024-06-19",
    ///         "doctorName": "Dr. Doof",
    ///         "hospitalName": "General Hospital",
    ///         "prescriptDetails": [
    ///             {
    ///                 "medicineName": "Paracetamol",
    ///                 "dateStart": "2024-06-19",
    ///                 "dateEnd": "2024-06-29",
    ///                 "totalDose": 80,
    ///                 "morningDose": 2,
    ///                 "noonDose": 2,
    ///                 "afternoonDose": 2,
    ///                 "nightDose": 2,
    ///                 "dosageInstruction": "Aftermeal"
    ///             }
    ///          ]   
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created prescript</response>
    /// <response code="422">If the input data is invalid</response>
    [AuthorizeRoles(Role.Customer)]
    [HttpPost(Name = "CreatePrescript")]
    [ProducesResponseType(typeof(PrescriptDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePrescriptAsync([FromBody] CreatePrescriptDto createPrescriptDto)
    {
        var prescript = await prescriptService.CreatePrescriptAsync(createPrescriptDto);

        return CreatedAtRoute("GetPrescriptById", new { prescriptId = prescript.Id }, prescript);
    }
}
