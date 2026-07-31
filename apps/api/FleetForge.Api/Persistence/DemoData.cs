using FleetForge.Api.Domain.Entities;
using FleetForge.Api.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FleetForge.Api.Persistence;

public static class DemoData
{
    private static readonly DateTime SeedTimestamp =
        new(2026, 7, 25, 8, 0, 0, DateTimeKind.Utc);

    public static void Apply(ModelBuilder modelBuilder)
    {
        var drivers = CreateDrivers();
        var trucks = CreateTrucks(drivers);
        var clients = CreateClients();
        var invoices = CreateInvoices(clients, trucks);
        var invoiceItems = CreateInvoiceItems(invoices);
        var expenses = CreateExpenses(trucks);
        var documents = CreateDocuments(trucks, drivers, clients);
        var activities = CreateActivities(trucks, invoices, expenses, documents);
        var companyProfile = CreateCompanyProfile();

        modelBuilder.Entity<Driver>().HasData(drivers);
        modelBuilder.Entity<Truck>().HasData(trucks);
        modelBuilder.Entity<Client>().HasData(clients);
        modelBuilder.Entity<Invoice>().HasData(invoices);
        modelBuilder.Entity<InvoiceItem>().HasData(invoiceItems);
        modelBuilder.Entity<Expense>().HasData(expenses);
        modelBuilder.Entity<Document>().HasData(documents);
        modelBuilder.Entity<ActivityLog>().HasData(activities);
        modelBuilder.Entity<CompanyProfile>().HasData(companyProfile);
    }

    private static Driver[] CreateDrivers()
    {
        string[] names =
        [
            "Elian Voss",
            "Mira Daneva",
            "Noah Petreski",
            "Lina Moreau",
            "Tomas Iliev",
            "Sora Lind",
            "Adrian Vale",
            "Nika Stojan",
            "Milan Rohe",
            "Elena Varga",
            "Dario Kelm",
            "Iris Novak"
        ];

        return names.Select((name, index) => new Driver
        {
            Id = Id(2, index + 1),
            FullName = name,
            Phone = $"+389 70 555 {index + 101:000}",
            Email = $"driver{index + 1:00}@invoicetrucker.example",
            LicenceNumber = $"IT-LIC-{index + 1:0000}",
            LicenceExpiration = new DateOnly(2027 + index % 3, 2 + index % 9, 10 + index % 15),
            Status = (DriverStatus)(index % 4),
            CompletedTrips = 72 + index * 9,
            CreatedAtUtc = SeedTimestamp.AddDays(-500 + index * 7),
            UpdatedAtUtc = SeedTimestamp.AddDays(-index)
        }).ToArray();
    }

    private static Truck[] CreateTrucks(IReadOnlyList<Driver> drivers)
    {
        (string Make, string Model)[] vehicles =
        [
            ("Volvo", "FH 460"),
            ("Scania", "R 450"),
            ("Mercedes-Benz", "Actros 1845"),
            ("DAF", "XF 480"),
            ("MAN", "TGX 18.470"),
            ("Iveco", "S-Way 460"),
            ("Renault Trucks", "T High 480"),
            ("Volvo", "FM 420"),
            ("Scania", "S 500"),
            ("DAF", "XG 480"),
            ("MAN", "TGX 18.510"),
            ("Mercedes-Benz", "Actros 1848"),
            ("Iveco", "S-Way 490"),
            ("Renault Trucks", "T 460"),
            ("Volvo", "FH 500")
        ];

        return vehicles.Select((vehicle, index) => new Truck
        {
            Id = Id(1, index + 1),
            RegistrationNumber = $"IT-DEMO-{index + 1:000}",
            Make = vehicle.Make,
            Model = vehicle.Model,
            Year = 2017 + index % 8,
            Status = (TruckStatus)(index % 4),
            AssignedDriverId = index < drivers.Count ? drivers[index].Id : null,
            CurrentMileage = 185_000 + index * 24_730L,
            InsuranceExpiration = new DateOnly(2026 + index % 2, 8 + index % 4, 8 + index % 18),
            TechnicalInspectionExpiration =
                new DateOnly(2026 + index % 2, 9 + index % 3, 5 + index % 20),
            CreatedAtUtc = SeedTimestamp.AddDays(-720 + index * 11),
            UpdatedAtUtc = SeedTimestamp.AddDays(-index)
        }).ToArray();
    }

