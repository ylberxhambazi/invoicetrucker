import { Badge, Container, SectionHeading } from "@invoicetrucker/ui";
import {
  ArrowRight,
  BarChart3,
  Check,
  CircleDollarSign,
  FileCheck2,
  FileText,
  LayoutDashboard,
  ReceiptText,
  Truck,
  UsersRound,
} from "lucide-react";
import Link from "next/link";

import { DashboardPreview } from "@/components/dashboard-preview";
import { NewsletterForm } from "@/components/newsletter-form";
import { ProductShowcase } from "@/components/product-showcase";
import { SiteFooter } from "@/components/site-footer";
import { SiteHeader } from "@/components/site-header";

const problems = [
  [
    "Loads waiting to be invoiced",
    "A delivery is complete, but the invoice doesn't get created until days later.",
  ],
  [
    "No clear view of unpaid invoices",
    "You know money is outstanding. Finding exactly how much and from whom takes time.",
  ],
  [
    "The same information entered again and again",
    "Customer, truck, load, rate, invoice — copied between multiple files and systems.",
  ],
  [
    "Paperwork stored everywhere",
    "PODs, rate confirmations, invoice PDFs, and other documents end up scattered across folders and inboxes.",
  ],
  [
    "Month-end becomes cleanup time",
    "Instead of reviewing the business, you spend hours figuring out what happened.",
  ],
];

const workflowSteps = [
  [
    "Keep your fleet and customers organized",
    "Maintain the information you need about trucks, drivers, and customers in one place.",
  ],
  [
    "Create and track invoices",
    "Turn completed work into invoices without rebuilding the same information every time.",
  ],
  [
    "Keep the paperwork connected",
    "Keep relevant invoice and load information together instead of searching through folders and email.",
  ],
  [
    "Know what has been paid",
    "See invoice statuses and outstanding balances without maintaining another spreadsheet.",
  ],
];

const productHighlights = [
  {
    description:
      "See revenue, invoice activity, outstanding balances, fleet information, and recent business activity from one place.",
    icon: LayoutDashboard,
    title: "Dashboard",
  },
  {
    description:
      "Search, filter, review, and track invoices without maintaining separate spreadsheets.",
    icon: ReceiptText,
    title: "Invoices",
  },
  {
    description: "Keep customer information and invoice history connected.",
    icon: UsersRound,
    title: "Customers",
  },
  {
    description:
      "Keep trucks and drivers organized alongside the financial side of the business.",
    icon: Truck,
    title: "Fleet",
  },
  {
    description:
      "Understand how the business is performing without manually rebuilding reports at the end of the month.",
    icon: BarChart3,
    title: "Reports",
  },
];

const benefits = [
  {
    description:
      "Reduce repeated data entry and move completed work toward invoicing faster.",
    icon: ReceiptText,
    title: "Invoice faster",
  },
  {
    description:
      "See outstanding invoices and payment status without searching multiple files.",
    icon: CircleDollarSign,
    title: "Know who owes you",
  },
  {
    description:
      "Stop treating invoices, customers, fleet information, and documents as separate worlds.",
    icon: FileCheck2,
    title: "Keep the paperwork together",
  },
  {
    description:
      "Get a clearer picture of revenue and activity without building another Excel report.",
    icon: BarChart3,
    title: "Understand the business",
  },
];

const audiences = [
  "Owner-operators growing beyond a single truck",
  "Small fleet owners",
  "Dispatch and back-office teams",
  "Trucking companies that have outgrown spreadsheets",
  "Businesses that don't need the complexity of a large enterprise TMS",
];

const comparisons = [
  ["Multiple files", "One organized system"],
  ["Manual invoice tracking", "Clear invoice statuses"],
  ["Paperwork stored elsewhere", "Information kept together"],
  ["Repeated data entry", "Reusable business data"],
  [
    "Outstanding invoices require manual checking",
    "Unpaid invoices are visible",
  ],
  ["Reports built manually", "Business overview available instantly"],
  ["Knowledge lives in someone's head", "Workflow is visible to the team"],
];

