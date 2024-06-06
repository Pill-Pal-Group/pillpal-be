using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Services;
using PillPal.Application.Features.ActiveIngredients;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class ActiveIngredientsController(IActiveIngredientService activeIngredientService)
    : ControllerBase
{
    /// <summary>
    /// Get all active ingredients
    /// </summary>
    /// <param name="queryParameter"></param>
    /// <response code="200">Returns a list of active ingredients</response>
    [HttpGet(Name = "GetActiveIngredients")]
    [ProducesResponseType(typeof(IEnumerable<ActiveIngredientDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActiveIngredientsAsync([FromQuery] ActiveIngredientQueryParameter queryParameter)
    {
        var activeIngredients = await activeIngredientService.GetActiveIngredientsAsync(queryParameter);

        return Ok(activeIngredients);
    }

    /// <summary>
    /// Get an active ingredient by id
    /// </summary>
    /// <param name="activeIngredientId"></param>
    /// <response code="200">Returns an active ingredient</response>
    /// <response code="404">If the active ingredient is not found</response>
    [HttpGet("{activeIngredientId:guid}", Name = "GetActiveIngredientById")]
    [ProducesResponseType(typeof(ActiveIngredientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetActiveIngredientByIdAsync(Guid activeIngredientId)
    {
        var activeIngredient = await activeIngredientService.GetActiveIngredientByIdAsync(activeIngredientId);

        return Ok(activeIngredient);
    }

    /// <summary>
    /// Create an active ingredient
    /// </summary>
    /// <param name="createActiveIngredientDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/active-ingredients
    ///     {
    ///         "ingredientName": "Ingredient Name",
    ///         "ingredientInformation": "Ingredient Information"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created active ingredient</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPost(Name = "CreateActiveIngredient")]
    [ProducesResponseType(typeof(ActiveIngredientDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateActiveIngredientAsync(CreateActiveIngredientDto createActiveIngredientDto)
    {
        var activeIngredient = await activeIngredientService.CreateActiveIngredientAsync(createActiveIngredientDto);

        return CreatedAtRoute("GetActiveIngredientById", new { activeIngredientId = activeIngredient.Id }, activeIngredient);
    }

    /// <summary>
    /// Update an active ingredient
    /// </summary>
    /// <param name="activeIngredientId"></param>
    /// <param name="updateActiveIngredientDto"></param>
    /// <remarks>
    /// Sample request:
    ///     
    ///     PUT /api/active-ingredients/{activeIngredientId}
    ///     {
    ///         "ingredientName": "Ingredient Name",
    ///         "ingredientInformation": "Ingredient Information"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated active ingredient</response>
    /// <response code="404">If the active ingredient is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [HttpPut("{activeIngredientId:guid}", Name = "UpdateActiveIngredient")]
    [ProducesResponseType(typeof(ActiveIngredientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateActiveIngredientAsync(Guid activeIngredientId, UpdateActiveIngredientDto updateActiveIngredientDto)
    {
        var activeIngredient = await activeIngredientService.UpdateActiveIngredientAsync(activeIngredientId, updateActiveIngredientDto);

        return Ok(activeIngredient);
    }

    /// <summary>
    /// Delete an active ingredient (soft delete)
    /// </summary>
    /// <param name="activeIngredientId"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the active ingredient is not found</response>
    [HttpDelete("{activeIngredientId:guid}", Name = "DeleteActiveIngredient")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteActiveIngredientAsync(Guid activeIngredientId)
    {
        await activeIngredientService.DeleteActiveIngredientAsync(activeIngredientId);

        return NoContent();
    }
}
