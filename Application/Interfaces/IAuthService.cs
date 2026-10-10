using ApiInvoices.Application.DTOs;

namespace ApiInvoices.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Task<LoginResponse?> RefreshAsync(string refreshToken);
    Task LogoutAsync(string refreshToken);
    string CreateRefreshToken(string username);
    bool ValidateRefreshToken(string refreshToken);
}
