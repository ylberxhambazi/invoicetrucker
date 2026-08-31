# Production deployment

InvoiceTrucker's production architecture is Vercel (frontend), Fly.io (API),
and the existing Supabase PostgreSQL database. Deploy database migrations
first, then the API, then the frontend. Never put the database connection
string in Vercel or in a tracked file.

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
ConnectionStrings__FleetForge=<Supabase Npgsql connection string>
Frontend__BaseUrl=https://invoicetrucker.example.com
Cors__AllowedOrigins__0=https://invoicetrucker.example.com
Database__ApplyMigrations=false
EmailNotifications__ResendApiKey=<Resend server-side API key>
EmailNotifications__RecipientAddress=info@invoicetrucker.com
EmailNotifications__FromAddress=InvoiceTrucker Early Access <early-access@invoicetrucker.com>
```

Early-access registrations are committed to PostgreSQL before a notification
is sent. Verify `invoicetrucker.com` in Resend and keep the API key only in Fly
secrets. A notification failure is logged without turning a saved registration
into a failed or duplicate browser submission.

For Supabase's session pooler, the Npgsql value has this exact shape (replace
every angle-bracket placeholder and do not commit the result):

```text
ConnectionStrings__FleetForge=Host=<region>.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.<project-ref>;Password=<database-password>;SSL Mode=Require;Trust Server Certificate=true
```

Use the host, port, and user shown by Supabase's **Connect** dialog. If the
password contains a semicolon, quote its connection-string value according to
Npgsql connection-string rules. `SSL Mode=Require` is mandatory for the hosted
connection. For strict certificate verification, install the Supabase CA and
use `SSL Mode=VerifyFull;Root Certificate=<path>` instead.

## Migrations

Apply migrations once from a trusted environment with
`ConnectionStrings__FleetForge` injected securely:

```bash
dotnet tool restore
dotnet tool run dotnet-ef database update \
  --project apps/api/FleetForge.Api \
  --startup-project apps/api/FleetForge.Api
```

Do not enable `Database__ApplyMigrations` on multiple replicas; concurrent
application startup is not the migration coordinator. Local Compose sets it to
`true` only for its single development API instance.

## Fly.io API

The root `fly.toml` builds the existing API Dockerfile from the monorepo root,
uses Frankfurt (`fra`), listens internally on port `8080`, forces HTTPS, checks
`/health`, and uses an auto-stopping 512 MB shared CPU machine. It requires no
persistent volume.

Authenticate and configure the app without placing values in `fly.toml`:

```bash
flyctl auth login
flyctl apps create invoicetrucker-api
flyctl secrets import < .env.production.fly
flyctl deploy
```

Create `.env.production.fly` with mode `0600`; it is covered by `.gitignore` and
must contain `ConnectionStrings__FleetForge=<value>`. `flyctl secrets import`
reads `NAME=VALUE` pairs from stdin, keeping the database value out of shell
history. Remove the local file after import. `ASPNETCORE_ENVIRONMENT=Production`,
`ASPNETCORE_HTTP_PORTS=8080`, and `Database__ApplyMigrations=false` are
non-secret runtime configuration in `fly.toml`.

After Vercel returns its production URL, set the exact origin and base URL:

```bash
flyctl secrets set Frontend__BaseUrl=https://<production-deployment>.vercel.app
flyctl secrets set Cors__AllowedOrigins__0=https://<production-deployment>.vercel.app
```

Add the custom-domain origins when their DNS is active:

```bash
flyctl secrets set Cors__AllowedOrigins__1=https://invoicetrucker.com
flyctl secrets set Cors__AllowedOrigins__2=https://app.invoicetrucker.com
```

## Vercel frontend

Use the repository root so pnpm workspace packages are visible:

```text
Root Directory: .
Install command: pnpm install --frozen-lockfile
Build command: pnpm build:web
Framework Preset: Next.js
Output Directory: leave blank (Vercel's Next.js default)
pnpm: 10.16.1
```

Only the `@invoicetrucker/web` workspace is built. Set these for the Production
environment before building:

```text
NEXT_PUBLIC_API_URL=https://invoicetrucker-api.fly.dev
NEXT_PUBLIC_SITE_URL=https://<production-deployment>.vercel.app
NEXT_PUBLIC_PRODUCT_URL=https://<product-or-demo-url>
```

After `vercel login`, import the existing Git repository in the Vercel dashboard
with the settings above or run `vercel --prod` from the repository root. Do not
add `ConnectionStrings__FleetForge` or any API secret to Vercel.

## CORS

The API must list each deployed frontend origin exactly. Indexed environment
variables add intentional origins:

```text
Cors__AllowedOrigins__0=https://<production-deployment>.vercel.app
Cors__AllowedOrigins__1=https://invoicetrucker.com
Cors__AllowedOrigins__2=https://app.invoicetrucker.com
```

Do not include trailing slashes and do not use wildcard origins in production.

## Custom domains

Do not run these commands until the domain has been purchased and DNS can be
edited.

For Fly, allocate the API certificate and follow the exact DNS records Fly
prints:

```bash
flyctl certs add api.invoicetrucker.com --app invoicetrucker-api
flyctl certs check api.invoicetrucker.com --app invoicetrucker-api
```

Create the printed A/AAAA records (or indicated CNAME) at the DNS provider,
then repeat `flyctl certs check` until the certificate is ready.

For Vercel, add `invoicetrucker.com` under **Project Settings → Domains** and
apply the DNS records Vercel displays. Add `app.invoicetrucker.com` to the same
project if it should serve the same deployment; the demo remains at `/demo`.
Use a separate Vercel project only if the app subdomain needs an independent
deployment. Equivalent CLI commands after login are:

```bash
vercel domains add invoicetrucker.com <vercel-project-name>
vercel domains add app.invoicetrucker.com <vercel-project-name>
```

Final mapping:

```text
invoicetrucker.com     -> Vercel frontend
app.invoicetrucker.com -> Vercel frontend (/demo)
api.invoicetrucker.com -> Fly.io API
```

## Production smoke checks

1. Request `GET https://invoicetrucker-api.fly.dev/health` and expect `200` with
   `{"status":"Healthy"}`.
2. Request `/api/dashboard`, `/api/trucks`, `/api/invoices`, and
   `/api/reports/overview` and confirm PostgreSQL-backed data is returned.
3. Open `/`, `/demo`, `/demo/trucks`, `/demo/invoices`, `/demo/reports`, and
   `/demo/settings` over HTTPS.
4. Exercise truck filters and open one deterministic invoice detail route.
5. Submit a unique fictional newsletter email, then repeat it and confirm the
   expected safe duplicate response.
6. Confirm API responses have the exact frontend CORS origin and that the
   browser shows no CORS or mixed-content errors.
7. Search built browser assets for database hosts, usernames, passwords, and
   connection-string keys; none should be present.
8. Run `pnpm test:e2e` with `NEXT_PUBLIC_API_URL` and `NEXT_PUBLIC_SITE_URL` set
   to the deployed HTTPS URLs when the production services are ready.

## Rollback notes

Application and database rollback are separate decisions. EF migration `Down`
methods exist, but production data migrations should be reviewed before
reversal. Prefer a forward fix when rollback could destroy or invalidate data.
