using PillPal.Application.Features.PharmacyStores;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class PharmacyStoresController(IPharmacyStoreService pharmacyStoreService)
    : ControllerBase
{
    /// <summary>
    /// Get all pharmacy stores
    /// </summary>
    /// <response code="200">Returns a list of pharmacy stores</response>
    [HttpGet(Name = "GetPharmacyStores")]
    [ProducesResponseType(typeof(IEnumerable<PharmacyStoreDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPharmacyStoresAsync()
    {
        var pharmacyStores = await pharmacyStoreService.GetPharmacyStoresAsync();

        return Ok(pharmacyStores);
    }

    /// <summary>
    /// Get a pharmacy store by id
    /// </summary>
    /// <param name="pharmacyStoreId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="200">Returns a pharmacy store</response>
    /// <response code="404">If the pharmacy store is not found</response>
    [HttpGet("{pharmacyStoreId:guid}", Name = "GetPharmacyStoreById")]
    [ProducesResponseType(typeof(PharmacyStoreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPharmacyStoreByIdAsync(Guid pharmacyStoreId)
    {
        var pharmacyStore = await pharmacyStoreService.GetPharmacyStoreByIdAsync(pharmacyStoreId);

        return Ok(pharmacyStore);
    }

    /// <summary>
    /// Create a pharmacy store
    /// </summary>
    /// <param name="createPharmacyStoreDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     POST /api/pharmacy-stores
    ///     {
    ///         "storeLocation": "Store Location",
    ///         "storeImage": "Store Image",
    ///         "brandId": "Brand Id"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201">Returns the created pharmacy store</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPost(Name = "CreatePharmacyStore")]
    [ProducesResponseType(typeof(PharmacyStoreDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePharmacyStoreAsync(CreatePharmacyStoreDto createPharmacyStoreDto)
    {
        var pharmacyStore = await pharmacyStoreService.CreatePharmacyStoreAsync(createPharmacyStoreDto);

        return CreatedAtRoute("GetPharmacyStoreById", new { pharmacyStoreId = pharmacyStore.Id }, pharmacyStore);
    }

    /// <summary>
    /// Update a pharmacy store
    /// </summary>
    /// <param name="pharmacyStoreId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <param name="updatePharmacyStoreDto"></param>
    /// <remarks>
    /// Sample request:
    /// 
    ///     PUT /api/pharmacy-stores/{pharmacyStoreId}
    ///     {
    ///         "storeLocation": "Store Location",
    ///         "storeImage": "Store Image",
    ///         "brandId": "Brand Id"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">Returns the updated pharmacy store</response>
    /// <response code="404">If the pharmacy store is not found</response>
    /// <response code="422">If the input data is invalid</response>
    [Authorize(Policy.Administrative)]
    [HttpPut("{pharmacyStoreId:guid}", Name = "UpdatePharmacyStore")]
    [ProducesResponseType(typeof(PharmacyStoreDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdatePharmacyStoreAsync(Guid pharmacyStoreId, UpdatePharmacyStoreDto updatePharmacyStoreDto)
    {
        var pharmacyStore = await pharmacyStoreService.UpdatePharmacyStoreAsync(pharmacyStoreId, updatePharmacyStoreDto);

        return Ok(pharmacyStore);
    }

    /// <summary>
    /// Delete a pharmacy store (soft delete)
    /// </summary>
    /// <param name="pharmacyStoreId" example="00000000-0000-0000-0000-000000000000"></param>
    /// <response code="204">No content</response>
    /// <response code="404">If the pharmacy store is not found</response>
    [Authorize(Policy.Administrative)]
    [HttpDelete("{pharmacyStoreId:guid}", Name = "DeletePharmacyStore")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePharmacyStoreAsync(Guid pharmacyStoreId)
    {
        await pharmacyStoreService.DeletePharmacyStoreAsync(pharmacyStoreId);

        return NoContent();
    }
}
