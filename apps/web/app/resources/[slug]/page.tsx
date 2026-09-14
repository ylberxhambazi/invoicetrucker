import type { Metadata } from "next";
import Link from "next/link";
import { notFound } from "next/navigation";

import { Container } from "@invoicetrucker/ui";
import { ArrowRight, ChevronRight } from "lucide-react";

import { ResourceCard } from "@/components/resource-card";
import { ResourceMarkdown } from "@/components/resource-markdown";
import { SiteFooter } from "@/components/site-footer";
import { SiteHeader } from "@/components/site-header";
import {
  formatResourceDate,
  getPublishedResources,
  getRelatedResources,
  getResourceBySlug,
} from "@/lib/resources";

const siteUrl = process.env.NEXT_PUBLIC_SITE_URL ?? "http://localhost:3000";
const defaultSocialImage = "/og-buyer.png";

function absoluteUrl(value: string) {
  return new URL(value, siteUrl).toString();
}

function jsonLd(value: object) {
  return JSON.stringify(value).replace(/</g, "\\u003c");
}

export const dynamicParams = false;

export function generateStaticParams() {
  return getPublishedResources().map(({ slug }) => ({ slug }));
}

export async function generateMetadata({
  params,
}: {
  params: Promise<{ slug: string }>;
}): Promise<Metadata> {
  const { slug } = await params;
  const article = getResourceBySlug(slug);
  if (!article) return {};

  const image = article.featuredImage ?? defaultSocialImage;
  const canonical = article.canonical ?? `/resources/${article.slug}`;

  return {
    alternates: { canonical },
    authors: [{ name: article.author }],
    description: article.description,
    openGraph: {
      authors: [article.author],
      description: article.description,
      images: [image],
      publishedTime: article.publishedAt,
      title: article.title,
      type: "article",
      ...(article.updatedAt ? { modifiedTime: article.updatedAt } : {}),
    },
    title: article.title,
    twitter: {
      card: "summary_large_image",
      description: article.description,
      images: [image],
      title: article.title,
    },
  };
}

export default async function ResourceArticlePage({
  params,
}: {
  params: Promise<{ slug: string }>;
}) {
  const { slug } = await params;
  const article = getResourceBySlug(slug);
  if (!article) notFound();

  const articleUrl = absoluteUrl(`/resources/${article.slug}`);
  const image = absoluteUrl(article.featuredImage ?? defaultSocialImage);
  const related = getRelatedResources(article);
  const articleSchema = {
    "@context": "https://schema.org",
    "@type": "BlogPosting",
    author: { "@type": "Organization", name: article.author },
    dateModified: article.updatedAt ?? article.publishedAt,
    datePublished: article.publishedAt,
    description: article.description,
    headline: article.title,
    image: [image],
    mainEntityOfPage: { "@id": articleUrl, "@type": "WebPage" },
    publisher: { "@type": "Organization", name: "InvoiceTrucker" },
  };
  const breadcrumbSchema = {
    "@context": "https://schema.org",
    "@type": "BreadcrumbList",
    itemListElement: [
      {
        "@type": "ListItem",
        item: absoluteUrl("/"),
        name: "Home",
        position: 1,
      },
      {
        "@type": "ListItem",
        item: absoluteUrl("/resources"),
        name: "Resources",
        position: 2,
      },
      {
        "@type": "ListItem",
        item: articleUrl,
        name: article.title,
        position: 3,
      },
    ],
  };

  return (
    <>
      <a className="resource-skip-link" href="#article-content">
        Skip to article
      </a>
      <SiteHeader />
      <main id="article-content">
        <article>
          <header className="resource-article-header">
            <Container>
              <nav aria-label="Breadcrumb">
                <ol className="resource-breadcrumbs">
                  <li>
                    <Link href="/">Home</Link>
                  </li>
                  <li>
                    <ChevronRight aria-hidden="true" size={14} />
                    <Link href="/resources">Resources</Link>
                  </li>
                  <li aria-current="page">
                    <ChevronRight aria-hidden="true" size={14} />
                    <span>{article.title}</span>
                  </li>
                </ol>
              </nav>
              <div className="mx-auto mt-10 max-w-3xl text-center">
                <p className="text-sm font-semibold uppercase tracking-[0.16em] text-blue-600">
                  {article.category}
                </p>
                <h1 className="mt-4 text-4xl font-semibold tracking-[-0.045em] text-slate-950 sm:text-6xl">
                  {article.title}
                </h1>
                <p className="mt-6 text-lg leading-8 text-slate-600">
                  {article.description}
                </p>
                <div className="resource-article-meta">
                  <span>By {article.author}</span>
                  <span aria-hidden="true">·</span>
                  <time dateTime={article.publishedAt}>
                    {formatResourceDate(article.publishedAt)}
                  </time>
                  {article.updatedAt ? (
                    <>
                      <span aria-hidden="true">·</span>
                      <span>
                        Updated{" "}
                        <time dateTime={article.updatedAt}>
                          {formatResourceDate(article.updatedAt)}
                        </time>
                      </span>
                    </>
                  ) : null}
                  <span aria-hidden="true">·</span>
                  <span>{article.readingTime} min read</span>
                </div>
              </div>
            </Container>
          </header>

          <Container className="py-14 sm:py-18">
            <div className="mx-auto max-w-3xl">
              <ResourceMarkdown content={article.content} />
              <aside
                className="resource-article-cta"
                aria-labelledby="article-cta-title"
              >
                <p className="text-sm font-semibold uppercase tracking-[0.16em] text-blue-200">
                  InvoiceTrucker
                </p>
                <h2 id="article-cta-title">
                  Ready to move beyond spreadsheets?
                </h2>
                <p>
                  See how InvoiceTrucker keeps invoices, customers, fleet
                  information, and business activity connected.
                </p>
                <Link href="/demo">
                  Explore InvoiceTrucker{" "}
                  <ArrowRight aria-hidden="true" size={17} />
                </Link>
              </aside>
            </div>
          </Container>
        </article>

        {related.length ? (
          <section
            className="border-t border-slate-200 bg-white py-16 sm:py-20"
            aria-labelledby="related-resources"
          >
            <Container>
              <h2
                className="text-3xl font-semibold tracking-[-0.035em] text-slate-950"
                id="related-resources"
              >
                Related resources
              </h2>
              <div className="mt-8 grid gap-6 lg:grid-cols-3">
                {related.map((item) => (
                  <ResourceCard article={item} key={item.slug} />
                ))}
              </div>
            </Container>
          </section>
        ) : null}
      </main>
      <SiteFooter />
      <script
        dangerouslySetInnerHTML={{ __html: jsonLd(articleSchema) }}
        type="application/ld+json"
      />
      <script
        dangerouslySetInnerHTML={{ __html: jsonLd(breadcrumbSchema) }}
        type="application/ld+json"
      />
    </>
  );
}
