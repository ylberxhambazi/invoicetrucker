# Contributing

Thanks for improving InvoiceTrucker.

## Before opening a pull request

1. Open an issue for substantial changes.
2. Keep the project fictional and read-only.
3. Do not submit real customer, employer, vehicle, invoice, credential, or
   production-product data.
4. Do not copy source, data, configuration, or branding from any private project.
5. Add or update focused tests.
6. Run:

```bash
pnpm install --frozen-lockfile
pnpm lint
pnpm typecheck
pnpm test
pnpm build
dotnet format apps/api/FleetForge.slnx --no-restore --verify-no-changes
```

Use clear commit messages and explain behavior, tests, migration impact, and
manual checks in the pull request. Never commit `.env` files or screenshots
containing personal or confidential information.
