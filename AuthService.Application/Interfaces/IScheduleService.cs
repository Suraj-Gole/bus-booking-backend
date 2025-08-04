using AuthService.Application.DTOs;
using AuthService.Application.DTOs.Schedule;

namespace AuthService.Application.Interfaces
{
    public interface IScheduleService
    {
        Task<ResponseDto<List<ScheduleDto>>> GetAllAsync();
        Task<ResponseDto<ScheduleDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<ScheduleDto>> CreateAsync(CreateScheduleRequest request);
        Task<ResponseDto<string>> DeleteAsync(Guid id);
    }
}
