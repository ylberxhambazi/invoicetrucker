import { ImageResponse } from "next/og";

export const size = {
  height: 32,
  width: 32,
};

export const contentType = "image/png";

export default function Icon() {
  return new ImageResponse(
    <div
      style={{
        alignItems: "center",
        background: "#0f172a",
        borderRadius: 8,
        display: "flex",
        height: "100%",
        justifyContent: "center",
        width: "100%",
      }}
    >
      <div style={{ display: "flex", flexDirection: "column", gap: 4 }}>
        <div
          style={{
            background: "#ffffff",
            borderRadius: 2,
            height: 5,
            width: 18,
          }}
        />
        <div
          style={{
            background: "#60a5fa",
            borderRadius: 2,
            height: 5,
            width: 13,
          }}
        />
      </div>
    </div>,
    size,
  );
}
