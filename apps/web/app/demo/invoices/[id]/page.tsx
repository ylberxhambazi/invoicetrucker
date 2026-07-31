import { InvoiceDetailView } from "@/components/demo/details";

export default async function InvoiceDetailPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;
  return <InvoiceDetailView id={id} />;
}
