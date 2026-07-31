import { defineConfig, devices } from "@playwright/test";

const apiUrl = process.env.NEXT_PUBLIC_API_URL ?? "http://127.0.0.1:5050";
const webUrl = process.env.NEXT_PUBLIC_SITE_URL ?? "http://127.0.0.1:3000";

export default defineConfig({
  expect: {
    timeout: 10_000,
  },
  forbidOnly: Boolean(process.env.CI),
  fullyParallel: false,
  projects: [
    {
      name: "chromium",
      use: { ...devices["Desktop Chrome"] },
    },
  ],
  reporter: process.env.CI ? "github" : "list",
  retries: process.env.CI ? 2 : 0,
  testDir: "./e2e",
  timeout: 30_000,
  use: {
    baseURL: webUrl,
    screenshot: "only-on-failure",
    trace: "on-first-retry",
  },
  webServer: [
    {
      command:
        "dotnet run --project ../api/FleetForge.Api --no-build --no-launch-profile",
      env: {
        ...process.env,
        ASPNETCORE_URLS: apiUrl,
      },
      reuseExistingServer: !process.env.CI,
      timeout: 120_000,
      url: `${apiUrl}/health`,
    },
    {
      command: "pnpm dev",
      env: {
        ...process.env,
        NEXT_PUBLIC_API_URL: apiUrl,
        NEXT_PUBLIC_PRODUCT_URL:
          process.env.NEXT_PUBLIC_PRODUCT_URL ?? "https://product.example",
        NEXT_PUBLIC_SITE_URL: webUrl,
      },
      reuseExistingServer: !process.env.CI,
      timeout: 120_000,
      url: webUrl,
    },
  ],
  workers: 1,
});
