using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.Medicines;

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
    /// <response code="200">Returns a list of medicines</response>
    [HttpGet(Name = "GetMedicines")]
    [ProducesResponseType(typeof(IEnumerable<MedicineDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMedicinesAsync()
    {
        var medicines = await medicineService.GetMedicinesAsync();

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
    /// <response code="201">Returns the created medicine</response>
    /// <response code="422">If the resource is not found</response>
    [HttpPost(Name = "CreateMedicine")]
    [ProducesResponseType(typeof(MedicineDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateMedicineAsync(CreateMedicineDto createMedicineDto)
    {
        var medicine = await medicineService.CreateMedicineAsync(createMedicineDto);

        return CreatedAtRoute("GetMedicineById", new { medicineId = medicine.Id }, medicine);
    }
}
