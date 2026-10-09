import { Container } from "@invoicetrucker/ui";
import Link from "next/link";

import { SiteFooter } from "@/components/site-footer";
import { SiteHeader } from "@/components/site-header";

export const legalLinks = [
  { href: "/legal/privacy", label: "Privacy Policy" },
  { href: "/legal/terms", label: "Terms of Service" },
  { href: "/legal/cookies", label: "Cookie & Storage Notice" },
  { href: "/legal/subprocessors", label: "Subprocessors" },
  { href: "/legal/dpa", label: "Data Processing Addendum" },
  { href: "/legal/legal-notice", label: "Legal Notice" },
] as const;

export function LegalPage({
  children,
  description,
  title,
}: Readonly<{
  children: React.ReactNode;
  description: string;
  title: string;
}>) {
  return (
    <>
      <a className="resource-skip-link" href="#legal-content">
        Skip to content
      </a>
      <SiteHeader />
      <main id="legal-content">
        <header className="border-b border-slate-200 bg-white">
          <Container className="py-12 sm:py-16">
            <p className="text-sm font-semibold uppercase tracking-[0.16em] text-blue-600">
              Legal &amp; privacy
            </p>
            <h1 className="mt-3 max-w-3xl text-4xl font-semibold tracking-[-0.04em] text-slate-950 sm:text-5xl">
              {title}
            </h1>
            <p className="mt-5 max-w-3xl text-base leading-7 text-slate-600 sm:text-lg">
              {description}
            </p>
            <p className="mt-4 text-sm text-slate-500">
              Last updated: October 9, 2026
            </p>
          </Container>
        </header>

        <Container className="grid gap-10 py-12 lg:grid-cols-[13rem_minmax(0,46rem)] lg:items-start lg:justify-center lg:py-16">
          <nav
            aria-label="Legal pages"
            className="rounded-2xl border border-slate-200 bg-white p-3 lg:sticky lg:top-24"
          >
            <ul className="grid gap-1 sm:grid-cols-2 lg:grid-cols-1">
              <li>
                <Link className="legal-nav-link" href="/legal">
                  Legal overview
                </Link>
              </li>
              {legalLinks.map((link) => (
                <li key={link.href}>
                  <Link className="legal-nav-link" href={link.href}>
                    {link.label}
                  </Link>
                </li>
              ))}
            </ul>
          </nav>

          <article className="legal-copy min-w-0">{children}</article>
        </Container>
      </main>
      <SiteFooter />
    </>
  );
}
