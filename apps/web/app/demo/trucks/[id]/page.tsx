import { TruckDetailView } from "@/components/demo/details";

export default async function TruckDetailPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;
  return <TruckDetailView id={id} />;
}
