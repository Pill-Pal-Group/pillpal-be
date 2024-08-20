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
    [HttpPost("register", Name = "Register")]
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
    /// For every 5 failed login attempts, the account will be locked for 30 minutes
    /// 
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
    /// <response code="401">If the user is not found, email/password is incorrect or is locked</response>
    /// <response code="422">If the input data fail validation</response>
    [HttpPost("login", Name = "Login")]
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
    /// <response code="401">If the user is locked</response>
    /// <response code="422">If the input data fail validation</response>
    [HttpPost("token-login", Name = "TokenLogin")]
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
    [HttpPost("refresh-token", Name = "RefreshToken")]
    [ProducesResponseType(typeof(AccessTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RefreshTokenAsync(RefreshRequest request)
    {
        var response = await authService.RefreshTokenAsync(request);

        return Ok(response);
    }

    /// <summary>
    /// Change the password of the user
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Requires authentication
    /// 
    /// Sample request:
    /// 
    ///     PUT /api/auths/change-password
    ///     {
    ///         "currentPassword": "P@ssword7",
    ///         "newPassword": "P@ssword7",
    ///         "confirmPassword": "P@ssword7"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the password is changed successfully</response>
    /// <response code="409">If the operation failed</response>
    /// <response code="422">If the input data fail validation</response>
    [Authorize]
    [HttpPut("change-password", Name = "ChangePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequest request)
    {
        await authService.ChangePasswordAsync(request);

        return Ok();
    }

    /// <summary>
    /// Create a new password for the user
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// Requires authentication
    /// 
    /// Sample request:
    /// 
    ///     PUT /api/auths/create-password
    ///     {
    ///         "password": "P@ssword7",
    ///         "confirmPassword": "P@ssword7"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200">If the password is created successfully</response>
    /// <response code="409">If the operation failed</response>
    /// <response code="422">If the input data fail validation</response>
    [Authorize]
    [HttpPut("create-password", Name = "CreatePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreatePasswordAsync(CreatePasswordRequest request)
    {
        await authService.CreatePasswordAsync(request);

        return Ok();
    }
}
