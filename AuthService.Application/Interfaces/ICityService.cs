using AuthService.Application.DTOs;
using AuthService.Application.DTOs.City;

namespace AuthService.Application.Interfaces
{
    public interface ICityService
    {
        Task<ResponseDto<List<CityDto>>> GetAllAsync();
        Task<ResponseDto<CityDto>> GetByIdAsync(Guid id);
        Task<ResponseDto<CityDto>> CreateAsync(CreateCityRequest request);
        Task<ResponseDto<string>> DeleteAsync(Guid id);
    }
}
