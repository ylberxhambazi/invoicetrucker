import type { Metadata } from "next";

import { LegalPage } from "../_components/legal-page";

export const metadata: Metadata = {
  alternates: { canonical: "/legal/subprocessors" },
  description:
    "Third-party providers used to host, operate, and support InvoiceTrucker.",
  title: "Subprocessors",
};

const providers = [
  {
    category: "Site usage and technical request data",
    href: "https://vercel.com/legal/privacy-notice",
    name: "Vercel",
    notes: "Global hosting network; processing location varies",
    purpose: "Frontend hosting and web analytics",
  },
  {
    category: "Application requests, IP addresses, and server logs",
    href: "https://fly.io/legal/privacy-policy/",
    name: "Fly.io",
    notes: "API is configured in Frankfurt; provider operations may be global",
    purpose: "Backend application hosting",
  },
  {
    category: "Early-access records and application database records",
    href: "https://supabase.com/privacy",
    name: "Supabase",
    notes:
      "Hosted PostgreSQL; project region requires owner confirmation; managed backups follow provider rotation",
    purpose: "Managed database infrastructure",
  },
  {
    category: "Billing contact, customer, subscription, and payment metadata",
    href: "https://stripe.com/privacy",
    name: "Stripe",
    notes:
      "Subscription cancellation is confirmed before final tenant deletion; Stripe retains records under its own policies",
    purpose: "Subscription billing and payment processing",
  },
  {
    category: "Recipient details and email content",
    href: "https://resend.com/legal/privacy-policy",
    name: "Resend",
    notes: "Processing location depends on provider infrastructure",
    purpose: "Transactional and early-access notification email",
  },
  {
    category: "Authentication identifiers and profile details",
    href: "https://policies.google.com/privacy",
    name: "Google",
    notes: "Processes data when a user chooses Google sign-in",
    purpose: "Optional account sign-in",
  },
  {
    category: "Authentication identifiers and profile details",
    href: "https://privacy.microsoft.com/en-us/privacystatement",
    name: "Microsoft",
    notes: "Processes data when a user chooses Microsoft sign-in",
    purpose: "Optional account sign-in",
  },
] as const;

export default function SubprocessorsPage() {
  return (
    <LegalPage
      description="These providers help deliver InvoiceTrucker and its account-enabled service."
      title="Subprocessors"
    >
      <section>
        <h2>Provider list</h2>
        <p>
          A provider acts as a subprocessor only to the extent it processes
          personal data on InvoiceTrucker&apos;s behalf. Google and Microsoft
          are included because they may process data when a user chooses the
          corresponding sign-in option.
        </p>
        <div className="legal-table-wrap legal-table-wide">
          <table>
            <thead>
              <tr>
                <th>Provider</th>
                <th>Purpose</th>
                <th>Data category</th>
                <th>Processing region / notes</th>
                <th>Provider policy</th>
              </tr>
            </thead>
            <tbody>
              {providers.map((provider) => (
                <tr key={provider.name}>
                  <td>{provider.name}</td>
                  <td>{provider.purpose}</td>
                  <td>{provider.category}</td>
                  <td>{provider.notes}</td>
                  <td>
                    <a href={provider.href} rel="noreferrer" target="_blank">
                      Privacy information
                    </a>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </section>

      <section>
        <h2>Changes</h2>
        <p>
          This list will be updated when providers or processing arrangements
          change. Inactive vendors are not included. Questions can be sent to{" "}
          <a href="mailto:info@invoicetrucker.com">info@invoicetrucker.com</a>.
        </p>
      </section>
    </LegalPage>
  );
}
