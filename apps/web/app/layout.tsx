import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";

import "./globals.css";
import { Analytics } from "@vercel/analytics/next";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  applicationName: "InvoiceTrucker",
  alternates: {
    canonical: "/",
    types: {
      "application/rss+xml": "/rss.xml",
    },
  },
  category: "business",
  metadataBase: new URL(
    process.env.NEXT_PUBLIC_SITE_URL ?? "http://localhost:3000",
  ),
  title: {
    default:
      "InvoiceTrucker | Invoicing & Fleet Management for Small Trucking Fleets",
    template: "%s | InvoiceTrucker",
  },
  description:
    "Manage trucking invoices, customers, fleet information, paperwork and payments in one place. Built for small fleets ready to move beyond spreadsheets.",
  keywords: [
    "fleet management",
    "trucking invoicing",
    "small trucking fleet",
    "invoice management",
  ],
  openGraph: {
    description:
      "Manage trucking invoices, customers, fleet information, paperwork and payments in one place. Built for small fleets ready to move beyond spreadsheets.",
    images: [
      {
        alt: "InvoiceTrucker fleet management dashboard",
        height: 907,
        url: "/og-buyer.png",
        width: 1734,
      },
    ],
    siteName: "InvoiceTrucker",
    title:
      "InvoiceTrucker | Invoicing & Fleet Management for Small Trucking Fleets",
    type: "website",
  },
  robots: {
    follow: true,
    index: true,
  },
  twitter: {
    card: "summary_large_image",
    description:
      "Manage trucking invoices, customers, fleet information, paperwork and payments in one place.",
    images: ["/og-buyer.png"],
    title:
      "InvoiceTrucker | Invoicing & Fleet Management for Small Trucking Fleets",
  },
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body
        className={`${geistSans.variable} ${geistMono.variable} font-sans antialiased`}
      >
        {children}
        <Analytics />
      </body>
    </html>
  );
}
