import type { ApiProblem } from "@invoicetrucker/types";

export const apiBaseUrl =
  process.env.NEXT_PUBLIC_API_URL ?? "http://localhost:5050";

export class ApiError extends Error {
  constructor(
    public readonly status: number,
    public readonly problem: ApiProblem,
  ) {
    super(problem.detail ?? problem.title);
    this.name = "ApiError";
  }
}

export async function apiGet<T>(
  path: string,
  params?: Record<string, string | number | boolean | undefined>,
  signal?: AbortSignal,
): Promise<T> {
  const url = new URL(path, apiBaseUrl);

  Object.entries(params ?? {}).forEach(([key, value]) => {
    if (value !== undefined && value !== "") {
      url.searchParams.set(key, String(value));
    }
  });

  const response = await fetch(url, {
    headers: { Accept: "application/json" },
    signal,
  });

  if (!response.ok) {
    const fallback: ApiProblem = {
      status: response.status,
      title: "Unable to load demo data",
    };
    const problem = await response.json().catch(() => fallback);
    throw new ApiError(response.status, problem as ApiProblem);
  }

  return (await response.json()) as T;
}

export async function apiPost<TResponse, TRequest>(
  path: string,
  body: TRequest,
  signal?: AbortSignal,
): Promise<TResponse> {
  const response = await fetch(new URL(path, apiBaseUrl), {
    body: JSON.stringify(body),
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    method: "POST",
    signal,
  });

  if (!response.ok) {
    const fallback: ApiProblem = {
      status: response.status,
      title: "Unable to submit the request",
    };
    const problem = await response.json().catch(() => fallback);
    throw new ApiError(response.status, problem as ApiProblem);
  }

  return (await response.json()) as TResponse;
}
