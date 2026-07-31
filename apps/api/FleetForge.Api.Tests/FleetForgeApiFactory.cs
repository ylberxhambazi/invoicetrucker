using FleetForge.Api.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FleetForge.Api.Tests;

public sealed class FleetForgeApiFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = $"FleetForgeTests-{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<FleetForgeDbContext>>();
            services.RemoveAll<
                IDbContextOptionsConfiguration<FleetForgeDbContext>>();
            services.RemoveAll<FleetForgeDbContext>();
            services.AddDbContext<FleetForgeDbContext>(options =>
                options.UseInMemoryDatabase(_databaseName));

            using var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var dbContext = scope.ServiceProvider
                .GetRequiredService<FleetForgeDbContext>();
            dbContext.Database.EnsureCreated();
        });
    }
}
