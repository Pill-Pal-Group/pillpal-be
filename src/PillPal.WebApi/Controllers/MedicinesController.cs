﻿using PillPal.Application.Features.MedicineInBrands;
using PillPal.Application.Features.Medicines;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    [ProducesResponseType(typeof(PaginationResponse<MedicineDto>), StatusCodes.Status200OK)]
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
    /// <param name="medicineId" example="00000000-0000-0000-0000-000000000000"></param>
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
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    ///
    ///     POST /api/medicines
    ///     {
    ///         "specificationId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///         "categories": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "pharmaceuticalCompanies": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "dosageForms": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "activeIngredients": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "medicineName": "Sedanxio",
    ///         "requirePrescript": true,
    ///         "image": "https://monke.com/sedanxio-image.jpg",
    ///         "registrationNumber": "VN-17384-13"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the created medicine</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost(Name = "CreateMedicine")]
    [ProducesResponseType(typeof(MedicineDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateMedicineAsync(CreateMedicineDto createMedicineDto)
    {
        var medicine = await medicineService.CreateMedicineAsync(createMedicineDto);

        return CreatedAtRoute("GetMedicineById", new { medicineId = medicine.Id }, medicine);
    }

    /// <summary>
    /// Create a medicine with full information
    /// </summary>
    /// <param name="createFullMedicineDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/medicines/full
    ///     {
    ///         "specificationId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///         "categories": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "pharmaceuticalCompanies": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "dosageForms": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "activeIngredients": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "medicineName": "Sedanxio",
    ///         "requirePrescript": true,
    ///         "image": "https://monke.com/sedanxio-image.jpg",
    ///         "registrationNumber": "VN-17384-13",
    ///         "medicineInBrands": [
    ///             {
    ///                 "brandId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///                 "price": 8000,
    ///                 "priceUnit": "VND",
    ///                 "medicineUrl": "https://monke.com/sedanxio.jpg"
    ///             }
    ///          ]
    ///      }
    ///      
    /// </remarks>
    /// <response code="201">Returns the created medicine</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost("full", Name = "CreateFullMedicine")]
    [ProducesResponseType(typeof(MedicineDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateFullMedicineAsync(CreateFullMedicineDto createFullMedicineDto)
    {
        var medicine = await medicineService.CreateFullMedicineAsync(createFullMedicineDto);

        return CreatedAtRoute("GetMedicineById", new { medicineId = medicine.Id }, medicine);
    }

    /// <summary>
    /// Get medicine price units
    /// </summary>
    /// <response code="200">Returns the list of medicine price units</response>
    [HttpGet("price-units", Name = "GetMedicinePriceUnits")]
    [ProducesResponseType(typeof(MedicinePriceUnitsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMedicinePriceUnitsAsync()
    {
        var priceUnits = await medicineService.GetMedicinePriceUnitsAsync();

        return Ok(priceUnits);
    }

    /// <summary>
    /// Add a medicine to a brand with price
    /// </summary>
    /// <param name="medicineId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="createMedicineInBrandDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    ///
    ///     POST /api/medicines/{medicineId}/brands
    ///     {
    ///         "brandId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///         "price": 8000,
    ///         "priceUnit": "VND",
    ///         "medicineUrl": "https://monke.com/paracetamol"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Returns the created medicine in brand</response>
    /// <response code="404">If the medicine id is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost("{medicineId:guid}/brands", Name = "CreateMedicineInBrand")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateMedicineInBrandAsync(Guid medicineId,
        CreateMedicineInBrandsDto createMedicineInBrandDto)
    {
        await medicineService.CreateMedicineInBrandAsync(medicineId, createMedicineInBrandDto);

        return Created();
    }

    /// <summary>
    /// Update a medicine
    /// </summary>
    /// <param name="medicineId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateMedicineDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    ///
    ///     PUT /api/medicines/{medicineId}
    ///     {
    ///         "specificationId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///         "categories": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "pharmaceuticalCompanies": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "dosageForms": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "activeIngredients": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "medicineName": "Sedanxio",
    ///         "requirePrescript": true,
    ///         "image": "https://monke.com/sedanxio-image.jpg"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Returns the updated medicine</response>
    /// <response code="404">If the medicine is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
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
    /// Update a medicine with full information
    /// </summary>
    /// <param name="medicineId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateFullMedicineDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     PUT /api/medicines/full/{medicineId}
    ///     {
    ///         "specificationId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///         "categories": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "pharmaceuticalCompanies": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "dosageForms": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "activeIngredients": [
    ///             "6b4aadb0-8189-467a-8aba-6572d3d4b972"
    ///         ],
    ///         "medicineName": "Sedanxio",
    ///         "requirePrescript": true,
    ///         "image": "https://monke.com/sedanxio-image.jpg",
    ///         "medicineInBrands": [
    ///             {
    ///                 "brandId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///                 "price": 8000,
    ///                 "priceUnit": "VND",
    ///                 "medicineUrl": "https://monke.com/sedanxio.jpg"
    ///             }
    ///          ]
    ///      }
    ///      
    /// </remarks>
    /// <response code="200">Returns the updated medicine</response>
    /// <response code="404">If the medicine is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPut("full/{medicineId:guid}", Name = "UpdateFullMedicine")]
    [ProducesResponseType(typeof(MedicineDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateFullMedicineAsync(Guid medicineId, UpdateFullMedicineDto updateFullMedicineDto)
    {
        var medicine = await medicineService.UpdateFullMedicineAsync(medicineId, updateFullMedicineDto);

        return Ok(medicine);
    }

    /// <summary>
    /// Update a medicine in a brand
    /// </summary>
    /// <param name="medicineId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateMedicineInBrandDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    ///
    ///     PUT /api/medicines/{medicineId}/brands
    ///     {
    ///         "brandId": "6b4aadb0-8189-467a-8aba-6572d3d4b972",
    ///         "price": 8000,
    ///         "priceUnit": "VND",
    ///         "medicineUrl": "https://monke.com/paracetamol"
    ///     }
    ///
    /// </remarks>
    /// <response code="204">No content</response>
    /// <response code="404">If the medicine is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPut("{medicineId:guid}/brands", Name = "UpdateMedicineInBrand")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateMedicineInBrandAsync(Guid medicineId,
        UpdateMedicineInBrandsDto updateMedicineInBrandDto)
    {
        await medicineService.UpdateMedicineInBrandAsync(medicineId, updateMedicineInBrandDto);

        return NoContent();
    }

    /// <summary>
    /// Delete a medicine (soft delete)
    /// </summary>
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="medicineId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the medicine is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{medicineId:guid}", Name = "DeleteMedicine")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMedicineAsync(Guid medicineId)
    {
        await medicineService.DeleteMedicineAsync(medicineId);

        return NoContent();
    }

    /// <summary>
    /// Delete a medicine in a brand (soft delete)
    /// </summary>
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="medicineId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="brandId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the medicine is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{medicineId:guid}/brands/{brandId:guid}", Name = "DeleteMedicineInBrand")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMedicineInBrandAsync(Guid medicineId, Guid brandId)
    {
        await medicineService.DeleteMedicineInBrandAsync(medicineId, brandId);

        return NoContent();
    }

    /// <summary>
    /// Import medicines from excel file
    /// </summary>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Excel file format should be as follows:
    /// | Property Name          | Default Header Name in Excel File |
    /// |------------------------|-----------------------------------|
    /// | MedicineName           | Product Name                      |
    /// | Image                  | Image-src                         |
    /// | MedicineUrl            | Link                              |
    /// | Price                  | Price                             |
    /// | Categories             | Category (delimiter: /)           |
    /// | DosageForms            | Dosage forms                      |
    /// | Specifications         | Specifications                    |
    /// | ActiveIngredients      | Ingredient (delimiter: ,)         |
    /// | PharmaceuticalCompanies| Pharmaceuticals                   |
    /// | Nation                 | Nation                            |
    /// | RegistrationNumber     | Registration number               |
    /// | RequirePrescript       | Medication requires prescription  |
    /// | BrandUrl               | Brand Url                         |  
    /// | Brand                  | Brand                             |
    /// | BrandLogo              | Brand Logo                        |
    /// Incase of different naming in excel file,
    /// properties and corresponding delimiters (if any) can be specified in query parameters
    /// </remarks>
    /// <param name="file">The excel file to import</param>
    /// <param name="excelProperties">The properties header name in excel file</param>
    /// <param name="propertyDelimiter">The delimiter of some props in excel file</param>
    /// <response code="200">Returns the file execution result</response>
    /// <response code="400">If the file format is invalid</response>
    [Authorize(Policy.Administrative)]
    [Consumes("multipart/form-data")]
    [HttpPost("import", Name = "ImportMedicines")]
    [ProducesResponseType(typeof(FileExecutionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ImportMedicinesAsync([Required] IFormFile file,
        [FromQuery] MedicineExcelProperties excelProperties,
        [FromQuery] ExcelPropertyDelimiters propertyDelimiter)
    {
        var extension = Path.GetExtension(file.FileName);

        if (extension != ".xlsx")
        {
            return BadRequest("Invalid file format");
        }

        await using var stream = file.OpenReadStream();

        var importedCount = await medicineService.ImportMedicinesAsync(stream, excelProperties, propertyDelimiter);

        return Ok(importedCount);
    }
}
