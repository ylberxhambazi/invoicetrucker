"use client";

import {
  AlertCircle,
  ArrowLeft,
  ChevronLeft,
  ChevronRight,
  RotateCcw,
  Search,
} from "lucide-react";
import Link from "next/link";
import { usePathname, useRouter, useSearchParams } from "next/navigation";
import { useState } from "react";

import { displayEnum } from "@/lib/format";

export function PageHeader({
  title,
  description,
  action,
}: {
  title: string;
  description: string;
  action?: React.ReactNode;
}) {
  return (
    <div className="mb-7 flex flex-col justify-between gap-4 sm:flex-row sm:items-end">
      <div>
        <h1 className="text-2xl font-semibold tracking-[-0.035em] text-slate-950 sm:text-3xl">
          {title}
        </h1>
        <p className="mt-2 max-w-2xl text-sm leading-6 text-slate-500">
          {description}
        </p>
      </div>
      {action}
    </div>
  );
}

export function DetailHeader({
  backHref,
  backLabel,
  title,
  subtitle,
  status,
  action,
}: {
  backHref: string;
  backLabel: string;
  title: string;
  subtitle: string;
  status?: string;
  action?: React.ReactNode;
}) {
  return (
    <div className="mb-7">
      <Link className="demo-back-link" href={backHref}>
        <ArrowLeft size={15} />
        {backLabel}
      </Link>
      <div className="mt-4 flex flex-col justify-between gap-4 sm:flex-row sm:items-end">
        <div>
          <div className="flex flex-wrap items-center gap-3">
            <h1 className="text-2xl font-semibold tracking-[-0.035em] text-slate-950 sm:text-3xl">
              {title}
            </h1>
            {status ? <StatusBadge value={status} /> : null}
          </div>
          <p className="mt-2 text-sm text-slate-500">{subtitle}</p>
        </div>
        {action}
      </div>
    </div>
  );
}

export function StatusBadge({ value }: { value: string }) {
  const normalized = value.toLowerCase();
  const tone =
    normalized.includes("paid") ||
    normalized.includes("active") ||
    normalized.includes("available") ||
    normalized.includes("valid")
      ? "success"
      : normalized.includes("overdue") ||
          normalized.includes("expired") ||
          normalized.includes("attention")
        ? "danger"
        : normalized.includes("route") || normalized.includes("sent")
          ? "blue"
          : normalized.includes("maintenance") ||
              normalized.includes("leave") ||
              normalized.includes("soon")
            ? "warning"
            : "neutral";

  return (
    <span className={`demo-status demo-status-${tone}`}>
      {displayEnum(value)}
    </span>
  );
}

export function QueryLoading({
  label = "Loading records",
}: {
  label?: string;
}) {
  return (
    <div aria-label={label} aria-live="polite" className="grid gap-3">
      {[0, 1, 2, 3].map((item) => (
        <div
          className="h-16 animate-pulse rounded-xl bg-slate-200/70"
          key={item}
        />
      ))}
    </div>
  );
}

export function QueryError({
  error,
  retry,
}: {
  error: Error;
  retry: () => void;
}) {
  return (
    <div className="demo-state">
      <span className="demo-state-icon bg-red-50 text-red-600">
        <AlertCircle size={22} />
      </span>
      <h2 className="mt-4 text-lg font-semibold text-slate-950">
        Demo data could not be loaded
      </h2>
      <p className="mt-2 max-w-md text-sm leading-6 text-slate-500">
        {error.message}. Confirm the InvoiceTrucker API and PostgreSQL database
        are running, then try again.
      </p>
      <button
        className="demo-button demo-button-secondary mt-5"
        onClick={retry}
        type="button"
      >
        <RotateCcw size={16} />
        Try again
      </button>
    </div>
  );
}

export function EmptyState({ message }: { message: string }) {
  return (
    <div className="demo-state">
      <span className="demo-state-icon bg-slate-100 text-slate-500">
        <Search size={22} />
      </span>
      <h2 className="mt-4 text-lg font-semibold text-slate-950">
        No records found
      </h2>
      <p className="mt-2 text-sm text-slate-500">{message}</p>
    </div>
  );
}

export interface FilterOption {
  label: string;
  value: string;
}

