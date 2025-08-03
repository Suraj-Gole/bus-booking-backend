namespace AuthService.Application.DTOs.Route
{
    public class CreateRouteRequest
    {
        public Guid FromCityId { get; set; }
        public Guid ToCityId { get; set; }
        public decimal DistanceInKm { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
