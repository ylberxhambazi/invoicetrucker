import { Container } from "@invoicetrucker/ui";
import { Github } from "lucide-react";
import Link from "next/link";

import { Brand } from "./brand";

const footerGroups = [
  {
    links: [
      { href: "#product", label: "Product" },
      { href: "#features", label: "Features" },
      { href: "/demo", label: "Live demo" },
      { href: "#early-access", label: "Early access" },
    ],
    title: "Product",
  },
  {
    links: [
      { href: "#technology", label: "Technology stack" },
      {
        href: "https://github.com/ylberxhambazi/InvoiceTrucker",
        label: "GitHub",
      },
      { href: "#technology", label: "Architecture" },
      { href: "#early-access", label: "Project updates" },
    ],
    title: "Project",
  },
];

export function SiteFooter() {
  return (
    <footer className="border-t border-slate-200 bg-white">
      <Container className="grid gap-10 py-12 md:grid-cols-[1.5fr_1fr_1fr] lg:py-16">
        <div className="max-w-md">
          <Brand />
          <p className="mt-5 text-sm leading-6 text-slate-500">
            A fictional fleet-management SaaS showcase demonstrating thoughtful
            product design, frontend architecture, API engineering, and database
            integration.
          </p>
          <p className="mt-4 text-xs leading-5 text-slate-400">
            Demo environment — fictional data only. Not intended for storing
            real business information.
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
          <p>© 2026 InvoiceTrucker. Fictional portfolio project.</p>
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
