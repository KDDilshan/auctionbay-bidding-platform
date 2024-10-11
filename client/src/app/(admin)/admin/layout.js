"use client";
import { MdDashboard } from "react-icons/md";
import Dashboard from "@/components/Dashboard";

export default function RootLayout({ children }) {
  const links = [
    { name: "Dashboard", icon: <MdDashboard />, href: "/admin" },
    {
      name: "Seller Requests",
      icon: <MdDashboard />,
      href: "/admin/sellerreq",
    },
    { name: "Users", icon: <MdDashboard />, href: "/admin/users" },
  ];
  return (
    <Dashboard links={links} name={"Admin Dashboard"}>
      {children}
    </Dashboard>
  );
}
