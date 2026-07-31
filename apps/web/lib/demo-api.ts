import type {
  ClientDetail,
  ClientSummary,
  DashboardResponse,
  DocumentRecord,
  DriverDetail,
  DriverSummary,
  Expense,
  InvoiceDetail,
  InvoiceSummary,
  OverviewReport,
  PaginatedResponse,
  SettingsResponse,
  TruckDetail,
  TruckSummary,
} from "@invoicetrucker/types";

import { apiGet } from "@/lib/api-client";

export interface ListParams {
  page: number;
  pageSize: number;
  search?: string;
  sortBy?: string;
  sortDirection?: string;
  [key: string]: string | number | boolean | undefined;
}

export const demoApi = {
  dashboard: (signal?: AbortSignal) =>
    apiGet<DashboardResponse>("/api/dashboard", undefined, signal),
  trucks: (params: ListParams, signal?: AbortSignal) =>
    apiGet<PaginatedResponse<TruckSummary>>("/api/trucks", params, signal),
  truck: (id: string, signal?: AbortSignal) =>
    apiGet<TruckDetail>(`/api/trucks/${id}`, undefined, signal),
  drivers: (params: ListParams, signal?: AbortSignal) =>
    apiGet<PaginatedResponse<DriverSummary>>("/api/drivers", params, signal),
  driver: (id: string, signal?: AbortSignal) =>
    apiGet<DriverDetail>(`/api/drivers/${id}`, undefined, signal),
  clients: (params: ListParams, signal?: AbortSignal) =>
    apiGet<PaginatedResponse<ClientSummary>>("/api/clients", params, signal),
  client: (id: string, signal?: AbortSignal) =>
    apiGet<ClientDetail>(`/api/clients/${id}`, undefined, signal),
  invoices: (params: ListParams, signal?: AbortSignal) =>
    apiGet<PaginatedResponse<InvoiceSummary>>("/api/invoices", params, signal),
  invoice: (id: string, signal?: AbortSignal) =>
    apiGet<InvoiceDetail>(`/api/invoices/${id}`, undefined, signal),
  expenses: (params: ListParams, signal?: AbortSignal) =>
    apiGet<PaginatedResponse<Expense>>("/api/expenses", params, signal),
  documents: (params: ListParams, signal?: AbortSignal) =>
    apiGet<PaginatedResponse<DocumentRecord>>("/api/documents", params, signal),
  reports: (signal?: AbortSignal) =>
    apiGet<OverviewReport>("/api/reports/overview", undefined, signal),
  settings: (signal?: AbortSignal) =>
    apiGet<SettingsResponse>("/api/settings", undefined, signal),
};
