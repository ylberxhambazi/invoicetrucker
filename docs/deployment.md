# Deployment

InvoiceTrucker is prepared for deployment but does not assume a single hosting
provider. Deploy the database first, then the API, then the frontend.

## Recommended options

Frontend:

- Vercel for native Next.js hosting
- Any Docker-compatible platform using `apps/web/Dockerfile`

Backend:

- Fly.io
- Railway
- Render
- Azure App Service
- Any Docker-compatible platform using `apps/api/FleetForge.Api/Dockerfile`

Database:

- Neon PostgreSQL
- Supabase PostgreSQL
- Railway PostgreSQL
- Another managed PostgreSQL service compatible with Npgsql

## Required configuration

Frontend build variables:

```text
NEXT_PUBLIC_API_URL=https://api.example.com
NEXT_PUBLIC_SITE_URL=https://invoicetrucker.example.com
NEXT_PUBLIC_PRODUCT_URL=https://optional-separate-product.example
```

`NEXT_PUBLIC_PRODUCT_URL` is optional. The CTA is hidden when it is empty.
Public variables are embedded during the frontend build and are not secrets.

API runtime variables:

```text
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_HTTP_PORTS=8080
ConnectionStrings__FleetForge=<managed PostgreSQL connection string>
Cors__AllowedOrigins__0=https://invoicetrucker.example.com
Database__ApplyMigrations=false
```

Use the hosting platform's secret manager for the connection string. Managed
PostgreSQL commonly requires TLS; follow the provider's Npgsql connection-string
instructions and certificate policy.

## Migration strategy

For hosted environments, apply migrations once as a release command before
starting the new API version:

```bash
dotnet tool restore
dotnet tool run dotnet-ef database update \
  --project apps/api/FleetForge.Api \
  --startup-project apps/api/FleetForge.Api
```

Run this command from a trusted build/release environment with the production
connection string injected securely. Do not enable
`Database__ApplyMigrations` on multiple replicas; concurrent application
startup is not the preferred migration coordinator.

Local Compose intentionally sets `Database__ApplyMigrations=true` for a
convenient single API instance.

## Vercel frontend

Use the repository root so pnpm workspace packages are visible.

```text
Install command: pnpm install --frozen-lockfile
Build command: pnpm build:web
Framework: Next.js
```

Set all `NEXT_PUBLIC_*` variables before building. Rebuild when their values
change.

## Docker-compatible hosting

Build from the repository root:

```bash
docker build -f apps/api/FleetForge.Api/Dockerfile -t invoicetrucker-api .
docker build \
  -f apps/web/Dockerfile \
  --build-arg NEXT_PUBLIC_API_URL=https://api.example.com \
  --build-arg NEXT_PUBLIC_SITE_URL=https://invoicetrucker.example.com \
  -t invoicetrucker-web .
```

The API listens on container port `8080`; the frontend listens on `3000`.
Configure platform health checks against `/health` for the API and `/` for the
frontend.

## CORS

The API must list the exact deployed frontend origin. Add indexed variables for
multiple intentional origins:

```text
Cors__AllowedOrigins__0=https://invoicetrucker.example.com
Cors__AllowedOrigins__1=https://www.invoicetrucker.example.com
```

Do not include a trailing slash and do not use wildcard origins for this public
API.

## Post-deployment smoke checks

1. Confirm database migrations completed.
2. Request `GET https://api.example.com/health` and expect `200`.
3. Request `/api/dashboard` and one paginated list.
4. Open the landing page and verify canonical metadata.
5. Open `/demo` and confirm metrics load without a fallback.
6. Filter `/demo/trucks`.
7. Open a deterministic invoice detail route.
8. Submit a unique fictional newsletter email and expect success.
9. Repeat the email and expect a safe duplicate message.
10. Confirm the external product CTA appears only when configured.
11. Confirm API responses include CORS headers for the frontend origin.
12. Review logs for structured requests without submitted email addresses.

## Rollback notes

Application rollback and database rollback are separate decisions. EF migration
`Down` methods exist, but production data migrations should be reviewed before
reversal. Prefer a forward-fix when a rollback could destroy or invalidate
data.
