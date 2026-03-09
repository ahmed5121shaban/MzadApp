using AuthService.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Infrastracture.Persistence.Seeders
{
    public class UserSeeder : IDbSeeder
    {
        public int Order => 2;

        public async Task SeedAsync(ApplicationDbContext context, CancellationToken ct)
        {
            if (context.Users.Any()) return;

            var pass = new PasswordHasher<ApplicationUser>().HashPassword(null, "Ahmed@1234");

            List<ApplicationUser> users = new List<ApplicationUser>
            {
                new ApplicationUser { UserName="Ahmed Shaban", Email="ahmed01shaban@gmail.com", PasswordHash=pass, PhoneNumber="01123711868", EmailConfirmed=true },
            };

            await context.Users.AddRangeAsync(users);
        }
    }
}
