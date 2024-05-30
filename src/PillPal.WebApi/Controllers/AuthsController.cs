using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Auth;
using PillPal.Application.Dtos.Auths;
using LoginRequest = PillPal.Application.Dtos.Auths.LoginRequest;
using RegisterRequest = PillPal.Application.Dtos.Auths.RegisterRequest;

namespace PillPal.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class AuthsController(IAuthService authService)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        await authService.RegisterAsync(request);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        return Ok(response);
    }

    [HttpPost("token-login")]
    public async Task<IActionResult> TokenLoginAsync(TokenLoginRequest request)
    {
        var response = await authService.TokenLoginAsync(request);

        return Ok(response);
    }
}
