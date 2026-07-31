"use client";

import {
  BarChart3,
  CircleDollarSign,
  ReceiptText,
  Truck,
  UsersRound,
} from "lucide-react";
import Link from "next/link";
import {
  Area,
  AreaChart,
  CartesianGrid,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";
import { useQuery } from "@tanstack/react-query";

import {
  MetricCard,
  PageHeader,
  QueryError,
  QueryLoading,
  StatusBadge,
} from "@/components/demo/ui";
import { demoApi } from "@/lib/demo-api";
import { formatCurrency, formatDate } from "@/lib/format";

export function Dashboard() {
  const query = useQuery({
    queryKey: ["dashboard"],
    queryFn: ({ signal }) => demoApi.dashboard(signal),
  });

  if (query.isPending) return <QueryLoading label="Loading dashboard" />;
  if (query.isError)
    return (
      <QueryError error={query.error} retry={() => void query.refetch()} />
    );

  const dashboard = query.data;

  return (
    <>
      <PageHeader
        description="A read-only operational snapshot generated from the deterministic PostgreSQL dataset."
        title="Operations overview"
      />
      <div className="grid gap-4 sm:grid-cols-2 xl:grid-cols-4">
        <MetricCard
          icon={<Truck size={19} />}
          label="Active trucks"
          value={String(dashboard.activeTrucks)}
          detail={`${dashboard.fleetUtilizationPercentage}% currently on route`}
        />
        <MetricCard
          icon={<UsersRound size={19} />}
          label="Available drivers"
          value={String(dashboard.availableDrivers)}
          detail="Ready for assignment"
        />
        <MetricCard
          icon={<ReceiptText size={19} />}
          label="Outstanding invoices"
          value={formatCurrency(dashboard.outstandingInvoices)}
          detail="Sent and overdue"
        />
        <MetricCard
          icon={<CircleDollarSign size={19} />}
          label="Estimated monthly profit"
          value={formatCurrency(dashboard.estimatedProfit)}
          detail={`${formatCurrency(dashboard.monthlyRevenue)} revenue`}
        />
      </div>

      <div className="mt-5 grid gap-5 xl:grid-cols-[1.45fr_0.55fr]">
        <section className="demo-card p-5 sm:p-6">
          <div className="flex items-center justify-between gap-4">
            <div>
              <h2 className="font-semibold text-slate-950">
                Financial performance
              </h2>
              <p className="mt-1 text-xs text-slate-500">
                Revenue and expenses across 12 months
              </p>
            </div>
            <BarChart3 className="text-blue-600" size={20} />
          </div>
          <div className="mt-6 h-72">
            <ResponsiveContainer height="100%" width="100%">
              <AreaChart data={dashboard.financialPerformance}>
                <defs>
                  <linearGradient
                    id="dashboardRevenue"
                    x1="0"
                    x2="0"
                    y1="0"
                    y2="1"
                  >
                    <stop offset="5%" stopColor="#2563eb" stopOpacity={0.25} />
                    <stop offset="95%" stopColor="#2563eb" stopOpacity={0} />
                  </linearGradient>
                </defs>
                <CartesianGrid
                  stroke="#e2e8f0"
                  strokeDasharray="3 3"
                  vertical={false}
                />
                <XAxis
                  axisLine={false}
                  dataKey="label"
                  fontSize={11}
                  tickLine={false}
                  tickFormatter={(label: string) => label.slice(0, 3)}
                />
                <YAxis
                  axisLine={false}
                  fontSize={11}
                  tickFormatter={(value: number) =>
                    `€${Math.round(value / 1000)}k`
                  }
                  tickLine={false}
                  width={46}
                />
                <Tooltip formatter={(value) => formatCurrency(Number(value))} />
                <Area
                  dataKey="revenue"
                  fill="url(#dashboardRevenue)"
                  name="Revenue"
                  stroke="#2563eb"
                  strokeWidth={2}
                  type="monotone"
                />
                <Area
                  dataKey="expenses"
                  fill="transparent"
                  name="Expenses"
                  stroke="#f59e0b"
                  strokeWidth={2}
                  type="monotone"
                />
              </AreaChart>
            </ResponsiveContainer>
          </div>
        </section>

        <section className="demo-card p-5 sm:p-6">
          <h2 className="font-semibold text-slate-950">Invoice status</h2>
          <div className="mt-5 grid gap-4">
            {dashboard.invoiceStatusSummary.map((item) => (
              <div
                className="flex items-center justify-between gap-4 border-b border-slate-100 pb-4 last:border-0 last:pb-0"
                key={item.status}
              >
                <div>
                  <StatusBadge value={item.status} />
                  <p className="mt-2 text-xs text-slate-500">
                    {item.count} invoices
                  </p>
                </div>
                <strong className="text-sm text-slate-800">
                  {formatCurrency(item.amount)}
                </strong>
              </div>
            ))}
          </div>
        </section>
      </div>

      <div className="mt-5 grid gap-5 lg:grid-cols-2">
        <section className="demo-card p-5 sm:p-6">
          <h2 className="font-semibold text-slate-950">Recent activity</h2>
          <ol className="mt-5 grid gap-4">
            {dashboard.recentActivity.slice(0, 6).map((activity) => (
              <li className="flex gap-3" key={activity.id}>
                <span
                  aria-hidden="true"
                  className="mt-2 size-2 shrink-0 rounded-full bg-blue-500"
                />
                <div>
                  <p className="text-sm font-medium text-slate-700">
                    {activity.message}
                  </p>
                  <time
                    className="mt-1 block text-xs text-slate-400"
                    dateTime={activity.occurredAtUtc}
                  >
                    {new Date(activity.occurredAtUtc).toLocaleString("en-GB", {
                      timeZone: "UTC",
                    })}{" "}
                    UTC
                  </time>
                </div>
              </li>
            ))}
          </ol>
        </section>
        <section className="demo-card p-5 sm:p-6">
          <h2 className="font-semibold text-slate-950">Upcoming expirations</h2>
          <div className="mt-5 grid gap-3">
            {dashboard.upcomingDocumentExpirations.length ? (
              dashboard.upcomingDocumentExpirations
                .slice(0, 6)
                .map((document) => (
                  <div
                    className="flex items-center justify-between gap-4 rounded-xl bg-slate-50 p-3"
                    key={document.id}
                  >
                    <div className="min-w-0">
                      <p className="truncate text-sm font-medium text-slate-700">
                        {document.name}
                      </p>
                      <p className="mt-1 text-xs text-slate-500">
                        {document.owner} · {formatDate(document.expirationDate)}
                      </p>
                    </div>
                    <span className="shrink-0 text-xs font-semibold text-amber-700">
                      {document.daysRemaining} days
                    </span>
                  </div>
                ))
            ) : (
              <p className="text-sm text-slate-500">
                No documents expire within 60 days.
              </p>
            )}
          </div>
          <Link
            className="demo-row-link mt-5 inline-flex"
            href="/demo/documents"
          >
            View all documents
          </Link>
        </section>
      </div>
    </>
  );
}
