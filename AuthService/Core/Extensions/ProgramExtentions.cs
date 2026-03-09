using AuthService.Domain.Entities.User;
using AuthService.Infrastracture.Persistence;
using AuthService.Infrastracture.Persistence.Seeders;
using MassTransit.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;

namespace AuthService.Core.Extensions
{
    public static class ProgramExtentions
    {
        /// <summary>
        /// Configures OpenIddict services for the application, enabling authentication and authorization features using
        /// the OpenID Connect and OAuth 2.0 protocols.
        /// </summary>
        /// <param name="options">The WebApplicationBuilder instance used to register OpenIddict services and configure authentication and
        /// authorization middleware.</param>
        public static void OpenIdDictConfig(this WebApplicationBuilder options)
        {
            options.Services.AddOpenIddict().AddCore(options =>
            {
                options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();
            }).AddServer(options =>
            {
                // supported flows
                options.AllowPasswordFlow();
                options.AllowClientCredentialsFlow();
                options.AllowRefreshTokenFlow();

                // endpoints
                options.SetTokenEndpointUris("/connect/token");
                options.SetIntrospectionEndpointUris("/connect/introspect");
                options.SetRevocationEndpointUris("/connect/revoke");
                options.SetUserInfoEndpointUris("/connect/userinfo");

                // token lifetimes
                options.SetAccessTokenLifetime(TimeSpan.FromMinutes(15));
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(30));

                options.AddDevelopmentEncryptionCertificate()
                       .AddDevelopmentSigningCertificate();

                options.UseAspNetCore()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserInfoEndpointPassthrough();

                // register valid scopes
                options.RegisterScopes(
                    OpenIddictConstants.Scopes.OpenId,
                    OpenIddictConstants.Scopes.Email,
                    OpenIddictConstants.Scopes.Profile,
                    OpenIddictConstants.Scopes.OfflineAccess,
                    "mzad-service",
                    "filter-service",
                    "chat-service"
                );
            });
        }

        /// <summary>
        /// Configures ASP.NET Core Identity services for the application, including password requirements and claim
        /// types, and registers Entity Framework stores for user and role management.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder instance used to configure the application's services. Must not be null.</param>
        /// <param name="connectionString">The connection string for the database used to persist identity information. Must be a valid connection
        /// string.</param>
        public static void IdentityConfig(this WebApplicationBuilder builder, string connectionString)
        {
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        }

        /// <summary>
        /// Configures the dependency injection container
        /// </summary>
        public static void DiContaonerConfig(this IServiceCollection services)
        {
            services.AddScoped<IDbSeeder, RoleSeeder>();
            services.AddScoped<IDbSeeder, UserSeeder>();
            services.AddScoped<DbSeederRunner>();
        }
    }
}
