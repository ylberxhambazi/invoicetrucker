import type { Metadata } from "next";

import { LegalPage } from "../_components/legal-page";

export const metadata: Metadata = {
  alternates: { canonical: "/legal/privacy" },
  description:
    "How InvoiceTrucker processes information and the privacy choices available to users.",
  title: "Privacy Policy",
};

// LEGAL_TODO: Add the formal legal entity and a serviceable physical address once available.
export default function PrivacyPage() {
  return (
    <LegalPage
      description="This policy explains what information InvoiceTrucker processes, why it is used, and the choices available to you."
      title="Privacy Policy"
    >
      <section>
        <h2>Who operates InvoiceTrucker</h2>
        <p>
          InvoiceTrucker is currently operated as an independent software
          project. For privacy questions, contact{" "}
          <a href="mailto:info@invoicetrucker.com">info@invoicetrucker.com</a>.
        </p>
      </section>

      <section>
        <h2>Information we process</h2>
        <p>InvoiceTrucker may process:</p>
        <ul>
          <li>
            Early-access information you submit: full name, email address and,
            if provided, company name and fleet size.
          </li>
          <li>
            Account and company information, including user and tenant profiles,
            authentication details, roles, and account settings.
          </li>
          <li>
            Business records you enter or generate, including clients, trucks,
            drivers, articles, invoice groups, invoices, invoice items,
            payments, and related company, financial, document, AI, country, and
            VAT settings.
          </li>
          <li>
            Subscription and billing metadata received from Stripe, such as
            customer, subscription, plan, status, and billing-event details.
          </li>
          <li>
            Basic application activity and technical information, such as page
            views, browser or device information, IP-derived information, and
            server, security, and audit-event logs used to operate, secure, and
            understand the service.
          </li>
          <li>
            Messages and other information you choose to send when requesting
            support.
          </li>
        </ul>
        <p>
          The public product preview contains demonstration business records. It
          remains separate from the account-enabled production application and
          should not be used to submit real customer, driver, invoice, or
          document information.
        </p>
      </section>

      <section>
        <h2>Why we use information</h2>
        <p>We use information as needed to:</p>
        <ul>
          <li>provide and operate the service;</li>
          <li>authenticate users and administer company accounts;</li>
          <li>process subscriptions and maintain billing records;</li>
          <li>manage early-access requests and contact applicants;</li>
          <li>send service and transactional email;</li>
          <li>maintain security, prevent abuse, and diagnose problems;</li>
          <li>improve reliability and understand service usage; and</li>
          <li>respond to questions and support requests.</li>
        </ul>
      </section>

      <section>
        <h2>Legal bases for EEA and UK users</h2>
        <p>
          For users in the EEA, UK, or other jurisdictions requiring a lawful
          basis, the basis depends on the specific activity. It may include
          performing a contract or taking requested pre-contract steps,
          legitimate interests in operating and securing the service, consent
          where required, or compliance with legal obligations. Not every basis
          applies to every processing activity.
        </p>
        <p>
          Where we rely on legitimate interests, those interests may include
          keeping the service reliable, responding to users, and understanding
          whether the service is useful, balanced against your rights and
          expectations.
        </p>
      </section>

      <section>
        <h2>Service providers</h2>
        <p>InvoiceTrucker uses:</p>
        <ul>
          <li>
            Vercel for frontend hosting and privacy-focused web analytics;
          </li>
          <li>Fly.io for backend hosting;</li>
          <li>Supabase for the hosted PostgreSQL database;</li>
          <li>Stripe for subscription billing and payment processing;</li>
          <li>Resend for transactional and early-access notification email;</li>
          <li>
            Google and Microsoft as identity providers when their sign-in
            options are available.
          </li>
        </ul>
        <p>
          Google and Microsoft process information under their own terms when a
          user chooses the corresponding sign-in option. See the{" "}
          <a href="/legal/subprocessors">Subprocessors page</a> for provider
          links and further detail.
        </p>
      </section>

      <section>
        <h2>International transfers</h2>
        <p>
          InvoiceTrucker and its service providers may process information in
          different countries. Where required, providers may use recognized
          transfer mechanisms or other safeguards for international transfers.
          The available mechanism can depend on the provider, service, and
          destination.
        </p>
      </section>

      <section>
        <h2>Retention and deletion</h2>
        <p>
          Operational and business data is retained while an account is active.
          Security logs are retained for 90 days, and audit logs are retained
          for 365 days. Expired refresh tokens, password-reset tokens, and OAuth
          login codes are cleaned up automatically.
        </p>
        <p>
          Only the company owner may request account deletion, and recent
          authentication is required. Once deletion is requested, normal
          application access is restricted immediately, active sessions and
          refresh tokens are revoked, and existing access tokens are rejected
          through session-version checks. A 30-day grace period then applies.
          During that period, the owner may export company data or cancel the
          deletion request.
        </p>
        <p>
          If the account has an active Stripe subscription, it is scheduled to
          cancel at the end of its current billing period rather than cancelled
          immediately. If Stripe is temporarily unavailable, account access
          remains restricted and cancellation is retried through the durable
          billing process. Final deletion does not occur until the grace period
          has ended and Stripe has confirmed the required subscription
          cancellation.
        </p>
        <p>
          If deletion is cancelled while the subscription is still active and it
          was scheduled for cancellation only because of the deletion flow,
          billing may be restored. If the subscription has already ended, the
          owner may need to select a paid plan again.
        </p>
        <p>
          Final deletion removes tenant data and local Stripe metadata. Stripe
          may retain its own records under its policies and legal obligations,
          and a non-identifying tenant tombstone may remain in InvoiceTrucker.
          Deleted data may also remain temporarily in Supabase-managed backups
          until the provider&apos;s normal backup rotation expires;
          InvoiceTrucker cannot directly delete individual records from those
          backups.
        </p>
        <p>
          Customers are responsible for exporting and retaining any business,
          invoice, accounting, or tax records they are legally required to keep
          before deleting their account. Other information may be retained where
          required or permitted by applicable law.
        </p>
      </section>

      <section>
        <h2>Data export</h2>
        <p>
          During the deletion grace period, the company owner may download a
          machine-readable ZIP export containing the tenant and company profile,
          users, clients, trucks and drivers, articles and invoice groups,
          invoices and invoice items, payments, company, financial,
          invoice-document, AI, country, and VAT settings, subscription
          metadata, and audit-event metadata.
        </p>
        <p>
          The export excludes password hashes, refresh tokens, password-reset
          token hashes, OAuth login-code hashes, OAuth provider identifiers,
          encrypted AI credentials, raw Stripe webhook payloads, internal
          security records, and embedded or generated document binaries where
          they are not applicable.
        </p>
      </section>

      <section>
        <h2>Your rights</h2>
        <p>
          Depending on applicable law, you may have rights to access, correct,
          delete, restrict, or object to processing of your information, and to
          receive portable information where applicable. Where processing relies
          on consent, you may withdraw that consent without affecting earlier
          lawful processing.
        </p>
        <p>
          Users in some US states may have additional privacy rights. This
          statement does not assume that any particular statutory threshold
          applies. To make a request, email us. We may need to verify your
          identity before completing it.
        </p>
      </section>

      <section>
        <h2>Children</h2>
        <p>
          InvoiceTrucker is intended for adults and is not intended for anyone
          under 18. Please contact us if you believe a child has submitted
          personal information.
        </p>
      </section>

      <section>
        <h2>Security</h2>
        <p>
          We use reasonable technical and organizational safeguards designed to
          protect information. These include revoking active sessions when
          deletion is requested and automatically cleaning up expired
          authentication tokens and login codes. No internet transmission or
          storage system can be guaranteed to be completely secure.
        </p>
      </section>

      <section>
        <h2>Changes and contact</h2>
        <p>
          We may update this policy as the service changes. The date above
          identifies the latest version. Questions and privacy requests can be
          sent to{" "}
          <a href="mailto:info@invoicetrucker.com">info@invoicetrucker.com</a>.
        </p>
      </section>
    </LegalPage>
  );
}
