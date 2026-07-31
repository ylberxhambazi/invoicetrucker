import type { Metadata } from "next";

import { DemoShell } from "@/components/demo/demo-shell";
import { QueryProvider } from "@/components/demo/query-provider";

export const metadata: Metadata = {
  title: "Read-only demo",
  description:
    "Explore the fictional InvoiceTrucker fleet operations portfolio demo.",
};

export default function DemoLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <QueryProvider>
      <DemoShell>{children}</DemoShell>
    </QueryProvider>
  );
}
