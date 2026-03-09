namespace AuthService.Infrastracture.Persistence.Seeders
{
    public interface IDbSeeder
    {
        int Order { get; }
        Task SeedAsync(ApplicationDbContext context, CancellationToken ct);
    }
}
