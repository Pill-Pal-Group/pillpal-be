﻿using Microsoft.AspNetCore.Mvc;
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
    /// <summary>
    /// Register a user, default role is Customer
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/auth/register
    ///     {
    ///         "email": "sample@mail.com",
    ///         "password": "Password@9"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">If the user is registered successfully</response>
    /// <response code="422">If the input data fail validation</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    ///     POST /api/auth/login
    ///     {
    ///         "email": "sample@mail.com",
    ///         "password": "password"
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
    ///     POST /api/auth/firebase-login
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
    ///     POST /api/auth/refresh-token
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
