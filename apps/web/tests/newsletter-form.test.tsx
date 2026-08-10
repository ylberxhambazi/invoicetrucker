import { fireEvent, render, screen, waitFor } from "@testing-library/react";
import { afterEach, describe, expect, it, vi } from "vitest";

import { NewsletterForm } from "@/components/newsletter-form";

function fillRequiredFields() {
  fireEvent.change(screen.getByLabelText(/full name/i), {
    target: { value: "Avery Demo" },
  });
  fireEvent.change(screen.getByLabelText(/^email/i), {
    target: { value: "avery@invoicetrucker.example" },
  });
}

afterEach(() => {
  vi.unstubAllGlobals();
});

describe("NewsletterForm", () => {
  it("provides a direct early-access contact email", () => {
    render(<NewsletterForm />);

    expect(
      screen.getByRole("link", { name: "ylber.xhambazi@gmail.com" }),
    ).toHaveAttribute(
      "href",
      "mailto:ylber.xhambazi@gmail.com?subject=InvoiceTrucker%20early%20access",
    );
  });

  it("submits optional fields to the real API contract and announces success", async () => {
    const request = vi.fn().mockResolvedValue(
      new Response(
        JSON.stringify({
          email: "avery@invoicetrucker.example",
          fullName: "Avery Demo",
          id: "00000000-0000-0000-0000-000000000001",
          subscribedAtUtc: "2026-07-30T08:00:00Z",
        }),
        { status: 201 },
      ),
    );
    vi.stubGlobal("fetch", request);
    render(<NewsletterForm />);

    fillRequiredFields();
    fireEvent.change(screen.getByLabelText(/company/i), {
      target: { value: "Demo Transport Studio" },
    });
    fireEvent.change(screen.getByLabelText(/fleet size/i), {
      target: { value: "8" },
    });
    fireEvent.click(screen.getByRole("button", { name: /join early access/i }));

    expect(await screen.findByRole("status", { name: "" })).toHaveTextContent(
      /thanks, avery demo/i,
    );
    expect(request).toHaveBeenCalledTimes(1);
    expect(JSON.parse(String(request.mock.calls[0]?.[1]?.body))).toEqual({
      companyName: "Demo Transport Studio",
      email: "avery@invoicetrucker.example",
      fleetSize: 8,
      fullName: "Avery Demo",
    });
  });

  it("surfaces duplicate registration as an accessible error", async () => {
    vi.stubGlobal(
      "fetch",
      vi.fn().mockResolvedValue(
        new Response(
          JSON.stringify({
            detail: "This email address is already on the early-access list.",
            status: 409,
            title: "Email already registered",
          }),
          { status: 409 },
        ),
      ),
    );
    render(<NewsletterForm />);

    fillRequiredFields();
    fireEvent.click(screen.getByRole("button", { name: /join early access/i }));

    expect(await screen.findByRole("alert")).toHaveTextContent(
      /already on the early-access list/i,
    );
  });

  it("explains a rate-limited response", async () => {
    vi.stubGlobal(
      "fetch",
      vi.fn().mockResolvedValue(
        new Response(
          JSON.stringify({
            detail: "Please wait before submitting another request.",
            status: 429,
            title: "Too many requests",
          }),
          { status: 429 },
        ),
      ),
    );
    render(<NewsletterForm />);

    fillRequiredFields();
    fireEvent.click(screen.getByRole("button", { name: /join early access/i }));

    expect(await screen.findByRole("alert")).toHaveTextContent(
      /wait a minute and try again/i,
    );
  });

  it("prevents duplicate submissions and links validation errors to fields", async () => {
    let resolveRequest: ((response: Response) => void) | undefined;
    const request = vi.fn(
      () =>
        new Promise<Response>((resolve) => {
          resolveRequest = resolve;
        }),
    );
    vi.stubGlobal("fetch", request);
    render(<NewsletterForm />);

    fireEvent.click(screen.getByRole("button", { name: /join early access/i }));

    const name = await screen.findByLabelText(/full name/i);
    expect(name).toHaveAttribute("aria-invalid", "true");
    expect(name).toHaveAttribute(
      "aria-describedby",
      "newsletter-full-name-error",
    );

    fillRequiredFields();
    const submit = screen.getByRole("button", { name: /join early access/i });
    fireEvent.click(submit);
    fireEvent.click(submit);

    await waitFor(() => expect(request).toHaveBeenCalledTimes(1));
    expect(submit).toBeDisabled();

    resolveRequest?.(
      new Response(
        JSON.stringify({
          email: "avery@invoicetrucker.example",
          fullName: "Avery Demo",
          id: "00000000-0000-0000-0000-000000000001",
          subscribedAtUtc: "2026-07-30T08:00:00Z",
        }),
        { status: 201 },
      ),
    );
    await screen.findByRole("status");
  });
});
