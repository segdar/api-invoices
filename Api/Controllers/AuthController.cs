using ApiInvoices.Application.DTOs;
using ApiInvoices.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiInvoices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await authService.LoginAsync(request);
        if (result == null)
            return Unauthorized(ApiResponse<object>.Error("invalid credentials or inactive user"));

        
        var refreshToken = authService.CreateRefreshToken(request.Username.Trim());
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });

        return Ok(ApiResponse<LoginResponse>.Ok(result, "login successful"));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var result = await authService.RefreshAsync(refreshToken ?? string.Empty);
        if (result == null)
            return Unauthorized(ApiResponse<object>.Error("invalid or expired refresh token"));

        return Ok(ApiResponse<LoginResponse>.Ok(result, "token refreshed"));
    }
}
