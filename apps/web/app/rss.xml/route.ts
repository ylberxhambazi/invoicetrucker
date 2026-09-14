import { getPublishedResources } from "@/lib/resources";

const siteUrl = process.env.NEXT_PUBLIC_SITE_URL ?? "http://localhost:3000";

function escapeXml(value: string) {
  return value
    .replaceAll("&", "&amp;")
    .replaceAll("<", "&lt;")
    .replaceAll(">", "&gt;")
    .replaceAll('"', "&quot;")
    .replaceAll("'", "&apos;");
}

export const dynamic = "force-static";

export function GET() {
  const articles = getPublishedResources();
  const items = articles
    .map((article) => {
      const url = new URL(`/resources/${article.slug}`, siteUrl).toString();
      return `<item>
  <title>${escapeXml(article.title)}</title>
  <link>${escapeXml(url)}</link>
  <guid isPermaLink="true">${escapeXml(url)}</guid>
  <description>${escapeXml(article.description)}</description>
  <category>${escapeXml(article.category)}</category>
  <dc:creator>${escapeXml(article.author)}</dc:creator>
  <pubDate>${new Date(`${article.publishedAt}T00:00:00Z`).toUTCString()}</pubDate>
</item>`;
    })
    .join("\n");
  const lastBuildDate = articles[0]
    ? new Date(
        `${articles[0].updatedAt ?? articles[0].publishedAt}T00:00:00Z`,
      ).toUTCString()
    : new Date(0).toUTCString();

  const feed = `<?xml version="1.0" encoding="UTF-8"?>
<rss version="2.0" xmlns:atom="http://www.w3.org/2005/Atom" xmlns:dc="http://purl.org/dc/elements/1.1/">
<channel>
  <title>InvoiceTrucker Resources</title>
  <link>${escapeXml(new URL("/resources", siteUrl).toString())}</link>
  <description>Practical trucking invoicing, paperwork, and fleet operations guides for small fleets.</description>
  <language>en</language>
  <lastBuildDate>${lastBuildDate}</lastBuildDate>
  <atom:link href="${escapeXml(new URL("/rss.xml", siteUrl).toString())}" rel="self" type="application/rss+xml" />
${items}
</channel>
</rss>`;

  return new Response(feed, {
    headers: {
      "Cache-Control": "public, max-age=3600, s-maxage=86400",
      "Content-Type": "application/rss+xml; charset=utf-8",
    },
  });
}
