# InvoiceTrucker API

InvoiceTrucker exposes a public, read-only demonstration API except for newsletter
registration. EF Core entities are never returned directly; endpoints project
explicit request and response DTOs.

## Base URL and environments

- Local default: `http://localhost:5050`
- Browser configuration: `NEXT_PUBLIC_API_URL`
- API listener: `ASPNETCORE_URLS` or `ASPNETCORE_HTTP_PORTS`
- PostgreSQL: `ConnectionStrings__FleetForge`

OpenAPI JSON is available at `/openapi/v1.json` only in the Development
environment. Production deployments should expose the API over HTTPS.

## Health

`GET /health`

The health check includes the EF Core PostgreSQL connection.

```json
{
  "status": "Healthy"
}
```

A failed database health check returns a non-success health status.

## Pagination and common query parameters

Every list endpoint accepts:

| Parameter       | Rules                                                      |
| --------------- | ---------------------------------------------------------- |
| `page`          | Positive integer, default `1`                              |
| `pageSize`      | `1–100`, default `20`                                      |
| `search`        | Case-insensitive PostgreSQL search, maximum 120 characters |
| `sortBy`        | Endpoint-specific allow-listed value                       |
| `sortDirection` | `asc` or `desc`, default `asc`                             |

Stable pagination adds the resource ID as a final ordering key.

```json
{
  "items": [],
  "page": 1,
  "pageSize": 20,
  "totalCount": 0,
  "totalPages": 0
}
```

## Endpoint reference

| Method | Route                   | Filters                                              | Sort values                                                                             |
| ------ | ----------------------- | ---------------------------------------------------- | --------------------------------------------------------------------------------------- |
| `GET`  | `/api/dashboard`        | —                                                    | —                                                                                       |
| `GET`  | `/api/trucks`           | `status`, `year`                                     | `name`, `registrationNumber`, `year`, `status`, `currentMileage`, `insuranceExpiration` |
| `GET`  | `/api/trucks/{id}`      | —                                                    | —                                                                                       |
| `GET`  | `/api/drivers`          | `status`                                             | `name`, `licenceExpiration`, `status`, `completedTrips`                                 |
| `GET`  | `/api/drivers/{id}`     | —                                                    | —                                                                                       |
| `GET`  | `/api/clients`          | `isActive`, `country`                                | `name`, `country`, `totalBilled`, `activeInvoices`                                      |
| `GET`  | `/api/clients/{id}`     | —                                                    | —                                                                                       |
| `GET`  | `/api/invoices`         | `status`, `clientId`, `issueDateFrom`, `issueDateTo` | `name`, `invoiceNumber`, `client`, `issueDate`, `dueDate`, `amount`, `status`           |
| `GET`  | `/api/invoices/{id}`    | —                                                    | —                                                                                       |
| `GET`  | `/api/expenses`         | `category`, `truckId`, `dateFrom`, `dateTo`          | `name`, `date`, `category`, `supplier`, `amount`                                        |
| `GET`  | `/api/reports/overview` | —                                                    | —                                                                                       |
| `GET`  | `/api/documents`        | `type`, `expiration`                                 | `name`, `type`, `issuedDate`, `expirationDate`                                          |
| `GET`  | `/api/settings`         | —                                                    | —                                                                                       |
| `POST` | `/api/newsletter`       | —                                                    | —                                                                                       |

Dates use ISO `YYYY-MM-DD`. UUID filters must be valid UUIDs. Enum filters are
case-insensitive. Document `expiration` accepts `expired`, `expiring`, or
`valid`.

### Truck list example

`GET /api/trucks?page=1&pageSize=2&search=volvo&status=Available&sortBy=registrationNumber`

```json
{
  "items": [
    {
      "id": "00000000-0000-0001-0000-000000000001",
      "registrationNumber": "IT-DEMO-001",
      "make": "Volvo",
      "model": "FH 460",
      "year": 2017,
      "status": "Available",
      "assignedDriver": "Elian Voss",
      "currentMileage": 185000,
      "insuranceExpiration": "2026-08-08",
      "technicalInspectionExpiration": "2026-09-05"
    }
  ],
  "page": 1,
  "pageSize": 2,
  "totalCount": 1,
  "totalPages": 1
}
```

### Invoice detail shape

Invoice detail includes nested client and optional truck DTOs, line items,
status, ISO dates, optional UTC payment timestamp, notes, and UTC audit
timestamps. Monetary JSON values originate from .NET `decimal` and PostgreSQL
`numeric` columns.

### Dashboard and report shapes

Dashboard responses include metric totals, invoice status groups, a continuous
12-month revenue/expense/profit series, recent activity, and upcoming document
expirations. The overview report includes the same monthly series plus expense
categories, estimated truck profitability, payment status, and utilization.

## Problem Details

Errors use `application/problem+json` and RFC 7807-compatible fields.

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more query parameters are invalid.",
  "status": 400,
  "errors": {
    "pageSize": ["Page size must be between 1 and 100."]
  }
}
```

Missing detail resources return `404`. Malformed route/query binding, validated
queries, newsletter validation, duplicate registration, and rate limiting all
return Problem Details without database or stack-trace details.

## Newsletter

`POST /api/newsletter`

Request:

```json
{
  "fullName": "Avery Demo",
  "email": "avery@invoicetrucker.example",
  "companyName": "Demo Transport Studio",
  "fleetSize": 8
}
```

`fullName` and `email` are required. `companyName` and `fleetSize` are optional.
Fleet size, when present, must be between `1` and `100000`.

Success: `201 Created`

```json
{
  "id": "1b62ebff-2d3e-4ef1-9907-8d145bffdb5a",
  "fullName": "Avery Demo",
  "email": "avery@invoicetrucker.example",
  "subscribedAtUtc": "2026-07-30T08:00:00Z"
}
```

Other responses:

- `400`: validation failure with an `errors` object
- `409`: normalized email already registered
- `429`: rate limit exceeded

Emails are trimmed and normalized with invariant lowercase. A unique database
index protects normalized email against concurrent duplicate requests.

The rate limit is five requests per remote IP per one-minute fixed window, with
no queue. When available, the response includes `Retry-After`.

## CORS

Allowed origins come from the indexed `Cors:AllowedOrigins` configuration:

```text
Cors__AllowedOrigins__0=https://invoicetrucker.example
Cors__AllowedOrigins__1=https://www.invoicetrucker.example
```

Only explicitly configured origins receive CORS access. Avoid wildcard origins
for hosted deployments.

## Database behavior

- All reads use `AsNoTracking()`.
- PostgreSQL search uses escaped `ILIKE` patterns.
- Queries project DTOs in the database and avoid lazy loading.
- Monetary values use explicit `numeric` precision.
- Application timestamps are UTC and map to `timestamp with time zone`.
- Database constraints supplement request validation.

## Environment notes

Local fictional credentials appear only in example files and Compose. Hosted
connection strings must be supplied through the platform's secret manager.
`Database__ApplyMigrations=true` is intended for the single API instance in
local Compose; managed deployments should use a separate migration release
step.
