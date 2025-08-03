using AuthService.Application.DTOs;

namespace AuthService.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request);
        Task LogoutAsync(string email);
    }
}
