/** @type {import('next').NextConfig} */
const nextConfig = {
  images: {
    remotePatterns: [
      {
        protocol: "https",
        hostname: "localhost",
        port: "7218",
        pathname: "/wwwroot/uploads/**",
      },
    ],
  },
};

export default nextConfig;
