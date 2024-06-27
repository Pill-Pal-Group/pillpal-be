using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.MedicationTakes;
using PillPal.Core.Constant;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class MedicationIntakesController(IMedicationTakeService medicationTakeService)
    : ControllerBase
{
    /// <summary>
    /// Get Medication Takes from given Prescript
    /// </summary>
    /// <param name="prescriptId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="dateTake" example="2024-06-19"></param>
    /// <response code="200">Returns a list of Medication Takes</response>
    /// <response code="404">If the prescript is not found</response>
    [HttpGet("{prescriptId:guid}", Name = "GetMedicationTakes")]
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
    /// Create Medication Take from given Prescript
    /// </summary>
    /// <param name="prescriptId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="201">Returns the created Medication Take</response>
    /// <response code="400">If the Medication Take is already created</response>
    /// <response code="404">If the prescript is not found</response>
    [AuthorizeRoles(Role.Customer)]
    [HttpPost("{prescriptId:guid}", Name = "CreateMedicationTake")]
    [ProducesResponseType(typeof(IEnumerable<MedicationTakesDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateMedicationTakeAsync(Guid prescriptId)
    {
        var medicationTakes = await medicationTakeService.CreateMedicationTakeAsync(prescriptId);

        return CreatedAtRoute("GetMedicationTakes", new { prescriptId }, medicationTakes);
    }

    /// <summary>
    /// Removes a Medication Take by performing a soft delete.
    /// </summary>
    /// <param name="medicationTakeId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the Medication Take is not found</response>
    [AuthorizeRoles(Role.Customer)]
    [HttpDelete("{medicationTakeId:guid}", Name = "DeleteMedicationTake")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMedicationTakeAsync(Guid medicationTakeId)
    {
        await medicationTakeService.DeleteMedicationTakeAsync(medicationTakeId);

        return NoContent();
    }
}
