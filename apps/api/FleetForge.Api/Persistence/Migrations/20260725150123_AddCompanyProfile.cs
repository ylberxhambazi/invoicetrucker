using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetForge.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                    LegalName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    Phone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    DefaultCurrency = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    TimeZone = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    VatNumber = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    InvoicePrefix = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    PaymentTermsDays = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyProfiles", x => x.Id);
                    table.CheckConstraint("CK_CompanyProfiles_PaymentTermsDays", "\"PaymentTermsDays\" BETWEEN 1 AND 365");
                });

            migrationBuilder.InsertData(
                table: "CompanyProfiles",
                columns: new[] { "Id", "Address", "City", "CompanyName", "Country", "CreatedAtUtc", "DefaultCurrency", "Email", "InvoicePrefix", "LegalName", "PaymentTermsDays", "Phone", "TimeZone", "UpdatedAtUtc", "VatNumber" },
                values: new object[] { new Guid("00000000-0000-0009-0000-000000000001"), "42 Fictional Freight Avenue", "Skopje", "Northstar Demo Logistics", "North Macedonia", new DateTime(2024, 2, 6, 8, 0, 0, 0, DateTimeKind.Utc), "EUR", "operations@northstar-demo.example", "FF", "Northstar Demo Logistics d.o.o.", 30, "+389 2 555 0140", "Europe/Skopje", new DateTime(2026, 7, 25, 8, 0, 0, 0, DateTimeKind.Utc), "MK-DEMO-402001" });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_CompanyName",
                table: "CompanyProfiles",
                column: "CompanyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyProfiles_Email",
                table: "CompanyProfiles",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyProfiles");
        }
    }
}
