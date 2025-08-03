namespace AuthService.Application.DTOs.Bus
{
    public class BusDto
    {
        public Guid Id { get; set; }
        public required string BusNumber { get; set; }
        public required string Type { get; set; }
        public int Capacity { get; set; }
    }
}
