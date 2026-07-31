import {
  ArrowDownRight,
  ArrowUpRight,
  Bell,
  ChevronDown,
  CircleDollarSign,
  FileText,
  LayoutDashboard,
  MoreHorizontal,
  Search,
  Truck,
  UsersRound,
} from "lucide-react";

const metrics = [
  {
    change: "+2 this month",
    icon: Truck,
    label: "Active trucks",
    positive: true,
    value: "13",
  },
  {
    change: "83% available",
    icon: UsersRound,
    label: "Available drivers",
    positive: true,
    value: "10",
  },
  {
    change: "5 need attention",
    icon: FileText,
    label: "Outstanding",
    positive: false,
    value: "€18,420",
  },
];

const chartBars = [
  { expense: 44, revenue: 67 },
  { expense: 49, revenue: 73 },
  { expense: 42, revenue: 69 },
  { expense: 55, revenue: 82 },
  { expense: 51, revenue: 77 },
  { expense: 58, revenue: 91 },
  { expense: 56, revenue: 86 },
  { expense: 60, revenue: 95 },
];

export function DashboardPreview() {
  return (
    <div
      className="dashboard-frame"
      aria-label="InvoiceTrucker dashboard preview"
    >
      <div className="dashboard-sidebar" aria-hidden="true">
        <span className="dashboard-mark">
          <span />
          <span />
        </span>
        <span className="dashboard-side-icon dashboard-side-icon-active">
          <LayoutDashboard size={15} />
        </span>
        <span className="dashboard-side-icon">
          <Truck size={15} />
        </span>
        <span className="dashboard-side-icon">
          <UsersRound size={15} />
        </span>
        <span className="dashboard-side-icon">
          <FileText size={15} />
        </span>
      </div>

      <div className="min-w-0 flex-1">
        <div className="dashboard-topbar">
          <div>
            <p className="text-[8px] font-semibold text-slate-950 sm:text-[10px]">
              Operations overview
            </p>
            <p className="mt-0.5 text-[6px] text-slate-400 sm:text-[8px]">
              Monday, 25 July
            </p>
          </div>
          <div className="flex items-center gap-1.5 sm:gap-2">
            <span className="dashboard-search">
              <Search size={9} />
              Search
            </span>
            <span className="dashboard-icon-button">
              <Bell size={10} />
            </span>
            <span className="dashboard-avatar">AM</span>
            <ChevronDown className="text-slate-400" size={9} />
          </div>
        </div>

        <div className="dashboard-content">
          <div className="dashboard-metrics">
            {metrics.map((metric) => {
              const Icon = metric.icon;
              const ChangeIcon = metric.positive
                ? ArrowUpRight
                : ArrowDownRight;

              return (
                <div className="dashboard-metric" key={metric.label}>
                  <div className="flex items-start justify-between">
                    <span className="dashboard-metric-icon">
                      <Icon size={10} />
                    </span>
                    <MoreHorizontal className="text-slate-300" size={11} />
                  </div>
                  <p className="mt-2 text-[6px] font-medium text-slate-400 sm:text-[8px]">
                    {metric.label}
                  </p>
                  <div className="mt-0.5 flex items-end justify-between gap-1">
                    <strong className="text-[11px] tracking-tight text-slate-950 sm:text-sm">
                      {metric.value}
                    </strong>
                    <span
                      className={
                        metric.positive
                          ? "dashboard-change text-emerald-600"
                          : "dashboard-change text-amber-600"
                      }
                    >
                      <ChangeIcon size={7} />
                      {metric.change}
                    </span>
                  </div>
                </div>
              );
            })}
          </div>

          <div className="dashboard-grid">
            <div className="dashboard-panel dashboard-revenue">
              <div className="flex items-start justify-between">
                <div>
                  <p className="dashboard-panel-title">Financial performance</p>
                  <p className="mt-1 text-[12px] font-semibold tracking-tight text-slate-950 sm:text-base">
                    €46,860
                  </p>
                </div>
                <span className="dashboard-period">Last 8 months</span>
              </div>
              <div className="mt-3 flex items-center gap-3 text-[6px] text-slate-400 sm:text-[8px]">
                <span className="flex items-center gap-1">
                  <i className="size-1.5 rounded-full bg-blue-600" /> Revenue
                </span>
                <span className="flex items-center gap-1">
                  <i className="size-1.5 rounded-full bg-slate-200" /> Expenses
                </span>
              </div>
              <div className="dashboard-chart">
                {chartBars.map((bar, index) => (
                  <div className="dashboard-bar-group" key={index}>
                    <span
                      className="dashboard-bar bg-slate-200"
                      style={{ height: `${bar.expense}%` }}
                    />
                    <span
                      className="dashboard-bar bg-blue-600"
                      style={{ height: `${bar.revenue}%` }}
                    />
                  </div>
                ))}
              </div>
            </div>

            <div className="dashboard-panel dashboard-utilization">
              <div className="flex items-center justify-between">
                <p className="dashboard-panel-title">Fleet utilization</p>
                <MoreHorizontal className="text-slate-300" size={11} />
              </div>
              <div className="dashboard-donut">
                <div>
                  <strong>87%</strong>
                  <span>utilized</span>
                </div>
              </div>
              <div className="mt-2 grid grid-cols-2 gap-1 text-[6px] sm:text-[8px]">
                <span className="text-slate-400">On route</span>
                <strong className="text-right text-slate-700">11 trucks</strong>
                <span className="text-slate-400">Available</span>
                <strong className="text-right text-slate-700">2 trucks</strong>
              </div>
            </div>

            <div className="dashboard-panel dashboard-invoices">
              <div className="flex items-center justify-between">
                <p className="dashboard-panel-title">Recent invoices</p>
                <span className="text-[6px] font-semibold text-blue-600 sm:text-[8px]">
                  View all
                </span>
              </div>
              <div className="mt-2 space-y-1.5">
                {[
                  ["IT-2048", "Northlane Foods", "€4,280", "Paid"],
                  ["IT-2047", "Aster Retail", "€2,940", "Sent"],
                  ["IT-2046", "Cobalt Works", "€3,160", "Overdue"],
                ].map(([invoice, client, amount, status]) => (
                  <div className="dashboard-invoice-row" key={invoice}>
                    <span className="dashboard-company-icon">
                      <CircleDollarSign size={8} />
                    </span>
                    <span className="min-w-0 flex-1">
                      <strong>{client}</strong>
                      <small>{invoice}</small>
                    </span>
                    <strong className="text-right">{amount}</strong>
                    <span
                      className={
                        status === "Paid"
                          ? "dashboard-status bg-emerald-50 text-emerald-700"
                          : status === "Overdue"
                            ? "dashboard-status bg-red-50 text-red-700"
                            : "dashboard-status bg-blue-50 text-blue-700"
                      }
                    >
                      {status}
                    </span>
                  </div>
                ))}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
