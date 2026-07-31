import { Suspense } from "react";

import { ClientsList } from "@/components/demo/resource-lists";
import { QueryLoading } from "@/components/demo/ui";

export default function ClientsPage() {
  return (
    <Suspense fallback={<QueryLoading />}>
      <ClientsList />
    </Suspense>
  );
}
