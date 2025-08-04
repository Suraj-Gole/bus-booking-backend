using AuthService.Application.DTOs;
using AuthService.Application.DTOs.Route;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Services
{
    public class RouteService : IRouteService
    {
        private readonly ApplicationDbContext _context;

        public RouteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<RouteDto>>> GetAllAsync()
        {
            var routes = await _context.Routes
                .Include(r => r.FromCity)
                .Include(r => r.ToCity)
                .Select(r => new RouteDto
                {
                    Id = r.Id,
                    FromCityId = r.FromCityId,
                    FromCityName = r.FromCity.Name,
                    ToCityId = r.ToCityId,
                    ToCityName = r.ToCity.Name,
                    DistanceInKm = r.DistanceInKm,
                    Duration = r.Duration
                })
                .ToListAsync();

            return ResponseDto<List<RouteDto>>.SuccessResponse(routes);
        }

        public async Task<ResponseDto<RouteDto>> GetByIdAsync(Guid id)
        {
            var route = await _context.Routes
                .Include(r => r.FromCity)
                .Include(r => r.ToCity)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null)
                return ResponseDto<RouteDto>.FailResponse("Route not found", 404);

            var routeDto = new RouteDto
            {
                Id = route.Id,
                FromCityId = route.FromCityId,
                FromCityName = route.FromCity.Name,
                ToCityId = route.ToCityId,
                ToCityName = route.ToCity.Name,
                DistanceInKm = route.DistanceInKm,
                Duration = route.Duration
            };

            return ResponseDto<RouteDto>.SuccessResponse(routeDto);
        }


        public async Task<ResponseDto<RouteDto>> CreateAsync(CreateRouteRequest request)
        {
            if (request.FromCityId == request.ToCityId)
                return ResponseDto<RouteDto>.FailResponse("Source and destination cities must be different", 400);

            var fromCity = await _context.Cities.FindAsync(request.FromCityId);
            var toCity = await _context.Cities.FindAsync(request.ToCityId);

            if (fromCity == null || toCity == null)
                return ResponseDto<RouteDto>.FailResponse("Invalid city selection", 404);

            var route = new Route
            {
                FromCityId = request.FromCityId,
                ToCityId = request.ToCityId,
                DistanceInKm = request.DistanceInKm,
                Duration = request.Duration
            };

            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            var responseDto = new RouteDto
            {
                Id = route.Id,
                FromCityId = route.FromCityId,
                FromCityName = fromCity.Name,
                ToCityId = route.ToCityId,
                ToCityName = toCity.Name,
                DistanceInKm = route.DistanceInKm,
                Duration = route.Duration
            };

            return ResponseDto<RouteDto>.SuccessResponse(responseDto, 201);
        }

        public async Task<ResponseDto<string>> DeleteAsync(Guid id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
                throw new Exception("Route not found");

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return ResponseDto<string>.SuccessResponse("Route deleted successfully");
        }
    }
}
