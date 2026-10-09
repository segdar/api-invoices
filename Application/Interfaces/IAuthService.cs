using ApiInvoices.Api.Dtos;

namespace ApiInvoices.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
    Task<AuthResponseDto> RefreshAsync(string refreshToken);
}