    private static Client[] CreateClients()
    {
        string[] companies =
        [
            "Northlane Foods",
            "Aster Retail",
            "Cobalt Works",
            "Silverpine Home",
            "Meridian Parts",
            "Brightfield Produce",
            "Oakline Packaging",
            "Harborlight Textiles",
            "Velora Components",
            "Juniper Market",
            "Bluepeak Supplies",
            "Stonepath Ceramics",
            "Amberline Trade",
            "Evertrail Goods",
            "Lumenridge Labs",
            "Redwood Fixtures",
            "Greenwell Paper",
            "Westford Equipment",
            "Clearbrook Foods",
            "Ironvale Furnishings"
        ];

        string[] contacts =
        [
            "Sofia Marin",
            "Leon Haas",
            "Eva Korhonen",
            "Luka Venn",
            "Anika Solberg",
            "Teo Marku",
            "Nora Vale",
            "Eli Danek",
            "Mara Klein",
            "Jonas Veld",
            "Lena Orlov",
            "Ivo Kolar",
            "Mia Soren",
            "Deni Pavel",
            "Sara Nordin",
            "Oren Iliev",
            "Kira Vos",
            "Niko Brandt",
            "Elin Marek",
            "Toma Vesel"
        ];

        string[] countries =
        [
            "Germany",
            "Austria",
            "Finland",
            "Croatia",
            "Netherlands",
            "North Macedonia",
            "Slovenia",
            "Belgium",
            "Germany",
            "Netherlands",
            "Sweden",
            "Czechia",
            "Denmark",
            "Poland",
            "Norway",
            "Serbia",
            "Slovenia",
            "Germany",
            "Croatia",
            "Austria"
        ];

        return companies.Select((company, index) => new Client
        {
            Id = Id(3, index + 1),
            CompanyName = company,
            ContactPerson = contacts[index],
            Country = countries[index],
            Email = $"billing{index + 1:00}@client.invoicetrucker.example",
            Phone = $"+389 71 440 {index + 201:000}",
            IsActive = index % 7 != 0,
            CreatedAtUtc = SeedTimestamp.AddDays(-600 + index * 8),
            UpdatedAtUtc = SeedTimestamp.AddDays(-index * 2)
        }).ToArray();
    }

