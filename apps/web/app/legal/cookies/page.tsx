import type { Metadata } from "next";

import { LegalPage } from "../_components/legal-page";

export const metadata: Metadata = {
  alternates: { canonical: "/legal/cookies" },
  description:
    "How InvoiceTrucker currently uses cookies, browser storage, and analytics.",
  title: "Cookie & Storage Notice",
};

export default function CookiesPage() {
  return (
    <LegalPage
      description="This notice distinguishes cookies from other browser and analytics technologies used by the current public service."
      title="Cookie & Storage Notice"
    >
      <section>
        <h2>Current use</h2>
        <p>
          The current InvoiceTrucker public site does not implement account
          authentication, authentication cookies, or application use of
          localStorage or sessionStorage. The public demo is read-only.
        </p>
        <p>
          Vercel Analytics is enabled to provide aggregate information about
          site usage. Its current implementation is designed to operate without
          placing cookies in a visitor&apos;s browser.
        </p>
      </section>

      <section>
        <h2>Technology summary</h2>
        <div className="legal-table-wrap">
          <table>
            <thead>
              <tr>
                <th>Technology</th>
                <th>Purpose</th>
                <th>Type</th>
                <th>Essential?</th>
                <th>Typical duration</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>Vercel Analytics</td>
                <td>Aggregate site traffic and performance measurement</td>
                <td>Cookie-less analytics request</td>
                <td>No</td>
                <td>
                  Request-derived visitor hash: up to 24 hours; no cookie or
                  browser storage
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>

      <section>
        <h2>Authentication and provider storage</h2>
        <p>
          The separate account-enabled production application uses custom
          authentication and may offer Google or Microsoft sign-in. This public
          marketing site does not set those application authentication cookies.
          Google or Microsoft may use their own provider-controlled cookies or
          storage when a user visits their sign-in pages.
        </p>
      </section>

      <section>
        <h2>Advertising and consent</h2>
        <p>
          Repository inspection found no advertising cookies, marketing pixels,
          or browser-storage-based tracking. For that reason, InvoiceTrucker
          does not currently display a cookie consent banner. This should be
          reviewed before adding embedded content, marketing tags, or new
          analytics tools to this public site.
        </p>
        <p>
          InvoiceTrucker does not currently use advertising cookies or marketing
          pixels unless this notice is updated.
        </p>
      </section>

      <section>
        <h2>Questions</h2>
        <p>
          Contact{" "}
          <a href="mailto:info@invoicetrucker.com">info@invoicetrucker.com</a>{" "}
          with questions about cookies or storage.
        </p>
      </section>
    </LegalPage>
  );
}
