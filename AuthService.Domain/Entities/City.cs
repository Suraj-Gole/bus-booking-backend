namespace AuthService.Domain.Entities
{
    public class City
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }

        public ICollection<Route>? RoutesFrom { get; set; }
        public ICollection<Route>? RoutesTo { get; set; }
    }
}
