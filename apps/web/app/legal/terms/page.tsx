import type { Metadata } from "next";

import { LegalPage } from "../_components/legal-page";

export const metadata: Metadata = {
  alternates: { canonical: "/legal/terms" },
  description: "Terms governing access to and use of InvoiceTrucker.",
  title: "Terms of Service",
};

// LEGAL_TODO: Governing law and operator jurisdiction must be finalized before paid public launch.
export default function TermsPage() {
  return (
    <LegalPage
      description="These terms set out the practical rules for using InvoiceTrucker."
      title="Terms of Service"
    >
      <section>
        <h2>1. Acceptance</h2>
        <p>
          By accessing or using InvoiceTrucker, you agree to these terms. If you
          use the service for a business, you confirm that you have authority to
          accept them for that business. If you do not agree, do not use the
          service.
        </p>
      </section>

      <section>
        <h2>2. Eligibility and intended use</h2>
        <p>
          You must be at least 18 and legally able to enter an agreement.
          InvoiceTrucker is designed primarily for owner-operators, trucking
          businesses, small fleets, and other professional users, although
          individuals may access the public site.
        </p>
      </section>

      <section>
        <h2>3. Accounts</h2>
        <p>
          Account access is not currently offered in this public product
          preview. When accounts become available, you will be responsible for
          accurate registration information, protecting your credentials, and
          activity under your account. Account terms will be updated before
          launch.
        </p>
      </section>

      <section>
        <h2>4. Your responsibilities and data</h2>
        <p>
          You are responsible for information you submit, including having the
          rights and permissions needed to use customer, driver, employee, and
          business data. You remain responsible for the accuracy of business
          records and for meeting tax, transport, employment, retention, and
          other obligations that apply to you.
        </p>
        <p>
          Do not enter real or confidential business information into the public
          demo. Demo content is illustrative and is not professional, legal,
          accounting, or tax advice.
        </p>
      </section>

      <section>
        <h2>5. Acceptable use</h2>
        <p>You must not use InvoiceTrucker to:</p>
        <ul>
          <li>break the law or infringe another person&apos;s rights;</li>
          <li>submit malicious code or attempt unauthorized access;</li>
          <li>interfere with or overload the service;</li>
          <li>misrepresent identity, records, invoices, or transactions; or</li>
          <li>scrape, resell, or exploit the service without permission.</li>
        </ul>
      </section>

      <section>
        <h2>6. Availability and third-party services</h2>
        <p>
          We may change, suspend, or discontinue features. The service may
          occasionally be unavailable because of maintenance, incidents, or
          events outside our control. InvoiceTrucker depends on third-party
          hosting, database, email, analytics, and identity services, whose own
          terms and availability may apply.
        </p>
      </section>

      <section>
        <h2>7. Intellectual property</h2>
        <p>
          InvoiceTrucker&apos;s software, brand, and site content are protected
          by applicable intellectual-property laws. These terms give you a
          limited, non-exclusive right to use the service as intended. You keep
          ownership of content you lawfully submit and grant us the limited
          rights needed to host, process, and provide it.
        </p>
      </section>

      <section>
        <h2>8. Fees and subscriptions</h2>
        <p>
          The public preview is currently offered without a paid subscription.
          If paid plans become available, applicable fees, billing intervals,
          renewal terms, and taxes will be displayed before purchase. We will
          not invent or apply a price that was not presented to you.
        </p>
      </section>

      <section>
        <h2>9. Beta and early-access features</h2>
        <p>
          Preview, beta, and early-access features may be incomplete, change
          without notice, or contain errors. They should not be relied on for
          critical records or regulatory compliance unless expressly stated.
        </p>
      </section>

      <section>
        <h2>10. Termination</h2>
        <p>
          You may stop using the service at any time. We may restrict or end
          access where reasonably necessary for security, unlawful or abusive
          use, non-payment under future paid plans, or material breach of these
          terms. Data handling after termination is described in the Privacy
          Policy and, where applicable, a signed DPA.
        </p>
      </section>

      <section>
        <h2>11. Disclaimers</h2>
        <p>
          To the extent permitted by law, the service is provided “as is” and
          “as available.” We do not promise uninterrupted operation, that every
          error will be corrected, or that the service will satisfy every
          business or legal requirement. Nothing in these terms excludes rights
          or warranties that cannot lawfully be excluded.
        </p>
      </section>

      <section>
        <h2>12. Limitation of liability</h2>
        <p>
          To the extent permitted by law, InvoiceTrucker&apos;s operator will
          not be liable for indirect, incidental, special, consequential, or
          punitive damages, or for lost profits, revenue, data, or business
          opportunity. Any further liability cap must be set before paid plans
          launch. These limits do not apply where liability cannot legally be
          limited.
        </p>
      </section>

      <section>
        <h2>13. Indemnity</h2>
        <p>
          Where permitted by law and reasonable in the circumstances, business
          users agree to protect the operator from third-party claims caused by
          their unlawful use of the service, their submitted data, or their
          material breach of these terms. This does not apply to the extent a
          claim was caused by the operator.
        </p>
      </section>

      <section>
        <h2>14. Changes</h2>
        <p>
          We may update these terms as the service develops. Material changes
          will be communicated in a reasonable way, and the date above will be
          updated. Continued use after a change takes effect means you accept
          the revised terms where applicable law allows.
        </p>
      </section>

      <section>
        <h2>15. Governing law</h2>
        <p>
          The governing law and operator jurisdiction have not yet been
          finalized. They must be specified before a paid public launch. Any
          mandatory rights available under the law where you live remain
          unaffected.
        </p>
      </section>

      <section>
        <h2>16. Contact</h2>
        <p>
          Questions about these terms can be sent to{" "}
          <a href="mailto:info@invoicetrucker.com">info@invoicetrucker.com</a>.
        </p>
      </section>
    </LegalPage>
  );
}
