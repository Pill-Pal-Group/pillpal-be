using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Dtos.PharmaceuticalCompanies;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class PharmaceuticalCompaniesController(IPharmaceuticalCompanyService pharmaceuticalCompanyService)
    : ControllerBase
{

    /// <summary>
    /// Get all pharmaceutical companies
    /// </summary>
    /// <response code="200">Returns a list of pharmaceutical companies</response>
    [HttpGet(Name = "GetPharmaceuticalCompanies")]
    [ProducesResponseType(typeof(IEnumerable<PharmaceuticalCompanyDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPharmaceuticalCompaniesAsync()
    {
        var pharmaceuticalCompanies = await pharmaceuticalCompanyService.GetPharmaceuticalCompaniesAsync();

        return Ok(pharmaceuticalCompanies);
    }

    /// <summary>
    /// Get a pharmaceutical company by id
    /// </summary>
    /// <param name="pharmaceuticalCompanyId"></param>
    /// <response code="200">Returns a pharmaceutical company</response>
    /// <response code="404">If the pharmaceutical company is not found</response>
    [HttpGet("{pharmaceuticalCompanyId:guid}", Name = "GetPharmaceuticalCompanyById")]
    [ProducesResponseType(typeof(PharmaceuticalCompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPharmaceuticalCompanyByIdAsync(Guid pharmaceuticalCompanyId)
    {
        var pharmaceuticalCompany = await pharmaceuticalCompanyService.GetPharmaceuticalCompanyByIdAsync(pharmaceuticalCompanyId);

        return Ok(pharmaceuticalCompany);
    }

    /// <summary>
    /// Create a pharmaceutical company
    /// </summary>
    /// <param name="createPharmaceuticalCompanyDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/pharmaceutical-companies
    ///     {
    ///         "companyName": "Pharmaceutical Company Name",
    ///         "nationId": "Nation Id"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created pharmaceutical company</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPost(Name = "CreatePharmaceuticalCompany")]
    [ProducesResponseType(typeof(PharmaceuticalCompanyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePharmaceuticalCompanyAsync(CreatePharmaceuticalCompanyDto createPharmaceuticalCompanyDto)
    {
        var pharmaceuticalCompany = await pharmaceuticalCompanyService.CreatePharmaceuticalCompanyAsync(createPharmaceuticalCompanyDto);

        return CreatedAtRoute("GetPharmaceuticalCompanyById", new { pharmaceuticalCompanyId = pharmaceuticalCompany.Id }, pharmaceuticalCompany);
    }

    /// <summary>
    /// Update a pharmaceutical company
    /// </summary>
    /// <param name="pharmaceuticalCompanyId"></param>
    /// <param name="updatePharmaceuticalCompanyDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/pharmaceutical-companies/{pharmaceuticalCompanyId}
    ///     {
    ///         "companyName": "Pharmaceutical Company Name",
    ///         "nationId": "Nation Id"
    ///     }
    ///    
    /// </remarks>
    /// <response code="200">Returns the updated pharmaceutical company</response>
    /// <response code="404">If the pharmaceutical company is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPut("{pharmaceuticalCompanyId:guid}", Name = "UpdatePharmaceuticalCompany")]
    [ProducesResponseType(typeof(PharmaceuticalCompanyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdatePharmaceuticalCompanyAsync(Guid pharmaceuticalCompanyId, UpdatePharmaceuticalCompanyDto updatePharmaceuticalCompanyDto)
    {
        var pharmaceuticalCompany = await pharmaceuticalCompanyService.UpdatePharmaceuticalCompanyAsync(pharmaceuticalCompanyId, updatePharmaceuticalCompanyDto);

        return Ok(pharmaceuticalCompany);
    }

    /// <summary>
    /// Delete a pharmaceutical company (soft delete)
    /// </summary>
    /// <param name="pharmaceuticalCompanyId"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the pharmaceutical company is not found</response>
    [HttpDelete("{pharmaceuticalCompanyId:guid}", Name = "DeletePharmaceuticalCompany")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePharmaceuticalCompanyAsync(Guid pharmaceuticalCompanyId)
    {
        await pharmaceuticalCompanyService.DeletePharmaceuticalCompanyAsync(pharmaceuticalCompanyId);

        return NoContent();
    }
}
