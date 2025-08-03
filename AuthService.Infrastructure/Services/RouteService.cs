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
            var routes = await _context.Routes.ToListAsync();

            return routes.Select(r => new RouteDto
            {
                Id = r.Id,
                StartCity = r.StartCity,
                EndCity = r.EndCity,
                DistanceInKm = r.DistanceInKm,
                Duration = r.Duration
            }).ToList();
        }

        public async Task<RouteDto> GetByIdAsync(Guid id)
        {
            var route = await _context.Routes.FindAsync(id);
            if (route == null)
                throw new Exception("Route not found");

            return new RouteDto
            {
                Id = route.Id,
                StartCity = route.StartCity,
                EndCity = route.EndCity,
                DistanceInKm = route.DistanceInKm,
                Duration = route.Duration
            };
        }

        public async Task<RouteDto> CreateAsync(CreateRouteRequest request)
        {
            var route = new Route
            {
                StartCity = request.StartCity,
                EndCity = request.EndCity,
                DistanceInKm = request.DistanceInKm,
                Duration = request.Duration
            };

            _context.Routes.Add(route);
            await _context.SaveChangesAsync();

            return new RouteDto
            {
                Id = route.Id,
                StartCity = route.StartCity,
                EndCity = route.EndCity,
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
