using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.TermsOfServices;
using PillPal.Core.Constant;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class TermsOfServicesController(ITermsOfService termsOfService)
    : ControllerBase
{
    /// <summary>
    /// Get all terms of services
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <response code="200">Returns a list of terms of services</response>
    [HttpGet(Name = "GetTermsOfServices")]
    [ProducesResponseType(typeof(IEnumerable<TermsOfServiceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTermsOfServicesAsync(
        [FromQuery] TermsOfServiceQueryParameter queryParameter)
    {
        var response = await termsOfService.GetTermsOfServicesAsync(queryParameter);

        return Ok(response);
    }

    /// <summary>
    /// Get a terms of service by id
    /// </summary>
    /// <param name="termsOfServiceId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a terms of service</response>
    /// <response code="404">If the terms of service is not found</response>
    [HttpGet("{termsOfServiceId:guid}", Name = "GetTermsOfServiceById")]
    [ProducesResponseType(typeof(TermsOfServiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTermsOfServiceByIdAsync(Guid termsOfServiceId)
    {
        var response = await termsOfService.GetTermsOfServiceByIdAsync(termsOfServiceId);

        return Ok(response);
    }

    /// <summary>
    /// Create a terms of service
    /// </summary>
    /// <param name="createTermsOfServiceDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/terms-of-services
    ///     {
    ///         "title": "Security Policy",
    ///         "content": "Content of the security policy."
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created terms of service</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost(Name = "CreateTermsOfService")]
    [ProducesResponseType(typeof(TermsOfServiceDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateTermsOfServiceAsync(CreateTermsOfServiceDto createTermsOfServiceDto)
    {
        var response = await termsOfService.CreateTermsOfServiceAsync(createTermsOfServiceDto);

        return CreatedAtRoute("GetTermsOfServiceById", new { termsOfServiceId = response.Id }, response);
    }

    /// <summary>
    /// Create bulk terms of services
    /// </summary>
    /// <param name="createTermsOfServiceDtos"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/terms-of-services/bulk
    ///     [
    ///         {
    ///             "title": "Security Policy",
    ///             "content": "Content of the security policy."
    ///         },
    ///         {
    ///             "title": "Privacy Policy",
    ///             "content": "Content of the privacy policy."
    ///         }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="201">Returns the created terms of services</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost("bulk", Name = "CreateBulkTermsOfService")]
    [ProducesResponseType(typeof(IEnumerable<TermsOfServiceDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBulkTermsOfServiceAsync(IEnumerable<CreateTermsOfServiceDto> createTermsOfServiceDtos)
    {
        var response = await termsOfService.CreateBulkTermsOfServiceAsync(createTermsOfServiceDtos);

        return CreatedAtRoute("GetTermsOfServices", response);
    }

    /// <summary>
    /// Update a terms of service
    /// </summary>
    /// <param name="termsOfServiceId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateTermsOfServiceDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/terms-of-services/{termsOfServiceId}
    ///     {
    ///         "title": "Security Policy",
    ///         "content": "Content of the security policy."
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated terms of service</response>
    /// <response code="404">If the terms of service is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPut("{termsOfServiceId:guid}", Name = "UpdateTermsOfService")]
    [ProducesResponseType(typeof(TermsOfServiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateTermsOfServiceAsync(Guid termsOfServiceId, UpdateTermsOfServiceDto updateTermsOfServiceDto)
    {
        var response = await termsOfService.UpdateTermsOfServiceAsync(termsOfServiceId, updateTermsOfServiceDto);

        return Ok(response);
    }

    /// <summary>
    /// Delete a terms of service
    /// </summary>
    /// <param name="termsOfServiceId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the terms of service is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{termsOfServiceId:guid}", Name = "DeleteTermsOfService")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTermsOfServiceAsync(Guid termsOfServiceId)
    {
        await termsOfService.DeleteTermsOfServiceAsync(termsOfServiceId);

        return NoContent();
    }
}
