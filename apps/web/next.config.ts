import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  allowedDevOrigins:
    process.env.NODE_ENV === "production"
      ? undefined
      : ["127.0.0.1", "localhost"],

  reactStrictMode: true,

  transpilePackages: ["@invoicetrucker/types", "@invoicetrucker/ui"],

  async redirects() {
    return [
      {
        destination: "/legal/privacy",
        permanent: true,
        source: "/privacy",
      },
    ];
  },

  async headers() {
    return [
      {
        source: "/(.*)",
        headers: [
          { key: "X-Content-Type-Options", value: "nosniff" },
          { key: "X-Frame-Options", value: "DENY" },
          {
            key: "Referrer-Policy",
            value: "strict-origin-when-cross-origin",
          },
        ],
      },
    ];
  },
};

export default nextConfig;
