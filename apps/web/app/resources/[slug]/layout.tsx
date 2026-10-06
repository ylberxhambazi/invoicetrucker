import type { ReactNode } from "react";

import { ResourceScrollToTop } from "@/components/resource-scroll-to-top";

export default function ResourceArticleLayout({
  children,
}: Readonly<{ children: ReactNode }>) {
  return (
    <>
      <ResourceScrollToTop />
      {children}
    </>
  );
}
