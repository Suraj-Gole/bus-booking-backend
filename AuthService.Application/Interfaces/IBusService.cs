using AuthService.Application.DTOs.Bus;

namespace AuthService.Application.Interfaces
{
    public interface IBusService
    {
        Task<IEnumerable<BusDto>> GetAllAsync();
        Task<BusDto> GetByIdAsync(Guid id);
        Task<BusDto> CreateAsync(CreateBusRequest request);
        Task DeleteAsync(Guid id);
    }
}
