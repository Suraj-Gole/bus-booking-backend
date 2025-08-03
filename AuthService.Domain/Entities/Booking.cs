namespace AuthService.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ScheduleId { get; set; }
        public required Schedule Schedule { get; set; }
        public Guid UserId { get; set; }
        public required User User { get; set; }
        public int SeatsBooked { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime BookingTime { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Booked"; // or Cancelled
    }
}
