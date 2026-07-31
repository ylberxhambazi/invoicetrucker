"use client";

import type {
  ClientSummary,
  DocumentRecord,
  DriverSummary,
  Expense,
  InvoiceSummary,
  PaginatedResponse,
  TruckSummary,
} from "@invoicetrucker/types";
import { useQuery } from "@tanstack/react-query";
import Link from "next/link";
import { useSearchParams } from "next/navigation";

import {
  EmptyState,
  ListToolbar,
  PageHeader,
  Pagination,
  QueryError,
  QueryLoading,
  StatusBadge,
  type FilterOption,
} from "@/components/demo/ui";
import { demoApi, type ListParams } from "@/lib/demo-api";
import {
  displayEnum,
  formatCurrency,
  formatDate,
  formatNumber,
} from "@/lib/format";

interface Column<T> {
  label: string;
  render: (item: T) => React.ReactNode;
  mobile?: boolean;
}

function useListParams(defaultSort: string): ListParams {
  const searchParams = useSearchParams();
  const values = Object.fromEntries(searchParams.entries());

  return {
    ...values,
    page: Math.max(1, Number(searchParams.get("page") ?? 1)),
    pageSize: 10,
    sortBy: searchParams.get("sortBy") ?? defaultSort,
    sortDirection: searchParams.get("sortDirection") ?? "asc",
  };
}

