namespace AuthService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public string Role { get; set; } = "User"; // User or Admin
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
