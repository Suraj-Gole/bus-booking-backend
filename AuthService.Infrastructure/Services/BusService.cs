using AuthService.Application.DTOs.Bus;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Services
{
    public class BusService : IBusService
    {
        private readonly ApplicationDbContext _context;

        public BusService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BusDto>> GetAllAsync()
        {
            var buses = await _context.Buses.ToListAsync();

            return buses.Select(bus => new BusDto
            {
                Id = bus.Id,
                BusNumber = bus.BusNumber,
                Type = bus.Type,
                Capacity = bus.Capacity
            });
        }

        public async Task<BusDto> GetByIdAsync(Guid id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null) throw new Exception("Bus not found");

            return new BusDto
            {
                Id = bus.Id,
                BusNumber = bus.BusNumber,
                Type = bus.Type,
                Capacity = bus.Capacity
            };
        }

        public async Task<BusDto> CreateAsync(CreateBusRequest request)
        {
            var bus = new Bus
            {
                BusNumber = request.BusNumber,
                Type = request.Type,
                Capacity = request.Capacity
            };

            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();

            return new BusDto
            {
                Id = bus.Id,
                BusNumber = bus.BusNumber,
                Type = bus.Type,
                Capacity = bus.Capacity
            };
        }

        public async Task DeleteAsync(Guid id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus == null) throw new Exception("Bus not found");

            _context.Buses.Remove(bus);
            await _context.SaveChangesAsync();
        }
    }
}
