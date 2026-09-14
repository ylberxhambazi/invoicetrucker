import fs from "node:fs";
import path from "node:path";

export const resourceCategories = [
  "Trucking Invoicing",
  "Trucking Paperwork",
  "Fleet Operations",
  "Moving Beyond Spreadsheets",
  "Guides",
  "Comparisons",
  "Templates",
] as const;

export type ResourceCategory = (typeof resourceCategories)[number];

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
  slug: string;
  tags: string[];
  title: string;
  updatedAt?: string;
}

type FrontmatterValue = boolean | string | string[];

const articlesDirectory = path.join(
  process.cwd(),
  "content",
  "resources",
  "articles",
);

function unquote(value: string) {
  const trimmed = value.trim();

  if (
    (trimmed.startsWith('"') && trimmed.endsWith('"')) ||
    (trimmed.startsWith("'") && trimmed.endsWith("'"))
  ) {
    return trimmed.slice(1, -1);
  }

  return trimmed;
}

function parseFrontmatter(source: string) {
  const match = source.match(/^---\r?\n([\s\S]*?)\r?\n---\r?\n?/);

  if (!match) {
    throw new Error("Resource article is missing frontmatter.");
  }

  const data: Record<string, FrontmatterValue> = {};
  const lines = match[1].split(/\r?\n/);
  let listKey: string | undefined;

  for (const line of lines) {
    const listItem = line.match(/^\s+-\s+(.+)$/);

    if (listItem && listKey) {
      const current = data[listKey];
      data[listKey] = [
        ...(Array.isArray(current) ? current : []),
        unquote(listItem[1]),
      ];
      continue;
    }

    const field = line.match(/^([A-Za-z][A-Za-z0-9]*):\s*(.*)$/);
    if (!field) continue;

    const [, key, rawValue] = field;
    if (!rawValue) {
      data[key] = [];
      listKey = key;
      continue;
    }

    listKey = undefined;
    data[key] =
      rawValue === "true"
        ? true
        : rawValue === "false"
          ? false
          : unquote(rawValue);
  }

  return { content: source.slice(match[0].length).trim(), data };
}

function requiredString(data: Record<string, FrontmatterValue>, field: string) {
  const value = data[field];
  if (typeof value !== "string" || !value.trim()) {
    throw new Error(`Resource frontmatter field "${field}" is required.`);
  }
  return value.trim();
}

function optionalString(data: Record<string, FrontmatterValue>, field: string) {
  const value = data[field];
  if (value === undefined || value === "") return undefined;
  if (typeof value !== "string") {
    throw new Error(`Resource frontmatter field "${field}" must be a string.`);
  }
  return value.trim();
}

function validDate(value: string, field: string) {
  if (!/^\d{4}-\d{2}-\d{2}$/.test(value) || Number.isNaN(Date.parse(value))) {
    throw new Error(
      `Resource frontmatter field "${field}" must be YYYY-MM-DD.`,
    );
  }
  return value;
}

export function calculateReadingTime(content: string) {
  const words = content
    .replace(/```[\s\S]*?```/g, " ")
    .replace(/[^\p{L}\p{N}'-]+/gu, " ")
    .trim()
    .split(/\s+/)
    .filter(Boolean).length;

  return Math.max(1, Math.ceil(words / 200));
}

export function parseResourceSource(source: string): ResourceArticle {
  const { content, data } = parseFrontmatter(source);
  const category = requiredString(data, "category");
  const slug = requiredString(data, "slug");
  const publishedAt = validDate(
    requiredString(data, "publishedAt"),
    "publishedAt",
  );
  const updatedAtValue = optionalString(data, "updatedAt");

  if (!resourceCategories.includes(category as ResourceCategory)) {
    throw new Error(`Unknown resource category "${category}".`);
  }

  if (!/^[a-z0-9]+(?:-[a-z0-9]+)*$/.test(slug)) {
    throw new Error("Resource slug must use lowercase kebab-case.");
  }

  if (!content) {
    throw new Error(`Resource article "${slug}" has no body content.`);
  }

  const tags = data.tags;
  if (!Array.isArray(tags) || tags.some((tag) => typeof tag !== "string")) {
    throw new Error('Resource frontmatter field "tags" must be a list.');
  }

  const draft = data.draft;
  if (typeof draft !== "boolean") {
    throw new Error(
      'Resource frontmatter field "draft" must be true or false.',
    );
  }

  return {
    author: requiredString(data, "author"),
    canonical: optionalString(data, "canonical"),
    category: category as ResourceCategory,
    content,
    description: requiredString(data, "description"),
    draft,
    featuredImage: optionalString(data, "featuredImage"),
    publishedAt,
    readingTime: calculateReadingTime(content),
    slug,
    tags,
    title: requiredString(data, "title"),
    updatedAt: updatedAtValue
      ? validDate(updatedAtValue, "updatedAt")
      : undefined,
  };
}

export function isPublishedResource(article: ResourceArticle) {
  return !article.draft;
}

export function getAllResources() {
  if (!fs.existsSync(articlesDirectory)) return [];

  return fs
    .readdirSync(articlesDirectory)
    .filter((fileName) => fileName.endsWith(".md"))
    .map((fileName) =>
      parseResourceSource(
        fs.readFileSync(path.join(articlesDirectory, fileName), "utf8"),
      ),
    )
    .sort((left, right) => right.publishedAt.localeCompare(left.publishedAt));
}

export function getPublishedResources() {
  return getAllResources().filter(isPublishedResource);
}

export function getResourceBySlug(slug: string) {
  return getPublishedResources().find((article) => article.slug === slug);
}

export function getRelatedResources(current: ResourceArticle, limit = 3) {
  return getPublishedResources()
    .filter((article) => article.slug !== current.slug)
    .map((article) => ({
      article,
      categoryMatch: Number(article.category === current.category),
      sharedTags: article.tags.filter((tag) => current.tags.includes(tag))
        .length,
    }))
    .sort(
      (left, right) =>
        right.categoryMatch - left.categoryMatch ||
        right.sharedTags - left.sharedTags ||
        right.article.publishedAt.localeCompare(left.article.publishedAt),
    )
    .slice(0, limit)
    .map(({ article }) => article);
}

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
