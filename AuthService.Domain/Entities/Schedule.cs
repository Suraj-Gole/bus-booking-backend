namespace AuthService.Domain.Entities
{
    public class Schedule
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BusId { get; set; }
        public required Bus Bus { get; set; }
        public Guid RouteId { get; set; }
        public required Route Route { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Price { get; set; }
        //public ICollection<Booking> Bookings { get; set; }
    }
}
