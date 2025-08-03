using AuthService.Application.DTOs.Route;

namespace AuthService.Application.Interfaces
{
    public interface IRouteService
    {
        Task<List<RouteDto>> GetAllAsync();
        Task<RouteDto> GetByIdAsync(Guid id);
        Task<RouteDto> CreateAsync(CreateRouteRequest request);
        Task DeleteAsync(Guid id);
    }
}
