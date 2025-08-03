namespace AuthService.Domain.Entities
{
    public class Route
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string StartCity { get; set; }
        public required string EndCity { get; set; }
        public double DistanceInKm { get; set; }
        public TimeSpan Duration { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
    }
}
