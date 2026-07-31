import { Suspense } from "react";

import { DocumentsList } from "@/components/demo/resource-lists";
import { QueryLoading } from "@/components/demo/ui";

export default function DocumentsPage() {
  return (
    <Suspense fallback={<QueryLoading />}>
      <DocumentsList />
    </Suspense>
  );
}
