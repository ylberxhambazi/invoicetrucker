import type { Metadata } from "next";

import { Container } from "@invoicetrucker/ui";

import { ResourceCard } from "@/components/resource-card";
import { SiteFooter } from "@/components/site-footer";
import { SiteHeader } from "@/components/site-header";
import {
  categoryId,
  getPublishedResources,
  resourceCategories,
} from "@/lib/resources";

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
  const populatedCategories = resourceCategories.filter((category) =>
    articles.some((article) => article.category === category),
  );

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

        <section
          className="border-y border-slate-200 bg-white py-8"
          aria-labelledby="browse-categories"
        >
          <Container>
            <h2 className="sr-only" id="browse-categories">
              Browse resource categories
            </h2>
            <nav aria-label="Resource categories">
              <ul className="resource-category-list">
                {resourceCategories.map((category) => {
                  const populated = populatedCategories.includes(category);
                  return (
                    <li key={category}>
                      {populated ? (
                        <a href={`#${categoryId(category)}`}>{category}</a>
                      ) : (
                        <span>{category}</span>
                      )}
                    </li>
                  );
                })}
              </ul>
            </nav>
          </Container>
        </section>

        <section className="py-20 sm:py-26">
          <Container>
            {populatedCategories.map((category) => (
              <div id={categoryId(category)} key={category}>
                <div className="max-w-2xl">
                  <p className="text-sm font-semibold uppercase tracking-[0.16em] text-blue-600">
                    Latest guides
                  </p>
                  <h2 className="mt-3 text-3xl font-semibold tracking-[-0.035em] text-slate-950 sm:text-4xl">
                    {category}
                  </h2>
                </div>
                <div className="mt-9 grid gap-6 lg:grid-cols-2">
                  {articles
                    .filter((article) => article.category === category)
                    .map((article) => (
                      <ResourceCard article={article} key={article.slug} />
                    ))}
                </div>
              </div>
            ))}
          </Container>
        </section>
      </main>
      <SiteFooter />
    </>
  );
}
