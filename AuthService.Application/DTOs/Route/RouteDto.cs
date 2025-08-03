namespace AuthService.Application.DTOs.Route
{
    public class RouteDto
    {
        public Guid Id { get; set; }
        public string? FromCity { get; set; }
        public string? ToCity { get; set; }
        public decimal DistanceInKm { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
