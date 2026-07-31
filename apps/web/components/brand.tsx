import Link from "next/link";

export function Brand() {
  return (
    <Link
      aria-label="InvoiceTrucker home"
      className="inline-flex items-center gap-2.5 rounded-lg focus-visible:outline-2 focus-visible:outline-offset-4 focus-visible:outline-blue-600"
      href="/"
    >
      <span
        aria-hidden="true"
        className="grid size-9 place-items-center rounded-xl bg-slate-950 shadow-sm"
      >
        <span className="relative block h-4 w-5">
          <span className="absolute left-0 top-0 h-1.5 w-5 rounded-sm bg-white" />
          <span className="absolute bottom-0 left-0 h-1.5 w-3.5 rounded-sm bg-blue-400" />
        </span>
      </span>
      <span className="text-lg font-semibold tracking-[-0.03em] text-slate-950">
        InvoiceTrucker
      </span>
    </Link>
  );
}
