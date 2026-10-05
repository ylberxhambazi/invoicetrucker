import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import CookiesPage, {
  metadata as cookiesMetadata,
} from "@/app/legal/cookies/page";
import DpaPage from "@/app/legal/dpa/page";
import LegalNoticePage from "@/app/legal/legal-notice/page";
import LegalHubPage, { metadata as legalMetadata } from "@/app/legal/page";
import PrivacyPage from "@/app/legal/privacy/page";
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

  it("uses canonical metadata for the legal hub and cookie notice", () => {
    expect(legalMetadata).toMatchObject({
      alternates: { canonical: "/legal" },
      title: "Legal & Privacy",
    });
    expect(cookiesMetadata).toMatchObject({
      alternates: { canonical: "/legal/cookies" },
      title: "Cookie & Storage Notice",
    });
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
