import { Suspense } from "react";

import { InvoicesList } from "@/components/demo/resource-lists";
import { QueryLoading } from "@/components/demo/ui";

export default function InvoicesPage() {
  return (
    <Suspense fallback={<QueryLoading />}>
      <InvoicesList />
    </Suspense>
  );
}
