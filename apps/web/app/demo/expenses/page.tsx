import { Suspense } from "react";

import { ExpensesList } from "@/components/demo/resource-lists";
import { QueryLoading } from "@/components/demo/ui";

export default function ExpensesPage() {
  return (
    <Suspense fallback={<QueryLoading />}>
      <ExpensesList />
    </Suspense>
  );
}
