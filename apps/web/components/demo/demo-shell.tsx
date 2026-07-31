"use client";

import {
  BarChart3,
  FileArchive,
  FileText,
  Gauge,
  Menu,
  ReceiptText,
  Settings,
  Truck,
  UsersRound,
  WalletCards,
  X,
} from "lucide-react";
import Link from "next/link";
import { usePathname } from "next/navigation";
import { useState } from "react";

import { Brand } from "@/components/brand";

const navigation = [
  { href: "/demo", icon: Gauge, label: "Dashboard" },
  { href: "/demo/trucks", icon: Truck, label: "Trucks" },
  { href: "/demo/drivers", icon: UsersRound, label: "Drivers" },
  { href: "/demo/clients", icon: WalletCards, label: "Clients" },
  { href: "/demo/invoices", icon: ReceiptText, label: "Invoices" },
  { href: "/demo/expenses", icon: FileText, label: "Expenses" },
  { href: "/demo/reports", icon: BarChart3, label: "Reports" },
  { href: "/demo/documents", icon: FileArchive, label: "Documents" },
  { href: "/demo/settings", icon: Settings, label: "Settings" },
];

function DemoNavigation({ onNavigate }: { onNavigate?: () => void }) {
  const pathname = usePathname();

  return (
    <nav aria-label="Demo navigation" className="mt-7 grid gap-1">
      {navigation.map(({ href, icon: Icon, label }) => {
        const active =
          pathname === href ||
          (href !== "/demo" && pathname.startsWith(`${href}/`));

        return (
          <Link
            aria-current={active ? "page" : undefined}
            className={
              active ? "demo-nav-link demo-nav-link-active" : "demo-nav-link"
            }
            href={href}
            key={href}
            onClick={onNavigate}
          >
            <Icon aria-hidden="true" size={18} />
            {label}
          </Link>
        );
      })}
    </nav>
  );
}

export function DemoShell({ children }: { children: React.ReactNode }) {
  const [menuOpen, setMenuOpen] = useState(false);

  return (
    <div className="demo-shell">
      <a className="demo-skip-link" href="#demo-content">
        Skip to demo content
      </a>
      <aside className="demo-sidebar">
        <Brand />
        <DemoNavigation />
        <div className="mt-auto rounded-xl border border-blue-100 bg-blue-50 p-4">
          <p className="text-xs font-semibold uppercase tracking-wide text-blue-700">
            Portfolio demo
          </p>
          <p className="mt-1 text-xs leading-5 text-slate-600">
            Read-only interface with deterministic fictional data.
          </p>
        </div>
      </aside>

      <div className="min-w-0">
        <header className="demo-mobile-header">
          <Brand />
          <button
            aria-expanded={menuOpen}
            aria-label={menuOpen ? "Close navigation" : "Open navigation"}
            className="demo-icon-button"
            onClick={() => setMenuOpen((open) => !open)}
            type="button"
          >
            {menuOpen ? <X size={20} /> : <Menu size={20} />}
          </button>
        </header>
        {menuOpen ? (
          <div className="demo-mobile-menu">
            <DemoNavigation onNavigate={() => setMenuOpen(false)} />
          </div>
        ) : null}
        <div className="demo-readonly-banner">
          <span
            aria-hidden="true"
            className="size-2 rounded-full bg-emerald-500"
          />
          Live fictional data from the InvoiceTrucker API · Read only
        </div>
        <main className="demo-content" id="demo-content">
          {children}
        </main>
      </div>
    </div>
  );
}
