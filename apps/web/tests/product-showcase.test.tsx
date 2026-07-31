import { fireEvent, render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import { ProductShowcase } from "../components/product-showcase";

describe("ProductShowcase", () => {
  it("switches between product data views", () => {
    render(<ProductShowcase />);

    fireEvent.click(screen.getByRole("tab", { name: "Invoices" }));

    expect(
      screen.getByRole("tabpanel", { name: "Invoices" }),
    ).toBeInTheDocument();
    expect(screen.getAllByText("IT-2048").length).toBeGreaterThan(0);
  });
});
