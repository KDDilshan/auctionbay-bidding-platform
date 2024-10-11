"use client";
import { MdDashboard } from "react-icons/md";
import Dashboard from "@/components/Dashboard";

export default function RootLayout({ children }) {
  const links = [
    { name: "Dashboard", icon: <MdDashboard />, href: "/seller" },
    {
      name: "Auctions",
      icon: <MdDashboard />,
      href: "/seller/auctions",
    },
    { name: "Settings", icon: <MdDashboard />, href: "/seller/users" },
  ];
  return (
    <Dashboard links={links} name={"Seller Dashboard"}>
      {children}
    </Dashboard>
  );
}
