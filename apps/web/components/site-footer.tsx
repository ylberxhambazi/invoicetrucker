import { Container } from "@invoicetrucker/ui";
import { Github } from "lucide-react";
import Link from "next/link";

import { Brand } from "./brand";

const footerGroups = [
  {
    links: [
      { href: "/#product", label: "Product" },
      { href: "/#how-it-works", label: "How it works" },
      { href: "/demo", label: "Product demo" },
      { href: "/#early-access", label: "Early access" },
    ],
    title: "Product",
  },
  {
    links: [
      { href: "/resources", label: "Resources" },
      { href: "/#small-fleets", label: "For small fleets" },
      { href: "/#faq", label: "FAQ" },
      { href: "/#about", label: "About" },
    ],
    title: "Company",
  },
  {
    links: [
      { href: "/legal/privacy", label: "Privacy" },
      { href: "/legal/terms", label: "Terms" },
      { href: "/legal/cookies", label: "Cookies" },
      { href: "/legal/subprocessors", label: "Subprocessors" },
      { href: "/legal/dpa", label: "DPA" },
      { href: "/legal", label: "Legal" },
    ],
    title: "Legal",
  },
];

export function SiteFooter() {
  return (
    <footer className="border-t border-slate-200 bg-white">
      <Container className="grid gap-10 py-12 sm:grid-cols-2 lg:grid-cols-[1.5fr_1fr_1fr_1fr] lg:py-16">
        <div className="max-w-md">
          <Brand />
          <p className="mt-5 text-sm leading-6 text-slate-500">
            Invoicing and fleet management built to help owner-operators and
            small trucking fleets move beyond spreadsheets.
          </p>
          <p className="mt-4 text-xs leading-5 text-slate-400">
            The public product preview uses demonstration data and is not
            intended for storing real business information.
          </p>
        </div>

        {footerGroups.map((group) => (
          <div key={group.title}>
            <h2 className="text-sm font-semibold text-slate-950">
              {group.title}
            </h2>
            <ul className="mt-4 space-y-3">
              {group.links.map((link) => (
                <li key={link.label}>
                  <Link
                    className="text-sm text-slate-500 transition-colors hover:text-slate-950 focus-visible:rounded focus-visible:outline-2 focus-visible:outline-blue-600"
                    href={link.href}
                    rel={
                      link.href.startsWith("http") ? "noreferrer" : undefined
                    }
                    target={link.href.startsWith("http") ? "_blank" : undefined}
                  >
                    {link.label}
                  </Link>
                </li>
              ))}
            </ul>
          </div>
        ))}
      </Container>

      <div className="border-t border-slate-200">
        <Container className="flex flex-col gap-4 py-6 text-xs text-slate-500 sm:flex-row sm:items-center sm:justify-between">
          <p>© 2026 InvoiceTrucker.</p>
          <div className="flex items-center gap-2">
            <Link
              aria-label="InvoiceTrucker on GitHub"
              className="grid size-9 place-items-center rounded-lg border border-slate-200 transition-colors hover:bg-slate-50 hover:text-slate-950 focus-visible:outline-2 focus-visible:outline-blue-600"
              href="https://github.com/ylberxhambazi/InvoiceTrucker"
              rel="noreferrer"
              target="_blank"
            >
              <Github aria-hidden="true" size={16} />
            </Link>
          </div>
        </Container>
      </div>
    </footer>
  );
}
