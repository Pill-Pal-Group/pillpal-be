using PillPal.Application.Common.Paginations;
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
    /// <param name="queryParameter"></param>
    /// <response code="200">Returns a list of dosage forms</response>
    [HttpGet(Name = "GetDosageForms")]
    [ProducesResponseType(typeof(PaginationResponse<DosageFormDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDosageFormsAsync([FromQuery] DosageFormQueryParameter queryParameter)
    {
        var dosageForms = await dosageFormService.GetDosageFormsAsync(queryParameter);

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
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
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
    [Authorize(Policy.Administrative)]
    [HttpPost(Name = "CreateDosageForm")]
    [ProducesResponseType(typeof(DosageFormDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateDosageFormAsync(CreateDosageFormDto createDosageFormDto)
    {
        var dosageForm = await dosageFormService.CreateDosageFormAsync(createDosageFormDto);

        return CreatedAtRoute("GetDosageFormById", new { dosageFormId = dosageForm.Id }, dosageForm);
    }

    /// <summary>
    /// Create multiple dosage forms
    /// </summary>
    /// <param name="createDosageFormDtos"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/dosage-forms/bulk
    ///     [
    ///         {
    ///             "formName": "Dosage Form Name 1"
    ///         },
    ///         {
    ///             "formName": "Dosage Form Name 2"
    ///         }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="201">Returns the created dosage forms</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost("bulk", Name = "CreateBulkDosageForms")]
    [ProducesResponseType(typeof(IEnumerable<DosageFormDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBulkDosageFormsAsync(IEnumerable<CreateDosageFormDto> createDosageFormDtos)
    {
        var dosageForms = await dosageFormService.CreateBulkDosageFormsAsync(createDosageFormDtos);

        return CreatedAtRoute("GetDosageForms", dosageForms);
    }

    /// <summary>
    /// Update a dosage form
    /// </summary>
    /// <param name="dosageFormId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateDosageFormDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
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
    [Authorize(Policy.Administrative)]
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
    /// Delete a dosage form (soft delete)
    /// </summary>
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="dosageFormId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the dosage form is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{dosageFormId:guid}", Name = "DeleteDosageForm")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDosageFormAsync(Guid dosageFormId)
    {
        await dosageFormService.DeleteDosageFormAsync(dosageFormId);

        return NoContent();
    }
}
