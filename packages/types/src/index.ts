export interface HealthStatus {
  status: "Healthy" | "Degraded" | "Unhealthy";
}

export interface ApiProblem {
  type?: string;
  title: string;
  status: number;
  detail?: string;
  traceId?: string;
  errors?: Record<string, string[]>;
}

export interface PaginatedResponse<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
}

export interface TruckSummary {
  id: string;
  registrationNumber: string;
  make: string;
  model: string;
  year: number;
  status: string;
  assignedDriver: string | null;
  currentMileage: number;
  insuranceExpiration: string;
  technicalInspectionExpiration: string;
}

export interface AssignedDriver {
  id: string;
  fullName: string;
  phone: string;
  status: string;
}

export interface TruckDetail {
  id: string;
  registrationNumber: string;
  make: string;
  model: string;
  year: number;
  status: string;
  currentMileage: number;
  insuranceExpiration: string;
  technicalInspectionExpiration: string;
  assignedDriver: AssignedDriver | null;
  totalExpenses: number;
  documentCount: number;
  createdAtUtc: string;
  updatedAtUtc: string;
}

export interface DriverSummary {
  id: string;
  fullName: string;
  phone: string;
  licenceNumber: string;
  licenceExpiration: string;
  assignedTruck: string | null;
  status: string;
  completedTrips: number;
}

export interface AssignedTruck {
  id: string;
  registrationNumber: string;
  make: string;
  model: string;
  status: string;
}

export interface DriverDetail {
  id: string;
  fullName: string;
  phone: string;
  email: string;
  licenceNumber: string;
  licenceExpiration: string;
  status: string;
  completedTrips: number;
  assignedTruck: AssignedTruck | null;
  documentCount: number;
  createdAtUtc: string;
  updatedAtUtc: string;
}

export interface ClientSummary {
  id: string;
  companyName: string;
  contactPerson: string;
  country: string;
  email: string;
  phone: string;
  activeInvoices: number;
  totalBilled: number;
  isActive: boolean;
}

export interface ClientInvoice {
  id: string;
  invoiceNumber: string;
  issueDate: string;
  dueDate: string;
  amount: number;
  currency: string;
  status: string;
}

export interface ClientDetail {
  id: string;
  companyName: string;
  contactPerson: string;
  country: string;
  email: string;
  phone: string;
  isActive: boolean;
  activeInvoices: number;
  totalBilled: number;
  recentInvoices: ClientInvoice[];
  createdAtUtc: string;
  updatedAtUtc: string;
}

export interface InvoiceSummary {
  id: string;
  invoiceNumber: string;
  clientId: string;
  client: string;
  truck: string | null;
  issueDate: string;
  dueDate: string;
  amount: number;
  currency: string;
  status: string;
}

export interface InvoiceDetail {
  id: string;
  invoiceNumber: string;
  client: {
    id: string;
    companyName: string;
    contactPerson: string;
    email: string;
    country: string;
  };
  truck: {
    id: string;
    registrationNumber: string;
    make: string;
    model: string;
  } | null;
  issueDate: string;
  dueDate: string;
  amount: number;
  currency: string;
  status: string;
  paidAtUtc: string | null;
  notes: string;
  items: {
    id: string;
    description: string;
    quantity: number;
    unitPrice: number;
    lineTotal: number;
  }[];
  createdAtUtc: string;
  updatedAtUtc: string;
}

export interface Expense {
  id: string;
  category: string;
  truckId: string | null;
  truck: string | null;
  date: string;
  supplier: string;
  description: string;
  amount: number;
  currency: string;
}

export interface DocumentRecord {
  id: string;
  type: string;
  name: string;
  referenceNumber: string;
  issuedDate: string;
  expirationDate: string | null;
  expirationStatus: string;
  owner: { type: string; id: string; name: string };
}

export interface MonthlyFinancial {
  year: number;
  month: number;
  label: string;
  revenue: number;
  expenses: number;
  profit: number;
}

export interface DashboardResponse {
  activeTrucks: number;
  availableDrivers: number;
  outstandingInvoices: number;
  monthlyRevenue: number;
  monthlyExpenses: number;
  estimatedProfit: number;
  fleetUtilizationPercentage: number;
  invoiceStatusSummary: { status: string; count: number; amount: number }[];
  financialPerformance: MonthlyFinancial[];
  recentActivity: {
    id: string;
    type: string;
    message: string;
    entityType: string;
    entityId: string | null;
    occurredAtUtc: string;
  }[];
  upcomingDocumentExpirations: {
    id: string;
    type: string;
    name: string;
    owner: string;
    expirationDate: string;
    daysRemaining: number;
  }[];
}

export interface OverviewReport {
  revenueByMonth: MonthlyFinancial[];
  expensesByCategory: { category: string; amount: number }[];
  profitByTruck: {
    truckId: string;
    registrationNumber: string;
    allocatedRevenue: number;
    expenses: number;
    estimatedProfit: number;
  }[];
  invoicePaymentStatus: { status: string; count: number; amount: number }[];
  fleetUtilization: {
    totalTrucks: number;
    utilizedTrucks: number;
    percentage: number;
  };
}

export interface SettingsResponse {
  companyName: string;
  legalName: string;
  email: string;
  phone: string;
  address: string;
  city: string;
  country: string;
  defaultCurrency: string;
  timeZone: string;
  vatNumber: string;
  invoicePrefix: string;
  paymentTermsDays: number;
  updatedAtUtc: string;
}

export interface NewsletterRequest {
  fullName: string;
  email: string;
  companyName?: string;
  fleetSize?: number;
}

export interface NewsletterResponse {
  id: string;
  fullName: string;
  email: string;
  subscribedAtUtc: string;
}
