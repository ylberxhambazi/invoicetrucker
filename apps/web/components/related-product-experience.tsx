import { ArrowUpRight, ScanLine, ShieldCheck } from "lucide-react";

import { Badge, Container } from "@invoicetrucker/ui";

export function RelatedProductExperience({
  productUrl,
}: {
  productUrl?: string;
}) {
  return (
    <section className="border-y border-slate-200 bg-white py-20 sm:py-26">
      <Container>
        <div className="related-product-panel">
          <div className="max-w-2xl">
            <Badge className="border-white/15 bg-white/10 text-blue-200">
              Related Product Experience
            </Badge>
            <h2 className="mt-5 text-3xl font-semibold tracking-[-0.04em] text-white sm:text-4xl">
              Production experience beyond this public showcase.
            </h2>
            <p className="mt-5 text-base leading-7 text-slate-300">
              The developer has separately built a full truck-focused invoicing
              platform with authentication, invoice workflows, client and fleet
              management, OCR/AI invoice scanning, exports, and subscription
              management.
            </p>
            {productUrl ? (
              <a
                className="mt-7 inline-flex min-h-11 items-center gap-2 rounded-xl border border-white/20 bg-white px-5 text-sm font-semibold text-slate-950 transition-colors hover:bg-blue-50 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-300"
                href={productUrl}
                rel="noopener noreferrer"
                target="_blank"
              >
                View Full Product
                <ArrowUpRight aria-hidden="true" size={17} />
              </a>
            ) : null}
          </div>
          <div className="grid gap-3">
            {[
              {
                icon: ShieldCheck,
                text: "InvoiceTrucker is a fictional open-source portfolio project.",
              },
              {
                icon: ScanLine,
                text: "All InvoiceTrucker companies, people, vehicles, and financial data are fictional.",
              },
              {
                icon: ArrowUpRight,
                text: "The separate production product source code is not part of this repository.",
              },
            ].map(({ icon: Icon, text }) => (
              <div className="related-product-note" key={text}>
                <span className="grid size-9 shrink-0 place-items-center rounded-lg bg-white/10 text-blue-200">
                  <Icon aria-hidden="true" size={17} />
                </span>
                <p className="text-sm leading-6 text-slate-300">{text}</p>
              </div>
            ))}
          </div>
        </div>
      </Container>
    </section>
  );
}