export function ListToolbar({
  placeholder,
  filters = [],
  sortOptions,
}: {
  placeholder: string;
  filters?: { name: string; label: string; options: FilterOption[] }[];
  sortOptions: FilterOption[];
}) {
  const router = useRouter();
  const pathname = usePathname();
  const searchParams = useSearchParams();
  const [search, setSearch] = useState(searchParams.get("search") ?? "");

  function update(values: Record<string, string>) {
    const params = new URLSearchParams(searchParams.toString());
    Object.entries(values).forEach(([key, value]) => {
      if (value) params.set(key, value);
      else params.delete(key);
    });
    params.set("page", "1");
    router.push(`${pathname}?${params.toString()}`);
  }

  return (
    <div className="demo-toolbar">
      <form
        className="relative min-w-0 flex-1"
        onSubmit={(event) => {
          event.preventDefault();
          update({ search: search.trim() });
        }}
      >
        <Search aria-hidden="true" className="demo-search-icon" size={17} />
        <label className="sr-only" htmlFor="demo-list-search">
          Search
        </label>
        <input
          className="demo-input w-full pl-10"
          id="demo-list-search"
          onChange={(event) => setSearch(event.target.value)}
          placeholder={placeholder}
          value={search}
        />
      </form>
      {filters.map((filter) => (
        <label className="demo-select-label" key={filter.name}>
          <span className="sr-only">{filter.label}</span>
          <select
            className="demo-select"
            onChange={(event) => update({ [filter.name]: event.target.value })}
            value={searchParams.get(filter.name) ?? ""}
          >
            <option value="">{filter.label}</option>
            {filter.options.map((option) => (
              <option key={option.value} value={option.value}>
                {option.label}
              </option>
            ))}
          </select>
        </label>
      ))}
      <label className="demo-select-label">
        <span className="sr-only">Sort records</span>
        <select
          className="demo-select"
          onChange={(event) => {
            const [sortBy, sortDirection] = event.target.value.split(":");
            update({ sortBy, sortDirection });
          }}
          value={`${searchParams.get("sortBy") ?? sortOptions[0]?.value.split(":")[0]}:${searchParams.get("sortDirection") ?? "asc"}`}
        >
          {sortOptions.map((option) => (
            <option key={option.value} value={option.value}>
              {option.label}
            </option>
          ))}
        </select>
      </label>
    </div>
  );
}

export function Pagination({
  page,
  totalPages,
  totalCount,
}: {
  page: number;
  totalPages: number;
  totalCount: number;
}) {
  const router = useRouter();
  const pathname = usePathname();
  const searchParams = useSearchParams();

  function go(nextPage: number) {
    const params = new URLSearchParams(searchParams.toString());
    params.set("page", String(nextPage));
    router.push(`${pathname}?${params.toString()}`);
  }

  return (
    <div className="demo-pagination">
      <p>
        {totalCount} {totalCount === 1 ? "record" : "records"}
      </p>
      <div className="flex items-center gap-2">
        <button
          aria-label="Previous page"
          className="demo-icon-button"
          disabled={page <= 1}
          onClick={() => go(page - 1)}
          type="button"
        >
          <ChevronLeft size={17} />
        </button>
        <span className="min-w-20 text-center text-sm font-medium text-slate-700">
          {page} of {Math.max(totalPages, 1)}
        </span>
        <button
          aria-label="Next page"
          className="demo-icon-button"
          disabled={page >= totalPages}
          onClick={() => go(page + 1)}
          type="button"
        >
          <ChevronRight size={17} />
        </button>
      </div>
    </div>
  );
}

export function MetricCard({
  label,
  value,
  detail,
  icon,
}: {
  label: string;
  value: string;
  detail?: string;
  icon: React.ReactNode;
}) {
  return (
    <article className="demo-card p-5">
      <div className="flex items-start justify-between gap-4">
        <div>
          <p className="text-sm font-medium text-slate-500">{label}</p>
          <p className="mt-2 text-2xl font-semibold tracking-tight text-slate-950">
            {value}
          </p>
          {detail ? (
            <p className="mt-2 text-xs text-slate-500">{detail}</p>
          ) : null}
        </div>
        <span className="grid size-10 place-items-center rounded-xl bg-blue-50 text-blue-600">
          {icon}
        </span>
      </div>
    </article>
  );
}

export function InfoGrid({
  items,
}: {
  items: { label: string; value: React.ReactNode }[];
}) {
  return (
    <dl className="grid gap-px overflow-hidden rounded-2xl border border-slate-200 bg-slate-200 sm:grid-cols-2">
      {items.map((item) => (
        <div className="bg-white p-5" key={item.label}>
          <dt className="text-xs font-semibold uppercase tracking-wide text-slate-400">
            {item.label}
          </dt>
          <dd className="mt-2 text-sm font-medium text-slate-800">
            {item.value}
          </dd>
        </div>
      ))}
    </dl>
  );
}
