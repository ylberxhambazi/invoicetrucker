import { Suspense } from "react";

import { TrucksList } from "@/components/demo/resource-lists";
import { QueryLoading } from "@/components/demo/ui";

export default function TrucksPage() {
  return (
    <Suspense fallback={<QueryLoading />}>
      <TrucksList />
    </Suspense>
  );
}
