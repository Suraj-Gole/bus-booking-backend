namespace AuthService.Domain.Entities
{
    public class Route
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid FromCityId { get; set; }
        public City? FromCity { get; set; }

        public Guid ToCityId { get; set; }
        public City? ToCity { get; set; }

        public decimal DistanceInKm { get; set; }
        public TimeSpan Duration { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
    }
}
