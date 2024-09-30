import { Link } from "@nextui-org/react";
import React from "react";
import { MdDashboard } from "react-icons/md";

function page() {
  const links = [
    { name: "Dashboard", icon: <MdDashboard />, href: "/" },
    { name: "Seller Requests", icon: <MdDashboard />, href: "/" },
    { name: "Users", icon: <MdDashboard />, href: "/" },
  ];
  return (
    <div className="container-w flex h-full grow">
      <div className="w-[20%] bg-zinc-900 p-5">
        <h1 className="text-3xl text-center">NFTFY</h1>
        <div className="mt-5">
          {links.map((link, index) => (
            <Link
              href={link.href}
              className="border rounded-lg px-5 py-2 flex items-center gap-3 hover:bg-zinc-800 border-zinc-900 hover:border-zinc-700 text-zinc-500 hover:text-white"
            >
              <div className="text-2xl">{link.icon}</div>
              <div className="text-lg font-semibold">{link.name}</div>
            </Link>
          ))}
        </div>
      </div>
      <div className="w-[80%] h-10 bg-white">dads</div>
    </div>
  );
}

export default page;
