using PillPal.Application.Features.ActiveIngredients;
using PillPal.Application.Common.Paginations;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    [ProducesResponseType(typeof(PaginationResponse<ActiveIngredientDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActiveIngredientsAsync([FromQuery] ActiveIngredientQueryParameter queryParameter)
    {
        var activeIngredients = await activeIngredientService.GetActiveIngredientsAsync(queryParameter);

        return Ok(activeIngredients);
    }

    /// <summary>
    /// Get an active ingredient by id
    /// </summary>
    /// <param name="activeIngredientId" example="00000000-0000-0000-0000-000000000000"></param>
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
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/active-ingredients
    ///     {
    ///         "ingredientName": "Ingredient Name"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created active ingredient</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost(Name = "CreateActiveIngredient")]
    [ProducesResponseType(typeof(ActiveIngredientDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateActiveIngredientAsync(CreateActiveIngredientDto createActiveIngredientDto)
    {
        var activeIngredient = await activeIngredientService.CreateActiveIngredientAsync(createActiveIngredientDto);

        return CreatedAtRoute("GetActiveIngredientById", new { activeIngredientId = activeIngredient.Id }, activeIngredient);
    }

    /// <summary>
    /// Create multiple active ingredients
    /// </summary>
    /// <param name="createActiveIngredientDtos"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    /// 
    ///     POST /api/active-ingredients/bulk
    ///     [
    ///         {
    ///             "ingredientName": "Ingredient Name 1"
    ///         },
    ///         {
    ///             "ingredientName": "Ingredient Name 2"
    ///         }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="201">Returns the created active ingredients</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost("bulk", Name = "CreateBulkActiveIngredients")]
    [ProducesResponseType(typeof(IEnumerable<ActiveIngredientDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateBulkActiveIngredientsAsync(IEnumerable<CreateActiveIngredientDto> createActiveIngredientDtos)
    {
        var activeIngredients = await activeIngredientService.CreateBulkActiveIngredientsAsync(createActiveIngredientDtos);

        return CreatedAtRoute("GetActiveIngredients", activeIngredients);
    }

    /// <summary>
    /// Update an active ingredient
    /// </summary>
    /// <param name="activeIngredientId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updateActiveIngredientDto"></param>
    /// <remarks>
    /// Requires administrative policy (e.g. Admin, Manager)
    /// 
    /// Sample request:
    ///     
    ///     PUT /api/active-ingredients/{activeIngredientId}
    ///     {
    ///         "ingredientName": "Ingredient Name"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated active ingredient</response>
    /// <response code="404">If the active ingredient is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
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
    /// <remarks>Requires administrative policy (e.g. Admin, Manager)</remarks>
    /// <param name="activeIngredientId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the active ingredient is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{activeIngredientId:guid}", Name = "DeleteActiveIngredient")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteActiveIngredientAsync(Guid activeIngredientId)
    {
        await activeIngredientService.DeleteActiveIngredientAsync(activeIngredientId);

        return NoContent();
    }
}
