import { ArrowRight } from "lucide-react";
import Image from "next/image";
import Link from "next/link";

import {
  formatResourceDate,
  type ResourceSummary,
} from "@/lib/resource-model";

const regionLabels = { EU: "EU", Global: "General", US: "US" } as const;

export function ResourceCard({ article }: { article: ResourceSummary }) {
  return (
    <article className="resource-card">
      {article.featuredImage ? (
        <div className="resource-card-image">
          <Image
            alt=""
            fill
            sizes="(max-width: 768px) 100vw, 560px"
            src={article.featuredImage}
          />
        </div>
      ) : null}
      <div className="resource-card-content">
        <div className="resource-card-taxonomy">
          <p className="resource-card-category">{article.category}</p>
          <span className="resource-card-region">
            {regionLabels[article.region]}
          </span>
        </div>
        <h3>
          <Link href={`/resources/${article.slug}`}>{article.title}</Link>
        </h3>
        <p>{article.description}</p>
        <div className="resource-card-meta">
          <time dateTime={article.publishedAt}>
            {formatResourceDate(article.publishedAt)}
          </time>
          <span aria-hidden="true">·</span>
          <span>{article.readingTime} min read</span>
        </div>
        <Link
          aria-label={`Read ${article.title}`}
          className="resource-card-link"
          href={`/resources/${article.slug}`}
        >
          Read guide <ArrowRight aria-hidden="true" size={16} />
        </Link>
      </div>
    </article>
  );
}
