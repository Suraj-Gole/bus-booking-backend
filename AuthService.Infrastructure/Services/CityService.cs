using AuthService.Application.DTOs;
using AuthService.Application.DTOs.City;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Services
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _context;

        public CityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<CityDto>>> GetAllAsync()
        {
            var cities = await _context.Cities
                .Select(c => new CityDto { Id = c.Id, Name = c.Name })
                .ToListAsync();

            return ResponseDto<List<CityDto>>.SuccessResponse(cities);
        }

        public async Task<ResponseDto<CityDto>> GetByIdAsync(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
                return ResponseDto<CityDto>.FailResponse("City not found", 404);

            return ResponseDto<CityDto>.SuccessResponse(new CityDto { Id = city.Id, Name = city.Name });
        }

        public async Task<ResponseDto<CityDto>> CreateAsync(CreateCityRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return ResponseDto<CityDto>.FailResponse("City name is required", 400);

            var exists = await _context.Cities.AnyAsync(c => c.Name.ToLower() == request.Name.ToLower());
            if (exists)
                return ResponseDto<CityDto>.FailResponse("City already exists", 409);

            var city = new City { Name = request.Name };
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return ResponseDto<CityDto>.SuccessResponse(new CityDto { Id = city.Id, Name = city.Name }, 201);
        }

        public async Task<ResponseDto<string>> DeleteAsync(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
                return ResponseDto<string>.FailResponse("City not found", 404);

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return ResponseDto<string>.SuccessResponse("City deleted successfully");
        }
    }
}
