using FleetForge.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Persistence;

public sealed class FleetForgeDbContext(DbContextOptions<FleetForgeDbContext> options)
    : DbContext(options)
{
    public DbSet<Truck> Trucks => Set<Truck>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<NewsletterSubscriber> NewsletterSubscribers =>
        Set<NewsletterSubscriber>();
    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();
    public DbSet<CompanyProfile> CompanyProfiles => Set<CompanyProfile>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetForgeDbContext).Assembly);
        DemoData.Apply(modelBuilder);
    }
}
