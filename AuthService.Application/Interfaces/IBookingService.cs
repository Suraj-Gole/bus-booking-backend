using AuthService.Application.DTOs.Booking;

namespace AuthService.Application.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookingDto>> GetMyBookingsAsync(Guid userId);
        Task<BookingDto> GetByIdAsync(Guid id, Guid userId);
        Task<BookingDto> CreateAsync(CreateBookingRequest request, Guid userId);
        Task CancelAsync(Guid id, Guid userId);
    }
}
