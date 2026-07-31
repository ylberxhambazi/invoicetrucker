import type { ReactNode } from "react";

export interface SectionHeadingProps {
  align?: "left" | "center";
  eyebrow: string;
  title: string;
  children?: ReactNode;
}

export function SectionHeading({
  align = "left",
  children,
  eyebrow,
  title,
}: SectionHeadingProps) {
  const alignment =
    align === "center" ? "mx-auto items-center text-center" : "items-start";

  return (
    <div className={`flex max-w-3xl flex-col ${alignment}`}>
      <p className="text-sm font-semibold uppercase tracking-[0.16em] text-blue-600">
        {eyebrow}
      </p>
      <h2 className="mt-3 text-3xl font-semibold tracking-[-0.035em] text-slate-950 sm:text-4xl">
        {title}
      </h2>
      {children ? (
        <div className="mt-5 text-base leading-7 text-slate-600 sm:text-lg">
          {children}
        </div>
      ) : null}
    </div>
  );
}
