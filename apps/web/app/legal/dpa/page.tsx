import type { Metadata } from "next";

import { LegalPage } from "../_components/legal-page";

export const metadata: Metadata = {
  alternates: { canonical: "/legal/dpa" },
  description:
    "InvoiceTrucker's data processing framework for business customers.",
  title: "Data Processing Addendum",
};

// LEGAL_TODO: Add the final legal entity, address, and jurisdiction before signing DPAs with customers.
export default function DpaPage() {
  return (
    <LegalPage
      description="This DPA is intended for business customers where InvoiceTrucker processes personal data on their behalf."
      title="Data Processing Addendum"
    >
      <section>
        <h2>1. Scope and roles</h2>
        <p>
          This Data Processing Addendum applies when a business customer uses
          InvoiceTrucker to process personal data for which that customer is a
          controller and InvoiceTrucker acts as its processor. It supplements
          the applicable service agreement. Each party remains responsible for
          its own obligations under applicable data-protection law.
        </p>
        <p>
          The customer determines the purpose and lawful basis for customer data
          and must provide any required notices. InvoiceTrucker processes that
          data to provide, secure, maintain, and support the service.
        </p>
      </section>

      <section>
        <h2>2. Instructions and processing details</h2>
        <p>
          The customer instructs InvoiceTrucker to process personal data as
          reasonably necessary to provide the service and as further directed
          through documented, lawful instructions. If an instruction appears to
          violate applicable data-protection law, InvoiceTrucker may notify the
          customer and pause the affected processing where permitted.
        </p>
        <p>
          Expected data may include business contact, customer, driver, vehicle,
          invoice, expense, and document-reference information. The actual
          account-enabled product and any file-upload processing must be
          confirmed before this DPA is signed.
        </p>
      </section>

      <section>
        <h2>3. Confidentiality and security</h2>
        <p>
          People authorized to process customer personal data will be subject to
          appropriate confidentiality obligations. InvoiceTrucker will use
          reasonable technical and organizational safeguards designed to protect
          personal data, taking account of the nature of processing and the
          risks involved.
        </p>
      </section>

      <section>
        <h2>4. Subprocessors</h2>
        <p>
          The customer authorizes use of the providers listed on the{" "}
          <a href="/legal/subprocessors">Subprocessors page</a>. InvoiceTrucker
          will require subprocessors to protect customer personal data through
          appropriate contractual terms. Material changes to the provider list
          will be communicated in a reasonable way before they take effect where
          required.
        </p>
      </section>

      <section>
        <h2>5. Assistance</h2>
        <p>
          Taking account of the nature of processing and information available,
          InvoiceTrucker will provide reasonable assistance with data-subject
          requests, data-protection impact assessments, regulator consultations,
          and other compliance duties that relate to the service. The customer
          remains responsible for responding to requests as controller.
        </p>
      </section>

      <section>
        <h2>6. Security incidents</h2>
        <p>
          InvoiceTrucker will notify the customer without undue delay after
          becoming aware of a confirmed personal-data breach affecting customer
          data and will provide information reasonably available to support the
          customer&apos;s response. Notice is not an admission of fault or
          liability.
        </p>
      </section>

      <section>
        <h2>7. Return and deletion</h2>
        <p>
          Following termination and on the customer&apos;s request,
          InvoiceTrucker will return or delete customer personal data within a
          reasonable period, unless retention is required by law or maintained
          temporarily in protected backups. Product-level export and deletion
          workflows, timeframes, and backup behavior must be finalized before
          paid launch.
        </p>
      </section>

      <section>
        <h2>8. International transfers</h2>
        <p>
          Where customer personal data is transferred across borders, the
          parties and relevant subprocessors will use a legally recognized
          transfer mechanism when required. The applicable mechanism and any
          supplementary measures depend on the locations and services involved.
        </p>
      </section>

      <section>
        <h2>9. Audit and cooperation</h2>
        <p>
          On reasonable request, InvoiceTrucker will provide information needed
          to demonstrate compliance with this DPA. If that information is not
          sufficient, the parties may agree to a proportionate audit that
          protects security, confidentiality, other customers, and service
          availability. The customer bears reasonable audit costs unless the
          audit identifies a material breach by InvoiceTrucker.
        </p>
      </section>

      <section>
        <h2>10. Contact and effectiveness</h2>
        <p>
          Contact{" "}
          <a href="mailto:info@invoicetrucker.com">info@invoicetrucker.com</a>{" "}
          to discuss or execute a DPA. This online framework is not a signed
          agreement by itself; party details, jurisdiction, processing scope,
          and signature terms must be completed for an executable DPA.
        </p>
      </section>
    </LegalPage>
  );
}
