"use client";
import { MdDashboard } from "react-icons/md";
import Dashboard from "@/components/Dashboard";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "@/app/providers";
import { useRouter } from "next/navigation";
import Loading from "@/components/Loading";

export default function RootLayout({ children }) {
  const { userInfo } = useContext(UserContext);
  const router = useRouter();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!userInfo) router.replace("/login");
    else if (userInfo.role !== "Admin") router.replace("/login");
    else setLoading(false);
  }, [userInfo]);

  const links = [
    { name: "Dashboard", icon: <MdDashboard />, href: "/admin" },
    {
      name: "Seller Requests",
      icon: <MdDashboard />,
      href: "/admin/sellerreq",
    },
    { name: "Users", icon: <MdDashboard />, href: "/admin/users" },
  ];

  if (loading) {
    return <Loading />;
  }

  return (
    <Dashboard links={links} name={"Admin Dashboard"}>
      {children}
    </Dashboard>
  );
}
