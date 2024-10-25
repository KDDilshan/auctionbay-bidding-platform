import Header from "@/components/Header";
import Footer from "@/components/Footer";
export const metadata = {
  title: "NFTFY",
  description: "NFTFY is a platform for creating, selling, and buying NFTs.",
};

export default function RootLayout({ children }) {
  return (
    <>
      <Header />
      {children}
      <Footer />
    </>
  );
}
