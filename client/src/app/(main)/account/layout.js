"use client";
import { Link } from "@nextui-org/react";
import React from "react";
import {
  FaUserAlt,
  FaBell,
  FaWallet,
  FaBoxOpen,
  FaStoreAlt,
} from "react-icons/fa";
import { usePathname } from "next/navigation";
function layout({ children }) {
  const pathname = usePathname();
  const links = [
    { name: "Account Settings", icon: <FaUserAlt />, href: "/account" },
    {
      name: "Email Preferences",
      icon: <FaBell />,
      href: "/account/notifiactions",
    },
    { name: "Transactions", icon: <FaWallet />, href: "/account/transactions" },
    { name: "Inventory", icon: <FaBoxOpen />, href: "/account/inventory" },
    {
      name: "Marketplace Seller",
      icon: <FaStoreAlt />,
      href: "/account/seller",
    },
  ];
  return (
    <div className="container-full">
      <div className="max-w-[1270px] w-full mx-auto flex max-sm:flex-col gap-5 my-10 items-start">
        <div className="w-full sm:w-[297.5px] bg-zinc-900 rounded-md border border-zinc-700 overflow-hidden">
          {links.map((link) => (
            <Link
              href={link.href}
              className={
                "flex px-[15px] py-[7px] gap-3 transition duration-200 ease-in-out" +
                (pathname === link.href
                  ? " bg-zinc-800 text-white"
                  : " hover:bg-zinc-800 text-zinc-500 hover:text-white")
              }
            >
              <div className="w-[40px] h-[40px] flex items-center justify-center text-lg">
                {link.icon}
              </div>
              <div className="flex items-center">{link.name}</div>
            </Link>
          ))}
        </div>
        <div className="bg-zinc-900 max-sm:w-full flex-1 p-8 rounded-md border border-zinc-700 account">
          {children}
        </div>
      </div>
    </div>
  );
}

export default layout;
