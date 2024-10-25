"use client";
import { MdDashboard, MdRequestPage } from "react-icons/md";
import Dashboard from "@/components/Dashboard";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "@/app/providers";
import { useRouter } from "next/navigation";
import Loading from "@/components/Loading";
import { RiUserFill } from "react-icons/ri";

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
      icon: <MdRequestPage />,
      href: "/admin/sellerreq",
    },
    { name: "Users", icon: <RiUserFill />, href: "/admin/users" },
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
