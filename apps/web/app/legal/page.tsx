import type { Metadata } from "next";

import { Container } from "@invoicetrucker/ui";
import Link from "next/link";

import { SiteFooter } from "@/components/site-footer";
import { SiteHeader } from "@/components/site-header";

import { legalLinks } from "./_components/legal-page";

export const metadata: Metadata = {
  alternates: { canonical: "/legal" },
  description:
    "InvoiceTrucker legal, privacy, cookie and data-processing information.",
  title: "Legal & Privacy",
};

const descriptions: Record<(typeof legalLinks)[number]["href"], string> = {
  "/legal/cookies": "How the public site uses cookies and browser storage.",
  "/legal/dpa": "A lightweight framework for business customer data.",
  "/legal/legal-notice": "Operator and contact information for the service.",
  "/legal/privacy": "What information is processed, why, and your choices.",
  "/legal/subprocessors": "Providers that help deliver InvoiceTrucker.",
  "/legal/terms": "The rules for accessing and using InvoiceTrucker.",
};

export default function LegalHubPage() {
  return (
    <>
      <a className="resource-skip-link" href="#legal-content">
        Skip to content
      </a>
      <SiteHeader />
      <main id="legal-content">
        <section className="border-b border-slate-200 bg-white">
          <Container className="py-14 sm:py-20">
            <p className="text-sm font-semibold uppercase tracking-[0.16em] text-blue-600">
              Legal &amp; privacy
            </p>
            <h1 className="mt-3 max-w-3xl text-4xl font-semibold tracking-[-0.04em] text-slate-950 sm:text-5xl">
              Clear information, in one place.
            </h1>
            <p className="mt-5 max-w-3xl text-lg leading-8 text-slate-600">
              InvoiceTrucker is built to be transparent about how the service
              works, what information is processed, and which third-party
              services help operate it.
            </p>
          </Container>
        </section>

        <Container className="py-12 sm:py-16">
          <div className="grid gap-4 sm:grid-cols-2 lg:grid-cols-3">
            {legalLinks.map((link) => (
              <Link
                className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm transition hover:-translate-y-0.5 hover:border-blue-200 hover:shadow-md focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600"
                href={link.href}
                key={link.href}
              >
                <h2 className="text-lg font-semibold text-slate-950">
                  {link.label}
                </h2>
                <p className="mt-2 text-sm leading-6 text-slate-600">
                  {descriptions[link.href]}
                </p>
              </Link>
            ))}
          </div>
        </Container>
      </main>
      <SiteFooter />
    </>
  );
}
