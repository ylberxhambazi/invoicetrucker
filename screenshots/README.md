# Screenshot guide

Only reviewed, fictional InvoiceTrucker screens belong here.

## Required captures

| File                           | Viewport  | Content                    |
| ------------------------------ | --------- | -------------------------- |
| `desktop/landing-hero.png`     | 1440×1000 | Hero and dashboard preview |
| `desktop/landing-features.png` | 1440×1000 | Feature section            |
| `desktop/demo-dashboard.png`   | 1440×1000 | Loaded API dashboard       |
| `desktop/trucks-list.png`      | 1440×1000 | Truck search/filter/table  |
| `desktop/invoice-detail.png`   | 1440×1000 | Fictional invoice detail   |
| `desktop/reports.png`          | 1440×1000 | Reporting charts           |
| `mobile/navigation.png`        | 390×844   | Open mobile navigation     |
| `mobile/dashboard.png`         | 390×844   | Mobile demo dashboard      |

## Capture checklist

1. Start PostgreSQL, apply migrations, and run the API and frontend.
2. Use only the deterministic seed records.
3. Hide browser chrome when practical and crop to the page viewport.
4. Confirm no localhost URL, extensions, developer tools, notifications,
   credentials, or unrelated applications are visible.
5. Check every image for personal or production-product data.
6. Remove metadata and optimize PNG files with a lossless optimizer or
   `pngquant --quality=75-90`.
7. Prefer files below 500 KB unless legibility requires more.

The Playwright suite verifies the same routes but stores screenshots only on
failure. Public screenshots should be captured deliberately after visual review
rather than updated automatically on every test run.
