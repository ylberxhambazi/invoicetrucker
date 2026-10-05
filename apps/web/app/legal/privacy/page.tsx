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
        <p>The current public service may process:</p>
        <ul>
          <li>
            Early-access information you submit: full name, email address and,
            if provided, company name and fleet size.
          </li>
          <li>
            Basic application activity and technical information, such as page
            views, browser or device information, IP-derived information, and
            server logs used to operate, secure, and understand the service.
          </li>
          <li>
            Messages and other information you choose to send when requesting
            support.
          </li>
        </ul>
        <p>
          The public product preview contains demonstration business records. It
          does not currently provide accounts, editing, or file uploads and
          should not be used to submit real customer, driver, invoice, or
          document information.
        </p>
        <p>
          Before account features are made available, this policy will need to
          be updated to describe the account, authentication, profile, and
          business information actually processed by that service.
        </p>
      </section>

      <section>
        <h2>Why we use information</h2>
        <p>We use information as needed to:</p>
        <ul>
          <li>provide and operate the service;</li>
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
          <li>Resend for transactional and early-access notification email;</li>
          <li>
            Google and Microsoft as identity providers when their sign-in
            options are available.
          </li>
        </ul>
        <p>
          The current public repository does not contain the Google or Microsoft
          sign-in implementation. Their production status and the exact account
          data exchanged must be confirmed before account launch. See the{" "}
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
          We retain information for as long as reasonably necessary to provide
          the service, maintain legitimate business records, comply with legal
          obligations, resolve disputes, and enforce agreements.
        </p>
        <p>
          The current product does not define fixed retention periods or offer
          automated account deletion. You may request deletion of early-access
          or support information by emailing us. Some information may be
          retained where required or permitted by applicable law.
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
          protect information. No internet transmission or storage system can be
          guaranteed to be completely secure.
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
