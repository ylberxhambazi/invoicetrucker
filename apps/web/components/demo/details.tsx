"use client";

import { useQuery } from "@tanstack/react-query";
import { Printer } from "lucide-react";
import Link from "next/link";

import {
  DetailHeader,
  InfoGrid,
  QueryError,
  QueryLoading,
  StatusBadge,
} from "@/components/demo/ui";
import { demoApi } from "@/lib/demo-api";
import { formatCurrency, formatDate, formatNumber } from "@/lib/format";

export function TruckDetailView({ id }: { id: string }) {
  const query = useQuery({
    queryKey: ["truck", id],
    queryFn: ({ signal }) => demoApi.truck(id, signal),
  });

  if (query.isPending) return <QueryLoading label="Loading truck" />;
  if (query.isError)
    return (
      <QueryError error={query.error} retry={() => void query.refetch()} />
    );

  const truck = query.data;

  return (
    <>
      <DetailHeader
        backHref="/demo/trucks"
        backLabel="Back to trucks"
        status={truck.status}
        subtitle={`${truck.make} ${truck.model} · ${truck.year}`}
        title={truck.registrationNumber}
      />
      <InfoGrid
        items={[
          {
            label: "Current mileage",
            value: `${formatNumber(truck.currentMileage)} km`,
          },
          {
            label: "Assigned driver",
            value: truck.assignedDriver ? (
              <Link
                className="demo-row-link"
                href={`/demo/drivers/${truck.assignedDriver.id}`}
              >
                {truck.assignedDriver.fullName}
              </Link>
            ) : (
              "Unassigned"
            ),
          },
          {
            label: "Insurance expires",
            value: formatDate(truck.insuranceExpiration),
          },
          {
            label: "Inspection expires",
            value: formatDate(truck.technicalInspectionExpiration),
          },
          {
            label: "Recorded expenses",
            value: formatCurrency(truck.totalExpenses),
          },
          { label: "Linked documents", value: truck.documentCount },
          { label: "Created", value: formatDate(truck.createdAtUtc) },
          { label: "Last updated", value: formatDate(truck.updatedAtUtc) },
        ]}
      />
    </>
  );
}

export function DriverDetailView({ id }: { id: string }) {
  const query = useQuery({
    queryKey: ["driver", id],
    queryFn: ({ signal }) => demoApi.driver(id, signal),
  });

  if (query.isPending) return <QueryLoading label="Loading driver" />;
  if (query.isError)
    return (
      <QueryError error={query.error} retry={() => void query.refetch()} />
    );

  const driver = query.data;

  return (
    <>
      <DetailHeader
        backHref="/demo/drivers"
        backLabel="Back to drivers"
        status={driver.status}
        subtitle={driver.email}
        title={driver.fullName}
      />
      <InfoGrid
        items={[
          { label: "Phone", value: driver.phone },
          { label: "Licence number", value: driver.licenceNumber },
          {
            label: "Licence expires",
            value: formatDate(driver.licenceExpiration),
          },
          {
            label: "Completed trips",
            value: formatNumber(driver.completedTrips),
          },
          {
            label: "Assigned truck",
            value: driver.assignedTruck ? (
              <Link
                className="demo-row-link"
                href={`/demo/trucks/${driver.assignedTruck.id}`}
              >
                {driver.assignedTruck.registrationNumber} ·{" "}
                {driver.assignedTruck.make} {driver.assignedTruck.model}
              </Link>
            ) : (
              "Unassigned"
            ),
          },
          { label: "Linked documents", value: driver.documentCount },
          { label: "Created", value: formatDate(driver.createdAtUtc) },
          { label: "Last updated", value: formatDate(driver.updatedAtUtc) },
        ]}
      />
    </>
  );
}

