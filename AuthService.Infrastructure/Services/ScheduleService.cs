using AuthService.Application.DTOs;
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

        public async Task<ResponseDto<ScheduleDto>> CreateAsync(CreateScheduleRequest request)
        {
            var bus = await _context.Buses.FindAsync(request.BusId);
            var route = await _context.Routes.FindAsync(request.RouteId);

            if (bus == null || route == null)
                return ResponseDto<ScheduleDto>.FailResponse("Invalid Bus or Route", 400);

            var schedule = new Schedule
            {
                BusId = request.BusId,
                RouteId = request.RouteId,
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime,
                Price = request.Price
            };

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            var scheduleDto = new ScheduleDto
            {
                Id = schedule.Id,
                BusId = schedule.BusId,
                RouteId = schedule.RouteId,
                DepartureTime = schedule.DepartureTime,
                ArrivalTime = schedule.ArrivalTime,
                Price = schedule.Price
            };

            return ResponseDto<ScheduleDto>.SuccessResponse(scheduleDto, 201);
        }

        public async Task<ResponseDto<List<ScheduleDto>>> GetAllAsync()
        {
            var schedules = await _context.Schedules
                .Select(s => new ScheduleDto
                {
                    Id = s.Id,
                    BusId = s.BusId,
                    RouteId = s.RouteId,
                    DepartureTime = s.DepartureTime,
                    ArrivalTime = s.ArrivalTime,
                    Price = s.Price
                })
                .ToListAsync();

            return ResponseDto<List<ScheduleDto>>.SuccessResponse(schedules);
        }

        public async Task<ResponseDto<ScheduleDto>> GetByIdAsync(Guid id)
        {
            var schedule = await _context.Schedules.FindAsync(id);

            if (schedule == null)
                return ResponseDto<ScheduleDto>.FailResponse("Schedule not found", 404);

            var dto = new ScheduleDto
            {
                Id = schedule.Id,
                BusId = schedule.BusId,
                RouteId = schedule.RouteId,
                DepartureTime = schedule.DepartureTime,
                ArrivalTime = schedule.ArrivalTime,
                Price = schedule.Price
            };

            return ResponseDto<ScheduleDto>.SuccessResponse(dto);
        }

        public async Task<ResponseDto<string>> DeleteAsync(Guid id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
                return ResponseDto<string>.FailResponse("Schedule not found", 404);

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return ResponseDto<string>.SuccessResponse("Schedule deleted successfully");
        }
    }
}
