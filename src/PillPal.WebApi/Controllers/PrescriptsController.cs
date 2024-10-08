﻿using PillPal.Application.Features.PrescriptDetails;
using PillPal.Application.Features.Prescripts;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PrescriptsController(IPrescriptService prescriptService)
    : ControllerBase
{
    /// <summary>
    /// Retrieves all prescripts
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <param name="includeParameter"></param>
    /// <remarks>Requires customer policy</remarks>
    /// <response code="200">Returns a list of prescripts</response>
    [Authorize(Policy.Customer)]
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
    /// Retrieves a prescript by its unique identifier
    /// </summary>
    /// <param name="prescriptId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <remarks>Requires customer policy</remarks>
    /// <response code="200">Returns a prescript</response>
    /// <response code="404">If the prescript is not found</response>
    [Authorize(Policy.Customer)]
    [HttpGet("{prescriptId:guid}", Name = "GetPrescriptById")]
    [ProducesResponseType(typeof(PrescriptDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPrescriptByIdAsync(Guid prescriptId)
    {
        var prescript = await prescriptService.GetPrescriptByIdAsync(prescriptId);

        return Ok(prescript);
    }

    /// <summary>
    /// Creates a new prescript
    /// </summary>
    /// <param name="createPrescriptDto"></param>
    /// <remarks>
    /// Requires customer policy
    /// 
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
    ///                 "medicineImage": "https://monke.com/med-image.jpg",
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
    [Authorize(Policy.Customer)]
    [HttpPost(Name = "CreatePrescript")]
    [ProducesResponseType(typeof(PrescriptDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePrescriptAsync(CreatePrescriptDto createPrescriptDto)
    {
        var prescript = await prescriptService.CreatePrescriptAsync(createPrescriptDto);

        return CreatedAtRoute("GetPrescriptById", new { prescriptId = prescript.Id }, prescript);
    }

    /// <summary>
    /// Updates medicine image of a prescript detail
    /// </summary>
    /// <param name="prescriptDetailId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updatePrescriptDetailImageDto"></param>
    /// <remarks>
    /// Requires customer policy
    ///
    /// Sample request:
    /// 
    ///     PUT /api/prescripts/prescript-details/00000000-0000-0000-0000-000000000000/image
    ///     {
    ///         "medicineImage": "https://monke.com/med-image.jpg"
    ///     }
    ///     
    /// </remarks>
    /// <response code="204">If the prescript detail image is updated successfully</response>
    /// <response code="404">If the prescript detail is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Customer)]
    [HttpPut("prescript-details/{prescriptDetailId:guid}/image", Name = "UpdatePrescriptDetailImage")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdatePrescriptDetailImageAsync(Guid prescriptDetailId, UpdatePrescriptDetailImageDto updatePrescriptDetailImageDto)
    {
        await prescriptService.UpdatePrescriptDetailImageAsync(prescriptDetailId, updatePrescriptDetailImageDto);

        return NoContent();
    }

    /// <summary>
    /// Deletes a prescript by its unique identifier (soft delete)
    /// </summary>
    /// <remarks>Requires customer policy</remarks>
    /// <param name="prescriptId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the prescript is not found</response>
    [Authorize(Policy.Customer)]
    [HttpDelete("{prescriptId:guid}", Name = "DeletePrescriptById")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePrescriptByIdAsync(Guid prescriptId)
    {
        await prescriptService.DeletePrescriptByIdAsync(prescriptId);

        return NoContent();
    }
}
