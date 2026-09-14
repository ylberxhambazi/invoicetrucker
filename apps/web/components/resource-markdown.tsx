import Image from "next/image";
import Link from "next/link";
import type { ReactNode } from "react";

function renderInline(text: string, keyPrefix: string): ReactNode[] {
  const pattern = /(\*\*[^*]+\*\*|`[^`]+`|\[[^\]]+\]\([^)]+\))/g;
  const parts = text.split(pattern).filter(Boolean);

  return parts.map((part, index) => {
    const key = `${keyPrefix}-${index}`;

    if (part.startsWith("**") && part.endsWith("**")) {
      return <strong key={key}>{part.slice(2, -2)}</strong>;
    }

    if (part.startsWith("`") && part.endsWith("`")) {
      return <code key={key}>{part.slice(1, -1)}</code>;
    }

    const link = part.match(/^\[([^\]]+)\]\(([^)]+)\)$/);
    if (link) {
      const [, label, href] = link;
      const external = href.startsWith("http");
      return (
        <Link
          href={href}
          key={key}
          rel={external ? "noopener noreferrer" : undefined}
          target={external ? "_blank" : undefined}
        >
          {label}
        </Link>
      );
    }

    return part;
  });
}

function tableCells(line: string) {
  return line
    .trim()
    .replace(/^\||\|$/g, "")
    .split("|")
    .map((cell) => cell.trim());
}

function startsBlock(lines: string[], index: number) {
  const line = lines[index] ?? "";
  const nextLine = lines[index + 1] ?? "";

  return (
    /^#{1,3}\s/.test(line) ||
    /^```/.test(line) ||
    /^>/.test(line) ||
    /^[-*]\s/.test(line) ||
    /^\d+\.\s/.test(line) ||
    /^!\[[^\]]*\]\([^)]+\)$/.test(line.trim()) ||
    (line.includes("|") && /^\s*\|?\s*:?-{3,}/.test(nextLine))
  );
}

export function ResourceMarkdown({ content }: { content: string }) {
  const lines = content.split(/\r?\n/);
  const blocks: ReactNode[] = [];
  let index = 0;

  while (index < lines.length) {
    const line = lines[index];
    if (!line.trim()) {
      index += 1;
      continue;
    }

    const heading = line.match(/^(#{1,3})\s+(.+)$/);
    if (heading) {
      const level = Math.max(2, heading[1].length);
      const Heading = level === 3 ? "h3" : "h2";
      blocks.push(
        <Heading key={`heading-${index}`}>
          {renderInline(heading[2], `heading-${index}`)}
        </Heading>,
      );
      index += 1;
      continue;
    }

    const image = line.trim().match(/^!\[([^\]]*)\]\(([^)]+)\)$/);
    if (image) {
      const [, alt, source] = image;
      blocks.push(
        source.startsWith("/") ? (
          <figure className="resource-image" key={`image-${index}`}>
            <Image
              alt={alt}
              fill
              sizes="(max-width: 768px) 100vw, 760px"
              src={source}
            />
          </figure>
        ) : (
          <p key={`image-link-${index}`}>
            <Link href={source}>{alt || "View image"}</Link>
          </p>
        ),
      );
      index += 1;
      continue;
    }

    if (line.startsWith("```")) {
      const language = line.slice(3).trim();
      const code: string[] = [];
      index += 1;
      while (index < lines.length && !lines[index].startsWith("```")) {
        code.push(lines[index]);
        index += 1;
      }
      index += 1;
      blocks.push(
        <pre key={`code-${index}`}>
          <code data-language={language || undefined}>{code.join("\n")}</code>
        </pre>,
      );
      continue;
    }

    if (line.startsWith(">")) {
      const quoteLines: string[] = [];
      while (index < lines.length && lines[index].startsWith(">")) {
        quoteLines.push(lines[index].replace(/^>\s?/, ""));
        index += 1;
      }
      const callout = quoteLines[0]?.match(/^\[!(NOTE|TIP|WARNING)\]$/);
      if (callout) {
        blocks.push(
          <aside className="resource-callout" key={`callout-${index}`}>
            <strong>{callout[1].toLowerCase()}</strong>
            <p>
              {renderInline(quoteLines.slice(1).join(" "), `callout-${index}`)}
            </p>
          </aside>,
        );
      } else {
        blocks.push(
          <blockquote key={`quote-${index}`}>
            {renderInline(quoteLines.join(" "), `quote-${index}`)}
          </blockquote>,
        );
      }
      continue;
    }

    const unordered = line.match(/^[-*]\s+(.+)$/);
    if (unordered) {
      const items: string[] = [];
      while (index < lines.length) {
        const item = lines[index].match(/^[-*]\s+(.+)$/);
        if (!item) break;
        items.push(item[1]);
        index += 1;
      }
      blocks.push(
        <ul key={`list-${index}`}>
          {items.map((item, itemIndex) => (
            <li key={item}>
              {renderInline(item, `list-${index}-${itemIndex}`)}
            </li>
          ))}
        </ul>,
      );
      continue;
    }

    const ordered = line.match(/^\d+\.\s+(.+)$/);
    if (ordered) {
      const items: string[] = [];
      while (index < lines.length) {
        const item = lines[index].match(/^\d+\.\s+(.+)$/);
        if (!item) break;
        items.push(item[1]);
        index += 1;
      }
      blocks.push(
        <ol key={`ordered-list-${index}`}>
          {items.map((item, itemIndex) => (
            <li key={item}>
              {renderInline(item, `ordered-list-${index}-${itemIndex}`)}
            </li>
          ))}
        </ol>,
      );
      continue;
    }

    if (
      line.includes("|") &&
      /^\s*\|?\s*:?-{3,}/.test(lines[index + 1] ?? "")
    ) {
      const headers = tableCells(line);
      index += 2;
      const rows: string[][] = [];
      while (index < lines.length && lines[index].includes("|")) {
        rows.push(tableCells(lines[index]));
        index += 1;
      }
      blocks.push(
        <div className="resource-table-wrap" key={`table-${index}`}>
          <table>
            <thead>
              <tr>
                {headers.map((header) => (
                  <th key={header} scope="col">
                    {header}
                  </th>
                ))}
              </tr>
            </thead>
            <tbody>
              {rows.map((row, rowIndex) => (
                <tr key={`row-${rowIndex}`}>
                  {row.map((cell, cellIndex) => (
                    <td key={`${cell}-${cellIndex}`}>
                      {renderInline(cell, `table-${rowIndex}-${cellIndex}`)}
                    </td>
                  ))}
                </tr>
              ))}
            </tbody>
          </table>
        </div>,
      );
      continue;
    }

    const paragraph = [line.trim()];
    index += 1;
    while (
      index < lines.length &&
      lines[index].trim() &&
      !startsBlock(lines, index)
    ) {
      paragraph.push(lines[index].trim());
      index += 1;
    }
    blocks.push(
      <p key={`paragraph-${index}`}>
        {renderInline(paragraph.join(" "), `paragraph-${index}`)}
      </p>,
    );
  }

  return <div className="resource-prose">{blocks}</div>;
}
