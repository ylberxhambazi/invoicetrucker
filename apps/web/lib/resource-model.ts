export const resourceCategories = [
  "Trucking Invoicing",
  "EU Freight Invoicing",
  "Trucking Paperwork",
  "Fleet Operations",
  "Moving Beyond Spreadsheets",
  "Guides",
  "Comparisons",
  "Templates",
] as const;

export const resourceRegions = ["US", "EU", "Global"] as const;

export type ResourceCategory = (typeof resourceCategories)[number];
export type ResourceRegion = (typeof resourceRegions)[number];

export interface ResourceArticle {
  author: string;
  canonical?: string;
  category: ResourceCategory;
  content: string;
  description: string;
  draft: boolean;
  featuredImage?: string;
  publishedAt: string;
  readingTime: number;
  region: ResourceRegion;
  slug: string;
  tags: string[];
  title: string;
  updatedAt?: string;
}

export type ResourceSummary = Omit<ResourceArticle, "content">;

export function formatResourceDate(date: string) {
  return new Intl.DateTimeFormat("en-US", {
    day: "numeric",
    month: "long",
    timeZone: "UTC",
    year: "numeric",
  }).format(new Date(`${date}T00:00:00Z`));
}

export function categoryId(category: ResourceCategory) {
  return category.toLowerCase().replace(/[^a-z0-9]+/g, "-");
}
