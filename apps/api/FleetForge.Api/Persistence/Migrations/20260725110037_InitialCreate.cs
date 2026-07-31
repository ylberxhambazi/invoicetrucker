using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FleetForge.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Message = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    EntityType = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    EntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    OccurredAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    ContactPerson = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Country = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    Phone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Phone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    LicenceNumber = table.Column<string>(type: "character varying(48)", maxLength: 48, nullable: false),
                    LicenceExpiration = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CompletedTrips = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                    table.CheckConstraint("CK_Drivers_CompletedTrips", "\"CompletedTrips\" >= 0");
                });

            migrationBuilder.CreateTable(
                name: "NewsletterSubscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: true),
                    FleetSize = table.Column<int>(type: "integer", nullable: true),
                    SubscribedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterSubscribers", x => x.Id);
                    table.CheckConstraint("CK_NewsletterSubscribers_FleetSize", "\"FleetSize\" IS NULL OR \"FleetSize\" > 0");
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Make = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Model = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CurrentMileage = table.Column<long>(type: "bigint", nullable: false),
                    InsuranceExpiration = table.Column<DateOnly>(type: "date", nullable: false),
                    TechnicalInspectionExpiration = table.Column<DateOnly>(type: "date", nullable: false),
                    AssignedDriverId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.CheckConstraint("CK_Trucks_CurrentMileage", "\"CurrentMileage\" >= 0");
                    table.CheckConstraint("CK_Trucks_Year", "\"Year\" BETWEEN 1990 AND 2100");
                    table.ForeignKey(
                        name: "FK_Trucks_Drivers_AssignedDriverId",
                        column: x => x.AssignedDriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    IssuedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    TruckId = table.Column<Guid>(type: "uuid", nullable: true),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Documents_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Documents_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    TruckId = table.Column<Guid>(type: "uuid", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Supplier = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.CheckConstraint("CK_Expenses_Amount", "\"Amount\" > 0");
                    table.ForeignKey(
                        name: "FK_Expenses_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    TruckId = table.Column<Guid>(type: "uuid", nullable: true),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    Status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    PaidAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Notes = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.CheckConstraint("CK_Invoices_DueDate", "\"DueDate\" >= \"IssueDate\"");
                    table.CheckConstraint("CK_Invoices_TotalAmount", "\"TotalAmount\" >= 0");
                    table.ForeignKey(
                        name: "FK_Invoices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "character varying(240)", maxLength: 240, nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    LineTotal = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.CheckConstraint("CK_InvoiceItems_LineTotal", "\"LineTotal\" >= 0");
                    table.CheckConstraint("CK_InvoiceItems_Quantity", "\"Quantity\" > 0");
                    table.CheckConstraint("CK_InvoiceItems_UnitPrice", "\"UnitPrice\" >= 0");
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ActivityLogs",
                columns: new[] { "Id", "CreatedAtUtc", "EntityId", "EntityType", "Message", "OccurredAtUtc", "Type", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0008-0000-000000000001"), new DateTime(2026, 7, 25, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0001-0000-000000000001"), "Truck", "Truck FF-DEMO-001 status updated", new DateTime(2026, 7, 25, 8, 0, 0, 0, DateTimeKind.Utc), "TruckStatusChanged", new DateTime(2026, 7, 25, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000002"), new DateTime(2026, 7, 24, 23, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000002"), "Invoice", "Invoice FF-2002 created", new DateTime(2026, 7, 24, 23, 0, 0, 0, DateTimeKind.Utc), "InvoiceCreated", new DateTime(2026, 7, 24, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000003"), new DateTime(2026, 7, 24, 14, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000003"), "Invoice", "Payment recorded for FF-2003", new DateTime(2026, 7, 24, 14, 0, 0, 0, DateTimeKind.Utc), "InvoicePaid", new DateTime(2026, 7, 24, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000004"), new DateTime(2026, 7, 24, 5, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0006-0000-000000000004"), "Expense", "Insurance expense recorded", new DateTime(2026, 7, 24, 5, 0, 0, 0, DateTimeKind.Utc), "ExpenseRecorded", new DateTime(2026, 7, 24, 5, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000005"), new DateTime(2026, 7, 23, 20, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000005"), "Document", "Contract document uploaded", new DateTime(2026, 7, 23, 20, 0, 0, 0, DateTimeKind.Utc), "DocumentUploaded", new DateTime(2026, 7, 23, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000006"), new DateTime(2026, 7, 23, 11, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000006"), "Document", "Insurance document requires attention", new DateTime(2026, 7, 23, 11, 0, 0, 0, DateTimeKind.Utc), "DocumentExpiring", new DateTime(2026, 7, 23, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000007"), new DateTime(2026, 7, 23, 2, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0001-0000-000000000007"), "Truck", "Truck FF-DEMO-007 status updated", new DateTime(2026, 7, 23, 2, 0, 0, 0, DateTimeKind.Utc), "TruckStatusChanged", new DateTime(2026, 7, 23, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000008"), new DateTime(2026, 7, 22, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000008"), "Invoice", "Invoice FF-2008 created", new DateTime(2026, 7, 22, 17, 0, 0, 0, DateTimeKind.Utc), "InvoiceCreated", new DateTime(2026, 7, 22, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000009"), new DateTime(2026, 7, 22, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000009"), "Invoice", "Payment recorded for FF-2009", new DateTime(2026, 7, 22, 8, 0, 0, 0, DateTimeKind.Utc), "InvoicePaid", new DateTime(2026, 7, 22, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000010"), new DateTime(2026, 7, 21, 23, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0006-0000-000000000010"), "Expense", "Maintenance expense recorded", new DateTime(2026, 7, 21, 23, 0, 0, 0, DateTimeKind.Utc), "ExpenseRecorded", new DateTime(2026, 7, 21, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000011"), new DateTime(2026, 7, 21, 14, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000011"), "Document", "Insurance document uploaded", new DateTime(2026, 7, 21, 14, 0, 0, 0, DateTimeKind.Utc), "DocumentUploaded", new DateTime(2026, 7, 21, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000012"), new DateTime(2026, 7, 21, 5, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000012"), "Document", "Vehicle Registration document requires attention", new DateTime(2026, 7, 21, 5, 0, 0, 0, DateTimeKind.Utc), "DocumentExpiring", new DateTime(2026, 7, 21, 5, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000013"), new DateTime(2026, 7, 20, 20, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0001-0000-000000000013"), "Truck", "Truck FF-DEMO-013 status updated", new DateTime(2026, 7, 20, 20, 0, 0, 0, DateTimeKind.Utc), "TruckStatusChanged", new DateTime(2026, 7, 20, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000014"), new DateTime(2026, 7, 20, 11, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000014"), "Invoice", "Invoice FF-2014 created", new DateTime(2026, 7, 20, 11, 0, 0, 0, DateTimeKind.Utc), "InvoiceCreated", new DateTime(2026, 7, 20, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000015"), new DateTime(2026, 7, 20, 2, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000015"), "Invoice", "Payment recorded for FF-2015", new DateTime(2026, 7, 20, 2, 0, 0, 0, DateTimeKind.Utc), "InvoicePaid", new DateTime(2026, 7, 20, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000016"), new DateTime(2026, 7, 19, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0006-0000-000000000016"), "Expense", "Toll expense recorded", new DateTime(2026, 7, 19, 17, 0, 0, 0, DateTimeKind.Utc), "ExpenseRecorded", new DateTime(2026, 7, 19, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000017"), new DateTime(2026, 7, 19, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000017"), "Document", "Vehicle Registration document uploaded", new DateTime(2026, 7, 19, 8, 0, 0, 0, DateTimeKind.Utc), "DocumentUploaded", new DateTime(2026, 7, 19, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000018"), new DateTime(2026, 7, 18, 23, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000018"), "Document", "Technical Inspection document requires attention", new DateTime(2026, 7, 18, 23, 0, 0, 0, DateTimeKind.Utc), "DocumentExpiring", new DateTime(2026, 7, 18, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000019"), new DateTime(2026, 7, 18, 14, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0001-0000-000000000004"), "Truck", "Truck FF-DEMO-004 status updated", new DateTime(2026, 7, 18, 14, 0, 0, 0, DateTimeKind.Utc), "TruckStatusChanged", new DateTime(2026, 7, 18, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000020"), new DateTime(2026, 7, 18, 5, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000020"), "Invoice", "Invoice FF-2020 created", new DateTime(2026, 7, 18, 5, 0, 0, 0, DateTimeKind.Utc), "InvoiceCreated", new DateTime(2026, 7, 18, 5, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000021"), new DateTime(2026, 7, 17, 20, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000021"), "Invoice", "Payment recorded for FF-2021", new DateTime(2026, 7, 17, 20, 0, 0, 0, DateTimeKind.Utc), "InvoicePaid", new DateTime(2026, 7, 17, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000022"), new DateTime(2026, 7, 17, 11, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0006-0000-000000000022"), "Expense", "Fuel expense recorded", new DateTime(2026, 7, 17, 11, 0, 0, 0, DateTimeKind.Utc), "ExpenseRecorded", new DateTime(2026, 7, 17, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000023"), new DateTime(2026, 7, 17, 2, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000023"), "Document", "Technical Inspection document uploaded", new DateTime(2026, 7, 17, 2, 0, 0, 0, DateTimeKind.Utc), "DocumentUploaded", new DateTime(2026, 7, 17, 2, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000024"), new DateTime(2026, 7, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000024"), "Document", "Driver Licence document requires attention", new DateTime(2026, 7, 16, 17, 0, 0, 0, DateTimeKind.Utc), "DocumentExpiring", new DateTime(2026, 7, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000025"), new DateTime(2026, 7, 16, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0001-0000-000000000010"), "Truck", "Truck FF-DEMO-010 status updated", new DateTime(2026, 7, 16, 8, 0, 0, 0, DateTimeKind.Utc), "TruckStatusChanged", new DateTime(2026, 7, 16, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000026"), new DateTime(2026, 7, 15, 23, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000026"), "Invoice", "Invoice FF-2026 created", new DateTime(2026, 7, 15, 23, 0, 0, 0, DateTimeKind.Utc), "InvoiceCreated", new DateTime(2026, 7, 15, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000027"), new DateTime(2026, 7, 15, 14, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0004-0000-000000000027"), "Invoice", "Payment recorded for FF-2027", new DateTime(2026, 7, 15, 14, 0, 0, 0, DateTimeKind.Utc), "InvoicePaid", new DateTime(2026, 7, 15, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000028"), new DateTime(2026, 7, 15, 5, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0006-0000-000000000028"), "Expense", "Other expense recorded", new DateTime(2026, 7, 15, 5, 0, 0, 0, DateTimeKind.Utc), "ExpenseRecorded", new DateTime(2026, 7, 15, 5, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000029"), new DateTime(2026, 7, 14, 20, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000004"), "Document", "Driver Licence document uploaded", new DateTime(2026, 7, 14, 20, 0, 0, 0, DateTimeKind.Utc), "DocumentUploaded", new DateTime(2026, 7, 14, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0008-0000-000000000030"), new DateTime(2026, 7, 14, 11, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0007-0000-000000000005"), "Document", "Contract document requires attention", new DateTime(2026, 7, 14, 11, 0, 0, 0, DateTimeKind.Utc), "DocumentExpiring", new DateTime(2026, 7, 14, 11, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "CompanyName", "ContactPerson", "Country", "CreatedAtUtc", "Email", "IsActive", "Phone", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0003-0000-000000000001"), "Northlane Foods", "Sofia Marin", "Germany", new DateTime(2024, 12, 2, 8, 0, 0, 0, DateTimeKind.Utc), "billing01@client.fleetforge.example", false, "+389 71 440 201", new DateTime(2026, 7, 25, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000002"), "Aster Retail", "Leon Haas", "Austria", new DateTime(2024, 12, 10, 8, 0, 0, 0, DateTimeKind.Utc), "billing02@client.fleetforge.example", true, "+389 71 440 202", new DateTime(2026, 7, 23, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000003"), "Cobalt Works", "Eva Korhonen", "Finland", new DateTime(2024, 12, 18, 8, 0, 0, 0, DateTimeKind.Utc), "billing03@client.fleetforge.example", true, "+389 71 440 203", new DateTime(2026, 7, 21, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000004"), "Silverpine Home", "Luka Venn", "Croatia", new DateTime(2024, 12, 26, 8, 0, 0, 0, DateTimeKind.Utc), "billing04@client.fleetforge.example", true, "+389 71 440 204", new DateTime(2026, 7, 19, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000005"), "Meridian Parts", "Anika Solberg", "Netherlands", new DateTime(2025, 1, 3, 8, 0, 0, 0, DateTimeKind.Utc), "billing05@client.fleetforge.example", true, "+389 71 440 205", new DateTime(2026, 7, 17, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000006"), "Brightfield Produce", "Teo Marku", "North Macedonia", new DateTime(2025, 1, 11, 8, 0, 0, 0, DateTimeKind.Utc), "billing06@client.fleetforge.example", true, "+389 71 440 206", new DateTime(2026, 7, 15, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000007"), "Oakline Packaging", "Nora Vale", "Slovenia", new DateTime(2025, 1, 19, 8, 0, 0, 0, DateTimeKind.Utc), "billing07@client.fleetforge.example", true, "+389 71 440 207", new DateTime(2026, 7, 13, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000008"), "Harborlight Textiles", "Eli Danek", "Belgium", new DateTime(2025, 1, 27, 8, 0, 0, 0, DateTimeKind.Utc), "billing08@client.fleetforge.example", false, "+389 71 440 208", new DateTime(2026, 7, 11, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000009"), "Velora Components", "Mara Klein", "Germany", new DateTime(2025, 2, 4, 8, 0, 0, 0, DateTimeKind.Utc), "billing09@client.fleetforge.example", true, "+389 71 440 209", new DateTime(2026, 7, 9, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000010"), "Juniper Market", "Jonas Veld", "Netherlands", new DateTime(2025, 2, 12, 8, 0, 0, 0, DateTimeKind.Utc), "billing10@client.fleetforge.example", true, "+389 71 440 210", new DateTime(2026, 7, 7, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000011"), "Bluepeak Supplies", "Lena Orlov", "Sweden", new DateTime(2025, 2, 20, 8, 0, 0, 0, DateTimeKind.Utc), "billing11@client.fleetforge.example", true, "+389 71 440 211", new DateTime(2026, 7, 5, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000012"), "Stonepath Ceramics", "Ivo Kolar", "Czechia", new DateTime(2025, 2, 28, 8, 0, 0, 0, DateTimeKind.Utc), "billing12@client.fleetforge.example", true, "+389 71 440 212", new DateTime(2026, 7, 3, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000013"), "Amberline Trade", "Mia Soren", "Denmark", new DateTime(2025, 3, 8, 8, 0, 0, 0, DateTimeKind.Utc), "billing13@client.fleetforge.example", true, "+389 71 440 213", new DateTime(2026, 7, 1, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000014"), "Evertrail Goods", "Deni Pavel", "Poland", new DateTime(2025, 3, 16, 8, 0, 0, 0, DateTimeKind.Utc), "billing14@client.fleetforge.example", true, "+389 71 440 214", new DateTime(2026, 6, 29, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000015"), "Lumenridge Labs", "Sara Nordin", "Norway", new DateTime(2025, 3, 24, 8, 0, 0, 0, DateTimeKind.Utc), "billing15@client.fleetforge.example", false, "+389 71 440 215", new DateTime(2026, 6, 27, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000016"), "Redwood Fixtures", "Oren Iliev", "Serbia", new DateTime(2025, 4, 1, 8, 0, 0, 0, DateTimeKind.Utc), "billing16@client.fleetforge.example", true, "+389 71 440 216", new DateTime(2026, 6, 25, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000017"), "Greenwell Paper", "Kira Vos", "Slovenia", new DateTime(2025, 4, 9, 8, 0, 0, 0, DateTimeKind.Utc), "billing17@client.fleetforge.example", true, "+389 71 440 217", new DateTime(2026, 6, 23, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000018"), "Westford Equipment", "Niko Brandt", "Germany", new DateTime(2025, 4, 17, 8, 0, 0, 0, DateTimeKind.Utc), "billing18@client.fleetforge.example", true, "+389 71 440 218", new DateTime(2026, 6, 21, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000019"), "Clearbrook Foods", "Elin Marek", "Croatia", new DateTime(2025, 4, 25, 8, 0, 0, 0, DateTimeKind.Utc), "billing19@client.fleetforge.example", true, "+389 71 440 219", new DateTime(2026, 6, 19, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0003-0000-000000000020"), "Ironvale Furnishings", "Toma Vesel", "Austria", new DateTime(2025, 5, 3, 8, 0, 0, 0, DateTimeKind.Utc), "billing20@client.fleetforge.example", true, "+389 71 440 220", new DateTime(2026, 6, 17, 8, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "CompletedTrips", "CreatedAtUtc", "Email", "FullName", "LicenceExpiration", "LicenceNumber", "Phone", "Status", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0002-0000-000000000001"), 72, new DateTime(2025, 3, 12, 8, 0, 0, 0, DateTimeKind.Utc), "driver01@fleetforge.example", "Elian Voss", new DateOnly(2027, 2, 10), "FF-LIC-0001", "+389 70 555 101", "Available", new DateTime(2026, 7, 25, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000002"), 81, new DateTime(2025, 3, 19, 8, 0, 0, 0, DateTimeKind.Utc), "driver02@fleetforge.example", "Mira Daneva", new DateOnly(2028, 3, 11), "FF-LIC-0002", "+389 70 555 102", "OnRoute", new DateTime(2026, 7, 24, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000003"), 90, new DateTime(2025, 3, 26, 8, 0, 0, 0, DateTimeKind.Utc), "driver03@fleetforge.example", "Noah Petreski", new DateOnly(2029, 4, 12), "FF-LIC-0003", "+389 70 555 103", "OffDuty", new DateTime(2026, 7, 23, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000004"), 99, new DateTime(2025, 4, 2, 8, 0, 0, 0, DateTimeKind.Utc), "driver04@fleetforge.example", "Lina Moreau", new DateOnly(2027, 5, 13), "FF-LIC-0004", "+389 70 555 104", "Leave", new DateTime(2026, 7, 22, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000005"), 108, new DateTime(2025, 4, 9, 8, 0, 0, 0, DateTimeKind.Utc), "driver05@fleetforge.example", "Tomas Iliev", new DateOnly(2028, 6, 14), "FF-LIC-0005", "+389 70 555 105", "Available", new DateTime(2026, 7, 21, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000006"), 117, new DateTime(2025, 4, 16, 8, 0, 0, 0, DateTimeKind.Utc), "driver06@fleetforge.example", "Sora Lind", new DateOnly(2029, 7, 15), "FF-LIC-0006", "+389 70 555 106", "OnRoute", new DateTime(2026, 7, 20, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000007"), 126, new DateTime(2025, 4, 23, 8, 0, 0, 0, DateTimeKind.Utc), "driver07@fleetforge.example", "Adrian Vale", new DateOnly(2027, 8, 16), "FF-LIC-0007", "+389 70 555 107", "OffDuty", new DateTime(2026, 7, 19, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000008"), 135, new DateTime(2025, 4, 30, 8, 0, 0, 0, DateTimeKind.Utc), "driver08@fleetforge.example", "Nika Stojan", new DateOnly(2028, 9, 17), "FF-LIC-0008", "+389 70 555 108", "Leave", new DateTime(2026, 7, 18, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000009"), 144, new DateTime(2025, 5, 7, 8, 0, 0, 0, DateTimeKind.Utc), "driver09@fleetforge.example", "Milan Rohe", new DateOnly(2029, 10, 18), "FF-LIC-0009", "+389 70 555 109", "Available", new DateTime(2026, 7, 17, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000010"), 153, new DateTime(2025, 5, 14, 8, 0, 0, 0, DateTimeKind.Utc), "driver10@fleetforge.example", "Elena Varga", new DateOnly(2027, 2, 19), "FF-LIC-0010", "+389 70 555 110", "OnRoute", new DateTime(2026, 7, 16, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000011"), 162, new DateTime(2025, 5, 21, 8, 0, 0, 0, DateTimeKind.Utc), "driver11@fleetforge.example", "Dario Kelm", new DateOnly(2028, 3, 20), "FF-LIC-0011", "+389 70 555 111", "OffDuty", new DateTime(2026, 7, 15, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0002-0000-000000000012"), 171, new DateTime(2025, 5, 28, 8, 0, 0, 0, DateTimeKind.Utc), "driver12@fleetforge.example", "Iris Novak", new DateOnly(2029, 4, 21), "FF-LIC-0012", "+389 70 555 112", "Leave", new DateTime(2026, 7, 14, 8, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Category", "CreatedAtUtc", "Currency", "Date", "Description", "Supplier", "TruckId", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0006-0000-000000000001"), 190m, "Fuel", new DateTime(2025, 8, 3, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 8, 3), "Fuel expense — fictional record", "Fictional Fuel Network", null, new DateTime(2025, 8, 3, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000010"), 657m, "Maintenance", new DateTime(2026, 5, 12, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 12), "Maintenance expense — fictional record", "Apex Garage Demo", null, new DateTime(2026, 5, 12, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000019"), 889m, "Repair", new DateTime(2026, 2, 21, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 21), "Repair expense — fictional record", "Transit Repair Studio", null, new DateTime(2026, 2, 21, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000028"), 1121m, "Other", new DateTime(2025, 11, 8, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 8), "Other expense — fictional record", "Fleet Office Demo", null, new DateTime(2025, 11, 8, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000037"), 1353m, "Toll", new DateTime(2025, 8, 17, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 8, 17), "Toll expense — fictional record", "EuroRoute Demo Tolls", null, new DateTime(2025, 8, 17, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000046"), 1585m, "Insurance", new DateTime(2026, 5, 4, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 4), "Insurance expense — fictional record", "Northstar Insurance Demo", null, new DateTime(2026, 5, 4, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000055"), 2052m, "DriverExpense", new DateTime(2026, 2, 13, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 13), "Driver Expense expense — fictional record", "Driver Services Demo", null, new DateTime(2026, 2, 13, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000064"), 2284m, "Fuel", new DateTime(2025, 11, 22, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 22), "Fuel expense — fictional record", "Fictional Fuel Network", null, new DateTime(2025, 11, 22, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000073"), 2516m, "Maintenance", new DateTime(2025, 8, 9, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 8, 9), "Maintenance expense — fictional record", "Apex Garage Demo", null, new DateTime(2025, 8, 9, 7, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "Id", "AssignedDriverId", "CreatedAtUtc", "CurrentMileage", "InsuranceExpiration", "Make", "Model", "RegistrationNumber", "Status", "TechnicalInspectionExpiration", "UpdatedAtUtc", "Year" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0001-0000-000000000013"), null, new DateTime(2024, 12, 14, 8, 0, 0, 0, DateTimeKind.Utc), 481760L, new DateOnly(2026, 8, 20), "Iveco", "S-Way 490", "FF-DEMO-013", "Available", new DateOnly(2026, 9, 17), new DateTime(2026, 7, 13, 8, 0, 0, 0, DateTimeKind.Utc), 2021 },
                    { new Guid("00000000-0000-0001-0000-000000000014"), null, new DateTime(2024, 12, 25, 8, 0, 0, 0, DateTimeKind.Utc), 506490L, new DateOnly(2027, 9, 21), "Renault Trucks", "T 460", "FF-DEMO-014", "OnRoute", new DateOnly(2027, 10, 18), new DateTime(2026, 7, 12, 8, 0, 0, 0, DateTimeKind.Utc), 2022 },
                    { new Guid("00000000-0000-0001-0000-000000000015"), null, new DateTime(2025, 1, 5, 8, 0, 0, 0, DateTimeKind.Utc), 531220L, new DateOnly(2026, 10, 22), "Volvo", "FH 500", "FF-DEMO-015", "Maintenance", new DateOnly(2026, 11, 19), new DateTime(2026, 7, 11, 8, 0, 0, 0, DateTimeKind.Utc), 2023 }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "ClientId", "CreatedAtUtc", "DriverId", "ExpirationDate", "IssuedDate", "Name", "ReferenceNumber", "TruckId", "Type", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0007-0000-000000000004"), null, new DateTime(2025, 4, 10, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0002-0000-000000000004"), new DateOnly(2027, 11, 8), new DateOnly(2025, 4, 10), "Driver Licence document", "FF-DOC-0004", null, "DriverLicence", new DateTime(2026, 7, 22, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000005"), new Guid("00000000-0000-0003-0000-000000000005"), new DateTime(2025, 5, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2025, 5, 10), "Contract document", "FF-DOC-0005", null, "Contract", new DateTime(2026, 7, 21, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000009"), null, new DateTime(2025, 9, 10, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0002-0000-000000000009"), new DateOnly(2026, 8, 13), new DateOnly(2025, 9, 10), "Driver Licence document", "FF-DOC-0009", null, "DriverLicence", new DateTime(2026, 7, 17, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000010"), new Guid("00000000-0000-0003-0000-000000000010"), new DateTime(2025, 10, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2027, 9, 14), new DateOnly(2025, 10, 10), "Contract document", "FF-DOC-0010", null, "Contract", new DateTime(2026, 7, 16, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000013"), null, new DateTime(2025, 3, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2026, 8, 17), new DateOnly(2025, 3, 10), "Technical Inspection document", "FF-DOC-0013", new Guid("00000000-0000-0001-0000-000000000013"), "TechnicalInspection", new DateTime(2026, 7, 13, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000014"), null, new DateTime(2025, 4, 10, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0002-0000-000000000002"), new DateOnly(2027, 9, 18), new DateOnly(2025, 4, 10), "Driver Licence document", "FF-DOC-0014", null, "DriverLicence", new DateTime(2026, 7, 12, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000015"), new Guid("00000000-0000-0003-0000-000000000015"), new DateTime(2025, 5, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2025, 5, 10), "Contract document", "FF-DOC-0015", null, "Contract", new DateTime(2026, 7, 11, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000019"), null, new DateTime(2025, 9, 10, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0002-0000-000000000007"), new DateOnly(2026, 10, 23), new DateOnly(2025, 9, 10), "Driver Licence document", "FF-DOC-0019", null, "DriverLicence", new DateTime(2026, 7, 7, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000020"), new Guid("00000000-0000-0003-0000-000000000020"), new DateTime(2025, 10, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2027, 11, 24), new DateOnly(2025, 10, 10), "Contract document", "FF-DOC-0020", null, "Contract", new DateTime(2026, 7, 6, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000024"), null, new DateTime(2025, 4, 10, 8, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0002-0000-000000000012"), new DateOnly(2027, 11, 8), new DateOnly(2025, 4, 10), "Driver Licence document", "FF-DOC-0024", null, "DriverLicence", new DateTime(2026, 7, 2, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000025"), new Guid("00000000-0000-0003-0000-000000000005"), new DateTime(2025, 5, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2025, 5, 10), "Contract document", "FF-DOC-0025", null, "Contract", new DateTime(2026, 7, 1, 8, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Category", "CreatedAtUtc", "Currency", "Date", "Description", "Supplier", "TruckId", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0006-0000-000000000013"), 656m, "DriverExpense", new DateTime(2025, 8, 15, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 8, 15), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000013"), new DateTime(2025, 8, 15, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000014"), 734m, "Other", new DateTime(2025, 9, 16, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 16), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000014"), new DateTime(2025, 9, 16, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000015"), 812m, "Fuel", new DateTime(2025, 10, 17, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 17), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000015"), new DateTime(2025, 10, 17, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000029"), 1199m, "Fuel", new DateTime(2025, 12, 9, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 9), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000014"), new DateTime(2025, 12, 9, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000030"), 1277m, "Toll", new DateTime(2026, 1, 10, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 10), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000015"), new DateTime(2026, 1, 10, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000043"), 1586m, "Fuel", new DateTime(2026, 2, 23, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 23), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000013"), new DateTime(2026, 2, 23, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000044"), 1664m, "Toll", new DateTime(2026, 3, 24, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 24), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000014"), new DateTime(2026, 3, 24, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000045"), 1742m, "Maintenance", new DateTime(2026, 4, 3, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 3), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000015"), new DateTime(2026, 4, 3, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000058"), 2051m, "Toll", new DateTime(2026, 5, 16, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 16), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000013"), new DateTime(2026, 5, 16, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000059"), 2129m, "Maintenance", new DateTime(2026, 6, 17, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 17), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000014"), new DateTime(2026, 6, 17, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000060"), 2207m, "Insurance", new DateTime(2026, 7, 18, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 18), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000015"), new DateTime(2026, 7, 18, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000074"), 2594m, "Insurance", new DateTime(2025, 9, 10, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 10), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000014"), new DateTime(2025, 9, 10, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000075"), 2672m, "Repair", new DateTime(2025, 10, 11, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 11), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000015"), new DateTime(2025, 10, 11, 7, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "ClientId", "CreatedAtUtc", "Currency", "DueDate", "InvoiceNumber", "IssueDate", "Notes", "PaidAtUtc", "Status", "TotalAmount", "TruckId", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0004-0000-000000000013"), new Guid("00000000-0000-0003-0000-000000000013"), new DateTime(2025, 8, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 7), "FF-2013", new DateOnly(2025, 8, 8), "Fictional transport services for the FleetForge demonstration.", new DateTime(2025, 8, 26, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 3494m, new Guid("00000000-0000-0001-0000-000000000013"), new DateTime(2025, 8, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000014"), new Guid("00000000-0000-0003-0000-000000000014"), new DateTime(2025, 9, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 8), "FF-2014", new DateOnly(2025, 9, 8), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 3674m, new Guid("00000000-0000-0001-0000-000000000014"), new DateTime(2025, 9, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000015"), new Guid("00000000-0000-0003-0000-000000000015"), new DateTime(2025, 10, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 7), "FF-2015", new DateOnly(2025, 10, 8), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 3854m, new Guid("00000000-0000-0001-0000-000000000015"), new DateTime(2025, 10, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000028"), new Guid("00000000-0000-0003-0000-000000000008"), new DateTime(2025, 11, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 13), "FF-2028", new DateOnly(2025, 11, 13), "Fictional transport services for the FleetForge demonstration.", new DateTime(2025, 12, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 5678m, new Guid("00000000-0000-0001-0000-000000000013"), new DateTime(2025, 11, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000029"), new Guid("00000000-0000-0003-0000-000000000009"), new DateTime(2025, 12, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 12), "FF-2029", new DateOnly(2025, 12, 13), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 5858m, new Guid("00000000-0000-0001-0000-000000000014"), new DateTime(2025, 12, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000030"), new Guid("00000000-0000-0003-0000-000000000010"), new DateTime(2026, 1, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 12), "FF-2030", new DateOnly(2026, 1, 13), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 6038m, new Guid("00000000-0000-0001-0000-000000000015"), new DateTime(2026, 1, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000043"), new Guid("00000000-0000-0003-0000-000000000003"), new DateTime(2026, 2, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 20), "FF-2043", new DateOnly(2026, 2, 18), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 3, 8, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 7862m, new Guid("00000000-0000-0001-0000-000000000013"), new DateTime(2026, 2, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000044"), new Guid("00000000-0000-0003-0000-000000000004"), new DateTime(2026, 3, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 17), "FF-2044", new DateOnly(2026, 3, 18), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 8042m, new Guid("00000000-0000-0001-0000-000000000014"), new DateTime(2026, 3, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000045"), new Guid("00000000-0000-0003-0000-000000000005"), new DateTime(2026, 4, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 18), "FF-2045", new DateOnly(2026, 4, 18), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 8222m, new Guid("00000000-0000-0001-0000-000000000015"), new DateTime(2026, 4, 21, 9, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Trucks",
                columns: new[] { "Id", "AssignedDriverId", "CreatedAtUtc", "CurrentMileage", "InsuranceExpiration", "Make", "Model", "RegistrationNumber", "Status", "TechnicalInspectionExpiration", "UpdatedAtUtc", "Year" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0001-0000-000000000001"), new Guid("00000000-0000-0002-0000-000000000001"), new DateTime(2024, 8, 4, 8, 0, 0, 0, DateTimeKind.Utc), 185000L, new DateOnly(2026, 8, 8), "Volvo", "FH 460", "FF-DEMO-001", "Available", new DateOnly(2026, 9, 5), new DateTime(2026, 7, 25, 8, 0, 0, 0, DateTimeKind.Utc), 2017 },
                    { new Guid("00000000-0000-0001-0000-000000000002"), new Guid("00000000-0000-0002-0000-000000000002"), new DateTime(2024, 8, 15, 8, 0, 0, 0, DateTimeKind.Utc), 209730L, new DateOnly(2027, 9, 9), "Scania", "R 450", "FF-DEMO-002", "OnRoute", new DateOnly(2027, 10, 6), new DateTime(2026, 7, 24, 8, 0, 0, 0, DateTimeKind.Utc), 2018 },
                    { new Guid("00000000-0000-0001-0000-000000000003"), new Guid("00000000-0000-0002-0000-000000000003"), new DateTime(2024, 8, 26, 8, 0, 0, 0, DateTimeKind.Utc), 234460L, new DateOnly(2026, 10, 10), "Mercedes-Benz", "Actros 1845", "FF-DEMO-003", "Maintenance", new DateOnly(2026, 11, 7), new DateTime(2026, 7, 23, 8, 0, 0, 0, DateTimeKind.Utc), 2019 },
                    { new Guid("00000000-0000-0001-0000-000000000004"), new Guid("00000000-0000-0002-0000-000000000004"), new DateTime(2024, 9, 6, 8, 0, 0, 0, DateTimeKind.Utc), 259190L, new DateOnly(2027, 11, 11), "DAF", "XF 480", "FF-DEMO-004", "OutOfService", new DateOnly(2027, 9, 8), new DateTime(2026, 7, 22, 8, 0, 0, 0, DateTimeKind.Utc), 2020 },
                    { new Guid("00000000-0000-0001-0000-000000000005"), new Guid("00000000-0000-0002-0000-000000000005"), new DateTime(2024, 9, 17, 8, 0, 0, 0, DateTimeKind.Utc), 283920L, new DateOnly(2026, 8, 12), "MAN", "TGX 18.470", "FF-DEMO-005", "Available", new DateOnly(2026, 10, 9), new DateTime(2026, 7, 21, 8, 0, 0, 0, DateTimeKind.Utc), 2021 },
                    { new Guid("00000000-0000-0001-0000-000000000006"), new Guid("00000000-0000-0002-0000-000000000006"), new DateTime(2024, 9, 28, 8, 0, 0, 0, DateTimeKind.Utc), 308650L, new DateOnly(2027, 9, 13), "Iveco", "S-Way 460", "FF-DEMO-006", "OnRoute", new DateOnly(2027, 11, 10), new DateTime(2026, 7, 20, 8, 0, 0, 0, DateTimeKind.Utc), 2022 },
                    { new Guid("00000000-0000-0001-0000-000000000007"), new Guid("00000000-0000-0002-0000-000000000007"), new DateTime(2024, 10, 9, 8, 0, 0, 0, DateTimeKind.Utc), 333380L, new DateOnly(2026, 10, 14), "Renault Trucks", "T High 480", "FF-DEMO-007", "Maintenance", new DateOnly(2026, 9, 11), new DateTime(2026, 7, 19, 8, 0, 0, 0, DateTimeKind.Utc), 2023 },
                    { new Guid("00000000-0000-0001-0000-000000000008"), new Guid("00000000-0000-0002-0000-000000000008"), new DateTime(2024, 10, 20, 8, 0, 0, 0, DateTimeKind.Utc), 358110L, new DateOnly(2027, 11, 15), "Volvo", "FM 420", "FF-DEMO-008", "OutOfService", new DateOnly(2027, 10, 12), new DateTime(2026, 7, 18, 8, 0, 0, 0, DateTimeKind.Utc), 2024 },
                    { new Guid("00000000-0000-0001-0000-000000000009"), new Guid("00000000-0000-0002-0000-000000000009"), new DateTime(2024, 10, 31, 8, 0, 0, 0, DateTimeKind.Utc), 382840L, new DateOnly(2026, 8, 16), "Scania", "S 500", "FF-DEMO-009", "Available", new DateOnly(2026, 11, 13), new DateTime(2026, 7, 17, 8, 0, 0, 0, DateTimeKind.Utc), 2017 },
                    { new Guid("00000000-0000-0001-0000-000000000010"), new Guid("00000000-0000-0002-0000-000000000010"), new DateTime(2024, 11, 11, 8, 0, 0, 0, DateTimeKind.Utc), 407570L, new DateOnly(2027, 9, 17), "DAF", "XG 480", "FF-DEMO-010", "OnRoute", new DateOnly(2027, 9, 14), new DateTime(2026, 7, 16, 8, 0, 0, 0, DateTimeKind.Utc), 2018 },
                    { new Guid("00000000-0000-0001-0000-000000000011"), new Guid("00000000-0000-0002-0000-000000000011"), new DateTime(2024, 11, 22, 8, 0, 0, 0, DateTimeKind.Utc), 432300L, new DateOnly(2026, 10, 18), "MAN", "TGX 18.510", "FF-DEMO-011", "Maintenance", new DateOnly(2026, 10, 15), new DateTime(2026, 7, 15, 8, 0, 0, 0, DateTimeKind.Utc), 2019 },
                    { new Guid("00000000-0000-0001-0000-000000000012"), new Guid("00000000-0000-0002-0000-000000000012"), new DateTime(2024, 12, 3, 8, 0, 0, 0, DateTimeKind.Utc), 457030L, new DateOnly(2027, 11, 19), "Mercedes-Benz", "Actros 1848", "FF-DEMO-012", "OutOfService", new DateOnly(2027, 11, 16), new DateTime(2026, 7, 14, 8, 0, 0, 0, DateTimeKind.Utc), 2020 }
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "Id", "ClientId", "CreatedAtUtc", "DriverId", "ExpirationDate", "IssuedDate", "Name", "ReferenceNumber", "TruckId", "Type", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0007-0000-000000000001"), null, new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2026, 8, 5), new DateOnly(2025, 1, 10), "Insurance document", "FF-DOC-0001", new Guid("00000000-0000-0001-0000-000000000001"), "Insurance", new DateTime(2026, 7, 25, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000002"), null, new DateTime(2025, 2, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2027, 9, 6), new DateOnly(2025, 2, 10), "Vehicle Registration document", "FF-DOC-0002", new Guid("00000000-0000-0001-0000-000000000002"), "VehicleRegistration", new DateTime(2026, 7, 24, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000003"), null, new DateTime(2025, 3, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2026, 10, 7), new DateOnly(2025, 3, 10), "Technical Inspection document", "FF-DOC-0003", new Guid("00000000-0000-0001-0000-000000000003"), "TechnicalInspection", new DateTime(2026, 7, 23, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000006"), null, new DateTime(2025, 6, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2027, 9, 10), new DateOnly(2025, 6, 10), "Insurance document", "FF-DOC-0006", new Guid("00000000-0000-0001-0000-000000000006"), "Insurance", new DateTime(2026, 7, 20, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000007"), null, new DateTime(2025, 7, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2026, 10, 11), new DateOnly(2025, 7, 10), "Vehicle Registration document", "FF-DOC-0007", new Guid("00000000-0000-0001-0000-000000000007"), "VehicleRegistration", new DateTime(2026, 7, 19, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000008"), null, new DateTime(2025, 8, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2027, 11, 12), new DateOnly(2025, 8, 10), "Technical Inspection document", "FF-DOC-0008", new Guid("00000000-0000-0001-0000-000000000008"), "TechnicalInspection", new DateTime(2026, 7, 18, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000011"), null, new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2026, 10, 15), new DateOnly(2025, 1, 10), "Insurance document", "FF-DOC-0011", new Guid("00000000-0000-0001-0000-000000000011"), "Insurance", new DateTime(2026, 7, 15, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000012"), null, new DateTime(2025, 2, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2027, 11, 16), new DateOnly(2025, 2, 10), "Vehicle Registration document", "FF-DOC-0012", new Guid("00000000-0000-0001-0000-000000000012"), "VehicleRegistration", new DateTime(2026, 7, 14, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000016"), null, new DateTime(2025, 6, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2027, 11, 20), new DateOnly(2025, 6, 10), "Insurance document", "FF-DOC-0016", new Guid("00000000-0000-0001-0000-000000000001"), "Insurance", new DateTime(2026, 7, 10, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000017"), null, new DateTime(2025, 7, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2026, 8, 21), new DateOnly(2025, 7, 10), "Vehicle Registration document", "FF-DOC-0017", new Guid("00000000-0000-0001-0000-000000000002"), "VehicleRegistration", new DateTime(2026, 7, 9, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000018"), null, new DateTime(2025, 8, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2027, 9, 22), new DateOnly(2025, 8, 10), "Technical Inspection document", "FF-DOC-0018", new Guid("00000000-0000-0001-0000-000000000003"), "TechnicalInspection", new DateTime(2026, 7, 8, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000021"), null, new DateTime(2025, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2026, 8, 5), new DateOnly(2025, 1, 10), "Insurance document", "FF-DOC-0021", new Guid("00000000-0000-0001-0000-000000000006"), "Insurance", new DateTime(2026, 7, 5, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000022"), null, new DateTime(2025, 2, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2027, 9, 6), new DateOnly(2025, 2, 10), "Vehicle Registration document", "FF-DOC-0022", new Guid("00000000-0000-0001-0000-000000000007"), "VehicleRegistration", new DateTime(2026, 7, 4, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0007-0000-000000000023"), null, new DateTime(2025, 3, 10, 8, 0, 0, 0, DateTimeKind.Utc), null, new DateOnly(2026, 10, 7), new DateOnly(2025, 3, 10), "Technical Inspection document", "FF-DOC-0023", new Guid("00000000-0000-0001-0000-000000000008"), "TechnicalInspection", new DateTime(2026, 7, 3, 8, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Category", "CreatedAtUtc", "Currency", "Date", "Description", "Supplier", "TruckId", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0006-0000-000000000002"), 268m, "Toll", new DateTime(2025, 9, 4, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 4), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2025, 9, 4, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000003"), 346m, "Maintenance", new DateTime(2025, 10, 5, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 5), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2025, 10, 5, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000004"), 424m, "Insurance", new DateTime(2025, 11, 6, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 6), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000004"), new DateTime(2025, 11, 6, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000005"), 502m, "Repair", new DateTime(2025, 12, 7, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 7), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2025, 12, 7, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000006"), 345m, "DriverExpense", new DateTime(2026, 1, 8, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 8), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000006"), new DateTime(2026, 1, 8, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000007"), 423m, "Other", new DateTime(2026, 2, 9, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 9), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000007"), new DateTime(2026, 2, 9, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000008"), 501m, "Fuel", new DateTime(2026, 3, 10, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 10), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000008"), new DateTime(2026, 3, 10, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000009"), 579m, "Toll", new DateTime(2026, 4, 11, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 11), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000009"), new DateTime(2026, 4, 11, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000011"), 500m, "Insurance", new DateTime(2026, 6, 13, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 13), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000011"), new DateTime(2026, 6, 13, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000012"), 578m, "Repair", new DateTime(2026, 7, 14, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 14), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000012"), new DateTime(2026, 7, 14, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000016"), 655m, "Toll", new DateTime(2025, 11, 18, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 18), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000001"), new DateTime(2025, 11, 18, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000017"), 733m, "Maintenance", new DateTime(2025, 12, 19, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 19), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2025, 12, 19, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000018"), 811m, "Insurance", new DateTime(2026, 1, 20, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 20), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2026, 1, 20, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000020"), 967m, "DriverExpense", new DateTime(2026, 3, 22, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 22), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2026, 3, 22, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000021"), 810m, "Other", new DateTime(2026, 4, 23, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 23), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000006"), new DateTime(2026, 4, 23, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000022"), 888m, "Fuel", new DateTime(2026, 5, 24, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 24), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000007"), new DateTime(2026, 5, 24, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000023"), 966m, "Toll", new DateTime(2026, 6, 3, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 3), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000008"), new DateTime(2026, 6, 3, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000024"), 1044m, "Maintenance", new DateTime(2026, 7, 4, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 4), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000009"), new DateTime(2026, 7, 4, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000025"), 1122m, "Insurance", new DateTime(2025, 8, 5, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 8, 5), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000010"), new DateTime(2025, 8, 5, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000026"), 965m, "Repair", new DateTime(2025, 9, 6, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 6), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000011"), new DateTime(2025, 9, 6, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000027"), 1043m, "DriverExpense", new DateTime(2025, 10, 7, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 7), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000012"), new DateTime(2025, 10, 7, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000031"), 1120m, "Maintenance", new DateTime(2026, 2, 11, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 11), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000001"), new DateTime(2026, 2, 11, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000032"), 1198m, "Insurance", new DateTime(2026, 3, 12, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 12), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2026, 3, 12, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000033"), 1276m, "Repair", new DateTime(2026, 4, 13, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 13), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2026, 4, 13, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000034"), 1354m, "DriverExpense", new DateTime(2026, 5, 14, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 14), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000004"), new DateTime(2026, 5, 14, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000035"), 1432m, "Other", new DateTime(2026, 6, 15, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 15), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2026, 6, 15, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000036"), 1275m, "Fuel", new DateTime(2026, 7, 16, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 16), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000006"), new DateTime(2026, 7, 16, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000038"), 1431m, "Maintenance", new DateTime(2025, 9, 18, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 18), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000008"), new DateTime(2025, 9, 18, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000039"), 1509m, "Insurance", new DateTime(2025, 10, 19, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 19), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000009"), new DateTime(2025, 10, 19, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000040"), 1587m, "Repair", new DateTime(2025, 11, 20, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 20), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000010"), new DateTime(2025, 11, 20, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000041"), 1430m, "DriverExpense", new DateTime(2025, 12, 21, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 21), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000011"), new DateTime(2025, 12, 21, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000042"), 1508m, "Other", new DateTime(2026, 1, 22, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 22), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000012"), new DateTime(2026, 1, 22, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000047"), 1663m, "Repair", new DateTime(2026, 6, 5, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 5), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2026, 6, 5, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000048"), 1741m, "DriverExpense", new DateTime(2026, 7, 6, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 6), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2026, 7, 6, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000049"), 1819m, "Other", new DateTime(2025, 8, 7, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 8, 7), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000004"), new DateTime(2025, 8, 7, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000050"), 1897m, "Fuel", new DateTime(2025, 9, 8, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 8), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2025, 9, 8, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000051"), 1740m, "Toll", new DateTime(2025, 10, 9, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 9), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000006"), new DateTime(2025, 10, 9, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000052"), 1818m, "Maintenance", new DateTime(2025, 11, 10, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 10), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000007"), new DateTime(2025, 11, 10, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000053"), 1896m, "Insurance", new DateTime(2025, 12, 11, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 11), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000008"), new DateTime(2025, 12, 11, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000054"), 1974m, "Repair", new DateTime(2026, 1, 12, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 12), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000009"), new DateTime(2026, 1, 12, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000056"), 1895m, "Other", new DateTime(2026, 3, 14, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 14), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000011"), new DateTime(2026, 3, 14, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000057"), 1973m, "Fuel", new DateTime(2026, 4, 15, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 15), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000012"), new DateTime(2026, 4, 15, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000061"), 2050m, "Repair", new DateTime(2025, 8, 19, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 8, 19), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000001"), new DateTime(2025, 8, 19, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000062"), 2128m, "DriverExpense", new DateTime(2025, 9, 20, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 20), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2025, 9, 20, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000063"), 2206m, "Other", new DateTime(2025, 10, 21, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 21), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2025, 10, 21, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000065"), 2362m, "Toll", new DateTime(2025, 12, 23, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 23), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2025, 12, 23, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000066"), 2205m, "Maintenance", new DateTime(2026, 1, 24, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 24), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000006"), new DateTime(2026, 1, 24, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000067"), 2283m, "Insurance", new DateTime(2026, 2, 3, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 3), "Insurance expense — fictional record", "Northstar Insurance Demo", new Guid("00000000-0000-0001-0000-000000000007"), new DateTime(2026, 2, 3, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000068"), 2361m, "Repair", new DateTime(2026, 3, 4, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 4), "Repair expense — fictional record", "Transit Repair Studio", new Guid("00000000-0000-0001-0000-000000000008"), new DateTime(2026, 3, 4, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000069"), 2439m, "DriverExpense", new DateTime(2026, 4, 5, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 5), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000009"), new DateTime(2026, 4, 5, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000070"), 2517m, "Other", new DateTime(2026, 5, 6, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 6), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000010"), new DateTime(2026, 5, 6, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000071"), 2360m, "Fuel", new DateTime(2026, 6, 7, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 7), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000011"), new DateTime(2026, 6, 7, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000072"), 2438m, "Toll", new DateTime(2026, 7, 8, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 8), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000012"), new DateTime(2026, 7, 8, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000076"), 2515m, "DriverExpense", new DateTime(2025, 11, 12, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 12), "Driver Expense expense — fictional record", "Driver Services Demo", new Guid("00000000-0000-0001-0000-000000000001"), new DateTime(2025, 11, 12, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000077"), 2593m, "Other", new DateTime(2025, 12, 13, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 13), "Other expense — fictional record", "Fleet Office Demo", new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2025, 12, 13, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000078"), 2671m, "Fuel", new DateTime(2026, 1, 14, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 14), "Fuel expense — fictional record", "Fictional Fuel Network", new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2026, 1, 14, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000079"), 2749m, "Toll", new DateTime(2026, 2, 15, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 15), "Toll expense — fictional record", "EuroRoute Demo Tolls", new Guid("00000000-0000-0001-0000-000000000004"), new DateTime(2026, 2, 15, 7, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0006-0000-000000000080"), 2827m, "Maintenance", new DateTime(2026, 3, 16, 7, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 16), "Maintenance expense — fictional record", "Apex Garage Demo", new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2026, 3, 16, 7, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "CreatedAtUtc", "Description", "InvoiceId", "LineTotal", "Quantity", "UnitPrice", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0005-0000-000000000025"), new DateTime(2025, 8, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 013", new Guid("00000000-0000-0004-0000-000000000013"), 2934.96m, 1m, 2934.96m, new DateTime(2025, 8, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000026"), new DateTime(2025, 8, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000013"), 559.04m, 1m, 559.04m, new DateTime(2025, 8, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000027"), new DateTime(2025, 9, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 014", new Guid("00000000-0000-0004-0000-000000000014"), 3086.16m, 1m, 3086.16m, new DateTime(2025, 9, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000028"), new DateTime(2025, 9, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000014"), 587.84m, 1m, 587.84m, new DateTime(2025, 9, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000029"), new DateTime(2025, 10, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 015", new Guid("00000000-0000-0004-0000-000000000015"), 3237.36m, 1m, 3237.36m, new DateTime(2025, 10, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000030"), new DateTime(2025, 10, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000015"), 616.64m, 1m, 616.64m, new DateTime(2025, 10, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000055"), new DateTime(2025, 11, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 028", new Guid("00000000-0000-0004-0000-000000000028"), 4769.52m, 1m, 4769.52m, new DateTime(2025, 11, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000056"), new DateTime(2025, 11, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000028"), 908.48m, 1m, 908.48m, new DateTime(2025, 11, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000057"), new DateTime(2025, 12, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 029", new Guid("00000000-0000-0004-0000-000000000029"), 4920.72m, 1m, 4920.72m, new DateTime(2025, 12, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000058"), new DateTime(2025, 12, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000029"), 937.28m, 1m, 937.28m, new DateTime(2025, 12, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000059"), new DateTime(2026, 1, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 030", new Guid("00000000-0000-0004-0000-000000000030"), 5071.92m, 1m, 5071.92m, new DateTime(2026, 1, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000060"), new DateTime(2026, 1, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000030"), 966.08m, 1m, 966.08m, new DateTime(2026, 1, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000085"), new DateTime(2026, 2, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 043", new Guid("00000000-0000-0004-0000-000000000043"), 6604.08m, 1m, 6604.08m, new DateTime(2026, 2, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000086"), new DateTime(2026, 2, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000043"), 1257.92m, 1m, 1257.92m, new DateTime(2026, 2, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000087"), new DateTime(2026, 3, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 044", new Guid("00000000-0000-0004-0000-000000000044"), 6755.28m, 1m, 6755.28m, new DateTime(2026, 3, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000088"), new DateTime(2026, 3, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000044"), 1286.72m, 1m, 1286.72m, new DateTime(2026, 3, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000089"), new DateTime(2026, 4, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 045", new Guid("00000000-0000-0004-0000-000000000045"), 6906.48m, 1m, 6906.48m, new DateTime(2026, 4, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000090"), new DateTime(2026, 4, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000045"), 1315.52m, 1m, 1315.52m, new DateTime(2026, 4, 21, 9, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "ClientId", "CreatedAtUtc", "Currency", "DueDate", "InvoiceNumber", "IssueDate", "Notes", "PaidAtUtc", "Status", "TotalAmount", "TruckId", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0004-0000-000000000001"), new Guid("00000000-0000-0003-0000-000000000001"), new DateTime(2025, 8, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 2), "FF-2001", new DateOnly(2025, 8, 3), "Fictional transport services for the FleetForge demonstration.", new DateTime(2025, 8, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 1850m, new Guid("00000000-0000-0001-0000-000000000001"), new DateTime(2025, 8, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000002"), new Guid("00000000-0000-0003-0000-000000000002"), new DateTime(2025, 9, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 3), "FF-2002", new DateOnly(2025, 9, 3), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 2030m, new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2025, 9, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000003"), new Guid("00000000-0000-0003-0000-000000000003"), new DateTime(2025, 10, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 2), "FF-2003", new DateOnly(2025, 10, 3), "Fictional transport services for the FleetForge demonstration.", new DateTime(2025, 10, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 2210m, new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2025, 10, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000004"), new Guid("00000000-0000-0003-0000-000000000004"), new DateTime(2025, 11, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 3), "FF-2004", new DateOnly(2025, 11, 3), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 2390m, new Guid("00000000-0000-0001-0000-000000000004"), new DateTime(2025, 11, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000005"), new Guid("00000000-0000-0003-0000-000000000005"), new DateTime(2025, 12, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 2), "FF-2005", new DateOnly(2025, 12, 3), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 2570m, new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2025, 12, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000006"), new Guid("00000000-0000-0003-0000-000000000006"), new DateTime(2026, 1, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 2), "FF-2006", new DateOnly(2026, 1, 3), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 1, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 2750m, new Guid("00000000-0000-0001-0000-000000000006"), new DateTime(2026, 1, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000007"), new Guid("00000000-0000-0003-0000-000000000007"), new DateTime(2026, 2, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 5), "FF-2007", new DateOnly(2026, 2, 3), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 2930m, new Guid("00000000-0000-0001-0000-000000000007"), new DateTime(2026, 2, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000008"), new Guid("00000000-0000-0003-0000-000000000008"), new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 2), "FF-2008", new DateOnly(2026, 3, 3), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 3, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 3110m, new Guid("00000000-0000-0001-0000-000000000008"), new DateTime(2026, 3, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000009"), new Guid("00000000-0000-0003-0000-000000000009"), new DateTime(2026, 4, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 3), "FF-2009", new DateOnly(2026, 4, 3), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 3290m, new Guid("00000000-0000-0001-0000-000000000009"), new DateTime(2026, 4, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000010"), new Guid("00000000-0000-0003-0000-000000000010"), new DateTime(2026, 5, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 2), "FF-2010", new DateOnly(2026, 5, 3), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 3470m, new Guid("00000000-0000-0001-0000-000000000010"), new DateTime(2026, 5, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000011"), new Guid("00000000-0000-0003-0000-000000000011"), new DateTime(2026, 6, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 3), "FF-2011", new DateOnly(2026, 6, 3), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 6, 21, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 3650m, new Guid("00000000-0000-0001-0000-000000000011"), new DateTime(2026, 6, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000012"), new Guid("00000000-0000-0003-0000-000000000012"), new DateTime(2026, 7, 3, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 8, 2), "FF-2012", new DateOnly(2026, 7, 3), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 3830m, new Guid("00000000-0000-0001-0000-000000000012"), new DateTime(2026, 7, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000016"), new Guid("00000000-0000-0003-0000-000000000016"), new DateTime(2025, 11, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 8), "FF-2016", new DateOnly(2025, 11, 8), "Fictional transport services for the FleetForge demonstration.", new DateTime(2025, 11, 26, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 4034m, new Guid("00000000-0000-0001-0000-000000000001"), new DateTime(2025, 11, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000017"), new Guid("00000000-0000-0003-0000-000000000017"), new DateTime(2025, 12, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 7), "FF-2017", new DateOnly(2025, 12, 8), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 4214m, new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2025, 12, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000018"), new Guid("00000000-0000-0003-0000-000000000018"), new DateTime(2026, 1, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 7), "FF-2018", new DateOnly(2026, 1, 8), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 1, 26, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 4394m, new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2026, 1, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000019"), new Guid("00000000-0000-0003-0000-000000000019"), new DateTime(2026, 2, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 10), "FF-2019", new DateOnly(2026, 2, 8), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 4574m, new Guid("00000000-0000-0001-0000-000000000004"), new DateTime(2026, 2, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000020"), new Guid("00000000-0000-0003-0000-000000000020"), new DateTime(2026, 3, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 7), "FF-2020", new DateOnly(2026, 3, 8), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 4754m, new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2026, 3, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000021"), new Guid("00000000-0000-0003-0000-000000000001"), new DateTime(2026, 4, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 8), "FF-2021", new DateOnly(2026, 4, 8), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 4, 26, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 4934m, new Guid("00000000-0000-0001-0000-000000000006"), new DateTime(2026, 4, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000022"), new Guid("00000000-0000-0003-0000-000000000002"), new DateTime(2026, 5, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 7), "FF-2022", new DateOnly(2026, 5, 8), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 5114m, new Guid("00000000-0000-0001-0000-000000000007"), new DateTime(2026, 5, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000023"), new Guid("00000000-0000-0003-0000-000000000003"), new DateTime(2026, 6, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 8), "FF-2023", new DateOnly(2026, 6, 8), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 6, 26, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 5294m, new Guid("00000000-0000-0001-0000-000000000008"), new DateTime(2026, 6, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000024"), new Guid("00000000-0000-0003-0000-000000000004"), new DateTime(2026, 7, 8, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 8, 7), "FF-2024", new DateOnly(2026, 7, 8), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 5474m, new Guid("00000000-0000-0001-0000-000000000009"), new DateTime(2026, 7, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000025"), new Guid("00000000-0000-0003-0000-000000000005"), new DateTime(2025, 8, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 12), "FF-2025", new DateOnly(2025, 8, 13), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 5138m, new Guid("00000000-0000-0001-0000-000000000010"), new DateTime(2025, 8, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000026"), new Guid("00000000-0000-0003-0000-000000000006"), new DateTime(2025, 9, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 13), "FF-2026", new DateOnly(2025, 9, 13), "Fictional transport services for the FleetForge demonstration.", new DateTime(2025, 10, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 5318m, new Guid("00000000-0000-0001-0000-000000000011"), new DateTime(2025, 9, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000027"), new Guid("00000000-0000-0003-0000-000000000007"), new DateTime(2025, 10, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 12), "FF-2027", new DateOnly(2025, 10, 13), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 5498m, new Guid("00000000-0000-0001-0000-000000000012"), new DateTime(2025, 10, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000031"), new Guid("00000000-0000-0003-0000-000000000011"), new DateTime(2026, 2, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 3, 15), "FF-2031", new DateOnly(2026, 2, 13), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 3, 3, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 6218m, new Guid("00000000-0000-0001-0000-000000000001"), new DateTime(2026, 2, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000032"), new Guid("00000000-0000-0003-0000-000000000012"), new DateTime(2026, 3, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 4, 12), "FF-2032", new DateOnly(2026, 3, 13), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 6398m, new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2026, 3, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000033"), new Guid("00000000-0000-0003-0000-000000000013"), new DateTime(2026, 4, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 5, 13), "FF-2033", new DateOnly(2026, 4, 13), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 5, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 6578m, new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2026, 4, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000034"), new Guid("00000000-0000-0003-0000-000000000014"), new DateTime(2026, 5, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 12), "FF-2034", new DateOnly(2026, 5, 13), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 6758m, new Guid("00000000-0000-0001-0000-000000000004"), new DateTime(2026, 5, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000035"), new Guid("00000000-0000-0003-0000-000000000015"), new DateTime(2026, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 13), "FF-2035", new DateOnly(2026, 6, 13), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 6938m, new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000036"), new Guid("00000000-0000-0003-0000-000000000016"), new DateTime(2026, 7, 13, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 8, 12), "FF-2036", new DateOnly(2026, 7, 13), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 7, 31, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 7118m, new Guid("00000000-0000-0001-0000-000000000006"), new DateTime(2026, 7, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000037"), new Guid("00000000-0000-0003-0000-000000000017"), new DateTime(2025, 8, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 17), "FF-2037", new DateOnly(2025, 8, 18), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 6782m, new Guid("00000000-0000-0001-0000-000000000007"), new DateTime(2025, 8, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000038"), new Guid("00000000-0000-0003-0000-000000000018"), new DateTime(2025, 9, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 18), "FF-2038", new DateOnly(2025, 9, 18), "Fictional transport services for the FleetForge demonstration.", new DateTime(2025, 10, 6, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 6962m, new Guid("00000000-0000-0001-0000-000000000008"), new DateTime(2025, 9, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000039"), new Guid("00000000-0000-0003-0000-000000000019"), new DateTime(2025, 10, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 11, 17), "FF-2039", new DateOnly(2025, 10, 18), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 7142m, new Guid("00000000-0000-0001-0000-000000000009"), new DateTime(2025, 10, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000040"), new Guid("00000000-0000-0003-0000-000000000020"), new DateTime(2025, 11, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 12, 18), "FF-2040", new DateOnly(2025, 11, 18), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 7322m, new Guid("00000000-0000-0001-0000-000000000010"), new DateTime(2025, 11, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000041"), new Guid("00000000-0000-0003-0000-000000000001"), new DateTime(2025, 12, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 1, 17), "FF-2041", new DateOnly(2025, 12, 18), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 1, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 7502m, new Guid("00000000-0000-0001-0000-000000000011"), new DateTime(2025, 12, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000042"), new Guid("00000000-0000-0003-0000-000000000002"), new DateTime(2026, 1, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 2, 17), "FF-2042", new DateOnly(2026, 1, 18), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 7682m, new Guid("00000000-0000-0001-0000-000000000012"), new DateTime(2026, 1, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000046"), new Guid("00000000-0000-0003-0000-000000000006"), new DateTime(2026, 5, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 6, 17), "FF-2046", new DateOnly(2026, 5, 18), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 6, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 8402m, new Guid("00000000-0000-0001-0000-000000000001"), new DateTime(2026, 5, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000047"), new Guid("00000000-0000-0003-0000-000000000007"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 7, 18), "FF-2047", new DateOnly(2026, 6, 18), "Fictional transport services for the FleetForge demonstration.", null, "Sent", 8582m, new Guid("00000000-0000-0001-0000-000000000002"), new DateTime(2026, 6, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000048"), new Guid("00000000-0000-0003-0000-000000000008"), new DateTime(2026, 7, 18, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2026, 8, 17), "FF-2048", new DateOnly(2026, 7, 18), "Fictional transport services for the FleetForge demonstration.", new DateTime(2026, 8, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Paid", 8762m, new Guid("00000000-0000-0001-0000-000000000003"), new DateTime(2026, 7, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000049"), new Guid("00000000-0000-0003-0000-000000000009"), new DateTime(2025, 8, 23, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 9, 22), "FF-2049", new DateOnly(2025, 8, 23), "Fictional transport services for the FleetForge demonstration.", null, "Overdue", 8426m, new Guid("00000000-0000-0001-0000-000000000004"), new DateTime(2025, 8, 26, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0004-0000-000000000050"), new Guid("00000000-0000-0003-0000-000000000010"), new DateTime(2025, 9, 23, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", new DateOnly(2025, 10, 23), "FF-2050", new DateOnly(2025, 9, 23), "Fictional transport services for the FleetForge demonstration.", null, "Draft", 8606m, new Guid("00000000-0000-0001-0000-000000000005"), new DateTime(2025, 9, 26, 9, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "CreatedAtUtc", "Description", "InvoiceId", "LineTotal", "Quantity", "UnitPrice", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0005-0000-000000000001"), new DateTime(2025, 8, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 001", new Guid("00000000-0000-0004-0000-000000000001"), 1554.00m, 1m, 1554.00m, new DateTime(2025, 8, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000002"), new DateTime(2025, 8, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000001"), 296.00m, 1m, 296.00m, new DateTime(2025, 8, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000003"), new DateTime(2025, 9, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 002", new Guid("00000000-0000-0004-0000-000000000002"), 1705.20m, 1m, 1705.20m, new DateTime(2025, 9, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000004"), new DateTime(2025, 9, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000002"), 324.80m, 1m, 324.80m, new DateTime(2025, 9, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000005"), new DateTime(2025, 10, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 003", new Guid("00000000-0000-0004-0000-000000000003"), 1856.40m, 1m, 1856.40m, new DateTime(2025, 10, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000006"), new DateTime(2025, 10, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000003"), 353.60m, 1m, 353.60m, new DateTime(2025, 10, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000007"), new DateTime(2025, 11, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 004", new Guid("00000000-0000-0004-0000-000000000004"), 2007.60m, 1m, 2007.60m, new DateTime(2025, 11, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000008"), new DateTime(2025, 11, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000004"), 382.40m, 1m, 382.40m, new DateTime(2025, 11, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000009"), new DateTime(2025, 12, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 005", new Guid("00000000-0000-0004-0000-000000000005"), 2158.80m, 1m, 2158.80m, new DateTime(2025, 12, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000010"), new DateTime(2025, 12, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000005"), 411.20m, 1m, 411.20m, new DateTime(2025, 12, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000011"), new DateTime(2026, 1, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 006", new Guid("00000000-0000-0004-0000-000000000006"), 2310.00m, 1m, 2310.00m, new DateTime(2026, 1, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000012"), new DateTime(2026, 1, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000006"), 440.00m, 1m, 440.00m, new DateTime(2026, 1, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000013"), new DateTime(2026, 2, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 007", new Guid("00000000-0000-0004-0000-000000000007"), 2461.20m, 1m, 2461.20m, new DateTime(2026, 2, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000014"), new DateTime(2026, 2, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000007"), 468.80m, 1m, 468.80m, new DateTime(2026, 2, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000015"), new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 008", new Guid("00000000-0000-0004-0000-000000000008"), 2612.40m, 1m, 2612.40m, new DateTime(2026, 3, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000016"), new DateTime(2026, 3, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000008"), 497.60m, 1m, 497.60m, new DateTime(2026, 3, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000017"), new DateTime(2026, 4, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 009", new Guid("00000000-0000-0004-0000-000000000009"), 2763.60m, 1m, 2763.60m, new DateTime(2026, 4, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000018"), new DateTime(2026, 4, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000009"), 526.40m, 1m, 526.40m, new DateTime(2026, 4, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000019"), new DateTime(2026, 5, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 010", new Guid("00000000-0000-0004-0000-000000000010"), 2914.80m, 1m, 2914.80m, new DateTime(2026, 5, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000020"), new DateTime(2026, 5, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000010"), 555.20m, 1m, 555.20m, new DateTime(2026, 5, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000021"), new DateTime(2026, 6, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 011", new Guid("00000000-0000-0004-0000-000000000011"), 3066.00m, 1m, 3066.00m, new DateTime(2026, 6, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000022"), new DateTime(2026, 6, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000011"), 584.00m, 1m, 584.00m, new DateTime(2026, 6, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000023"), new DateTime(2026, 7, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 012", new Guid("00000000-0000-0004-0000-000000000012"), 3217.20m, 1m, 3217.20m, new DateTime(2026, 7, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000024"), new DateTime(2026, 7, 3, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000012"), 612.80m, 1m, 612.80m, new DateTime(2026, 7, 6, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000031"), new DateTime(2025, 11, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 016", new Guid("00000000-0000-0004-0000-000000000016"), 3388.56m, 1m, 3388.56m, new DateTime(2025, 11, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000032"), new DateTime(2025, 11, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000016"), 645.44m, 1m, 645.44m, new DateTime(2025, 11, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000033"), new DateTime(2025, 12, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 017", new Guid("00000000-0000-0004-0000-000000000017"), 3539.76m, 1m, 3539.76m, new DateTime(2025, 12, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000034"), new DateTime(2025, 12, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000017"), 674.24m, 1m, 674.24m, new DateTime(2025, 12, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000035"), new DateTime(2026, 1, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 018", new Guid("00000000-0000-0004-0000-000000000018"), 3690.96m, 1m, 3690.96m, new DateTime(2026, 1, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000036"), new DateTime(2026, 1, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000018"), 703.04m, 1m, 703.04m, new DateTime(2026, 1, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000037"), new DateTime(2026, 2, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 019", new Guid("00000000-0000-0004-0000-000000000019"), 3842.16m, 1m, 3842.16m, new DateTime(2026, 2, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000038"), new DateTime(2026, 2, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000019"), 731.84m, 1m, 731.84m, new DateTime(2026, 2, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000039"), new DateTime(2026, 3, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 020", new Guid("00000000-0000-0004-0000-000000000020"), 3993.36m, 1m, 3993.36m, new DateTime(2026, 3, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000040"), new DateTime(2026, 3, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000020"), 760.64m, 1m, 760.64m, new DateTime(2026, 3, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000041"), new DateTime(2026, 4, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 021", new Guid("00000000-0000-0004-0000-000000000021"), 4144.56m, 1m, 4144.56m, new DateTime(2026, 4, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000042"), new DateTime(2026, 4, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000021"), 789.44m, 1m, 789.44m, new DateTime(2026, 4, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000043"), new DateTime(2026, 5, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 022", new Guid("00000000-0000-0004-0000-000000000022"), 4295.76m, 1m, 4295.76m, new DateTime(2026, 5, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000044"), new DateTime(2026, 5, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000022"), 818.24m, 1m, 818.24m, new DateTime(2026, 5, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000045"), new DateTime(2026, 6, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 023", new Guid("00000000-0000-0004-0000-000000000023"), 4446.96m, 1m, 4446.96m, new DateTime(2026, 6, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000046"), new DateTime(2026, 6, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000023"), 847.04m, 1m, 847.04m, new DateTime(2026, 6, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000047"), new DateTime(2026, 7, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 024", new Guid("00000000-0000-0004-0000-000000000024"), 4598.16m, 1m, 4598.16m, new DateTime(2026, 7, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000048"), new DateTime(2026, 7, 8, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000024"), 875.84m, 1m, 875.84m, new DateTime(2026, 7, 11, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000049"), new DateTime(2025, 8, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 025", new Guid("00000000-0000-0004-0000-000000000025"), 4315.92m, 1m, 4315.92m, new DateTime(2025, 8, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000050"), new DateTime(2025, 8, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000025"), 822.08m, 1m, 822.08m, new DateTime(2025, 8, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000051"), new DateTime(2025, 9, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 026", new Guid("00000000-0000-0004-0000-000000000026"), 4467.12m, 1m, 4467.12m, new DateTime(2025, 9, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000052"), new DateTime(2025, 9, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000026"), 850.88m, 1m, 850.88m, new DateTime(2025, 9, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000053"), new DateTime(2025, 10, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 027", new Guid("00000000-0000-0004-0000-000000000027"), 4618.32m, 1m, 4618.32m, new DateTime(2025, 10, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000054"), new DateTime(2025, 10, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000027"), 879.68m, 1m, 879.68m, new DateTime(2025, 10, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000061"), new DateTime(2026, 2, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 031", new Guid("00000000-0000-0004-0000-000000000031"), 5223.12m, 1m, 5223.12m, new DateTime(2026, 2, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000062"), new DateTime(2026, 2, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000031"), 994.88m, 1m, 994.88m, new DateTime(2026, 2, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000063"), new DateTime(2026, 3, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 032", new Guid("00000000-0000-0004-0000-000000000032"), 5374.32m, 1m, 5374.32m, new DateTime(2026, 3, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000064"), new DateTime(2026, 3, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000032"), 1023.68m, 1m, 1023.68m, new DateTime(2026, 3, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000065"), new DateTime(2026, 4, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 033", new Guid("00000000-0000-0004-0000-000000000033"), 5525.52m, 1m, 5525.52m, new DateTime(2026, 4, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000066"), new DateTime(2026, 4, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000033"), 1052.48m, 1m, 1052.48m, new DateTime(2026, 4, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000067"), new DateTime(2026, 5, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 034", new Guid("00000000-0000-0004-0000-000000000034"), 5676.72m, 1m, 5676.72m, new DateTime(2026, 5, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000068"), new DateTime(2026, 5, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000034"), 1081.28m, 1m, 1081.28m, new DateTime(2026, 5, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000069"), new DateTime(2026, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 035", new Guid("00000000-0000-0004-0000-000000000035"), 5827.92m, 1m, 5827.92m, new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000070"), new DateTime(2026, 6, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000035"), 1110.08m, 1m, 1110.08m, new DateTime(2026, 6, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000071"), new DateTime(2026, 7, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 036", new Guid("00000000-0000-0004-0000-000000000036"), 5979.12m, 1m, 5979.12m, new DateTime(2026, 7, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000072"), new DateTime(2026, 7, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000036"), 1138.88m, 1m, 1138.88m, new DateTime(2026, 7, 16, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000073"), new DateTime(2025, 8, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 037", new Guid("00000000-0000-0004-0000-000000000037"), 5696.88m, 1m, 5696.88m, new DateTime(2025, 8, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000074"), new DateTime(2025, 8, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000037"), 1085.12m, 1m, 1085.12m, new DateTime(2025, 8, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000075"), new DateTime(2025, 9, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 038", new Guid("00000000-0000-0004-0000-000000000038"), 5848.08m, 1m, 5848.08m, new DateTime(2025, 9, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000076"), new DateTime(2025, 9, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000038"), 1113.92m, 1m, 1113.92m, new DateTime(2025, 9, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000077"), new DateTime(2025, 10, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 039", new Guid("00000000-0000-0004-0000-000000000039"), 5999.28m, 1m, 5999.28m, new DateTime(2025, 10, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000078"), new DateTime(2025, 10, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000039"), 1142.72m, 1m, 1142.72m, new DateTime(2025, 10, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000079"), new DateTime(2025, 11, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 040", new Guid("00000000-0000-0004-0000-000000000040"), 6150.48m, 1m, 6150.48m, new DateTime(2025, 11, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000080"), new DateTime(2025, 11, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000040"), 1171.52m, 1m, 1171.52m, new DateTime(2025, 11, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000081"), new DateTime(2025, 12, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 041", new Guid("00000000-0000-0004-0000-000000000041"), 6301.68m, 1m, 6301.68m, new DateTime(2025, 12, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000082"), new DateTime(2025, 12, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000041"), 1200.32m, 1m, 1200.32m, new DateTime(2025, 12, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000083"), new DateTime(2026, 1, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 042", new Guid("00000000-0000-0004-0000-000000000042"), 6452.88m, 1m, 6452.88m, new DateTime(2026, 1, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000084"), new DateTime(2026, 1, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000042"), 1229.12m, 1m, 1229.12m, new DateTime(2026, 1, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000091"), new DateTime(2026, 5, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 046", new Guid("00000000-0000-0004-0000-000000000046"), 7057.68m, 1m, 7057.68m, new DateTime(2026, 5, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000092"), new DateTime(2026, 5, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000046"), 1344.32m, 1m, 1344.32m, new DateTime(2026, 5, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000093"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 047", new Guid("00000000-0000-0004-0000-000000000047"), 7208.88m, 1m, 7208.88m, new DateTime(2026, 6, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000094"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000047"), 1373.12m, 1m, 1373.12m, new DateTime(2026, 6, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000095"), new DateTime(2026, 7, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 048", new Guid("00000000-0000-0004-0000-000000000048"), 7360.08m, 1m, 7360.08m, new DateTime(2026, 7, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000096"), new DateTime(2026, 7, 18, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000048"), 1401.92m, 1m, 1401.92m, new DateTime(2026, 7, 21, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000097"), new DateTime(2025, 8, 23, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 049", new Guid("00000000-0000-0004-0000-000000000049"), 7077.84m, 1m, 7077.84m, new DateTime(2025, 8, 26, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000098"), new DateTime(2025, 8, 23, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000049"), 1348.16m, 1m, 1348.16m, new DateTime(2025, 8, 26, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000099"), new DateTime(2025, 9, 23, 8, 0, 0, 0, DateTimeKind.Utc), "Road freight service — route 050", new Guid("00000000-0000-0004-0000-000000000050"), 7229.04m, 1m, 7229.04m, new DateTime(2025, 9, 26, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("00000000-0000-0005-0000-000000000100"), new DateTime(2025, 9, 23, 8, 0, 0, 0, DateTimeKind.Utc), "Fuel and toll adjustment", new Guid("00000000-0000-0004-0000-000000000050"), 1376.96m, 1m, 1376.96m, new DateTime(2025, 9, 26, 9, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_EntityType_EntityId",
                table: "ActivityLogs",
                columns: new[] { "EntityType", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_OccurredAtUtc",
                table: "ActivityLogs",
                column: "OccurredAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompanyName",
                table: "Clients",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IsActive_CompanyName",
                table: "Clients",
                columns: new[] { "IsActive", "CompanyName" });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ClientId",
                table: "Documents",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DriverId",
                table: "Documents",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ReferenceNumber",
                table: "Documents",
                column: "ReferenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TruckId",
                table: "Documents",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Type_ExpirationDate",
                table: "Documents",
                columns: new[] { "Type", "ExpirationDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Email",
                table: "Drivers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_LicenceExpiration",
                table: "Drivers",
                column: "LicenceExpiration");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_LicenceNumber",
                table: "Drivers",
                column: "LicenceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_Status",
                table: "Drivers",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Category",
                table: "Expenses",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Date",
                table: "Expenses",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_TruckId_Date",
                table: "Expenses",
                columns: new[] { "TruckId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ClientId_Status",
                table: "Invoices",
                columns: new[] { "ClientId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_InvoiceNumber",
                table: "Invoices",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_IssueDate",
                table: "Invoices",
                column: "IssueDate");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Status_DueDate",
                table: "Invoices",
                columns: new[] { "Status", "DueDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TruckId_IssueDate",
                table: "Invoices",
                columns: new[] { "TruckId", "IssueDate" });

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterSubscribers_NormalizedEmail",
                table: "NewsletterSubscribers",
                column: "NormalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterSubscribers_SubscribedAtUtc",
                table: "NewsletterSubscribers",
                column: "SubscribedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_AssignedDriverId",
                table: "Trucks",
                column: "AssignedDriverId",
                unique: true,
                filter: "\"AssignedDriverId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_InsuranceExpiration",
                table: "Trucks",
                column: "InsuranceExpiration");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_RegistrationNumber",
                table: "Trucks",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_Status",
                table: "Trucks",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_TechnicalInspectionExpiration",
                table: "Trucks",
                column: "TechnicalInspectionExpiration");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "NewsletterSubscribers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
