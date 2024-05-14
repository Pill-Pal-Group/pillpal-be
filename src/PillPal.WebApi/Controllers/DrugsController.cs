using Microsoft.AspNetCore.Mvc;
using PillPal.Core.Dtos.Drugs.Commands;
using PillPal.Service.Applications.Drugs;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DrugsController : ControllerBase
{
    private readonly IDrugService _drugService;

    public DrugsController(IDrugService drugService)
    {
        _drugService = drugService;
    }

    [HttpGet(Name = "Get Drug List")]
    public async Task<IActionResult> GetDrugList()
    {
        return Ok(await _drugService.GetDrugsAsync());
    }

    [HttpPost(Name = "Add Drug")]
    public async Task<IActionResult> PostDrug([FromBody] CreateDrugCommand drug)
    {
        await _drugService.CreateDrugAsync(drug);
        return Ok();
    }
}
