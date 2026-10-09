import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import CookiesPage, {
  metadata as cookiesMetadata,
} from "@/app/legal/cookies/page";
import DpaPage from "@/app/legal/dpa/page";
import LegalNoticePage from "@/app/legal/legal-notice/page";
import LegalHubPage, { metadata as legalMetadata } from "@/app/legal/page";
import PrivacyPage, {
  metadata as privacyMetadata,
} from "@/app/legal/privacy/page";
import SubprocessorsPage from "@/app/legal/subprocessors/page";
import TermsPage from "@/app/legal/terms/page";
import { SiteFooter } from "@/components/site-footer";

describe("legal pages", () => {
  it("links to every legal route from the hub", () => {
    render(<LegalHubPage />);

    const expectedRoutes = [
      "/legal/privacy",
      "/legal/terms",
      "/legal/cookies",
      "/legal/subprocessors",
      "/legal/dpa",
      "/legal/legal-notice",
    ];

    for (const route of expectedRoutes) {
      expect(document.querySelector(`a[href="${route}"]`)).toBeInTheDocument();
    }
  });

  it.each([
    [PrivacyPage, "Privacy Policy"],
    [TermsPage, "Terms of Service"],
    [CookiesPage, "Cookie & Storage Notice"],
    [SubprocessorsPage, "Subprocessors"],
    [DpaPage, "Data Processing Addendum"],
    [LegalNoticePage, "Legal Notice"],
  ])("renders %s with its primary heading", (Page, heading) => {
    const { unmount } = render(<Page />);

    expect(
      screen.getByRole("heading", { level: 1, name: heading }),
    ).toBeVisible();
    expect(
      screen.getAllByRole("link", { name: "info@invoicetrucker.com" })[0],
    ).toHaveAttribute("href", "mailto:info@invoicetrucker.com");

    unmount();
  });

  it("uses canonical metadata for the legal hub, privacy policy, and cookie notice", () => {
    expect(legalMetadata).toMatchObject({
      alternates: { canonical: "/legal" },
      title: "Legal & Privacy",
    });
    expect(cookiesMetadata).toMatchObject({
      alternates: { canonical: "/legal/cookies" },
      title: "Cookie & Storage Notice",
    });
    expect(privacyMetadata).toMatchObject({
      alternates: { canonical: "/legal/privacy" },
      title: "Privacy Policy",
    });
  });

  it("describes the implemented deletion, export, and retention lifecycle", () => {
    render(<PrivacyPage />);

    expect(screen.getByText(/30-day grace period/i)).toBeVisible();
    expect(
      screen.getByText(/security logs are retained for 90 days/i),
    ).toBeVisible();
    expect(
      screen.getByText(/audit logs are retained for 365 days/i),
    ).toBeVisible();
    expect(screen.getByRole("heading", { name: "Data export" })).toBeVisible();
    expect(screen.getByText(/Supabase-managed backups/i)).toBeVisible();
    expect(screen.getByText(/Stripe has confirmed/i)).toBeVisible();
  });

  it("exposes all requested legal links in the public footer", () => {
    render(<SiteFooter />);

    for (const name of [
      "Privacy",
      "Terms",
      "Cookies",
      "Subprocessors",
      "DPA",
      "Legal",
    ]) {
      expect(screen.getByRole("link", { name })).toBeVisible();
    }
  });
});
