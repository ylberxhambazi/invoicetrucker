import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import HomePage from "../app/page";

describe("HomePage", () => {
  it("renders the product proposition and major landing sections", () => {
    render(<HomePage />);

    expect(
      screen.getByRole("heading", {
        name: /stop managing trucking invoices across spreadsheets/i,
      }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("heading", {
        name: /from completed load to paid invoice/i,
      }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("heading", {
        name: /see the business without digging through spreadsheets/i,
      }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("heading", {
        name: /your trucks shouldn't be easier to track than your money/i,
      }),
    ).toBeInTheDocument();
  });

  it("exposes the primary landing page actions", () => {
    render(<HomePage />);

    expect(
      screen.getAllByRole("link", { name: /get early access/i })[0],
    ).toHaveAttribute("href", "#early-access");
    expect(
      screen.getAllByRole("link", { name: /see how it works/i })[0],
    ).toHaveAttribute("href", "#how-it-works");
    expect(
      screen.getByRole("link", { name: /explore the product/i }),
    ).toHaveAttribute("href", "/demo");
  });

  it("keeps developer messaging out of the primary buyer journey", () => {
    render(<HomePage />);

    expect(
      screen.queryByRole("heading", {
        name: /technology|engineering showcase/i,
      }),
    ).not.toBeInTheDocument();
    expect(
      screen.getByRole("link", { name: /invoicetrucker on github/i }),
    ).toHaveAttribute(
      "href",
      "https://github.com/ylberxhambazi/InvoiceTrucker",
    );
  });
});
