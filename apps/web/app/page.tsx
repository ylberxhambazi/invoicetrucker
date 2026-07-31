import { Badge, Container, SectionHeading } from "@invoicetrucker/ui";
import {
  ArrowRight,
  BarChart3,
  Boxes,
  Check,
  CircleDollarSign,
  Database,
  FileArchive,
  Fuel,
  Github,
  LayoutDashboard,
  ReceiptText,
  ShieldCheck,
  Truck,
  UsersRound,
  WalletCards,
} from "lucide-react";
import Link from "next/link";

import { DashboardPreview } from "@/components/dashboard-preview";
import { ProductShowcase } from "@/components/product-showcase";
import { RelatedProductExperience } from "@/components/related-product-experience";
import { NewsletterForm } from "@/components/newsletter-form";
import { SiteFooter } from "@/components/site-footer";
import { SiteHeader } from "@/components/site-header";

const problems = [
  "Business information spread across Excel files",
  "Lost or incomplete documents",
  "Difficult invoice tracking",
  "No clear view of vehicle profitability",
  "Manual expense calculations",
  "Limited operational reporting",
];

const features = [
  {
    description:
      "Keep vehicles, mileage, assignments, service status, and compliance dates together.",
    icon: Truck,
    title: "Fleet management",
  },
  {
    description:
      "Track licences, availability, assigned vehicles, contact details, and completed trips.",
    icon: UsersRound,
    title: "Driver management",
  },
  {
    description:
      "Maintain customer records, contacts, billing history, and outstanding balances.",
    icon: WalletCards,
    title: "Customer management",
  },
  {
    description:
      "Create clear invoice records with line items, due dates, currencies, and statuses.",
    icon: ReceiptText,
    title: "Invoice generation",
  },
  {
    description:
      "Record fuel, tolls, repairs, insurance, and driver costs against each vehicle.",
    icon: Fuel,
    title: "Expense tracking",
  },
  {
    description:
      "Monitor insurance, registrations, licences, contracts, and upcoming expirations.",
    icon: FileArchive,
    title: "Document management",
  },
  {
    description:
      "See revenue, expenses, invoice status, fleet utilization, and operational trends.",
    icon: BarChart3,
    title: "Operational reporting",
  },
  {
    description:
      "Understand estimated margin and cost performance across individual trucks.",
    icon: CircleDollarSign,
    title: "Profitability insights",
  },
];

const steps = [
  {
    description:
      "Build a reliable operating picture with vehicles, team members, and commercial relationships in one place.",
    number: "01",
    title: "Add your fleet, drivers, and customers",
  },
  {
    description:
      "Capture the day-to-day work without scattering records across spreadsheets, inboxes, and folders.",
    number: "02",
    title: "Record invoices, trips, expenses, and documents",
  },
  {
    description:
      "Use clear summaries and reports to see what is earning, what is costing, and what needs attention.",
    number: "03",
    title: "Track financial and operational performance",
  },
];

