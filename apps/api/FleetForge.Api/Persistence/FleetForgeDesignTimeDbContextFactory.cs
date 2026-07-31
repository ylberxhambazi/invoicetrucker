using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FleetForge.Api.Persistence;

public sealed class FleetForgeDesignTimeDbContextFactory
    : IDesignTimeDbContextFactory<FleetForgeDbContext>
{
    public FleetForgeDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("FleetForge")
            ?? throw new InvalidOperationException(
                "Connection string 'FleetForge' is not configured.");
        var options = new DbContextOptionsBuilder<FleetForgeDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new FleetForgeDbContext(options);
    }
}
