import { ClientDetailView } from "@/components/demo/details";

export default async function ClientDetailPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;
  return <ClientDetailView id={id} />;
}
