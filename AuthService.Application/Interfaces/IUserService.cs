using AuthService.Application.DTOs;

namespace AuthService.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterRequest request);
    }
}
