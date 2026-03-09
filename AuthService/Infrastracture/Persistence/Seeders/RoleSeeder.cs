using AuthService.Domain.Entities.Roles;

namespace AuthService.Infrastracture.Persistence.Seeders
{
    public class RoleSeeder : IDbSeeder
    {
        public int Order => 1;

        public async Task SeedAsync(ApplicationDbContext context, CancellationToken ct)
        {
            if (context.Roles.Any())  return;

            List<ApplicationRole> roles = new List<ApplicationRole>
            {
                new ApplicationRole { Name = "Admin", NormalizedName = "ADMIN" },
                new ApplicationRole { Name = "User", NormalizedName = "USER" }
            };

            await context.Roles.AddRangeAsync(roles);
        }
    }
}
