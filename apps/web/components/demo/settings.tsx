"use client";

import { useQuery } from "@tanstack/react-query";
import { Building2, Database, LockKeyhole } from "lucide-react";

import {
  InfoGrid,
  PageHeader,
  QueryError,
  QueryLoading,
} from "@/components/demo/ui";
import { demoApi } from "@/lib/demo-api";

export function DemoSettings() {
  const query = useQuery({
    queryKey: ["settings"],
    queryFn: ({ signal }) => demoApi.settings(signal),
  });

  if (query.isPending) return <QueryLoading label="Loading settings" />;
  if (query.isError)
    return (
      <QueryError error={query.error} retry={() => void query.refetch()} />
    );

  const settings = query.data;

  return (
    <>
      <PageHeader
        description="A read-only view of the fictional company profile and invoice preferences stored in PostgreSQL."
        title="Settings"
      />
      <div className="mb-5 grid gap-4 sm:grid-cols-3">
        {[
          {
            icon: Building2,
            title: "Fictional profile",
            text: "No real organization or contact data.",
          },
          {
            icon: Database,
            title: "API sourced",
            text: "Loaded from the seeded database.",
          },
          {
            icon: LockKeyhole,
            title: "Read only",
            text: "No save, edit, or account controls.",
          },
        ].map(({ icon: Icon, title, text }) => (
          <article className="demo-card p-5" key={title}>
            <Icon className="text-blue-600" size={20} />
            <h2 className="mt-3 text-sm font-semibold text-slate-900">
              {title}
            </h2>
            <p className="mt-1 text-xs leading-5 text-slate-500">{text}</p>
          </article>
        ))}
      </div>
      <InfoGrid
        items={[
          { label: "Display name", value: settings.companyName },
          { label: "Legal name", value: settings.legalName },
          { label: "Contact email", value: settings.email },
          { label: "Phone", value: settings.phone },
          { label: "Address", value: `${settings.address}, ${settings.city}` },
          { label: "Country", value: settings.country },
          { label: "VAT number", value: settings.vatNumber },
          { label: "Default currency", value: settings.defaultCurrency },
          { label: "Time zone", value: settings.timeZone },
          { label: "Invoice prefix", value: settings.invoicePrefix },
          {
            label: "Payment terms",
            value: `${settings.paymentTermsDays} days`,
          },
          {
            label: "Last seeded update",
            value:
              new Date(settings.updatedAtUtc).toLocaleString("en-GB", {
                timeZone: "UTC",
              }) + " UTC",
          },
        ]}
      />
    </>
  );
}
