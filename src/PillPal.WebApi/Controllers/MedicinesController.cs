using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.Medicines;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class MedicinesController(IMedicineService medicineService)
    : ControllerBase
{
    /// <summary>
    /// Get all medicines
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <param name="includeParameter"></param>
    /// <response code="200">Returns a list of medicines</response>
    [HttpGet(Name = "GetMedicines")]
    [ProducesResponseType(typeof(IEnumerable<MedicineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMedicinesAsync(
        [FromQuery] MedicineQueryParameter queryParameter,
        [FromQuery] MedicineIncludeParameter includeParameter)
    {
        var medicines = await medicineService.GetMedicinesAsync(queryParameter, includeParameter);

        return Ok(medicines);
    }

    /// <summary>
    /// Get a medicine by id
    /// </summary>
    /// <param name="medicineId"></param>
    /// <response code="200">Returns a medicine</response>
    /// <response code="404">If the medicine is not found</response>
    [HttpGet("{medicineId:guid}", Name = "GetMedicineById")]
    [ProducesResponseType(typeof(MedicineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMedicineByIdAsync(Guid medicineId)
    {
        var medicine = await medicineService.GetMedicineByIdAsync(medicineId);

        return Ok(medicine);
    }

    /// <summary>
    /// Create a medicine
    /// </summary>
    /// <param name="createMedicineDto"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/medicines
    ///     {
    ///         "medicineName": "Medicine Name",
    ///         "requirePrescript": true,
    ///         "image": "Image Url",
    ///         "specificationId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///         "pharmaceuticalCompanys": ["6b4aadb0-8189-467a-8aba-6572d3d4b972"],
    ///         "dosageForms": ["6b4aadb0-8189-467a-8aba-6572d3d4b972"],
    ///         "activeIngredients": ["6b4aadb0-8189-467a-8aba-6572d3d4b972"],
    ///         "brands": ["6b4aadb0-8189-467a-8aba-6572d3d4b972"]
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the created medicine</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPost(Name = "CreateMedicine")]
    [ProducesResponseType(typeof(MedicineDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateMedicineAsync(CreateMedicineDto createMedicineDto)
    {
        var medicine = await medicineService.CreateMedicineAsync(createMedicineDto);

        return CreatedAtRoute("GetMedicineById", new { medicineId = medicine.Id }, medicine);
    }

    /// <summary>
    /// Update a medicine
    /// </summary>
    /// <param name="medicineId"></param>
    /// <param name="updateMedicineDto"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /api/medicines/{medicineId}
    ///     {
    ///         "medicineName": "Medicine Name",
    ///         "requirePrescript": true,
    ///         "image": "Image Url",
    ///         "specificationId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///         "pharmaceuticalCompanys": ["6b4aadb0-8189-467a-8aba-6572d3d4b972"],
    ///         "dosageForms": ["6b4aadb0-8189-467a-8aba-6572d3d4b972"],
    ///         "activeIngredients": ["6b4aadb0-8189-467a-8aba-6572d3d4b972"],
    ///         "brands": ["6b4aadb0-8189-467a-8aba-6572d3d4b972"]
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the updated medicine</response>
    /// <response code="404">If the medicine is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPut("{medicineId:guid}", Name = "UpdateMedicine")]
    [ProducesResponseType(typeof(MedicineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateMedicineAsync(Guid medicineId, UpdateMedicineDto updateMedicineDto)
    {
        var medicine = await medicineService.UpdateMedicineAsync(medicineId, updateMedicineDto);

        return Ok(medicine);
    }

    /// <summary>
    /// Delete a medicine (soft delete)
    /// </summary>
    /// <param name="medicineId"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the medicine is not found</response>
    [HttpDelete("{medicineId:guid}", Name = "DeleteMedicine")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMedicineAsync(Guid medicineId)
    {
        await medicineService.DeleteMedicineAsync(medicineId);

        return NoContent();
    }
}
