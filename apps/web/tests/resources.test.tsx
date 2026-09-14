import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import ResourceArticlePage, {
  generateMetadata,
  generateStaticParams,
} from "@/app/resources/[slug]/page";
import ResourcesPage from "@/app/resources/page";
import { GET as getRss } from "@/app/rss.xml/route";
import sitemap from "@/app/sitemap";
import {
  getPublishedResources,
  isPublishedResource,
  parseResourceSource,
} from "@/lib/resources";

const articleSlug = "what-should-a-trucking-invoice-include";

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
    expect(generateStaticParams()).not.toContainEqual({ slug: "draft-guide" });
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
  });
});
