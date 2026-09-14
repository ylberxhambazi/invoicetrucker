"use client";

import { Menu, X } from "lucide-react";
import Link from "next/link";
import { useEffect, useState } from "react";

import { Brand } from "./brand";

const navigation = [
  { href: "/#product", label: "Product" },
  { href: "/#how-it-works", label: "How It Works" },
  { href: "/#small-fleets", label: "For Small Fleets" },
  { href: "/resources", label: "Resources" },
  { href: "/#faq", label: "FAQ" },
  { href: "/#about", label: "About" },
];

export function SiteHeader() {
  const [isOpen, setIsOpen] = useState(false);

  useEffect(() => {
    if (!isOpen) return;

    const closeOnEscape = (event: KeyboardEvent) => {
      if (event.key === "Escape") setIsOpen(false);
    };

    window.addEventListener("keydown", closeOnEscape);
    return () => window.removeEventListener("keydown", closeOnEscape);
  }, [isOpen]);

  return (
    <header className="sticky top-0 z-50 border-b border-slate-200/90 bg-white/95 backdrop-blur-sm">
      <div className="mx-auto flex h-18 w-full max-w-7xl items-center justify-between px-5 sm:px-8 lg:px-10">
        <Brand />

        <nav aria-label="Primary navigation" className="hidden lg:block">
          <ul className="flex items-center gap-1">
            {navigation.map((item) => (
              <li key={item.label}>
                <Link
                  className="rounded-lg px-3 py-2 text-sm font-medium text-slate-600 transition-colors hover:bg-slate-50 hover:text-slate-950 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600"
                  href={item.href}
                >
                  {item.label}
                </Link>
              </li>
            ))}
          </ul>
        </nav>

        <div className="hidden items-center gap-3 lg:flex">
          <Link
            className="rounded-lg px-3 py-2 text-sm font-semibold text-slate-700 transition-colors hover:text-slate-950 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600"
            href="/demo"
          >
            Explore Product
          </Link>
          <Link
            className="inline-flex min-h-10 items-center rounded-xl bg-blue-600 px-4 py-2 text-sm font-semibold text-white shadow-sm transition-colors hover:bg-blue-700 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600"
            href="/#early-access"
          >
            Get Early Access
          </Link>
        </div>

        <button
          aria-controls="mobile-navigation"
          aria-expanded={isOpen}
          aria-label={isOpen ? "Close navigation menu" : "Open navigation menu"}
          className="grid size-11 place-items-center rounded-xl border border-slate-200 bg-white text-slate-700 transition-colors hover:bg-slate-50 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600 lg:hidden"
          onClick={() => setIsOpen((current) => !current)}
          type="button"
        >
          {isOpen ? (
            <X aria-hidden="true" size={20} />
          ) : (
            <Menu aria-hidden="true" size={20} />
          )}
        </button>
      </div>

      {isOpen ? (
        <nav
          aria-label="Mobile navigation"
          className="border-t border-slate-200 bg-white px-5 py-5 lg:hidden"
          id="mobile-navigation"
        >
          <ul className="mx-auto flex max-w-7xl flex-col gap-1">
            {navigation.map((item) => (
              <li key={item.label}>
                <Link
                  className="block rounded-xl px-3 py-3 text-base font-medium text-slate-700 hover:bg-slate-50 hover:text-slate-950 focus-visible:outline-2 focus-visible:outline-blue-600"
                  href={item.href}
                  onClick={() => setIsOpen(false)}
                >
                  {item.label}
                </Link>
              </li>
            ))}
            <li className="mt-3 grid gap-3 sm:grid-cols-2">
              <Link
                className="inline-flex min-h-11 items-center justify-center rounded-xl border border-slate-300 bg-white px-4 text-sm font-semibold text-slate-900"
                href="/demo"
                onClick={() => setIsOpen(false)}
              >
                Explore Product
              </Link>
              <Link
                className="inline-flex min-h-11 items-center justify-center rounded-xl bg-blue-600 px-4 text-sm font-semibold text-white"
                href="/#early-access"
                onClick={() => setIsOpen(false)}
              >
                Get Early Access
              </Link>
            </li>
          </ul>
        </nav>
      ) : null}
    </header>
  );
}
