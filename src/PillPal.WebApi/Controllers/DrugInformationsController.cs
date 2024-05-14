using Microsoft.AspNetCore.Mvc;
using PillPal.Core.Dtos.DrugInformations.Commands;
using PillPal.Service.Applications.DrugInformations;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DrugInformationsController : ControllerBase
{
    private readonly IDrugInformationService _drugInformationService;

    public DrugInformationsController(IDrugInformationService drugInformationService)
    {
        _drugInformationService = drugInformationService;
    }

    [HttpGet(Name = "Get Drug Information List")]
    public async Task<IActionResult> GetDrugInformationList()
    {
        return Ok(await _drugInformationService.GetDrugInformationsAsync());
    }

    [HttpGet("{drugId:guid}", Name = "Get Drug Information by Drug Id")]
    public async Task<IActionResult> GetDrugInformationByDrugId(Guid drugId)
    {
        return Ok(await _drugInformationService.GetDrugInformationByDrugIdAsync(drugId));
    }

    [HttpPost(Name = "Add Drug Information")]
    public async Task<IActionResult> PostDrugInformation([FromBody] CreateDrugInformationCommand drugInformation)
    {
        await _drugInformationService.CreateDrugInformationAsync(drugInformation);
        return Ok();
    }
}
