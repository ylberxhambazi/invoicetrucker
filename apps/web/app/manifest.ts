import type { MetadataRoute } from "next";

export default function manifest(): MetadataRoute.Manifest {
  return {
    background_color: "#f8fafc",
    description:
      "Invoicing and fleet management for owner-operators and small trucking fleets.",
    display: "standalone",
    icons: [
      {
        sizes: "32x32",
        src: "/icon",
        type: "image/png",
      },
    ],
    name: "InvoiceTrucker",
    short_name: "InvoiceTrucker",
    start_url: "/",
    theme_color: "#0f172a",
  };
}