const technologies = [
  { category: "Web framework", name: "Next.js", value: "16" },
  { category: "User interface", name: "React", value: "19" },
  { category: "Type safety", name: "TypeScript", value: "Strict" },
  { category: "API platform", name: "ASP.NET Core", value: "10" },
  { category: "Data store", name: "PostgreSQL", value: "18" },
  { category: "Local runtime", name: "Docker", value: "Compose" },
];

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
          <Container className="grid items-center gap-14 py-18 lg:grid-cols-[0.88fr_1.12fr] lg:gap-12 lg:py-24">
            <div className="relative z-10">
              <Badge tone="blue">
                <span className="mr-2 size-1.5 rounded-full bg-blue-600" />
                Fleet operations, made clear
              </Badge>
              <h1 className="mt-6 max-w-3xl text-5xl font-semibold tracking-[-0.055em] text-slate-950 sm:text-6xl lg:text-[4.15rem] lg:leading-[1.02]">
                Run your transport business from one place.
              </h1>
              <p className="mt-6 max-w-2xl text-lg leading-8 text-slate-600">
                Manage trucks, drivers, clients, invoices, expenses, and
                operational performance through one modern fleet management
                platform.
              </p>
              <div className="mt-8 flex flex-col gap-3 sm:flex-row">
                <Link className="button-link button-link-primary" href="/demo">
                  View Live Demo
                  <ArrowRight aria-hidden="true" size={17} />
                </Link>
                <Link
                  className="button-link button-link-secondary"
                  href="https://github.com/ylberxhambazi/InvoiceTrucker"
                  rel="noreferrer"
                  target="_blank"
                >
                  <Github aria-hidden="true" size={17} />
                  Explore on GitHub
                </Link>
              </div>
              <div className="mt-8 flex flex-wrap gap-x-6 gap-y-3 text-sm text-slate-500">
                {[
                  "No registration required",
                  "Fictional data",
                  "Open source",
                ].map((item) => (
                  <span className="inline-flex items-center gap-2" key={item}>
                    <span className="grid size-5 place-items-center rounded-full bg-emerald-50 text-emerald-600">
                      <Check aria-hidden="true" size={12} strokeWidth={3} />
                    </span>
                    {item}
                  </span>
                ))}
              </div>
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
                    app.invoicetrucker.demo
                  </span>
                  <span className="w-8" aria-hidden="true" />
                </div>
                <DashboardPreview />
              </div>
              <div className="preview-float-card left-[-1rem] top-[22%] hidden xl:flex">
                <span className="grid size-9 place-items-center rounded-lg bg-emerald-50 text-emerald-600">
                  <CircleDollarSign size={17} />
                </span>
                <span>
                  <small>Estimated profit</small>
                  <strong>€17,720</strong>
                </span>
              </div>
              <div className="preview-float-card bottom-[8%] right-[-1rem] hidden xl:flex">
                <span className="grid size-9 place-items-center rounded-lg bg-blue-50 text-blue-600">
                  <Truck size={17} />
                </span>
                <span>
                  <small>Fleet utilization</small>
                  <strong>87%</strong>
                </span>
              </div>
            </div>
          </Container>
        </section>

        <section className="border-y border-slate-200 bg-white py-18 sm:py-22">
          <Container>
            <SectionHeading
              align="center"
              eyebrow="The operational gap"
              title="Transport businesses outgrow disconnected tools."
            >
              <p>
                When daily operations live in different files and systems,
                routine work becomes slower and important decisions become
                harder.
              </p>
            </SectionHeading>
            <div className="mt-12 grid gap-3 sm:grid-cols-2 lg:grid-cols-3">
              {problems.map((problem, index) => (
                <article
                  className="group flex min-h-28 items-start gap-4 rounded-2xl border border-slate-200 bg-slate-50/70 p-5 transition-all hover:-translate-y-0.5 hover:border-slate-300 hover:bg-white hover:shadow-sm"
                  key={problem}
                >
                  <span className="grid size-9 shrink-0 place-items-center rounded-xl border border-slate-200 bg-white text-xs font-semibold text-slate-500 transition-colors group-hover:border-blue-200 group-hover:text-blue-600">
                    {String(index + 1).padStart(2, "0")}
                  </span>
                  <p className="pt-1.5 text-sm font-medium leading-6 text-slate-700">
                    {problem}
                  </p>
                </article>
              ))}
            </div>
          </Container>
        </section>

        <section className="py-20 sm:py-26" id="features">
          <Container>
            <SectionHeading
              eyebrow="One connected workspace"
              title="Everything needed to keep the business moving."
            >
              <p>
                InvoiceTrucker brings operational and financial records into a
                focused workspace designed for small and medium transport
                companies.
              </p>
            </SectionHeading>
            <div className="mt-12 grid gap-4 sm:grid-cols-2 lg:grid-cols-4">
              {features.map((feature) => {
                const Icon = feature.icon;

                return (
                  <article className="feature-card" key={feature.title}>
                    <span className="feature-icon">
                      <Icon aria-hidden="true" size={20} />
                    </span>
                    <h3 className="mt-5 text-base font-semibold text-slate-950">
                      {feature.title}
                    </h3>
                    <p className="mt-2 text-sm leading-6 text-slate-500">
                      {feature.description}
                    </p>
                  </article>
                );
              })}
            </div>
          </Container>
        </section>

        <section
          className="border-y border-slate-200 bg-white py-20 sm:py-26"
          id="product"
        >
          <Container>
            <div className="flex flex-col justify-between gap-6 lg:flex-row lg:items-end">
              <SectionHeading
                eyebrow="Product showcase"
                title="A practical workspace for every part of the operation."
              >
                <p>
                  Explore realistic interface previews built around the records
                  transport teams work with every day.
                </p>
              </SectionHeading>
              <p className="max-w-sm text-sm leading-6 text-slate-500">
                All companies, people, vehicle identifiers, and financial
                records shown here are entirely fictional.
              </p>
            </div>
            <div className="mt-12">
              <ProductShowcase />
            </div>
          </Container>
        </section>

        <section className="py-20 sm:py-26" id="demo">
          <Container>
            <div className="grid gap-12 lg:grid-cols-[0.8fr_1.2fr] lg:items-center">
              <SectionHeading
                eyebrow="How it works"
                title="From scattered records to clear decisions."
              >
                <p>
                  A straightforward workflow keeps operational data current and
                  turns it into useful context for the whole company.
                </p>
                <Link
                  className="mt-7 button-link button-link-primary"
                  href="/demo"
                >
                  Explore the demo
                  <ArrowRight aria-hidden="true" size={17} />
                </Link>
              </SectionHeading>

              <ol className="grid gap-4">
                {steps.map((step) => (
                  <li className="step-card" key={step.number}>
                    <span className="step-number">{step.number}</span>
                    <div>
                      <h3 className="text-base font-semibold text-slate-950 sm:text-lg">
                        {step.title}
                      </h3>
                      <p className="mt-2 text-sm leading-6 text-slate-500">
                        {step.description}
                      </p>
                    </div>
                  </li>
                ))}
              </ol>
            </div>
          </Container>
        </section>

        <section className="technology-section py-20 sm:py-26" id="technology">
          <Container>
            <div className="grid gap-12 lg:grid-cols-[0.85fr_1.15fr] lg:items-center">
              <div>
                <Badge className="border-white/15 bg-white/10 text-blue-200">
                  Engineering showcase
                </Badge>
                <h2 className="mt-5 text-3xl font-semibold tracking-[-0.04em] text-white sm:text-4xl">
                  Modern technology, chosen for maintainability.
                </h2>
                <p className="mt-5 max-w-xl text-base leading-7 text-slate-300">
                  The demonstration pairs a typed React frontend with an ASP.NET
                  Core API and PostgreSQL data layer, packaged for consistent
                  local development.
                </p>
                <Link
                  className="mt-7 inline-flex min-h-11 items-center gap-2 rounded-xl border border-white/20 bg-white/10 px-5 text-sm font-semibold text-white transition-colors hover:bg-white/15 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-400"
                  href="https://github.com/ylberxhambazi/InvoiceTrucker"
                  rel="noreferrer"
                  target="_blank"
                >
                  <Github aria-hidden="true" size={17} />
                  View the source
                </Link>
              </div>

              <div className="grid gap-3 sm:grid-cols-2">
                {technologies.map((technology) => (
                  <article className="technology-card" key={technology.name}>
                    <div className="flex items-center gap-3">
                      <span className="technology-icon">
                        {technology.name === "PostgreSQL" ? (
                          <Database size={18} />
                        ) : technology.name === "Docker" ? (
                          <Boxes size={18} />
                        ) : technology.name === "ASP.NET Core" ? (
                          <ShieldCheck size={18} />
                        ) : (
                          <LayoutDashboard size={18} />
                        )}
                      </span>
                      <span>
                        <small>{technology.category}</small>
                        <strong>{technology.name}</strong>
                      </span>
                    </div>
                    <span className="technology-value">{technology.value}</span>
                  </article>
                ))}
              </div>
            </div>
          </Container>
        </section>

        <RelatedProductExperience
          productUrl={process.env.NEXT_PUBLIC_PRODUCT_URL}
        />

        <section className="bg-white py-20 sm:py-26" id="early-access">
          <Container>
            <div className="early-access-panel early-access-panel-form">
              <div className="relative z-10 max-w-xl">
                <Badge tone="blue">Early access</Badge>
                <h2 className="mt-5 text-3xl font-semibold tracking-[-0.04em] text-slate-950 sm:text-4xl">
                  Follow InvoiceTrucker as the public showcase evolves.
                </h2>
                <p className="mt-5 text-base leading-7 text-slate-600 sm:text-lg">
                  Join the early-access list for project updates, technical
                  notes, and future demo releases. Registration connects
                  directly to the InvoiceTrucker API.
                </p>
              </div>
              <div className="relative z-10">
                <NewsletterForm />
              </div>
            </div>
          </Container>
        </section>
      </main>

      <SiteFooter />
    </>
  );
}
