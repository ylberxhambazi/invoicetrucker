"use client";

import { useQuery } from "@tanstack/react-query";
import {
  Bar,
  BarChart,
  CartesianGrid,
  Cell,
  Legend,
  Pie,
  PieChart,
  ResponsiveContainer,
  Tooltip,
  XAxis,
  YAxis,
} from "recharts";

import {
  MetricCard,
  PageHeader,
  QueryError,
  QueryLoading,
} from "@/components/demo/ui";
import { demoApi } from "@/lib/demo-api";
import { displayEnum, formatCurrency } from "@/lib/format";
import { BarChart3, CircleDollarSign, Truck } from "lucide-react";

const colors = [
  "#2563eb",
  "#0f766e",
  "#d97706",
  "#7c3aed",
  "#dc2626",
  "#64748b",
  "#0891b2",
];

export function Reports() {
  const query = useQuery({
    queryKey: ["reports"],
    queryFn: ({ signal }) => demoApi.reports(signal),
  });

  if (query.isPending) return <QueryLoading label="Loading reports" />;
  if (query.isError)
    return (
      <QueryError error={query.error} retry={() => void query.refetch()} />
    );

  const report = query.data;
  const totalRevenue = report.revenueByMonth.reduce(
    (sum, month) => sum + month.revenue,
    0,
  );
  const totalExpenses = report.revenueByMonth.reduce(
    (sum, month) => sum + month.expenses,
    0,
  );

  return (
    <>
      <PageHeader
        description="Twelve months of financial and fleet performance calculated by the API."
        title="Reports"
      />
      <div className="grid gap-4 sm:grid-cols-3">
        <MetricCard
          icon={<CircleDollarSign size={19} />}
          label="12-month revenue"
          value={formatCurrency(totalRevenue)}
        />
        <MetricCard
          icon={<BarChart3 size={19} />}
          label="12-month estimated profit"
          value={formatCurrency(totalRevenue - totalExpenses)}
        />
        <MetricCard
          icon={<Truck size={19} />}
          label="Fleet utilization"
          value={`${report.fleetUtilization.percentage}%`}
          detail={`${report.fleetUtilization.utilizedTrucks} of ${report.fleetUtilization.totalTrucks} trucks on route`}
        />
      </div>

      <div className="mt-5 grid gap-5 xl:grid-cols-[1.35fr_0.65fr]">
        <section className="demo-card p-5 sm:p-6">
          <h2 className="font-semibold text-slate-950">
            Revenue, expenses, and profit
          </h2>
          <div className="mt-6 h-80">
            <ResponsiveContainer height="100%" width="100%">
              <BarChart data={report.revenueByMonth}>
                <CartesianGrid
                  stroke="#e2e8f0"
                  strokeDasharray="3 3"
                  vertical={false}
                />
                <XAxis
                  axisLine={false}
                  dataKey="label"
                  fontSize={11}
                  tickFormatter={(label: string) => label.slice(0, 3)}
                  tickLine={false}
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
                <Legend />
                <Bar
                  dataKey="revenue"
                  fill="#2563eb"
                  name="Revenue"
                  radius={[4, 4, 0, 0]}
                />
                <Bar
                  dataKey="expenses"
                  fill="#f59e0b"
                  name="Expenses"
                  radius={[4, 4, 0, 0]}
                />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </section>
        <section className="demo-card p-5 sm:p-6">
          <h2 className="font-semibold text-slate-950">Expenses by category</h2>
          <div className="mt-4 h-64">
            <ResponsiveContainer height="100%" width="100%">
              <PieChart>
                <Pie
                  data={report.expensesByCategory}
                  dataKey="amount"
                  innerRadius={58}
                  nameKey="category"
                  outerRadius={90}
                  paddingAngle={2}
                >
                  {report.expensesByCategory.map((item, index) => (
                    <Cell
                      fill={colors[index % colors.length]}
                      key={item.category}
                    />
                  ))}
                </Pie>
                <Tooltip formatter={(value) => formatCurrency(Number(value))} />
              </PieChart>
            </ResponsiveContainer>
          </div>
          <div className="grid gap-2">
            {report.expensesByCategory.map((item, index) => (
              <div
                className="flex items-center justify-between text-xs"
                key={item.category}
              >
                <span className="flex items-center gap-2 text-slate-600">
                  <span
                    className="size-2.5 rounded-full"
                    style={{ backgroundColor: colors[index % colors.length] }}
                  />
                  {displayEnum(item.category)}
                </span>
                <strong className="text-slate-800">
                  {formatCurrency(item.amount)}
                </strong>
              </div>
            ))}
          </div>
        </section>
      </div>

      <section className="demo-card mt-5 overflow-hidden">
        <div className="border-b border-slate-200 p-5 sm:p-6">
          <h2 className="font-semibold text-slate-950">
            Estimated profitability by truck
          </h2>
          <p className="mt-1 text-xs text-slate-500">
            Revenue allocated through invoices minus recorded truck expenses.
          </p>
        </div>
        <div className="overflow-x-auto">
          <table className="demo-table">
            <thead>
              <tr>
                <th>Truck</th>
                <th>Revenue</th>
                <th>Expenses</th>
                <th>Estimated profit</th>
              </tr>
            </thead>
            <tbody>
              {report.profitByTruck.map((truck) => (
                <tr key={truck.truckId}>
                  <td>
                    <strong>{truck.registrationNumber}</strong>
                  </td>
                  <td>{formatCurrency(truck.allocatedRevenue)}</td>
                  <td>{formatCurrency(truck.expenses)}</td>
                  <td
                    className={
                      truck.estimatedProfit >= 0
                        ? "text-emerald-700"
                        : "text-red-700"
                    }
                  >
                    <strong>{formatCurrency(truck.estimatedProfit)}</strong>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </section>
    </>
  );
}
