namespace AuthService.Application.DTOs.Route
{
    public class CreateRouteRequest
    {
        public required string StartCity { get; set; }
        public required string EndCity { get; set; }
        public double DistanceInKm { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