    private static Invoice[] CreateInvoices(
        IReadOnlyList<Client> clients,
        IReadOnlyList<Truck> trucks)
    {
        var statuses = new[]
        {
            InvoiceStatus.Paid,
            InvoiceStatus.Sent,
            InvoiceStatus.Paid,
            InvoiceStatus.Overdue,
            InvoiceStatus.Draft
        };

        return Enumerable.Range(0, 50).Select(index =>
        {
            var monthOffset = index % 12;
            var issueDate = new DateOnly(2025, 8, 1)
                .AddMonths(monthOffset)
                .AddDays(2 + index / 12 * 5);
            var status = statuses[index % statuses.Length];
            var amount = 1_850m + index * 137m + monthOffset * 43m;

            return new Invoice
            {
                Id = Id(4, index + 1),
                InvoiceNumber = $"IT-{2001 + index}",
                ClientId = clients[index % clients.Count].Id,
                TruckId = trucks[index % trucks.Count].Id,
                IssueDate = issueDate,
                DueDate = issueDate.AddDays(30),
                TotalAmount = amount,
                Currency = "EUR",
                Status = status,
                PaidAtUtc = status == InvoiceStatus.Paid
                    ? issueDate.AddDays(18).ToDateTime(
                        TimeOnly.FromTimeSpan(TimeSpan.FromHours(10)),
                        DateTimeKind.Utc)
                    : null,
                Notes = "Fictional transport services for the InvoiceTrucker demonstration.",
                CreatedAtUtc = issueDate.ToDateTime(
                    TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)),
                    DateTimeKind.Utc),
                UpdatedAtUtc = issueDate.AddDays(3).ToDateTime(
                    TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)),
                    DateTimeKind.Utc)
            };
        }).ToArray();
    }

    private static InvoiceItem[] CreateInvoiceItems(IReadOnlyList<Invoice> invoices)
    {
        return invoices.SelectMany((invoice, index) =>
        {
            var transportAmount = decimal.Round(invoice.TotalAmount * 0.84m, 2);
            var surchargeAmount = invoice.TotalAmount - transportAmount;

            return new[]
            {
                new InvoiceItem
                {
                    Id = Id(5, index * 2 + 1),
                    InvoiceId = invoice.Id,
                    Description = $"Road freight service — route {index + 1:000}",
                    Quantity = 1m,
                    UnitPrice = transportAmount,
                    LineTotal = transportAmount,
                    CreatedAtUtc = invoice.CreatedAtUtc,
                    UpdatedAtUtc = invoice.UpdatedAtUtc
                },
                new InvoiceItem
                {
                    Id = Id(5, index * 2 + 2),
                    InvoiceId = invoice.Id,
                    Description = "Fuel and toll adjustment",
                    Quantity = 1m,
                    UnitPrice = surchargeAmount,
                    LineTotal = surchargeAmount,
                    CreatedAtUtc = invoice.CreatedAtUtc,
                    UpdatedAtUtc = invoice.UpdatedAtUtc
                }
            };
        }).ToArray();
    }

    private static Expense[] CreateExpenses(IReadOnlyList<Truck> trucks)
    {
        string[] suppliers =
        [
            "Fictional Fuel Network",
            "EuroRoute Demo Tolls",
            "Apex Garage Demo",
            "Northstar Insurance Demo",
            "Transit Repair Studio",
            "Driver Services Demo",
            "Fleet Office Demo"
        ];

        var categories = Enum.GetValues<ExpenseCategory>();

        return Enumerable.Range(0, 80).Select(index =>
        {
            var category = categories[index % categories.Length];
            var date = new DateOnly(2025, 8, 3)
                .AddMonths(index % 12)
                .AddDays(index % 22);

            return new Expense
            {
                Id = Id(6, index + 1),
                Category = category,
                TruckId = index % 9 == 0 ? null : trucks[index % trucks.Count].Id,
                Date = date,
                Supplier = suppliers[index % suppliers.Length],
                Description = $"{ToDisplayName(category)} expense — fictional record",
                Amount = 190m + index * 31m + (index % 5) * 47m,
                Currency = "EUR",
                CreatedAtUtc = date.ToDateTime(
                    TimeOnly.FromTimeSpan(TimeSpan.FromHours(7)),
                    DateTimeKind.Utc),
                UpdatedAtUtc = date.ToDateTime(
                    TimeOnly.FromTimeSpan(TimeSpan.FromHours(7)),
                    DateTimeKind.Utc)
            };
        }).ToArray();
    }

    private static Document[] CreateDocuments(
        IReadOnlyList<Truck> trucks,
        IReadOnlyList<Driver> drivers,
        IReadOnlyList<Client> clients)
    {
        var types = Enum.GetValues<DocumentType>();

        return Enumerable.Range(0, 25).Select(index =>
        {
            var type = types[index % types.Length];
            var issuedDate = new DateOnly(2025, 1, 10).AddMonths(index % 10);
            Guid? truckId = null;
            Guid? driverId = null;
            Guid? clientId = null;

            if (type is DocumentType.Insurance
                or DocumentType.VehicleRegistration
                or DocumentType.TechnicalInspection)
            {
                truckId = trucks[index % trucks.Count].Id;
            }
            else if (type == DocumentType.DriverLicence)
            {
                driverId = drivers[index % drivers.Count].Id;
            }
            else
            {
                clientId = clients[index % clients.Count].Id;
            }

            return new Document
            {
                Id = Id(7, index + 1),
                Type = type,
                Name = $"{ToDisplayName(type)} document",
                ReferenceNumber = $"IT-DOC-{index + 1:0000}",
                IssuedDate = issuedDate,
                ExpirationDate = type == DocumentType.Contract && index % 2 == 0
                    ? null
                    : new DateOnly(2026 + index % 2, 8 + index % 4, 5 + index % 20),
                TruckId = truckId,
                DriverId = driverId,
                ClientId = clientId,
                CreatedAtUtc = issuedDate.ToDateTime(
                    TimeOnly.FromTimeSpan(TimeSpan.FromHours(8)),
                    DateTimeKind.Utc),
                UpdatedAtUtc = SeedTimestamp.AddDays(-index)
            };
        }).ToArray();
    }

    private static ActivityLog[] CreateActivities(
        IReadOnlyList<Truck> trucks,
        IReadOnlyList<Invoice> invoices,
        IReadOnlyList<Expense> expenses,
        IReadOnlyList<Document> documents)
    {
        return Enumerable.Range(0, 30).Select(index =>
        {
            var type = (ActivityType)(index % 6);
            var (entityType, entityId, message) = type switch
            {
                ActivityType.TruckStatusChanged => (
                    "Truck",
                    trucks[index % trucks.Count].Id,
                    $"Truck {trucks[index % trucks.Count].RegistrationNumber} status updated"),
                ActivityType.InvoiceCreated => (
                    "Invoice",
                    invoices[index % invoices.Count].Id,
                    $"Invoice {invoices[index % invoices.Count].InvoiceNumber} created"),
                ActivityType.InvoicePaid => (
                    "Invoice",
                    invoices[index % invoices.Count].Id,
                    $"Payment recorded for {invoices[index % invoices.Count].InvoiceNumber}"),
                ActivityType.ExpenseRecorded => (
                    "Expense",
                    expenses[index % expenses.Count].Id,
                    $"{ToDisplayName(expenses[index % expenses.Count].Category)} expense recorded"),
                ActivityType.DocumentUploaded => (
                    "Document",
                    documents[index % documents.Count].Id,
                    $"{documents[index % documents.Count].Name} uploaded"),
                _ => (
                    "Document",
                    documents[index % documents.Count].Id,
                    $"{documents[index % documents.Count].Name} requires attention")
            };

            return new ActivityLog
            {
                Id = Id(8, index + 1),
                Type = type,
                Message = message,
                EntityType = entityType,
                EntityId = entityId,
                OccurredAtUtc = SeedTimestamp.AddHours(-index * 9),
                CreatedAtUtc = SeedTimestamp.AddHours(-index * 9),
                UpdatedAtUtc = SeedTimestamp.AddHours(-index * 9)
            };
        }).ToArray();
    }

    private static CompanyProfile CreateCompanyProfile() =>
        new()
        {
            Id = Id(9, 1),
            CompanyName = "Northstar Demo Logistics",
            LegalName = "Northstar Demo Logistics d.o.o.",
            Email = "operations@northstar-demo.example",
            Phone = "+389 2 555 0140",
            Address = "42 Fictional Freight Avenue",
            City = "Skopje",
            Country = "North Macedonia",
            DefaultCurrency = "EUR",
            TimeZone = "Europe/Skopje",
            VatNumber = "MK-DEMO-402001",
            InvoicePrefix = "IT",
            PaymentTermsDays = 30,
            CreatedAtUtc = SeedTimestamp.AddDays(-900),
            UpdatedAtUtc = SeedTimestamp
        };

    private static Guid Id(int group, int index) =>
        Guid.Parse($"00000000-0000-{group:0000}-0000-{index:000000000000}");

    private static string ToDisplayName<TEnum>(TEnum value)
        where TEnum : struct, Enum =>
        string.Concat(
            value.ToString().Select((character, index) =>
                index > 0 && char.IsUpper(character)
                    ? $" {character}"
                    : character.ToString()));
}
