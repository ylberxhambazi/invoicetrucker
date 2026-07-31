import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";

import "./globals.css";

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
  },
  category: "technology",
  metadataBase: new URL(
    process.env.NEXT_PUBLIC_SITE_URL ?? "http://localhost:3000",
  ),
  title: {
    default: "InvoiceTrucker — Fleet management, made clear",
    template: "%s | InvoiceTrucker",
  },
  description:
    "A fictional open-source fleet operations portfolio project with a read-only Next.js demo, ASP.NET Core API, and PostgreSQL data.",
  keywords: [
    "fleet management",
    "Next.js portfolio",
    "ASP.NET Core",
    "PostgreSQL",
    "open source",
  ],
  openGraph: {
    description:
      "A fictional open-source fleet operations portfolio demonstration built with Next.js, ASP.NET Core, and PostgreSQL.",
    images: [
      {
        alt: "InvoiceTrucker fleet management dashboard",
        height: 907,
        url: "/og.png",
        width: 1734,
      },
    ],
    siteName: "InvoiceTrucker",
    title: "Run your transport business from one place.",
    type: "website",
  },
  robots: {
    follow: true,
    index: true,
  },
  twitter: {
    card: "summary_large_image",
    description:
      "A fictional open-source fleet operations portfolio demonstration.",
    images: ["/og.png"],
    title: "Run your transport business from one place.",
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
      </body>
    </html>
  );
}
