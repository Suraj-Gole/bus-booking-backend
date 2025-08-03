namespace AuthService.Application.DTOs.Booking
{
    public class CreateBookingRequest
    {
        public Guid ScheduleId { get; set; }
        public int SeatsBooked { get; set; }
    }
}
