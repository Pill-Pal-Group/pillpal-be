using Microsoft.AspNetCore.Mvc;
using PillPal.Core.Identity;
using PillPal.Service.Identities;
using PillPal.WebApi.Service;
using System.ComponentModel.DataAnnotations;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJWTService<ApplicationUser> _jwtService;

    public AuthsController(IUserService userService, IJWTService<ApplicationUser> jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
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
    public async Task<IActionResult> FirebaseRegister([FromBody] LoginRequest loginRequest)
    {
        dynamic result = await _userService.RegisterAsync(loginRequest.Token);
        return Ok(result);
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] Login loginRequest)
    {
        dynamic result = await _userService.RegisterAsync(loginRequest);

        //check if the result is a bool
        if (result is bool && result is true)
        {
            var token = _jwtService.CreateToken(new ApplicationUser
            {
                Email = loginRequest.email
            });

            return Ok(new
            {
                token = token
            });
        }

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

public class Login
{
    [Required]
    public string email { get; set; }

    [Required]
    public string password { get; set; }
}