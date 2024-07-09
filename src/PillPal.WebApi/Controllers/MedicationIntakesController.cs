using PillPal.Application.Features.MedicationTakes;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class MedicationIntakesController(IMedicationTakeService medicationTakeService)
    : ControllerBase
{
    /// <summary>
    /// Get a list of Medication Takes from a given Prescript.
    /// </summary>
    /// <param name="prescriptId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="dateTake" example="2024-06-19"></param>
    /// <remarks>Requires customer policy</remarks>
    /// <response code="200">Returns a list of Medication Takes</response>
    /// <response code="404">If the prescript is not found</response>
    [Authorize(Policy.Customer)]
    [HttpGet("prescripts/{prescriptId:guid}", Name = "GetMedicationTakes")]
    [ProducesResponseType(typeof(IEnumerable<MedicationTakesListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMedicationTakesAsync(
        Guid prescriptId,
        [FromQuery] DateTimeOffset? dateTake)
    {
        var medicationTakes = await medicationTakeService.GetMedicationTakesAsync(prescriptId, dateTake);

        return Ok(medicationTakes);
    }

    /// <summary>
    /// Get an individual Medication Take.
    /// </summary>
    /// <param name="medicationTakeId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <remarks>Requires customer policy</remarks>
    /// <response code="200">Returns the Medication Take</response>
    /// <response code="404">If the Medication Take is not found</response>
    [Authorize(Policy.Customer)]
    [HttpGet("{medicationTakeId:guid}", Name = "GetIndividualMedicationTakes")]
    [ProducesResponseType(typeof(MedicationTakesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetIndividualMedicationTakesAsync(Guid medicationTakeId)
    {
        var medicationTakes = await medicationTakeService.GetIndividualMedicationTakesAsync(medicationTakeId);

        return Ok(medicationTakes);
    }

    /// <summary>
    /// Create Medication Take from given Prescript
    /// </summary>
    /// <remarks>Requires customer policy</remarks>
    /// <param name="prescriptId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="201">Returns the created Medication Take</response>
    /// <response code="400">If the Medication Take is already created</response>
    /// <response code="404">If the prescript is not found</response>
    [Authorize(Policy.Customer)]
    [HttpPost("prescripts/{prescriptId:guid}", Name = "CreateMedicationTake")]
    [ProducesResponseType(typeof(IEnumerable<MedicationTakesDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateMedicationTakeAsync(Guid prescriptId)
    {
        var medicationTakes = await medicationTakeService.CreateMedicationTakeAsync(prescriptId);

        return CreatedAtRoute("GetMedicationTakes", new { prescriptId }, medicationTakes);
    }

    /// <summary>
    /// Manually create a Medication Take.
    /// </summary>
    /// <remarks>Requires customer policy</remarks>
    /// <param name="createMedicationTakesDto">The Medication Take to create.</param>
    /// <remarks>
    /// Requires customer policy
    /// 
    /// Sample request:
    /// 
    ///    POST /api/medication-intakes
    ///    {
    ///         "dateTake": "2024-06-19",
    ///         "timeTake": "08:00",
    ///         "dose": 2,
    ///         "prescriptDetailId": "00000000-0000-0000-0000-000000000000"
    ///    }
    ///    
    /// </remarks>
    /// <response code="201">Returns the created Medication Take</response>
    /// <response code="404">If the prescript detail is not found</response>
    /// <response code="422">If the Medication Take is invalid</response>
    [Authorize(Policy.Customer)]
    [HttpPost(Name = "ManualCreateMedicationTake")]
    [ProducesResponseType(typeof(MedicationTakesDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateMedicationTakeAsync(
        [FromBody] CreateMedicationTakesDto createMedicationTakesDto)
    {
        var medicationTake = await medicationTakeService.CreateMedicationTakeAsync(createMedicationTakesDto);

        return CreatedAtRoute("GetIndividualMedicationTakes", new { medicationTakeId = medicationTake.Id }, medicationTake);
    }

    /// <summary>
    /// Removes a Medication Take by performing a soft delete.
    /// </summary>
    /// <remarks>Requires customer policy</remarks>
    /// <param name="medicationTakeId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the Medication Take is not found</response>
    [Authorize(Policy.Customer)]
    [HttpDelete("{medicationTakeId:guid}", Name = "DeleteMedicationTake")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMedicationTakeAsync(Guid medicationTakeId)
    {
        await medicationTakeService.DeleteMedicationTakeAsync(medicationTakeId);

        return NoContent();
    }
}
