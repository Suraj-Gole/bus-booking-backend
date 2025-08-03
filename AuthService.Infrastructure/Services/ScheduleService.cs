using AuthService.Application.DTOs.Schedule;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ScheduleDto>> GetAllAsync()
        {
            var schedules = await _context.Schedules.ToListAsync();

            return schedules.Select(s => new ScheduleDto
            {
                Id = s.Id,
                BusId = s.BusId,
                RouteId = s.RouteId,
                DepartureTime = s.DepartureTime,
                ArrivalTime = s.ArrivalTime,
                Price = s.Price
            }).ToList();
        }

        public async Task<ScheduleDto> GetByIdAsync(Guid id)
        {
            var s = await _context.Schedules.FindAsync(id);
            if (s == null) throw new Exception("Schedule not found");

            return new ScheduleDto
            {
                Id = s.Id,
                BusId = s.BusId,
                RouteId = s.RouteId,
                DepartureTime = s.DepartureTime,
                ArrivalTime = s.ArrivalTime,
                Price = s.Price
            };
        }

        public async Task<ScheduleDto> CreateAsync(CreateScheduleRequest request)
        {
            // Fetch required Bus and Route entities from the database
            var bus = await _context.Buses.FindAsync(request.BusId);
            if (bus == null) throw new Exception("Bus not found");

            var route = await _context.Routes.FindAsync(request.RouteId);
            if (route == null) throw new Exception("Route not found");

            var schedule = new Schedule
            {
                BusId = request.BusId,
                Bus = bus,
                RouteId = request.RouteId,
                Route = route,
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime,
                Price = request.Price
            };

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return new ScheduleDto
            {
                Id = schedule.Id,
                BusId = schedule.BusId,
                RouteId = schedule.RouteId,
                DepartureTime = schedule.DepartureTime,
                ArrivalTime = schedule.ArrivalTime,
                Price = schedule.Price
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var s = await _context.Schedules.FindAsync(id);
            if (s == null) throw new Exception("Schedule not found");

            _context.Schedules.Remove(s);
            await _context.SaveChangesAsync();
        }
    }
}
