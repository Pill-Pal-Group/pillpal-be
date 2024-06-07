using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.DosageForms;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class DosageFormsController(IDosageFormService dosageFormService)
    : ControllerBase
{

    /// <summary>
    /// Get all dosage forms
    /// </summary>
    /// <response code="200">Returns a list of dosage forms</response>
    [HttpGet(Name = "GetDosageForms")]
    [ProducesResponseType(typeof(IEnumerable<DosageFormDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDosageFormsAsync()
    {
        var dosageForms = await dosageFormService.GetDosageFormsAsync();

        return Ok(dosageForms);
    }

    /// <summary>
    /// Get a dosage form by id
    /// </summary>
    /// <param name="dosageFormId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a dosage form</response>
    /// <response code="404">If the dosage form is not found</response>
    [HttpGet("{dosageFormId:guid}", Name = "GetDosageFormById")]
    [ProducesResponseType(typeof(DosageFormDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDosageFormByIdAsync(Guid dosageFormId)
    {
        var dosageForm = await dosageFormService.GetDosageFormByIdAsync(dosageFormId);

        return Ok(dosageForm);
    }

    /// <summary>
    /// Create a dosage form
    /// </summary>
    /// <param name="createDosageFormDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/dosage-forms
    ///     {
    ///         "formName": "Dosage Form Name"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created dosage form</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPost(Name = "CreateDosageForm")]
    [ProducesResponseType(typeof(DosageFormDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateDosageFormAsync(CreateDosageFormDto createDosageFormDto)
    {
        var dosageForm = await dosageFormService.CreateDosageFormAsync(createDosageFormDto);

        return CreatedAtRoute("GetDosageFormById", new { dosageFormId = dosageForm.Id }, dosageForm);
    }

    /// <summary>
    /// Update a dosage form
    /// </summary>
    /// <param name="dosageFormId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateDosageFormDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/dosage-forms/{dosageFormId}
    ///     {
    ///         "formName": "Updated Dosage Form Name"
    ///     }
    ///    
    /// </remarks>
    /// <response code="200">Returns the updated dosage form</response>
    /// <response code="404">If the dosage form is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPut("{dosageFormId:guid}", Name = "UpdateDosageForm")]
    [ProducesResponseType(typeof(DosageFormDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateDosageFormAsync(Guid dosageFormId, UpdateDosageFormDto updateDosageFormDto)
    {
        var dosageForm = await dosageFormService.UpdateDosageFormAsync(dosageFormId, updateDosageFormDto);

        return Ok(dosageForm);
    }

    /// <summary>
    /// Delete a dosage form
    /// </summary>
    /// <param name="dosageFormId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the dosage form is not found</response>
    [HttpDelete("{dosageFormId:guid}", Name = "DeleteDosageForm")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDosageFormAsync(Guid dosageFormId)
    {
        await dosageFormService.DeleteDosageFormAsync(dosageFormId);

        return NoContent();
    }
}
