namespace AuthService.Application.DTOs.Bus
{
    public class CreateBusRequest
    {
        public required string BusNumber { get; set; }
        public required string Type { get; set; }
        public int Capacity { get; set; }
    }
}
