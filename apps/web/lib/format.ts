export function formatCurrency(amount: number, currency = "EUR") {
  return new Intl.NumberFormat("en-GB", {
    style: "currency",
    currency,
    maximumFractionDigits: 0,
  }).format(amount);
}

export function formatDate(value: string | null) {
  if (!value) return "Not set";

  return new Intl.DateTimeFormat("en-GB", {
    day: "2-digit",
    month: "short",
    year: "numeric",
    timeZone: "UTC",
  }).format(new Date(`${value.slice(0, 10)}T00:00:00Z`));
}

export function formatNumber(value: number) {
  return new Intl.NumberFormat("en-GB").format(value);
}

export function displayEnum(value: string) {
  return value.replace(/([a-z])([A-Z])/g, "$1 $2");
}
