import { fireEvent, render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import ResourceArticlePage, {
  generateMetadata,
  generateStaticParams,
} from "@/app/resources/[slug]/page";
import ResourcesPage from "@/app/resources/page";
import { GET as getRss } from "@/app/rss.xml/route";
import sitemap from "@/app/sitemap";
import { ResourceBrowser } from "@/components/resource-browser";
import type { ResourceRegion, ResourceSummary } from "@/lib/resource-model";
import {
  getPublishedResources,
  getRelatedResources,
  isPublishedResource,
  parseResourceSource,
  resourceCategories,
} from "@/lib/resources";

const articleSlug = "what-should-a-trucking-invoice-include";
const euDraftSlugs = [
  "vat-on-freight-transport-invoices-eu",
  "transport-invoice-requirements-eu",
] as const;

function resourceSummary(
  overrides: Pick<ResourceSummary, "region" | "slug" | "title">,
): ResourceSummary {
  return {
    author: "InvoiceTrucker",
    category: "Guides",
    description: "Test resource summary.",
    draft: false,
    publishedAt: "2026-09-14",
    readingTime: 2,
    tags: ["regional"],
    ...overrides,
  };
}

function regionalArticle({
  publishedAt,
  region,
  slug,
}: {
  publishedAt: string;
  region: ResourceRegion;
  slug: string;
}) {
  return parseResourceSource(`---
title: "${slug}"
description: "Explicit related-resource fixture."
slug: "${slug}"
publishedAt: "${publishedAt}"
category: "Guides"
region: "${region}"
tags:
  - shared-tag
author: "InvoiceTrucker"
draft: false
---

Related-resource fixture body.
`);
}

describe("resource content system", () => {
  it("loads the repository article and renders the resources hub", () => {
    expect(getPublishedResources().length).toBeGreaterThanOrEqual(3);

    render(<ResourcesPage />);

    expect(
      screen.getByRole("heading", {
        level: 1,
        name: /practical resources for running a clearer trucking business/i,
      }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("link", {
        name: "What Should a Trucking Invoice Include?",
      }),
    ).toHaveAttribute("href", `/resources/${articleSlug}`);
  });

  it("filters published resource fixtures by region without changing article links", () => {
    const articles = [
      resourceSummary({
        region: "US",
        slug: "published-us-guide",
        title: "Published US fixture",
      }),
      resourceSummary({
        region: "EU",
        slug: "vat-on-freight-transport-invoices-eu",
        title: "Published EU fixture",
      }),
      resourceSummary({
        region: "Global",
        slug: "published-general-guide",
        title: "Published general fixture",
      }),
    ];
    const { container } = render(
      <ResourceBrowser articles={articles} categories={resourceCategories} />,
    );

    fireEvent.click(
      screen.getByRole("button", { name: "EU Freight" }),
    );

    expect(
      container.querySelector(
        'a[href="/resources/vat-on-freight-transport-invoices-eu"]',
      ),
    ).toBeInTheDocument();
    expect(
      container.querySelector('a[href="/resources/published-us-guide"]'),
    ).not.toBeInTheDocument();

    fireEvent.click(
      screen.getByRole("button", { name: "US Trucking" }),
    );
    expect(
      container.querySelector('a[href="/resources/published-us-guide"]'),
    ).toBeInTheDocument();

    fireEvent.click(screen.getByRole("button", { name: "General" }));
    expect(
      container.querySelector('a[href="/resources/published-general-guide"]'),
    ).toBeInTheDocument();
  });

  it("renders the sample article with editorial elements", async () => {
    render(
      await ResourceArticlePage({
        params: Promise.resolve({ slug: articleSlug }),
      }),
    );

    expect(
      screen.getByRole("heading", {
        level: 1,
        name: "What Should a Trucking Invoice Include?",
      }),
    ).toBeInTheDocument();
    expect(screen.getAllByRole("table")).toHaveLength(4);
    const productLinks = screen.getAllByRole("link", {
      name: /explore invoicetrucker/i,
    });
    expect(productLinks).toHaveLength(2);
    expect(
      productLinks.every((link) => link.getAttribute("href") === "/demo"),
    ).toBe(true);
  });

  it("generates article metadata and static params from frontmatter", async () => {
    const metadata = await generateMetadata({
      params: Promise.resolve({ slug: articleSlug }),
    });

    const params = generateStaticParams();

    expect(params).toEqual(
      expect.arrayContaining([
        { slug: "how-to-create-a-trucking-invoice" },
        { slug: "trucking-invoice-template" },
        { slug: "what-should-a-trucking-invoice-include" },
      ])
    );
    expect(metadata.title).toBe("What Should a Trucking Invoice Include?");
    expect(metadata.description).toMatch(/practical checklist/i);
    expect(metadata.alternates).toEqual({
      canonical: `/resources/${articleSlug}`,
    });
  });

  it("keeps drafts out of published routes", () => {
    const draft = parseResourceSource(`---
title: "Draft guide"
description: "Not ready to publish."
slug: "draft-guide"
publishedAt: "2026-09-14"
category: "Guides"
tags:
  - draft
author: "InvoiceTrucker"
draft: true
---

Draft body content.
`);

    expect(isPublishedResource(draft)).toBe(false);
    expect(draft.region).toBe("Global");
    expect(generateStaticParams()).not.toContainEqual({ slug: "draft-guide" });
  });

  it("parses valid regions and rejects invalid region values clearly", () => {
    const source = (region: string) => `---
title: "Regional guide"
description: "A region parsing test."
slug: "regional-guide"
publishedAt: "2026-09-14"
category: "Guides"
region: "${region}"
tags:
  - regional
author: "InvoiceTrucker"
draft: false
---

Regional body content.
`;

    expect(parseResourceSource(source("US")).region).toBe("US");
    expect(() => parseResourceSource(source("North America"))).toThrow(
      'Unknown resource region "North America". Expected US, EU, or Global.',
    );
  });

  it("uses region as a tie-breaker for related resources", () => {
    const current = regionalArticle({
      publishedAt: "2026-09-14",
      region: "US",
      slug: "current-guide",
    });
    const newerCrossRegion = regionalArticle({
      publishedAt: "2026-09-16",
      region: "EU",
      slug: "newer-cross-region-guide",
    });
    const olderSameRegion = regionalArticle({
      publishedAt: "2026-09-15",
      region: "US",
      slug: "older-same-region-guide",
    });

    expect(
      getRelatedResources(current, 1, [newerCrossRegion, olderSameRegion])[0]
        ?.slug,
    ).toBe("older-same-region-guide");
  });

  it("adds the published article to the sitemap and RSS feed", async () => {
    expect(sitemap()).toEqual(
      expect.arrayContaining([
        expect.objectContaining({
          lastModified: "2026-09-14",
          url: expect.stringContaining(`/resources/${articleSlug}`),
        }),
      ]),
    );

    const response = getRss();
    const feed = await response.text();

    expect(response.headers.get("content-type")).toContain(
      "application/rss+xml",
    );
    expect(feed).toContain("What Should a Trucking Invoice Include?");
    expect(feed).toContain(`/resources/${articleSlug}`);
    expect(feed).not.toContain("/resources/us/");
    expect(feed).not.toContain("/resources/eu/");

    const publishedSlugs = getPublishedResources().map(({ slug }) => slug);
    const staticSlugs = generateStaticParams().map(({ slug }) => slug);
    const sitemapUrls = sitemap().map(({ url }) => url);

    for (const slug of euDraftSlugs) {
      expect(publishedSlugs).not.toContain(slug);
      expect(staticSlugs).not.toContain(slug);
      expect(sitemapUrls).not.toContainEqual(expect.stringContaining(slug));
      expect(feed).not.toContain(`/resources/${slug}`);
    }
  });

  it("keeps article and breadcrumb JSON-LD on the existing resource route", async () => {
    const { container } = render(
      await ResourceArticlePage({
        params: Promise.resolve({ slug: articleSlug }),
      }),
    );
    const schemas = Array.from(
      container.querySelectorAll('script[type="application/ld+json"]'),
      (script) => JSON.parse(script.textContent ?? "{}"),
    );

    expect(schemas).toEqual(
      expect.arrayContaining([
        expect.objectContaining({
          "@type": "BlogPosting",
          url: expect.stringContaining(`/resources/${articleSlug}`),
        }),
        expect.objectContaining({ "@type": "BreadcrumbList" }),
      ]),
    );
  });
});
