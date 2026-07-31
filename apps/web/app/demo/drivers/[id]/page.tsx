import { DriverDetailView } from "@/components/demo/details";

export default async function DriverDetailPage({
  params,
}: {
  params: Promise<{ id: string }>;
}) {
  const { id } = await params;
  return <DriverDetailView id={id} />;
}
