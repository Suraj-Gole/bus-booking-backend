namespace AuthService.Domain.Entities
{
    public class Bus
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string BusNumber { get; set; }
        public required string Type { get; set; } // AC, Non-AC, Sleeper, etc.
        public int Capacity { get; set; }
        //public ICollection<Schedule> Schedules { get; set; }
    }
}
