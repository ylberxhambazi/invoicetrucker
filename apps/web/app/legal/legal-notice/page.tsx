import type { Metadata } from "next";

import { LegalPage } from "../_components/legal-page";

export const metadata: Metadata = {
  alternates: { canonical: "/legal/legal-notice" },
  description: "Operator and contact details for InvoiceTrucker.",
  title: "Legal Notice",
};

// LEGAL_TODO: For German commercial targeting, add a proper Impressum with a
// serviceable physical address and all legally required operator details before
// relying on this page as the German §5 DDG notice.
export default function LegalNoticePage() {
  return (
    <LegalPage
      description="Current operator and contact information for the InvoiceTrucker product and website."
      title="Legal Notice"
    >
      <section>
        <h2>Service information</h2>
        <dl className="legal-details">
          <div>
            <dt>Product</dt>
            <dd>InvoiceTrucker</dd>
          </div>
          <div>
            <dt>Operator</dt>
            <dd>Independent software project</dd>
          </div>
          <div>
            <dt>Contact</dt>
            <dd>
              <a href="mailto:info@invoicetrucker.com">
                info@invoicetrucker.com
              </a>
            </dd>
          </div>
          <div>
            <dt>Website</dt>
            <dd>
              <a href="https://www.invoicetrucker.com">
                https://www.invoicetrucker.com
              </a>
            </dd>
          </div>
        </dl>
      </section>

      <section>
        <h2>Status of this notice</h2>
        <p>
          InvoiceTrucker is currently operated by the project owner as an
          individual, unincorporated product. A formal legal entity, physical
          service address, registration details, tax details, and final
          jurisdiction have not been provided and are not invented here.
        </p>
        <p>
          This is a general, global legal notice. It is not presented as a
          German Impressum and should not be relied on as one without the
          required operator details and a serviceable physical address.
        </p>
      </section>
    </LegalPage>
  );
}
