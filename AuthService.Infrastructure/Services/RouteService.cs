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

        public async Task<List<RouteDto>> GetAllAsync()
        {
            var routes = await _context.Routes
                .Include(r => r.FromCity)
                .Include(r => r.ToCity)
                .ToListAsync();

            return routes.Select(r => new RouteDto
            {
                Id = r.Id,
                FromCity = r.FromCity!.Name,
                ToCity = r.ToCity!.Name,
                DistanceInKm = r.DistanceInKm,
                Duration = r.Duration
            }).ToList();
        }

        public async Task<RouteDto> GetByIdAsync(Guid id)
        {
            var route = await _context.Routes
                .Include(r => r.FromCity)
                .Include(r => r.ToCity)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (route == null)
                throw new Exception("Route not found");

            return new RouteDto
            {
                Id = route.Id,
                FromCity = route.FromCity!.Name,
                ToCity = route.ToCity!.Name,
                DistanceInKm = route.DistanceInKm,
                Duration = route.Duration
            };
        }

        public async Task<RouteDto> CreateAsync(CreateRouteRequest request)
        {
            if (request.FromCityId == request.ToCityId)
                throw new Exception("From and To cities must be different.");

            var fromCity = await _context.Cities.FindAsync(request.FromCityId);
            var toCity = await _context.Cities.FindAsync(request.ToCityId);

            if (fromCity == null || toCity == null)
                throw new Exception("One or both cities not found.");

            var route = new Route
            {
                FromCityId = request.FromCityId,
                ToCityId = request.ToCityId,
                DistanceInKm = request.DistanceInKm,
                Duration = request.Duration
            };

            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            return new RouteDto
            {
                Id = route.Id,
                FromCity = fromCity.Name,
                ToCity = toCity.Name,
                DistanceInKm = route.DistanceInKm,
                Duration = route.Duration
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
                throw new Exception("Route not found");

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
        }
    }
}
