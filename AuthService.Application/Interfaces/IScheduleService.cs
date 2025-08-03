using AuthService.Application.DTOs.Schedule;

namespace AuthService.Application.Interfaces
{
    public interface IScheduleService
    {
        Task<List<ScheduleDto>> GetAllAsync();
        Task<ScheduleDto> GetByIdAsync(Guid id);
        Task<ScheduleDto> CreateAsync(CreateScheduleRequest request);
        Task DeleteAsync(Guid id);
    }
}
