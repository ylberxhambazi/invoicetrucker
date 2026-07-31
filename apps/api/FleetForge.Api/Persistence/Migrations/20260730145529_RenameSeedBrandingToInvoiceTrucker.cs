using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetForge.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameSeedBrandingToInvoiceTrucker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000001"),
                column: "Message",
                value: "Truck IT-DEMO-001 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000002"),
                column: "Message",
                value: "Invoice IT-2002 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000003"),
                column: "Message",
                value: "Payment recorded for IT-2003");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000007"),
                column: "Message",
                value: "Truck IT-DEMO-007 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000008"),
                column: "Message",
                value: "Invoice IT-2008 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000009"),
                column: "Message",
                value: "Payment recorded for IT-2009");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000013"),
                column: "Message",
                value: "Truck IT-DEMO-013 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000014"),
                column: "Message",
                value: "Invoice IT-2014 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000015"),
                column: "Message",
                value: "Payment recorded for IT-2015");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000019"),
                column: "Message",
                value: "Truck IT-DEMO-004 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000020"),
                column: "Message",
                value: "Invoice IT-2020 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000021"),
                column: "Message",
                value: "Payment recorded for IT-2021");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000025"),
                column: "Message",
                value: "Truck IT-DEMO-010 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000026"),
                column: "Message",
                value: "Invoice IT-2026 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000027"),
                column: "Message",
                value: "Payment recorded for IT-2027");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000001"),
                column: "Email",
                value: "billing01@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000002"),
                column: "Email",
                value: "billing02@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000003"),
                column: "Email",
                value: "billing03@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000004"),
                column: "Email",
                value: "billing04@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000005"),
                column: "Email",
                value: "billing05@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000006"),
                column: "Email",
                value: "billing06@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000007"),
                column: "Email",
                value: "billing07@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000008"),
                column: "Email",
                value: "billing08@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000009"),
                column: "Email",
                value: "billing09@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000010"),
                column: "Email",
                value: "billing10@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000011"),
                column: "Email",
                value: "billing11@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000012"),
                column: "Email",
                value: "billing12@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000013"),
                column: "Email",
                value: "billing13@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000014"),
                column: "Email",
                value: "billing14@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000015"),
                column: "Email",
                value: "billing15@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000016"),
                column: "Email",
                value: "billing16@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000017"),
                column: "Email",
                value: "billing17@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000018"),
                column: "Email",
                value: "billing18@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000019"),
                column: "Email",
                value: "billing19@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000020"),
                column: "Email",
                value: "billing20@client.invoicetrucker.example");

            migrationBuilder.UpdateData(
                table: "CompanyProfiles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0009-0000-000000000001"),
                column: "InvoicePrefix",
                value: "IT");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000001"),
                column: "ReferenceNumber",
                value: "IT-DOC-0001");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000002"),
                column: "ReferenceNumber",
                value: "IT-DOC-0002");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000003"),
                column: "ReferenceNumber",
                value: "IT-DOC-0003");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000004"),
                column: "ReferenceNumber",
                value: "IT-DOC-0004");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000005"),
                column: "ReferenceNumber",
                value: "IT-DOC-0005");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000006"),
                column: "ReferenceNumber",
                value: "IT-DOC-0006");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000007"),
                column: "ReferenceNumber",
                value: "IT-DOC-0007");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000008"),
                column: "ReferenceNumber",
                value: "IT-DOC-0008");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000009"),
                column: "ReferenceNumber",
                value: "IT-DOC-0009");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000010"),
                column: "ReferenceNumber",
                value: "IT-DOC-0010");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000011"),
                column: "ReferenceNumber",
                value: "IT-DOC-0011");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000012"),
                column: "ReferenceNumber",
                value: "IT-DOC-0012");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000013"),
                column: "ReferenceNumber",
                value: "IT-DOC-0013");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000014"),
                column: "ReferenceNumber",
                value: "IT-DOC-0014");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000015"),
                column: "ReferenceNumber",
                value: "IT-DOC-0015");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000016"),
                column: "ReferenceNumber",
                value: "IT-DOC-0016");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000017"),
                column: "ReferenceNumber",
                value: "IT-DOC-0017");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000018"),
                column: "ReferenceNumber",
                value: "IT-DOC-0018");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000019"),
                column: "ReferenceNumber",
                value: "IT-DOC-0019");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000020"),
                column: "ReferenceNumber",
                value: "IT-DOC-0020");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000021"),
                column: "ReferenceNumber",
                value: "IT-DOC-0021");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000022"),
                column: "ReferenceNumber",
                value: "IT-DOC-0022");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000023"),
                column: "ReferenceNumber",
                value: "IT-DOC-0023");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000024"),
                column: "ReferenceNumber",
                value: "IT-DOC-0024");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000025"),
                column: "ReferenceNumber",
                value: "IT-DOC-0025");

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000001"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver01@invoicetrucker.example", "IT-LIC-0001" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000002"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver02@invoicetrucker.example", "IT-LIC-0002" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000003"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver03@invoicetrucker.example", "IT-LIC-0003" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000004"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver04@invoicetrucker.example", "IT-LIC-0004" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000005"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver05@invoicetrucker.example", "IT-LIC-0005" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000006"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver06@invoicetrucker.example", "IT-LIC-0006" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000007"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver07@invoicetrucker.example", "IT-LIC-0007" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000008"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver08@invoicetrucker.example", "IT-LIC-0008" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000009"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver09@invoicetrucker.example", "IT-LIC-0009" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000010"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver10@invoicetrucker.example", "IT-LIC-0010" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000011"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver11@invoicetrucker.example", "IT-LIC-0011" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000012"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver12@invoicetrucker.example", "IT-LIC-0012" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000001"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2001", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000002"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2002", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000003"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2003", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000004"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2004", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000005"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2005", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000006"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2006", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000007"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2007", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000008"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2008", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000009"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2009", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000010"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2010", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000011"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2011", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000012"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2012", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000013"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2013", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000014"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2014", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000015"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2015", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000016"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2016", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000017"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2017", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000018"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2018", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000019"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2019", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000020"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2020", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000021"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2021", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000022"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2022", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000023"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2023", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000024"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2024", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000025"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2025", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000026"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2026", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000027"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2027", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000028"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2028", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000029"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2029", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000030"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2030", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000031"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2031", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000032"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2032", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000033"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2033", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000034"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2034", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000035"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2035", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000036"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2036", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000037"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2037", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000038"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2038", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000039"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2039", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000040"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2040", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000041"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2041", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000042"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2042", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000043"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2043", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000044"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2044", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000045"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2045", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000046"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2046", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000047"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2047", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000048"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2048", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000049"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2049", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000050"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "IT-2050", "Fictional transport services for the InvoiceTrucker demonstration." });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000001"),
                column: "RegistrationNumber",
                value: "IT-DEMO-001");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000002"),
                column: "RegistrationNumber",
                value: "IT-DEMO-002");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000003"),
                column: "RegistrationNumber",
                value: "IT-DEMO-003");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000004"),
                column: "RegistrationNumber",
                value: "IT-DEMO-004");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000005"),
                column: "RegistrationNumber",
                value: "IT-DEMO-005");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000006"),
                column: "RegistrationNumber",
                value: "IT-DEMO-006");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000007"),
                column: "RegistrationNumber",
                value: "IT-DEMO-007");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000008"),
                column: "RegistrationNumber",
                value: "IT-DEMO-008");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000009"),
                column: "RegistrationNumber",
                value: "IT-DEMO-009");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000010"),
                column: "RegistrationNumber",
                value: "IT-DEMO-010");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000011"),
                column: "RegistrationNumber",
                value: "IT-DEMO-011");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000012"),
                column: "RegistrationNumber",
                value: "IT-DEMO-012");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000013"),
                column: "RegistrationNumber",
                value: "IT-DEMO-013");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000014"),
                column: "RegistrationNumber",
                value: "IT-DEMO-014");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000015"),
                column: "RegistrationNumber",
                value: "IT-DEMO-015");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000001"),
                column: "Message",
                value: "Truck FF-DEMO-001 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000002"),
                column: "Message",
                value: "Invoice FF-2002 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000003"),
                column: "Message",
                value: "Payment recorded for FF-2003");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000007"),
                column: "Message",
                value: "Truck FF-DEMO-007 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000008"),
                column: "Message",
                value: "Invoice FF-2008 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000009"),
                column: "Message",
                value: "Payment recorded for FF-2009");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000013"),
                column: "Message",
                value: "Truck FF-DEMO-013 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000014"),
                column: "Message",
                value: "Invoice FF-2014 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000015"),
                column: "Message",
                value: "Payment recorded for FF-2015");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000019"),
                column: "Message",
                value: "Truck FF-DEMO-004 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000020"),
                column: "Message",
                value: "Invoice FF-2020 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000021"),
                column: "Message",
                value: "Payment recorded for FF-2021");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000025"),
                column: "Message",
                value: "Truck FF-DEMO-010 status updated");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000026"),
                column: "Message",
                value: "Invoice FF-2026 created");

            migrationBuilder.UpdateData(
                table: "ActivityLogs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0008-0000-000000000027"),
                column: "Message",
                value: "Payment recorded for FF-2027");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000001"),
                column: "Email",
                value: "billing01@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000002"),
                column: "Email",
                value: "billing02@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000003"),
                column: "Email",
                value: "billing03@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000004"),
                column: "Email",
                value: "billing04@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000005"),
                column: "Email",
                value: "billing05@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000006"),
                column: "Email",
                value: "billing06@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000007"),
                column: "Email",
                value: "billing07@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000008"),
                column: "Email",
                value: "billing08@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000009"),
                column: "Email",
                value: "billing09@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000010"),
                column: "Email",
                value: "billing10@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000011"),
                column: "Email",
                value: "billing11@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000012"),
                column: "Email",
                value: "billing12@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000013"),
                column: "Email",
                value: "billing13@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000014"),
                column: "Email",
                value: "billing14@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000015"),
                column: "Email",
                value: "billing15@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000016"),
                column: "Email",
                value: "billing16@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000017"),
                column: "Email",
                value: "billing17@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000018"),
                column: "Email",
                value: "billing18@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000019"),
                column: "Email",
                value: "billing19@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0003-0000-000000000020"),
                column: "Email",
                value: "billing20@client.fleetforge.example");

            migrationBuilder.UpdateData(
                table: "CompanyProfiles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0009-0000-000000000001"),
                column: "InvoicePrefix",
                value: "FF");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000001"),
                column: "ReferenceNumber",
                value: "FF-DOC-0001");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000002"),
                column: "ReferenceNumber",
                value: "FF-DOC-0002");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000003"),
                column: "ReferenceNumber",
                value: "FF-DOC-0003");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000004"),
                column: "ReferenceNumber",
                value: "FF-DOC-0004");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000005"),
                column: "ReferenceNumber",
                value: "FF-DOC-0005");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000006"),
                column: "ReferenceNumber",
                value: "FF-DOC-0006");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000007"),
                column: "ReferenceNumber",
                value: "FF-DOC-0007");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000008"),
                column: "ReferenceNumber",
                value: "FF-DOC-0008");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000009"),
                column: "ReferenceNumber",
                value: "FF-DOC-0009");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000010"),
                column: "ReferenceNumber",
                value: "FF-DOC-0010");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000011"),
                column: "ReferenceNumber",
                value: "FF-DOC-0011");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000012"),
                column: "ReferenceNumber",
                value: "FF-DOC-0012");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000013"),
                column: "ReferenceNumber",
                value: "FF-DOC-0013");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000014"),
                column: "ReferenceNumber",
                value: "FF-DOC-0014");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000015"),
                column: "ReferenceNumber",
                value: "FF-DOC-0015");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000016"),
                column: "ReferenceNumber",
                value: "FF-DOC-0016");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000017"),
                column: "ReferenceNumber",
                value: "FF-DOC-0017");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000018"),
                column: "ReferenceNumber",
                value: "FF-DOC-0018");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000019"),
                column: "ReferenceNumber",
                value: "FF-DOC-0019");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000020"),
                column: "ReferenceNumber",
                value: "FF-DOC-0020");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000021"),
                column: "ReferenceNumber",
                value: "FF-DOC-0021");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000022"),
                column: "ReferenceNumber",
                value: "FF-DOC-0022");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000023"),
                column: "ReferenceNumber",
                value: "FF-DOC-0023");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000024"),
                column: "ReferenceNumber",
                value: "FF-DOC-0024");

            migrationBuilder.UpdateData(
                table: "Documents",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0007-0000-000000000025"),
                column: "ReferenceNumber",
                value: "FF-DOC-0025");

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000001"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver01@fleetforge.example", "FF-LIC-0001" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000002"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver02@fleetforge.example", "FF-LIC-0002" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000003"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver03@fleetforge.example", "FF-LIC-0003" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000004"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver04@fleetforge.example", "FF-LIC-0004" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000005"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver05@fleetforge.example", "FF-LIC-0005" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000006"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver06@fleetforge.example", "FF-LIC-0006" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000007"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver07@fleetforge.example", "FF-LIC-0007" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000008"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver08@fleetforge.example", "FF-LIC-0008" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000009"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver09@fleetforge.example", "FF-LIC-0009" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000010"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver10@fleetforge.example", "FF-LIC-0010" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000011"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver11@fleetforge.example", "FF-LIC-0011" });

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0002-0000-000000000012"),
                columns: new[] { "Email", "LicenceNumber" },
                values: new object[] { "driver12@fleetforge.example", "FF-LIC-0012" });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000001"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2001", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000002"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2002", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000003"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2003", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000004"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2004", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000005"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2005", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000006"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2006", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000007"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2007", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000008"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2008", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000009"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2009", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000010"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2010", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000011"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2011", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000012"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2012", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000013"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2013", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000014"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2014", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000015"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2015", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000016"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2016", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000017"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2017", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000018"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2018", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000019"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2019", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000020"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2020", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000021"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2021", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000022"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2022", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000023"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2023", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000024"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2024", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000025"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2025", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000026"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2026", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000027"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2027", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000028"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2028", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000029"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2029", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000030"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2030", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000031"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2031", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000032"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2032", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000033"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2033", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000034"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2034", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000035"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2035", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000036"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2036", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000037"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2037", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000038"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2038", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000039"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2039", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000040"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2040", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000041"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2041", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000042"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2042", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000043"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2043", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000044"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2044", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000045"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2045", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000046"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2046", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000047"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2047", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000048"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2048", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000049"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2049", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0004-0000-000000000050"),
                columns: new[] { "InvoiceNumber", "Notes" },
                values: new object[] { "FF-2050", "Fictional transport services for the FleetForge demonstration." });

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000001"),
                column: "RegistrationNumber",
                value: "FF-DEMO-001");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000002"),
                column: "RegistrationNumber",
                value: "FF-DEMO-002");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000003"),
                column: "RegistrationNumber",
                value: "FF-DEMO-003");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000004"),
                column: "RegistrationNumber",
                value: "FF-DEMO-004");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000005"),
                column: "RegistrationNumber",
                value: "FF-DEMO-005");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000006"),
                column: "RegistrationNumber",
                value: "FF-DEMO-006");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000007"),
                column: "RegistrationNumber",
                value: "FF-DEMO-007");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000008"),
                column: "RegistrationNumber",
                value: "FF-DEMO-008");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000009"),
                column: "RegistrationNumber",
                value: "FF-DEMO-009");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000010"),
                column: "RegistrationNumber",
                value: "FF-DEMO-010");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000011"),
                column: "RegistrationNumber",
                value: "FF-DEMO-011");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000012"),
                column: "RegistrationNumber",
                value: "FF-DEMO-012");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000013"),
                column: "RegistrationNumber",
                value: "FF-DEMO-013");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000014"),
                column: "RegistrationNumber",
                value: "FF-DEMO-014");

            migrationBuilder.UpdateData(
                table: "Trucks",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0001-0000-000000000015"),
                column: "RegistrationNumber",
                value: "FF-DEMO-015");
        }
    }
}
