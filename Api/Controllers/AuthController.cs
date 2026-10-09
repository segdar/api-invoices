using ApiInvoices.Api.Dtos;
using ApiInvoices.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiInvoices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly IConfiguration configuration;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        this.authService = authService;
        this.configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login(LoginRequestDto request)
    {
        var response = await authService.LoginAsync(request);
        SetRefreshCookie(response.RefreshToken);
        return Ok(ApiResponse<AuthResponseDto>.Ok("Authenticated successfully.", response));
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Refresh()
    {
        var cookieName = configuration["RefreshToken:CookieName"] ?? "refreshToken";
        var refreshToken = Request.Cookies[cookieName] ?? string.Empty;
        var response = await authService.RefreshAsync(refreshToken);
        SetRefreshCookie(response.RefreshToken);
        return Ok(ApiResponse<AuthResponseDto>.Ok("Token refreshed successfully.", response));
    }

    private void SetRefreshCookie(string refreshToken)
    {
        var cookieName = configuration["RefreshToken:CookieName"] ?? "refreshToken";
        var refreshDays = configuration.GetValue("RefreshToken:Days", 7);

        Response.Cookies.Append(cookieName, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(refreshDays)
        });
    }
}
