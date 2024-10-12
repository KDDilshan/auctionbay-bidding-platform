"use client";
import { MdAdd, MdDashboard } from "react-icons/md";
import Dashboard from "@/components/Dashboard";

export default function RootLayout({ children }) {
  const links = [
    { name: "Dashboard", icon: <MdDashboard />, href: "/seller" },
    { name: "Create Auction", icon: <MdAdd />, href: "/seller/create" },
    {
      name: "Auctions",
      icon: <MdDashboard />,
      href: "/seller/auctions",
    },
  ];
  return (
    <Dashboard links={links} name={"Seller Dashboard"}>
      {children}
    </Dashboard>
  );
}
