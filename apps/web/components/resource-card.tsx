import { ArrowRight } from "lucide-react";
import Image from "next/image";
import Link from "next/link";

import { formatResourceDate, type ResourceArticle } from "@/lib/resources";

export function ResourceCard({ article }: { article: ResourceArticle }) {
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
        <p className="resource-card-category">{article.category}</p>
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