const faqs = [
  {
    answer:
      "InvoiceTrucker is designed primarily for owner-operators and small trucking fleets that need a better way to organize invoices, customers, fleet information, and financial activity.",
    question: "Who is InvoiceTrucker for?",
  },
  {
    answer:
      "No. InvoiceTrucker is focused on trucking operations and invoicing workflows. It is not intended to replace full accounting software.",
    question: "Is InvoiceTrucker an accounting system?",
  },
  {
    answer:
      "That is one of the main use cases. InvoiceTrucker is intended for businesses that have started to outgrow spreadsheet-based invoice and fleet management.",
    question: "Can I use it instead of Excel?",
  },
  {
    answer:
      "The product is being designed with small and growing fleets in mind. Larger operations with complex dispatch, compliance, or enterprise TMS requirements may need more specialized systems.",
    question: "Does InvoiceTrucker support larger fleets?",
  },
  {
    answer:
      "You can explore the current product experience using demonstration data and join the early-access list if you want to follow development or test future releases.",
    question: "Can I try it?",
  },
];

const faqStructuredData = {
  "@context": "https://schema.org",
  "@type": "FAQPage",
  mainEntity: faqs.map(({ answer, question }) => ({
    "@type": "Question",
    acceptedAnswer: { "@type": "Answer", text: answer },
    name: question,
  })),
};

