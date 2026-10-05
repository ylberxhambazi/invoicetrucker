import type { MetadataRoute } from "next";

import { getPublishedResources } from "@/lib/resources";

const siteUrl = process.env.NEXT_PUBLIC_SITE_URL ?? "http://localhost:3000";

const routes = [
  "",
  "/demo",
  "/demo/trucks",
  "/demo/drivers",
  "/demo/clients",
  "/demo/invoices",
  "/demo/expenses",
  "/demo/reports",
  "/demo/documents",
  "/demo/settings",
  "/resources",
];

const legalRoutes = [
  "/legal",
  "/legal/privacy",
  "/legal/terms",
  "/legal/cookies",
  "/legal/subprocessors",
  "/legal/dpa",
  "/legal/legal-notice",
];

export default function sitemap(): MetadataRoute.Sitemap {
  const staticRoutes: MetadataRoute.Sitemap = routes.map((route) => ({
    changeFrequency: route === "" ? "monthly" : "weekly",
    priority: route === "" ? 1 : route === "/demo" ? 0.9 : 0.7,
    url: `${siteUrl}${route}`,
  }));

  const resources: MetadataRoute.Sitemap = getPublishedResources().map(
    (article) => ({
      changeFrequency: "monthly",
      lastModified: article.updatedAt ?? article.publishedAt,
      priority: 0.7,
      url: `${siteUrl}/resources/${article.slug}`,
    }),
  );

  const legal: MetadataRoute.Sitemap = legalRoutes.map((route) => ({
    changeFrequency: "yearly",
    priority: route === "/legal" ? 0.4 : 0.3,
    url: `${siteUrl}${route}`,
  }));

  return [...staticRoutes, ...resources, ...legal];
}
