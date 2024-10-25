import { Inter } from "next/font/google";
import "./globals.css";
import { Providers } from "./providers";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
const inter = Inter({ subsets: ["latin"] });
export const metadata = {
  title: "NFTFY",
  description: "NFTFY is a platform for creating, selling, and buying NFTs.",
};

export default function RootLayout({ children }) {
  return (
    <html lang="en" className="dark">
      <body className={inter.className}>
        <Providers>
          <main className="w-full min-h-screen flex flex-col">
            <ToastContainer />
            {children}
          </main>
        </Providers>
      </body>
    </html>
  );
}
