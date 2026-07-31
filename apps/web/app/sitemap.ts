import type { MetadataRoute } from "next";

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
];

export default function sitemap(): MetadataRoute.Sitemap {
  return routes.map((route) => ({
    changeFrequency: route === "" ? "monthly" : "weekly",
    priority: route === "" ? 1 : route === "/demo" ? 0.9 : 0.7,
    url: `${siteUrl}${route}`,
  }));
}
