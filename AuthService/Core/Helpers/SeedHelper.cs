using AuthService.Infrastracture.Persistence.Seeders;

namespace AuthService.Core.Helpers
{
    public static class SeedHelper
    {
        public static async Task SeedAsync(IServiceProvider service)
        {
            using var scope = service.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<DbSeederRunner>();
            await runner.Run();
        }
    }
}
