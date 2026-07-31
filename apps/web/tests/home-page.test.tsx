import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import HomePage from "../app/page";

describe("HomePage", () => {
  it("renders the product proposition and major landing sections", () => {
    render(<HomePage />);

    expect(
      screen.getByRole("heading", {
        name: /run your transport business from one place/i,
      }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("heading", {
        name: /everything needed to keep the business moving/i,
      }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("heading", {
        name: /modern technology, chosen for maintainability/i,
      }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("heading", {
        name: /production experience beyond this public showcase/i,
      }),
    ).toBeInTheDocument();
  });

  it("exposes the primary landing page actions", () => {
    render(<HomePage />);

    expect(
      screen.getAllByRole("link", { name: /view live demo/i })[0],
    ).toHaveAttribute("href", "/demo");
    expect(
      screen.getAllByRole("link", { name: /explore on github/i })[0],
    ).toHaveAttribute(
      "href",
      "https://github.com/ylberxhambazi/InvoiceTrucker",
    );
  });
});
