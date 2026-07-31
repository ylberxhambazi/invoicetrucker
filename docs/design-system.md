# InvoiceTrucker design system

InvoiceTrucker uses a restrained business-software visual language designed to keep
operational information clear and credible.

## Foundations

- **Background:** `#F8FAFC`
- **Surface:** `#FFFFFF`
- **Primary text:** `#0F172A`
- **Secondary text:** `#475569`
- **Accent:** `#2563EB`
- **Accent hover:** `#1D4ED8`
- **Border:** `#E2E8F0`
- **Success:** `#16A34A`
- **Warning:** `#D97706`
- **Danger:** `#DC2626`
- **Typography:** Geist with a system sans-serif fallback
- **Radii:** 10–16px for interactive elements and surfaces

Tokens are declared in `apps/web/app/globals.css` through CSS custom properties
and Tailwind's theme layer.

## Shared components

The `@invoicetrucker/ui` package owns reusable primitives that are independent of a
specific route:

- `Badge`
- `Button`
- `Container`
- `SectionHeading`

Product-specific compositions remain in `apps/web/components` so shared
primitives do not accumulate landing-page or domain behavior.

## Interaction and accessibility

- All interactive controls expose visible keyboard focus.
- Touch targets are at least 40–44px tall.
- The header supports keyboard dismissal and an accessible mobile menu state.
- Motion is subtle and disabled for users who prefer reduced motion.
- Tables switch to structured cards on narrow screens.
- Page landmarks, navigation labels, ordered steps, and heading hierarchy use
  semantic HTML.