export default function HomePage() {
  return (
    <>
      <a
        className="fixed left-4 top-3 z-[100] -translate-y-20 rounded-lg bg-slate-950 px-4 py-2 text-sm font-semibold text-white transition-transform focus:translate-y-0"
        href="#main-content"
      >
        Skip to content
      </a>
      <SiteHeader />

      <main id="main-content">
        <section className="hero-section overflow-hidden">
          <Container className="grid items-center gap-14 py-18 lg:grid-cols-[0.9fr_1.1fr] lg:gap-12 lg:py-24">
            <div className="relative z-10">
              <Badge tone="blue">Built for small trucking fleets</Badge>
              <h1 className="mt-6 max-w-3xl text-5xl font-semibold tracking-[-0.055em] text-slate-950 sm:text-6xl lg:text-[4rem] lg:leading-[1.02]">
                Stop managing trucking invoices across spreadsheets, emails, and
                folders.
              </h1>
              <p className="mt-6 max-w-2xl text-lg leading-8 text-slate-600">
                InvoiceTrucker gives small trucking fleets one place to manage
                loads, invoices, customers, paperwork, and payments — without
                the spreadsheet mess.
              </p>
              <div className="mt-8 flex flex-col gap-3 sm:flex-row">
                <Link
                  className="button-link button-link-primary"
                  href="#early-access"
                >
                  Get Early Access <ArrowRight aria-hidden="true" size={17} />
                </Link>
                <Link
                  className="button-link button-link-secondary"
                  href="#how-it-works"
                >
                  See How It Works
                </Link>
              </div>
              <p className="mt-8 text-sm font-semibold tracking-wide text-slate-500">
                Small fleet. Less admin. Better visibility.
              </p>
            </div>

            <div className="relative lg:translate-x-5">
              <div aria-hidden="true" className="hero-preview-glow" />
              <div className="relative rounded-[1.25rem] border border-slate-200 bg-white p-2 shadow-[0_28px_70px_-28px_rgba(15,23,42,0.32)] sm:p-3">
                <div className="mb-2 flex items-center justify-between px-2 py-1 sm:mb-3">
                  <div className="flex gap-1.5" aria-hidden="true">
                    <span className="size-2 rounded-full bg-slate-200" />
                    <span className="size-2 rounded-full bg-slate-200" />
                    <span className="size-2 rounded-full bg-slate-200" />
                  </div>
                  <span className="rounded-md bg-slate-50 px-2 py-1 text-[8px] font-medium text-slate-400 sm:text-[10px]">
                    InvoiceTrucker dashboard
                  </span>
                  <span className="w-8" aria-hidden="true" />
                </div>
                <DashboardPreview />
              </div>
            </div>
          </Container>
        </section>

        <section
          className="border-y border-slate-200 bg-white py-20 sm:py-26"
          id="problem"
        >
          <Container>
            <SectionHeading
              eyebrow="The paperwork problem"
              title="Running a trucking business gets messy fast."
            >
              <p className="font-medium text-slate-800">
                The trucks may be moving, but the paperwork never stops.
              </p>
              <p className="mt-4">
                A completed load needs documents. Documents need to become an
                invoice. The invoice needs to be sent. Someone needs to remember
                whether it was paid.
              </p>
              <p className="mt-4">
                When that process lives across Excel files, email threads,
                folders, and memory, things start slipping through the cracks.
              </p>
            </SectionHeading>
            <div className="mt-12 grid gap-4 md:grid-cols-2 lg:grid-cols-6">
              {problems.map(([title, description], index) => (
                <article
                  className={`problem-card ${index < 3 ? "lg:col-span-2" : "lg:col-span-3"}`}
                  key={title}
                >
                  <span className="problem-number">
                    {String(index + 1).padStart(2, "0")}
                  </span>
                  <h3 className="mt-5 text-base font-semibold text-slate-950">
                    {title}
                  </h3>
                  <p className="mt-2 text-sm leading-6 text-slate-500">
                    {description}
                  </p>
                </article>
              ))}
            </div>
          </Container>
        </section>

        <section className="cash-flow-section py-18 sm:py-22">
          <Container>
            <div className="max-w-4xl">
              <p className="text-sm font-semibold uppercase tracking-[0.16em] text-blue-300">
                The real cost
              </p>
              <h2 className="mt-4 text-3xl font-semibold tracking-[-0.04em] text-white sm:text-5xl">
                A missed invoice isn&apos;t an admin problem. It&apos;s a
                cash-flow problem.
              </h2>
              <p className="mt-6 max-w-3xl text-lg leading-8 text-slate-300">
                Saving a few minutes on paperwork is useful. Missing a $2,000,
                $4,000, or $6,000 invoice because it disappeared inside a
                spreadsheet is something else entirely.
              </p>
              <p className="mt-5 max-w-3xl text-lg font-medium leading-8 text-white">
                InvoiceTrucker is designed to help you keep the process visible
                from completed work to paid invoice.
              </p>
            </div>
          </Container>
        </section>

        <section className="py-20 sm:py-26" id="how-it-works">
          <Container>
            <SectionHeading
              eyebrow="How it works"
              title="From completed load to paid invoice — one clear workflow."
            >
              <p>
                Simple enough for a small fleet. Structured enough to grow with
                you.
              </p>
            </SectionHeading>
            <div
              aria-label="Load to payment workflow"
              className="workflow-strip mt-10"
            >
              {["Load", "Documents", "Invoice", "Sent", "Paid"].map(
                (stage, index, stages) => (
                  <div className="contents" key={stage}>
                    <span className="workflow-stage">{stage}</span>
                    {index < stages.length - 1 ? (
                      <ArrowRight
                        aria-hidden="true"
                        className="workflow-arrow"
                        size={18}
                      />
                    ) : null}
                  </div>
                ),
              )}
            </div>
            <ol className="mt-10 grid gap-4 md:grid-cols-2">
              {workflowSteps.map(([title, description], index) => (
                <li className="step-card" key={title}>
                  <span className="step-number">{index + 1}</span>
                  <div>
                    <h3 className="text-base font-semibold text-slate-950 sm:text-lg">
                      {title}
                    </h3>
                    <p className="mt-2 text-sm leading-6 text-slate-500">
                      {description}
                    </p>
                  </div>
                </li>
              ))}
            </ol>
          </Container>
        </section>

        <section
          className="border-y border-slate-200 bg-white py-20 sm:py-26"
          id="product"
        >
          <Container>
            <SectionHeading
              eyebrow="The product"
              title="See the business without digging through spreadsheets."
            >
              <p>
                Move between the same core records your team relies on every
                day, without rebuilding the bigger picture by hand.
              </p>
            </SectionHeading>
            <div className="mt-10 grid gap-3 sm:grid-cols-2 lg:grid-cols-5">
              {productHighlights.map(({ description, icon: Icon, title }) => (
                <article
                  className="rounded-2xl border border-slate-200 bg-slate-50/70 p-5"
                  key={title}
                >
                  <Icon
                    aria-hidden="true"
                    className="text-blue-600"
                    size={20}
                  />
                  <h3 className="mt-4 font-semibold text-slate-950">{title}</h3>
                  <p className="mt-2 text-sm leading-6 text-slate-500">
                    {description}
                  </p>
                </article>
              ))}
            </div>
            <div className="mt-10">
              <ProductShowcase />
            </div>
          </Container>
        </section>

        <section className="py-20 sm:py-26" id="benefits">
          <Container>
            <SectionHeading
              eyebrow="Core benefits"
              title="Less admin. More control."
            >
              <p>
                Keep routine work moving while maintaining a clearer view of
                what has happened and what needs attention next.
              </p>
            </SectionHeading>
            <div className="mt-12 grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
              {benefits.map(({ description, icon: Icon, title }) => (
                <article className="benefit-card" key={title}>
                  <span className="feature-icon">
                    <Icon aria-hidden="true" size={20} />
                  </span>
                  <h3 className="mt-5 text-base font-semibold text-slate-950">
                    {title}
                  </h3>
                  <p className="mt-2 text-sm leading-6 text-slate-500">
                    {description}
                  </p>
                </article>
              ))}
            </div>
          </Container>
        </section>

        <section
          className="border-y border-slate-200 bg-white py-20 sm:py-26"
          id="small-fleets"
        >
          <Container className="grid gap-10 lg:grid-cols-[0.9fr_1.1fr] lg:items-center">
            <SectionHeading
              eyebrow="Who it is for"
              title="Built for small trucking fleets."
            >
              <p>
                InvoiceTrucker is designed for teams that need a clear,
                practical operating system without the weight of enterprise
                transportation software.
              </p>
            </SectionHeading>
            <div className="rounded-3xl border border-slate-200 bg-slate-50 p-6 sm:p-8">
              <ul className="grid gap-4">
                {audiences.map((audience) => (
                  <li
                    className="flex items-start gap-3 text-sm leading-6 text-slate-700 sm:text-base"
                    key={audience}
                  >
                    <span className="mt-0.5 grid size-6 shrink-0 place-items-center rounded-full bg-blue-100 text-blue-700">
                      <Check aria-hidden="true" size={14} strokeWidth={3} />
                    </span>
                    {audience}
                  </li>
                ))}
              </ul>
              <p className="mt-7 border-t border-slate-200 pt-6 text-base font-semibold leading-7 text-slate-950">
                You shouldn&apos;t need enterprise software just to know what
                has been invoiced and what still needs to be paid.
              </p>
            </div>
          </Container>
        </section>

        <section className="py-20 sm:py-26" id="comparison">
          <Container>
            <SectionHeading
              eyebrow="A better next step"
              title="Excel works. Until it doesn't."
            >
              <p>
                Spreadsheets are useful. Most small trucking businesses start
                with them for a reason.
              </p>
              <p className="mt-4">
                But as trucks, customers, invoices, and paperwork increase, the
                system becomes harder to control.
              </p>
            </SectionHeading>
            <div className="comparison-table-wrap mt-10">
              <table className="comparison-table">
                <thead>
                  <tr>
                    <th>Spreadsheets</th>
                    <th>InvoiceTrucker</th>
                  </tr>
                </thead>
                <tbody>
                  {comparisons.map(([spreadsheet, invoiceTrucker]) => (
                    <tr key={spreadsheet}>
                      <td>{spreadsheet}</td>
                      <td>
                        <Check aria-hidden="true" size={16} strokeWidth={2.5} />
                        {invoiceTrucker}
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
            <p className="mt-8 max-w-3xl text-lg leading-8 text-slate-600">
              InvoiceTrucker doesn&apos;t exist because spreadsheets are bad. It
              exists because eventually they become the wrong tool for the job.
            </p>
          </Container>
        </section>

        <section
          className="border-y border-slate-200 bg-white py-20 sm:py-26"
          id="about"
        >
          <Container>
            <div className="why-panel">
              <Badge className="border-white/15 bg-white/10 text-blue-200">
                Why InvoiceTrucker
              </Badge>
              <h2 className="mt-5 max-w-3xl text-3xl font-semibold tracking-[-0.04em] text-white sm:text-4xl">
                Built around the way small fleets actually work.
              </h2>
              <p className="mt-5 max-w-3xl text-base leading-7 text-slate-300 sm:text-lg">
                InvoiceTrucker isn&apos;t trying to become another massive
                transportation platform packed with features a five-truck
                company will never use.
              </p>
              <p className="mt-5 max-w-3xl text-base leading-7 text-slate-300 sm:text-lg">
                The goal is simpler: help small trucking businesses keep the
                operational and invoicing side of the business organized without
                turning software into another full-time job.
              </p>
            </div>
          </Container>
        </section>

        <section className="py-20 sm:py-26" id="product-demo">
          <Container className="grid gap-10 lg:grid-cols-[1fr_0.8fr] lg:items-center">
            <SectionHeading
              eyebrow="Product preview"
              title="See InvoiceTrucker in action."
            >
              <p>
                Explore the current product experience and see how fleet
                information, customers, invoices, expenses, documents, and
                reporting can work together.
              </p>
              <Link
                className="mt-7 button-link button-link-primary"
                href="/demo"
              >
                Explore the Product <ArrowRight aria-hidden="true" size={17} />
              </Link>
              <p className="mt-4 text-sm text-slate-500">
                The current public environment uses demonstration data so you
                can explore the workflow safely.
              </p>
            </SectionHeading>
            <div className="rounded-3xl border border-blue-100 bg-blue-50 p-7 sm:p-9">
              <FileText
                aria-hidden="true"
                className="text-blue-600"
                size={34}
              />
              <p className="mt-5 text-xl font-semibold tracking-tight text-slate-950">
                Explore the workflow at your own pace.
              </p>
              <p className="mt-3 text-sm leading-6 text-slate-600">
                Move through the dashboard, fleet, customers, invoices,
                expenses, documents, and reports without creating an account.
              </p>
            </div>
          </Container>
        </section>

        <section
          className="border-y border-slate-200 bg-white py-20 sm:py-26"
          id="early-access"
        >
          <Container>
            <div className="early-access-panel early-access-panel-form">
              <div className="relative z-10 max-w-xl">
                <Badge tone="blue">Early access</Badge>
                <h2 className="mt-5 text-3xl font-semibold tracking-[-0.04em] text-slate-950 sm:text-4xl">
                  Help shape InvoiceTrucker around real trucking workflows.
                </h2>
                <p className="mt-5 text-base leading-7 text-slate-600 sm:text-lg">
                  We&apos;re building InvoiceTrucker for small trucking
                  businesses that want something better than spreadsheets
                  without jumping into expensive enterprise software.
                </p>
                <p className="mt-4 text-base leading-7 text-slate-600">
                  Join early access and tell us where your current workflow
                  wastes the most time.
                </p>
                <p className="mt-5 text-sm font-semibold text-slate-700">
                  No sales pressure. We want to understand how your fleet works.
                </p>
              </div>
              <div className="relative z-10">
                <NewsletterForm />
              </div>
            </div>
          </Container>
        </section>

        <section className="py-20 sm:py-26" id="faq">
          <Container className="grid gap-10 lg:grid-cols-[0.7fr_1.3fr]">
            <SectionHeading
              eyebrow="FAQ"
              title="Straight answers for small fleets."
            >
              <p>
                What InvoiceTrucker is designed to do, and where a more
                specialized system may be the better fit.
              </p>
            </SectionHeading>
            <div className="grid gap-3">
              {faqs.map(({ answer, question }) => (
                <details className="faq-item" key={question}>
                  <summary>{question}</summary>
                  <p>{answer}</p>
                </details>
              ))}
            </div>
          </Container>
        </section>

        <section className="final-cta-section py-20 sm:py-26">
          <Container className="relative z-10 text-center">
            <h2 className="mx-auto max-w-4xl text-4xl font-semibold tracking-[-0.045em] text-white sm:text-5xl">
              Your trucks shouldn&apos;t be easier to track than your money.
            </h2>
            <p className="mx-auto mt-6 max-w-2xl text-lg leading-8 text-slate-300">
              Keep invoices, customers, fleet information, and business activity
              in one place. Spend less time rebuilding spreadsheets and more
              time knowing where the business stands.
            </p>
            <div className="mt-8 flex flex-col justify-center gap-3 sm:flex-row">
              <Link
                className="button-link bg-white text-slate-950 hover:bg-blue-50"
                href="#early-access"
              >
                Get Early Access <ArrowRight aria-hidden="true" size={17} />
              </Link>
              <Link
                className="button-link border border-white/25 bg-white/10 text-white hover:bg-white/15"
                href="#how-it-works"
              >
                See How It Works
              </Link>
            </div>
          </Container>
        </section>
      </main>

      <SiteFooter />
      <script
        dangerouslySetInnerHTML={{ __html: JSON.stringify(faqStructuredData) }}
        type="application/ld+json"
      />
    </>
  );
}
