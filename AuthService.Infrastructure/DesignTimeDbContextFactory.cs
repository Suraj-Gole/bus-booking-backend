using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthService.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=SURAJ\\SQLEXPRESS;Database=BusBooking.Auth;Trusted_Connection=True;Encrypt=False;"
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
