namespace AuthService.Application.DTOs.Schedule
{
    public class ScheduleDto
    {
        public Guid Id { get; set; }

        public Guid BusId { get; set; }
        public Guid RouteId { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public decimal Price { get; set; }
    }
}
