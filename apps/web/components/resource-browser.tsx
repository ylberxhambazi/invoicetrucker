"use client";

import { useState } from "react";

import { Container } from "@invoicetrucker/ui";

import { ResourceCard } from "@/components/resource-card";
import {
  categoryId,
  type ResourceCategory,
  type ResourceRegion,
  type ResourceSummary,
} from "@/lib/resource-model";

type RegionFilter = "All" | ResourceRegion;

const regionFilters: { label: string; value: RegionFilter }[] = [
  { label: "All", value: "All" },
  { label: "US Trucking", value: "US" },
  { label: "EU Freight", value: "EU" },
  { label: "General", value: "Global" },
];

export function ResourceBrowser({
  articles,
  categories,
}: {
  articles: ResourceSummary[];
  categories: readonly ResourceCategory[];
}) {
  const [region, setRegion] = useState<RegionFilter>("All");
  const visibleArticles =
    region === "All"
      ? articles
      : articles.filter((article) => article.region === region);
  const populatedCategories = categories.filter((category) =>
    visibleArticles.some((article) => article.category === category),
  );

  return (
    <>
      <section
        className="border-y border-slate-200 bg-white py-8"
        aria-labelledby="browse-regions"
      >
        <Container>
          <h2 className="sr-only" id="browse-regions">
            Filter resources by region
          </h2>
          <nav aria-label="Resource regions">
            <ul className="resource-region-list">
              {regionFilters.map((filter) => (
                <li key={filter.value}>
                  <button
                    aria-pressed={region === filter.value}
                    onClick={() => setRegion(filter.value)}
                    type="button"
                  >
                    {filter.label}
                  </button>
                </li>
              ))}
            </ul>
          </nav>

          {populatedCategories.length ? (
            <nav aria-label="Resource categories" className="mt-5">
              <ul className="resource-category-list">
                {populatedCategories.map((category) => (
                  <li key={category}>
                    <a href={`#${categoryId(category)}`}>{category}</a>
                  </li>
                ))}
              </ul>
            </nav>
          ) : null}
        </Container>
      </section>

      <section className="py-20 sm:py-26" aria-live="polite">
        <Container>
          {populatedCategories.length ? (
            populatedCategories.map((category, index) => (
              <div
                id={categoryId(category)}
                key={category}
                className={index === 0 ? "" : "mt-20 sm:mt-24"}
              >
                <div className="max-w-2xl">
                  <p className="text-sm font-semibold uppercase tracking-[0.16em] text-blue-600">
                    Latest guides
                  </p>
                  <h2 className="mt-3 text-3xl font-semibold tracking-[-0.035em] text-slate-950 sm:text-4xl">
                    {category}
                  </h2>
                </div>

                <div className="mt-9 grid gap-6 lg:grid-cols-2">
                  {visibleArticles
                    .filter((article) => article.category === category)
                    .map((article) => (
                      <ResourceCard article={article} key={article.slug} />
                    ))}
                </div>
              </div>
            ))
          ) : (
            <p className="text-slate-600">No resources in this region yet.</p>
          )}
        </Container>
      </section>
    </>
  );
}
