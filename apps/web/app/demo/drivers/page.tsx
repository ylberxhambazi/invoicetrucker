import { Suspense } from "react";

import { DriversList } from "@/components/demo/resource-lists";
import { QueryLoading } from "@/components/demo/ui";

export default function DriversPage() {
  return (
    <Suspense fallback={<QueryLoading />}>
      <DriversList />
    </Suspense>
  );
}
