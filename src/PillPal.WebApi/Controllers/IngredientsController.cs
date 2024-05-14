using Microsoft.AspNetCore.Mvc;
using PillPal.Core.Dtos.Ingredients.Commands;
using PillPal.Service.Applications.Ingredients;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientService _ingredient;

    public IngredientsController(IIngredientService ingredient)
    {
        _ingredient = ingredient;
    }

    [HttpGet(Name = "Get Ingredient List")]
    public async Task<IActionResult> GetIngredientList()
    {
        return Ok(await _ingredient.GetIngredientsAsync());
    }

    [HttpPost(Name = "Add ingredient")]
    public async Task<IActionResult> PostIngredient([FromBody] CreateIngredientCommand ingredient)
    {
        await _ingredient.CreateIngredientAsync(ingredient);
        return Ok();
    }
}
