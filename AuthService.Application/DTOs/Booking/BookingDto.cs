namespace AuthService.Application.DTOs.Booking
{
    public class BookingDto
    {
        public Guid Id { get; set; }

        public Guid ScheduleId { get; set; }
        public Guid UserId { get; set; }

        public int SeatsBooked { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime BookingTime { get; set; }
        public string Status { get; set; }
    }
}
