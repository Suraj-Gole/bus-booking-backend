namespace AuthService.Application.DTOs.Route
{
    public class RouteDto
    {
        public Guid Id { get; set; }

        public Guid FromCityId { get; set; }
        public string FromCityName { get; set; }

        public Guid ToCityId { get; set; }
        public string ToCityName { get; set; }

        public decimal DistanceInKm { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
