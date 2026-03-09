using AuthService.Infrastracture.Persistence;
using AuthService.Infrastracture.Persistence.Seeders;
using OpenIddict.Abstractions;

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
        public static async Task SeedClientsAsync(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync();

            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("web-client") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "web-client",
                    ClientSecret = "web-client-super-secret",
                    DisplayName = "Web Application",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.Password,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                        OpenIddictConstants.Permissions.GrantTypes.RefreshToken,
                        OpenIddictConstants.Permissions.Scopes.Email,
                        OpenIddictConstants.Permissions.Scopes.Profile,
                    }
                });
            }

            if (await manager.FindByClientIdAsync("filter-service") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "filter-service",
                    ClientSecret = "filter-service-super-secret",
                    DisplayName = "Filter Microservice",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    }
                });
            }

            if (await manager.FindByClientIdAsync("mzad-service") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "mzad-service",
                    ClientSecret = "mzad-service-super-secret",
                    DisplayName = "Mzad Microservice",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    }
                });
            }

            if (await manager.FindByClientIdAsync("chat-service") == null)
            {
                await manager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "chat-service",
                    ClientSecret = "chat-service-super-secret",
                    DisplayName = "Chat Microservice",
                    Permissions =
                    {
                        OpenIddictConstants.Permissions.Endpoints.Token,
                        OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    }
                });
            }
        }

    }
}
