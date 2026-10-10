using ApiInvoices.Application.DTOs;
using ApiInvoices.Application.Interfaces;
using ApiInvoices.Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ApiInvoices.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration configuration;

    private static readonly ConcurrentDictionary<string, (string Username, DateTime ExpiresAt)> RefreshTokens = new();

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.configuration = configuration;
    }


    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var username = (request.Username ?? string.Empty).Trim();
        var password = (request.Password ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            return null;

        var user = await userRepository.GetByUsernameAsync(username);
        if (user == null) return null;

        
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        if (user.Status != "active")
            return null;

        var refreshToken = CreateRefreshToken(user.Username);

        return new LoginResponse
        {
            AccessToken = GenerateJwt(user.Username, user.Email),
            RefreshToken = refreshToken,
            Name = user.Name,
            Email = user.Email
        };
    }

    public Task LogoutAsync(string refreshToken)
    {
        if (!string.IsNullOrWhiteSpace(refreshToken))
            RefreshTokens.TryRemove(refreshToken, out _);
        return Task.CompletedTask;
    }

    public string CreateRefreshToken(string username)
    {
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        RefreshTokens[token] = (username, DateTime.UtcNow.AddDays(7));
        return token;
    }

    public bool ValidateRefreshToken(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return false;

        if (!RefreshTokens.TryGetValue(refreshToken, out var entry))
            return false;

        if (entry.ExpiresAt < DateTime.UtcNow)
        {
            RefreshTokens.TryRemove(refreshToken, out _);
            return false;
        }

        return true;
    }

    public async Task<LoginResponse?> RefreshAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return null;

        if (!RefreshTokens.TryGetValue(refreshToken, out var entry))
            return null;

        
        if (entry.ExpiresAt < DateTime.UtcNow)
        {
            RefreshTokens.TryRemove(refreshToken, out _);
            return null;
        }

        
        var user = await userRepository.GetByUsernameAsync(entry.Username);
        if (user == null || user.Status != "active")
        {
            RefreshTokens.TryRemove(refreshToken, out _);
            return null;
        }

        
        RefreshTokens.TryRemove(refreshToken, out _);
        var newRefreshToken = CreateRefreshToken(user.Username);

        return new LoginResponse
        {
            AccessToken = GenerateJwt(user.Username, user.Email),
            RefreshToken = newRefreshToken,
            Name = user.Name,
            Email = user.Email
        };
    }


    private string GenerateJwt(string username, string email)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, email)
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(configuration["Jwt:ExpiresMinutes"] ?? "60")),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
