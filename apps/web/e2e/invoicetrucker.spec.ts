import { expect, test } from "@playwright/test";

test("landing page presents the buyer journey and product demo", async ({
  page,
}) => {
  await page.goto("/");

  await expect(
    page.getByRole("heading", {
      name: "Stop managing trucking invoices across spreadsheets, emails, and folders.",
    }),
  ).toBeVisible();
  await expect(
    page.getByRole("link", { name: "Explore the Product" }),
  ).toHaveAttribute("href", "/demo");
  await expect(
    page.getByRole("heading", { name: "Built for small trucking fleets." }),
  ).toBeVisible();
});

test("resources hub opens the published trucking invoice guide", async ({
  page,
}) => {
  await page.goto("/resources");

  await expect(
    page.getByRole("heading", {
      name: "Practical resources for running a clearer trucking business.",
    }),
  ).toBeVisible();
  await page
    .getByRole("link", {
      exact: true,
      name: "What Should a Trucking Invoice Include?",
    })
    .click();
  await expect(page).toHaveURL(
    /\/resources\/what-should-a-trucking-invoice-include$/,
  );
  await expect(
    page.getByRole("heading", {
      level: 1,
      name: "What Should a Trucking Invoice Include?",
    }),
  ).toBeVisible();
  await expect(page.getByRole("table").first()).toBeVisible();
});

test("demo dashboard loads PostgreSQL-backed API data", async ({ page }) => {
  await page.goto("/demo");

  await expect(
    page.getByRole("heading", { name: "Operations overview" }),
  ).toBeVisible();
  await expect(page.getByText("Active trucks")).toBeVisible();
  await expect(page.getByText("Financial performance")).toBeVisible();
});

test("truck status filter updates the URL and results", async ({ page }) => {
  await page.goto("/demo/trucks");

  await page.getByLabel("All statuses").selectOption("Available");
  await expect(page).toHaveURL(/status=Available/);
  await expect(page.getByRole("table")).toContainText("Available");
});

test("invoice detail renders a printable fictional invoice", async ({
  page,
}) => {
  await page.goto("/demo/invoices/00000000-0000-0004-0000-000000000001");

  await expect(page.getByRole("heading", { name: "IT-2001" })).toBeVisible();
  await expect(
    page.getByRole("button", { name: "Print preview" }),
  ).toBeVisible();
  await expect(
    page.getByText("This is a fictional portfolio invoice."),
  ).toBeVisible();
});

test("newsletter registration succeeds and then reports a duplicate", async ({
  page,
}, testInfo) => {
  const runId = process.env.GITHUB_RUN_ID ?? process.pid;
  const email = `playwright-${runId}-${testInfo.retry}-${testInfo.workerIndex}@invoicetrucker.example`;
  await page.goto("/");
  await page.getByLabel(/full name/i).fill("Playwright Demo");
  await page.getByLabel(/^email/i).fill(email.toUpperCase());
  await page.getByLabel(/company/i).fill("Automated Demo Company");
  await page.getByLabel(/fleet size/i).fill("4");
  await page.getByRole("button", { name: "Join Early Access" }).click();

  await expect(page.getByRole("status")).toContainText(
    "Thanks, Playwright Demo",
  );

  await page.getByLabel(/full name/i).fill("Playwright Demo");
  await page.getByLabel(/^email/i).fill(email);
  await page.getByRole("button", { name: "Join Early Access" }).click();

  await expect(
    page
      .getByRole("alert")
      .filter({ hasText: "already on the early-access list" }),
  ).toBeVisible();
});

test("mobile navigation opens and reaches the product demo", async ({
  page,
}) => {
  await page.setViewportSize({ height: 844, width: 390 });
  await page.goto("/");

  await page.getByRole("button", { name: "Open navigation menu" }).click();
  const navigation = page.getByRole("navigation", {
    name: "Mobile navigation",
  });
  await expect(navigation).toBeVisible();
  await navigation.getByRole("link", { name: "Explore Product" }).click();
  await expect(page).toHaveURL(/\/demo$/);
});
