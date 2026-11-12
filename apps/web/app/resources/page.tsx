import type { Metadata } from "next";

import { Container } from "@invoicetrucker/ui";

import { ResourceBrowser } from "@/components/resource-browser";
import { SiteFooter } from "@/components/site-footer";
import { SiteHeader } from "@/components/site-header";
import { getPublishedResources, resourceCategories } from "@/lib/resources";

export const metadata: Metadata = {
  alternates: { canonical: "/resources" },
  description:
    "Practical guides for small trucking fleets that want to invoice faster, organize paperwork, and move beyond spreadsheet-based workflows.",
  openGraph: {
    description:
      "Practical trucking invoicing, paperwork, and fleet operations guides for small fleets.",
    images: ["/og-buyer.png"],
    title: "Trucking Invoicing Resources | InvoiceTrucker",
    type: "website",
  },
  title: "Trucking Invoicing Resources",
  twitter: {
    card: "summary_large_image",
    description:
      "Practical trucking invoicing, paperwork, and fleet operations guides for small fleets.",
    images: ["/og-buyer.png"],
    title: "Trucking Invoicing Resources | InvoiceTrucker",
  },
};

export default function ResourcesPage() {
  const articles = getPublishedResources();
  const articleSummaries = articles.map(({ content, ...article }) => {
    void content;
    return article;
  });

  return (
    <>
      <a className="resource-skip-link" href="#resources-content">
        Skip to content
      </a>
      <SiteHeader />
      <main id="resources-content">
        <section className="resources-hero">
          <Container className="py-18 sm:py-24">
            <p className="text-sm font-semibold uppercase tracking-[0.16em] text-blue-600">
              Resources
            </p>
            <h1 className="mt-4 max-w-4xl text-4xl font-semibold tracking-[-0.045em] text-slate-950 sm:text-6xl">
              Practical resources for running a clearer trucking business.
            </h1>
            <p className="mt-6 max-w-3xl text-lg leading-8 text-slate-600">
              Practical guides for small trucking fleets that want to invoice
              faster, organize paperwork, and move beyond spreadsheet-based
              workflows.
            </p>
          </Container>
        </section>

        <ResourceBrowser
          articles={articleSummaries}
          categories={resourceCategories}
        />
      </main>
      <SiteFooter />
    </>
  );
}
