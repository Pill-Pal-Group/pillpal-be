using Microsoft.AspNetCore.Mvc;
using PillPal.Application.Common.Interfaces.Auth;
using PillPal.Application.Features.Auths;
using LoginRequest = PillPal.Application.Features.Auths.LoginRequest;
using RegisterRequest = PillPal.Application.Features.Auths.RegisterRequest;

namespace PillPal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class AuthsController(IAuthService authService)
    : ControllerBase
{
    /// <summary>
    /// Register a user, default role is Customer
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/auths/register
    ///     {
    ///         "email": "monke@mail.com",
    ///         "password": "P@ssword7"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">If the user is registered successfully</response>
    /// <response code="409">If the user is already registered</response>
    /// <response code="422">If the input data fail validation</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RegisterAsync(RegisterRequest request)
    {
        await authService.RegisterAsync(request);

        return Ok();
    }

    /// <summary>
    /// Login a user
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/auths/login
    ///     {
    ///         "email": "monke@mail.com",
    ///         "password": "P@ssword7"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Access token and refresh token</response>
    /// <response code="401">If the user is not found or password is incorrect</response>
    /// <response code="422">If the input data fail validation</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        var response = await authService.LoginAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Login a user with firebase token, in case user is not registered, register the user
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/auths/token-login
    ///     {
    ///         "token": "firebase_token"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Access token and refresh token</response>
    /// <response code="422">If the input data fail validation</response>
    [HttpPost("token-login")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> TokenLoginAsync(TokenLoginRequest request)
    {
        var response = await authService.TokenLoginAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Refresh invalid access token
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/auths/refresh-token
    ///     {
    ///         "expiredToken": "expired jwt token",
    ///         "refreshToken": "refresh token"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Access token and refresh token</response>
    /// <response code="401">If the token is invalid</response>
    /// <response code="422">If the input data fail validation</response>
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RefreshTokenAsync(RefreshRequest request)
    {
        var response = await authService.RefreshTokenAsync(request);

        return Ok(response);
    }
}