export function ClientDetailView({ id }: { id: string }) {
  const query = useQuery({
    queryKey: ["client", id],
    queryFn: ({ signal }) => demoApi.client(id, signal),
  });

  if (query.isPending) return <QueryLoading label="Loading client" />;
  if (query.isError)
    return (
      <QueryError error={query.error} retry={() => void query.refetch()} />
    );

  const client = query.data;

  return (
    <>
      <DetailHeader
        backHref="/demo/clients"
        backLabel="Back to clients"
        status={client.isActive ? "Active" : "Inactive"}
        subtitle={`${client.contactPerson} · ${client.country}`}
        title={client.companyName}
      />
      <InfoGrid
        items={[
          { label: "Email", value: client.email },
          { label: "Phone", value: client.phone },
          { label: "Active invoices", value: client.activeInvoices },
          { label: "Total billed", value: formatCurrency(client.totalBilled) },
        ]}
      />
      <section className="demo-card mt-5 overflow-hidden">
        <div className="border-b border-slate-200 p-5">
          <h2 className="font-semibold text-slate-950">Recent invoices</h2>
        </div>
        {client.recentInvoices.length ? (
          <div className="overflow-x-auto">
            <table className="demo-table">
              <thead>
                <tr>
                  <th>Invoice</th>
                  <th>Issued</th>
                  <th>Due</th>
                  <th>Status</th>
                  <th>Amount</th>
                  <th>
                    <span className="sr-only">Action</span>
                  </th>
                </tr>
              </thead>
              <tbody>
                {client.recentInvoices.map((invoice) => (
                  <tr key={invoice.id}>
                    <td>
                      <strong>{invoice.invoiceNumber}</strong>
                    </td>
                    <td>{formatDate(invoice.issueDate)}</td>
                    <td>{formatDate(invoice.dueDate)}</td>
                    <td>
                      <StatusBadge value={invoice.status} />
                    </td>
                    <td>{formatCurrency(invoice.amount, invoice.currency)}</td>
                    <td className="text-right">
                      <Link
                        className="demo-row-link"
                        href={`/demo/invoices/${invoice.id}`}
                      >
                        View
                      </Link>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        ) : (
          <p className="p-5 text-sm text-slate-500">No recent invoices.</p>
        )}
      </section>
    </>
  );
}

export function InvoiceDetailView({ id }: { id: string }) {
  const query = useQuery({
    queryKey: ["invoice", id],
    queryFn: ({ signal }) => demoApi.invoice(id, signal),
  });

  if (query.isPending) return <QueryLoading label="Loading invoice" />;
  if (query.isError)
    return (
      <QueryError error={query.error} retry={() => void query.refetch()} />
    );

  const invoice = query.data;

  return (
    <div className="invoice-print-area">
      <DetailHeader
        action={
          <button
            className="demo-button demo-button-secondary print-hidden"
            onClick={() => window.print()}
            type="button"
          >
            <Printer size={16} />
            Print preview
          </button>
        }
        backHref="/demo/invoices"
        backLabel="Back to invoices"
        status={invoice.status}
        subtitle={`Issued ${formatDate(invoice.issueDate)} · Due ${formatDate(invoice.dueDate)}`}
        title={invoice.invoiceNumber}
      />
      <article className="demo-card overflow-hidden">
        <header className="invoice-header">
          <div>
            <p className="text-xs font-semibold uppercase tracking-[0.18em] text-blue-600">
              InvoiceTrucker
            </p>
            <h2 className="mt-2 text-2xl font-semibold text-slate-950">
              Invoice
            </h2>
            <p className="mt-1 text-sm text-slate-500">
              {invoice.invoiceNumber}
            </p>
          </div>
          <div className="text-left sm:text-right">
            <p className="text-sm font-semibold text-slate-900">
              Northstar Demo Logistics
            </p>
            <p className="mt-1 text-xs leading-5 text-slate-500">
              42 Fictional Freight Avenue
              <br />
              Skopje, North Macedonia
            </p>
          </div>
        </header>
        <div className="grid gap-6 border-b border-slate-200 p-6 sm:grid-cols-2">
          <div>
            <p className="invoice-label">Bill to</p>
            <Link
              className="mt-2 inline-block font-semibold text-slate-900 hover:text-blue-700"
              href={`/demo/clients/${invoice.client.id}`}
            >
              {invoice.client.companyName}
            </Link>
            <p className="mt-1 text-sm leading-6 text-slate-500">
              {invoice.client.contactPerson}
              <br />
              {invoice.client.email}
              <br />
              {invoice.client.country}
            </p>
          </div>
          <dl className="grid grid-cols-2 gap-4 sm:text-right">
            <div>
              <dt className="invoice-label">Issue date</dt>
              <dd className="mt-2 text-sm font-medium">
                {formatDate(invoice.issueDate)}
              </dd>
            </div>
            <div>
              <dt className="invoice-label">Due date</dt>
              <dd className="mt-2 text-sm font-medium">
                {formatDate(invoice.dueDate)}
              </dd>
            </div>
            <div>
              <dt className="invoice-label">Truck</dt>
              <dd className="mt-2 text-sm font-medium">
                {invoice.truck ? (
                  <Link
                    className="demo-row-link"
                    href={`/demo/trucks/${invoice.truck.id}`}
                  >
                    {invoice.truck.registrationNumber}
                  </Link>
                ) : (
                  "Not assigned"
                )}
              </dd>
            </div>
            <div>
              <dt className="invoice-label">Currency</dt>
              <dd className="mt-2 text-sm font-medium">{invoice.currency}</dd>
            </div>
          </dl>
        </div>
        <div className="overflow-x-auto p-6">
          <table className="invoice-table">
            <thead>
              <tr>
                <th>Description</th>
                <th>Quantity</th>
                <th>Unit price</th>
                <th>Total</th>
              </tr>
            </thead>
            <tbody>
              {invoice.items.map((item) => (
                <tr key={item.id}>
                  <td>{item.description}</td>
                  <td>{item.quantity}</td>
                  <td>{formatCurrency(item.unitPrice, invoice.currency)}</td>
                  <td>
                    <strong>
                      {formatCurrency(item.lineTotal, invoice.currency)}
                    </strong>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          <div className="ml-auto mt-6 max-w-xs border-t-2 border-slate-900 pt-4">
            <div className="flex items-center justify-between">
              <span className="font-semibold text-slate-700">Total</span>
              <strong className="text-xl text-slate-950">
                {formatCurrency(invoice.amount, invoice.currency)}
              </strong>
            </div>
          </div>
        </div>
        <footer className="border-t border-slate-200 bg-slate-50 p-6">
          <p className="invoice-label">Notes</p>
          <p className="mt-2 text-sm leading-6 text-slate-600">
            {invoice.notes}
          </p>
          <p className="mt-4 text-xs text-slate-400">
            This is a fictional portfolio invoice. It is not a request for
            payment.
          </p>
        </footer>
      </article>
    </div>
  );
}