function ResourceList<T extends { id: string }>({
  data,
  columns,
  detailBase,
  emptyMessage,
}: {
  data: PaginatedResponse<T>;
  columns: Column<T>[];
  detailBase?: string;
  emptyMessage: string;
}) {
  if (data.items.length === 0) return <EmptyState message={emptyMessage} />;

  return (
    <div className="demo-card overflow-hidden">
      <div className="hidden overflow-x-auto md:block">
        <table className="demo-table">
          <thead>
            <tr>
              {columns.map((column) => (
                <th key={column.label}>{column.label}</th>
              ))}
              {detailBase ? (
                <th>
                  <span className="sr-only">Actions</span>
                </th>
              ) : null}
            </tr>
          </thead>
          <tbody>
            {data.items.map((item) => (
              <tr key={item.id}>
                {columns.map((column) => (
                  <td key={column.label}>{column.render(item)}</td>
                ))}
                {detailBase ? (
                  <td className="text-right">
                    <Link
                      className="demo-row-link"
                      href={`${detailBase}/${item.id}`}
                    >
                      View
                    </Link>
                  </td>
                ) : null}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className="divide-y divide-slate-200 md:hidden">
        {data.items.map((item) => (
          <article className="p-5" key={item.id}>
            <dl className="grid grid-cols-2 gap-x-4 gap-y-3">
              {columns
                .filter((column) => column.mobile !== false)
                .map((column) => (
                  <div className="min-w-0" key={column.label}>
                    <dt className="text-[11px] font-semibold uppercase tracking-wide text-slate-400">
                      {column.label}
                    </dt>
                    <dd className="mt-1 truncate text-sm font-medium text-slate-700">
                      {column.render(item)}
                    </dd>
                  </div>
                ))}
            </dl>
            {detailBase ? (
              <Link
                className="demo-row-link mt-4 inline-flex"
                href={`${detailBase}/${item.id}`}
              >
                View details
              </Link>
            ) : null}
          </article>
        ))}
      </div>
      <Pagination
        page={data.page}
        totalCount={data.totalCount}
        totalPages={data.totalPages}
      />
    </div>
  );
}

function ListQueryView<T extends { id: string }>({
  query,
  columns,
  detailBase,
  emptyMessage,
}: {
  query: ReturnType<typeof useQuery<PaginatedResponse<T>, Error>>;
  columns: Column<T>[];
  detailBase?: string;
  emptyMessage: string;
}) {
  if (query.isPending) return <QueryLoading />;
  if (query.isError)
    return (
      <QueryError error={query.error} retry={() => void query.refetch()} />
    );

  return (
    <ResourceList
      columns={columns}
      data={query.data}
      detailBase={detailBase}
      emptyMessage={emptyMessage}
    />
  );
}

const directions: FilterOption[] = [
  { label: "Name A–Z", value: "name:asc" },
  { label: "Name Z–A", value: "name:desc" },
];

export function TrucksList() {
  const params = useListParams("registrationNumber");
  const query = useQuery({
    queryKey: ["trucks", params],
    queryFn: ({ signal }) => demoApi.trucks(params, signal),
  });

  const columns: Column<TruckSummary>[] = [
    {
      label: "Truck",
      render: (truck) => (
        <span>
          <strong className="block font-semibold text-slate-900">
            {truck.registrationNumber}
          </strong>
          <span className="text-xs text-slate-500">
            {truck.make} {truck.model}
          </span>
        </span>
      ),
    },
    { label: "Year", render: (truck) => truck.year },
    {
      label: "Status",
      render: (truck) => <StatusBadge value={truck.status} />,
    },
    {
      label: "Driver",
      render: (truck) => truck.assignedDriver ?? "Unassigned",
    },
    {
      label: "Mileage",
      render: (truck) => `${formatNumber(truck.currentMileage)} km`,
    },
    {
      label: "Insurance",
      render: (truck) => formatDate(truck.insuranceExpiration),
      mobile: false,
    },
  ];

  return (
    <>
      <PageHeader
        description="Fleet availability, assignments, mileage, and compliance dates from the live demo API."
        title="Trucks"
      />
      <ListToolbar
        filters={[
          {
            name: "status",
            label: "All statuses",
            options: [
              "Available",
              "OnRoute",
              "Maintenance",
              "OutOfService",
            ].map((value) => ({
              label: displayEnum(value),
              value,
            })),
          },
        ]}
        placeholder="Search registration, make, or model"
        sortOptions={[
          { label: "Registration A–Z", value: "registrationNumber:asc" },
          { label: "Registration Z–A", value: "registrationNumber:desc" },
          { label: "Mileage high to low", value: "currentMileage:desc" },
          { label: "Insurance due first", value: "insuranceExpiration:asc" },
        ]}
      />
      <ListQueryView
        columns={columns}
        detailBase="/demo/trucks"
        emptyMessage="No trucks match the selected search and status."
        query={query}
      />
    </>
  );
}

export function DriversList() {
  const params = useListParams("name");
  const query = useQuery({
    queryKey: ["drivers", params],
    queryFn: ({ signal }) => demoApi.drivers(params, signal),
  });
  const columns: Column<DriverSummary>[] = [
    { label: "Driver", render: (driver) => <strong>{driver.fullName}</strong> },
    {
      label: "Status",
      render: (driver) => <StatusBadge value={driver.status} />,
    },
    {
      label: "Assigned truck",
      render: (driver) => driver.assignedTruck ?? "Unassigned",
    },
    { label: "Licence", render: (driver) => driver.licenceNumber },
    {
      label: "Licence expires",
      render: (driver) => formatDate(driver.licenceExpiration),
      mobile: false,
    },
    { label: "Trips", render: (driver) => formatNumber(driver.completedTrips) },
  ];

  return (
    <>
      <PageHeader
        description="Driver availability, assignments, licence validity, and completed work."
        title="Drivers"
      />
      <ListToolbar
        filters={[
          {
            name: "status",
            label: "All statuses",
            options: ["Available", "OnRoute", "OffDuty", "Leave"].map(
              (value) => ({
                label: displayEnum(value),
                value,
              }),
            ),
          },
        ]}
        placeholder="Search name, licence, or email"
        sortOptions={[
          ...directions,
          { label: "Most completed trips", value: "completedTrips:desc" },
          { label: "Licence due first", value: "licenceExpiration:asc" },
        ]}
      />
      <ListQueryView
        columns={columns}
        detailBase="/demo/drivers"
        emptyMessage="No drivers match these filters."
        query={query}
      />
    </>
  );
}

export function ClientsList() {
  const params = useListParams("name");
  const query = useQuery({
    queryKey: ["clients", params],
    queryFn: ({ signal }) => demoApi.clients(params, signal),
  });
  const columns: Column<ClientSummary>[] = [
    {
      label: "Company",
      render: (client) => (
        <span>
          <strong className="block">{client.companyName}</strong>
          <span className="text-xs text-slate-500">{client.contactPerson}</span>
        </span>
      ),
    },
    {
      label: "Status",
      render: (client) => (
        <StatusBadge value={client.isActive ? "Active" : "Inactive"} />
      ),
    },
    { label: "Country", render: (client) => client.country },
    { label: "Active invoices", render: (client) => client.activeInvoices },
    {
      label: "Total billed",
      render: (client) => formatCurrency(client.totalBilled),
      mobile: false,
    },
    { label: "Email", render: (client) => client.email, mobile: false },
  ];

  return (
    <>
      <PageHeader
        description="Fictional customer contacts, billing activity, and account status."
        title="Clients"
      />
      <ListToolbar
        filters={[
          {
            name: "isActive",
            label: "All accounts",
            options: [
              { label: "Active", value: "true" },
              { label: "Inactive", value: "false" },
            ],
          },
        ]}
        placeholder="Search company, contact, or email"
        sortOptions={[
          ...directions,
          { label: "Highest billed", value: "totalBilled:desc" },
          { label: "Most active invoices", value: "activeInvoices:desc" },
        ]}
      />
      <ListQueryView
        columns={columns}
        detailBase="/demo/clients"
        emptyMessage="No clients match these filters."
        query={query}
      />
    </>
  );
}

export function InvoicesList() {
  const params = useListParams("issueDate");
  const query = useQuery({
    queryKey: ["invoices", params],
    queryFn: ({ signal }) => demoApi.invoices(params, signal),
  });
  const columns: Column<InvoiceSummary>[] = [
    {
      label: "Invoice",
      render: (invoice) => <strong>{invoice.invoiceNumber}</strong>,
    },
    { label: "Client", render: (invoice) => invoice.client },
    {
      label: "Status",
      render: (invoice) => <StatusBadge value={invoice.status} />,
    },
    { label: "Issued", render: (invoice) => formatDate(invoice.issueDate) },
    {
      label: "Due",
      render: (invoice) => formatDate(invoice.dueDate),
      mobile: false,
    },
    {
      label: "Amount",
      render: (invoice) => formatCurrency(invoice.amount, invoice.currency),
    },
  ];

  return (
    <>
      <PageHeader
        description="Read-only invoice history with payment status, due dates, and linked fleet records."
        title="Invoices"
      />
      <ListToolbar
        filters={[
          {
            name: "status",
            label: "All statuses",
            options: ["Draft", "Sent", "Paid", "Overdue"].map((value) => ({
              label: value,
              value,
            })),
          },
        ]}
        placeholder="Search invoice, client, or truck"
        sortOptions={[
          { label: "Newest issued", value: "issueDate:desc" },
          { label: "Oldest issued", value: "issueDate:asc" },
          { label: "Amount high to low", value: "amount:desc" },
          { label: "Due date soonest", value: "dueDate:asc" },
        ]}
      />
      <ListQueryView
        columns={columns}
        detailBase="/demo/invoices"
        emptyMessage="No invoices match these filters."
        query={query}
      />
    </>
  );
}

export function ExpensesList() {
  const params = useListParams("date");
  const query = useQuery({
    queryKey: ["expenses", params],
    queryFn: ({ signal }) => demoApi.expenses(params, signal),
  });
  const columns: Column<Expense>[] = [
    {
      label: "Expense",
      render: (expense) => (
        <span>
          <strong className="block">{expense.description}</strong>
          <span className="text-xs text-slate-500">{expense.supplier}</span>
        </span>
      ),
    },
    { label: "Category", render: (expense) => displayEnum(expense.category) },
    { label: "Truck", render: (expense) => expense.truck ?? "General" },
    { label: "Date", render: (expense) => formatDate(expense.date) },
    {
      label: "Amount",
      render: (expense) => formatCurrency(expense.amount, expense.currency),
    },
  ];

  return (
    <>
      <PageHeader
        description="Fuel, toll, maintenance, insurance, and operating costs linked to the fleet."
        title="Expenses"
      />
      <ListToolbar
        filters={[
          {
            name: "category",
            label: "All categories",
            options: [
              "Fuel",
              "Toll",
              "Maintenance",
              "Insurance",
              "Repair",
              "DriverExpense",
              "Other",
            ].map((value) => ({
              label: displayEnum(value),
              value,
            })),
          },
        ]}
        placeholder="Search supplier or description"
        sortOptions={[
          { label: "Newest first", value: "date:desc" },
          { label: "Oldest first", value: "date:asc" },
          { label: "Amount high to low", value: "amount:desc" },
          { label: "Supplier A–Z", value: "supplier:asc" },
        ]}
      />
      <ListQueryView
        columns={columns}
        emptyMessage="No expenses match these filters."
        query={query}
      />
    </>
  );
}

export function DocumentsList() {
  const params = useListParams("expirationDate");
  const query = useQuery({
    queryKey: ["documents", params],
    queryFn: ({ signal }) => demoApi.documents(params, signal),
  });
  const columns: Column<DocumentRecord>[] = [
    {
      label: "Document",
      render: (document) => (
        <span>
          <strong className="block">{document.name}</strong>
          <span className="text-xs text-slate-500">
            {document.referenceNumber}
          </span>
        </span>
      ),
    },
    { label: "Type", render: (document) => displayEnum(document.type) },
    { label: "Owner", render: (document) => document.owner.name },
    {
      label: "Status",
      render: (document) => <StatusBadge value={document.expirationStatus} />,
    },
    {
      label: "Expires",
      render: (document) => formatDate(document.expirationDate),
    },
  ];

  return (
    <>
      <PageHeader
        description="Compliance documents and expiry status across trucks, drivers, and clients."
        title="Documents"
      />
      <ListToolbar
        filters={[
          {
            name: "type",
            label: "All types",
            options: [
              "Insurance",
              "VehicleRegistration",
              "TechnicalInspection",
              "DriverLicence",
              "Contract",
            ].map((value) => ({
              label: displayEnum(value),
              value,
            })),
          },
          {
            name: "expiration",
            label: "Any expiration",
            options: [
              { label: "Expired", value: "expired" },
              { label: "Expiring soon", value: "expiring" },
              { label: "Valid", value: "valid" },
            ],
          },
        ]}
        placeholder="Search name, reference, or owner"
        sortOptions={[
          { label: "Expires first", value: "expirationDate:asc" },
          { label: "Expires last", value: "expirationDate:desc" },
          { label: "Name A–Z", value: "name:asc" },
          { label: "Issued newest", value: "issuedDate:desc" },
        ]}
      />
      <ListQueryView
        columns={columns}
        emptyMessage="No documents match these filters."
        query={query}
      />
    </>
  );
}
