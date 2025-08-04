using AuthService.Application.DTOs;
using AuthService.Application.DTOs.Route;

namespace AuthService.Application.Interfaces
{
    public interface IRouteService
    {
        Task<ResponseDto<List<RouteDto>>> GetAllAsync();
        Task<ResponseDto<RouteDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<RouteDto>> CreateAsync(CreateRouteRequest request);
        Task<ResponseDto<string>> DeleteAsync(Guid id);
    }
}
