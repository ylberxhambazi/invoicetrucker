"use client";

import {
  ChartNoAxesCombined,
  CircleDollarSign,
  FileText,
  Fuel,
  Truck,
  UsersRound,
} from "lucide-react";
import { useState } from "react";

const views = [
  {
    columns: ["Vehicle", "Driver", "Status", "Mileage"],
    icon: Truck,
    id: "trucks",
    rows: [
      ["IT-TRK-014", "Elian Voss", "On route", "428,190 km"],
      ["IT-TRK-009", "Mira Daneva", "Available", "316,880 km"],
      ["IT-TRK-003", "Noah Petreski", "Service", "509,240 km"],
    ],
    title: "Trucks",
  },
  {
    columns: ["Driver", "Assigned truck", "Status", "Trips"],
    icon: UsersRound,
    id: "drivers",
    rows: [
      ["Elian Voss", "IT-TRK-014", "On route", "128"],
      ["Mira Daneva", "IT-TRK-009", "Available", "116"],
      ["Noah Petreski", "IT-TRK-003", "Off duty", "104"],
    ],
    title: "Drivers",
  },
  {
    columns: ["Client", "Contact", "Invoices", "Total billed"],
    icon: UsersRound,
    id: "clients",
    rows: [
      ["Northlane Foods", "Sofia Marin", "4 active", "€38,420"],
      ["Aster Retail", "Leon Haas", "2 active", "€26,180"],
      ["Cobalt Works", "Eva Korhonen", "1 active", "€18,940"],
    ],
    title: "Clients",
  },
  {
    columns: ["Invoice", "Client", "Amount", "Status"],
    icon: FileText,
    id: "invoices",
    rows: [
      ["IT-2048", "Northlane Foods", "€4,280", "Paid"],
      ["IT-2047", "Aster Retail", "€2,940", "Sent"],
      ["IT-2046", "Cobalt Works", "€3,160", "Overdue"],
    ],
    title: "Invoices",
  },
  {
    columns: ["Category", "Truck", "Supplier", "Amount"],
    icon: Fuel,
    id: "expenses",
    rows: [
      ["Fuel", "IT-TRK-014", "Fictional Fuel Co.", "€1,420"],
      ["Toll", "IT-TRK-009", "EuroRoute Demo", "€680"],
      ["Maintenance", "IT-TRK-003", "Apex Garage", "€2,180"],
    ],
    title: "Expenses",
  },
  {
    columns: ["Report", "Period", "Result", "Trend"],
    icon: ChartNoAxesCombined,
    id: "reports",
    rows: [
      ["Revenue", "July 2026", "€46,860", "+12.4%"],
      ["Expenses", "July 2026", "€29,140", "+4.8%"],
      ["Estimated profit", "July 2026", "€17,720", "+18.2%"],
    ],
    title: "Reports",
  },
];

export function ProductShowcase() {
  const [activeView, setActiveView] = useState(views[0]);

  return (
    <div className="showcase-shell">
      <div className="showcase-tabs" role="tablist" aria-label="Product views">
        {views.map((view) => {
          const Icon = view.icon;
          const isActive = activeView.id === view.id;

          return (
            <button
              aria-controls="showcase-panel"
              aria-selected={isActive}
              className={
                isActive ? "showcase-tab showcase-tab-active" : "showcase-tab"
              }
              id={`tab-${view.id}`}
              key={view.id}
              onClick={() => setActiveView(view)}
              role="tab"
              type="button"
            >
              <Icon aria-hidden="true" size={16} />
              {view.title}
            </button>
          );
        })}
      </div>

      <div
        aria-labelledby={`tab-${activeView.id}`}
        className="showcase-panel"
        id="showcase-panel"
        role="tabpanel"
      >
        <div className="flex flex-col justify-between gap-4 border-b border-slate-200 px-5 py-5 sm:flex-row sm:items-center sm:px-7">
          <div>
            <p className="text-sm font-semibold text-slate-950">
              {activeView.title}
            </p>
            <p className="mt-1 text-sm text-slate-500">
              Live operational records across your business.
            </p>
          </div>
          <div className="flex items-center gap-2">
            <span className="showcase-search">
              Search {activeView.title.toLowerCase()}
            </span>
            <span className="showcase-action">Filter</span>
          </div>
        </div>

        <div className="hidden overflow-x-auto sm:block">
          <table className="w-full border-collapse text-left">
            <thead>
              <tr>
                {activeView.columns.map((column) => (
                  <th key={column}>{column}</th>
                ))}
              </tr>
            </thead>
            <tbody>
              {activeView.rows.map((row) => (
                <tr key={row[0]}>
                  {row.map((cell, index) => (
                    <td key={cell}>
                      {index === 0 ? (
                        <span className="flex items-center gap-2.5 font-semibold text-slate-900">
                          <span className="grid size-8 place-items-center rounded-lg bg-slate-100 text-slate-500">
                            <CircleDollarSign size={14} />
                          </span>
                          {cell}
                        </span>
                      ) : (
                        cell
                      )}
                    </td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
        </div>

        <div className="grid gap-3 p-4 sm:hidden">
          {activeView.rows.map((row) => (
            <article
              className="rounded-xl border border-slate-200 bg-white p-4"
              key={row[0]}
            >
              <strong className="text-sm text-slate-950">{row[0]}</strong>
              <dl className="mt-3 grid grid-cols-2 gap-x-4 gap-y-2 text-xs">
                {row.slice(1).map((cell, index) => (
                  <div key={cell}>
                    <dt className="text-slate-400">
                      {activeView.columns[index + 1]}
                    </dt>
                    <dd className="mt-0.5 font-medium text-slate-700">
                      {cell}
                    </dd>
                  </div>
                ))}
              </dl>
            </article>
          ))}
        </div>

        <div className="flex items-center justify-between border-t border-slate-200 px-5 py-4 text-xs text-slate-500 sm:px-7">
          <span>Showing 3 of 15 fictional records</span>
          <span>1 / 5</span>
        </div>
      </div>
    </div>
  );
}
