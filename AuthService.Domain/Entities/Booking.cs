namespace AuthService.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public required User User { get; set; }
        public Guid ScheduleId { get; set; }
        public required Schedule Schedule { get; set; }
        public int NumberOfSeats { get; set; }
        public DateTime BookingTime { get; set; } = DateTime.UtcNow;
    }
}
