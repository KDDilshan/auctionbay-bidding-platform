"use client";
import { MdAdd, MdDashboard } from "react-icons/md";
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
    else if (userInfo.role !== "Seller") router.replace("/login");
    else setLoading(false);
  }, [userInfo]);

  const links = [
    { name: "Dashboard", icon: <MdDashboard />, href: "/seller" },
    { name: "Create Auction", icon: <MdAdd />, href: "/seller/create" },
    {
      name: "Auctions",
      icon: <MdDashboard />,
      href: "/seller/auctions",
    },
  ];

  if (loading) {
    return <Loading />;
  }

  return (
    <Dashboard links={links} name={"Seller Dashboard"}>
      {children}
    </Dashboard>
  );
}
