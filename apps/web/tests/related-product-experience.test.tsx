import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import { RelatedProductExperience } from "@/components/related-product-experience";

describe("RelatedProductExperience", () => {
  it("separates the fictional showcase from the private production product", () => {
    render(<RelatedProductExperience />);

    expect(
      screen.getByRole("heading", {
        name: /production experience beyond this public showcase/i,
      }),
    ).toBeInTheDocument();
    expect(
      screen.getByText(
        /invoicetrucker is a fictional open-source portfolio project/i,
      ),
    ).toBeInTheDocument();
    expect(
      screen.getByText(/source code is not part of this repository/i),
    ).toBeInTheDocument();
    expect(
      screen.queryByRole("link", { name: /view full product/i }),
    ).not.toBeInTheDocument();
  });

  it("uses the configured product URL with safe external-link attributes", () => {
    render(
      <RelatedProductExperience productUrl="https://product.example/demo" />,
    );

    expect(
      screen.getByRole("link", { name: /view full product/i }),
    ).toHaveAttribute("href", "https://product.example/demo");
    expect(
      screen.getByRole("link", { name: /view full product/i }),
    ).toHaveAttribute("target", "_blank");
    expect(
      screen.getByRole("link", { name: /view full product/i }),
    ).toHaveAttribute("rel", "noopener noreferrer");
  });
});
