import { fireEvent, render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";

import { SiteHeader } from "../components/site-header";

describe("SiteHeader", () => {
  it("opens and closes the responsive navigation", () => {
    render(<SiteHeader />);

    const openButton = screen.getByRole("button", {
      name: /open navigation menu/i,
    });

    fireEvent.click(openButton);

    expect(
      screen.getByRole("navigation", { name: /mobile navigation/i }),
    ).toBeInTheDocument();
    expect(
      screen.getByRole("button", { name: /close navigation menu/i }),
    ).toHaveAttribute("aria-expanded", "true");

    fireEvent.keyDown(window, { key: "Escape" });

    expect(
      screen.queryByRole("navigation", { name: /mobile navigation/i }),
    ).not.toBeInTheDocument();
  });
});
