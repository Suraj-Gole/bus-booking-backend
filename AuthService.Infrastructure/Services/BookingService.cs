using AuthService.Application.DTOs.Booking;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookingDto> CreateAsync(CreateBookingRequest request, Guid userId)
        {
            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.Id == request.ScheduleId);

            if (schedule == null)
                throw new Exception("Schedule not found");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new Exception("User not found");

            var totalAmount = schedule.Price * request.SeatsBooked;

            var booking = new Booking
            {
                ScheduleId = request.ScheduleId,
                Schedule = schedule,
                UserId = userId,
                User = user,
                SeatsBooked = request.SeatsBooked,
                TotalAmount = totalAmount,
                BookingTime = DateTime.UtcNow,
                Status = "Booked"
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return new BookingDto
            {
                Id = booking.Id,
                ScheduleId = booking.ScheduleId,
                UserId = booking.UserId,
                SeatsBooked = booking.SeatsBooked,
                TotalAmount = booking.TotalAmount,
                BookingTime = booking.BookingTime,
                Status = booking.Status
            };
        }

        public async Task<List<BookingDto>> GetMyBookingsAsync(Guid userId)
        {
            var bookings = await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return bookings.Select(b => new BookingDto
            {
                Id = b.Id,
                ScheduleId = b.ScheduleId,
                UserId = b.UserId,
                SeatsBooked = b.SeatsBooked,
                TotalAmount = b.TotalAmount,
                BookingTime = b.BookingTime,
                Status = b.Status
            }).ToList();
        }

        public async Task<BookingDto> GetByIdAsync(Guid id, Guid userId)
        {
            var b = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (b == null) throw new Exception("Booking not found");

            return new BookingDto
            {
                Id = b.Id,
                ScheduleId = b.ScheduleId,
                UserId = b.UserId,
                SeatsBooked = b.SeatsBooked,
                TotalAmount = b.TotalAmount,
                BookingTime = b.BookingTime,
                Status = b.Status
            };
        }

        public async Task CancelAsync(Guid id, Guid userId)
        {
            var b = await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (b == null) throw new Exception("Booking not found");

            b.Status = "Cancelled";
            await _context.SaveChangesAsync();
        }
    }
}
