using Microsoft.AspNetCore.Mvc;
using PillPal.Service.Identities;
using System.ComponentModel.DataAnnotations;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthsController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// for test purpose
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpPost("firebase/register")]
    public async Task<IActionResult> Register([FromBody] LoginRequest loginRequest)
    {
        dynamic result = await _userService.RegisterAsync(loginRequest.Token);
        return Ok(result);
    }

    [HttpPost("firebase/login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        dynamic result = await _userService.LoginAsync(loginRequest.Token);
        return Ok(result);
    }
}

public class LoginRequest
{
    [Required]
    public string Token { get; set; }
}