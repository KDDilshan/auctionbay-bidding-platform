import { Link } from "@nextui-org/react";
import { usePathname } from "next/navigation";
import React from "react";

function DashboardMenu({ links, name }) {
  const pathname = usePathname();
  return (
    <div className="hidden lg:flex flex-col lg:w-[20%] bg-zinc-900 p-5 sticky top-0 h-screen">
      <h1 className="text-3xl text-center">NFTFY</h1>
      <h2 className="text-center font-bold">{name}</h2>
      <div className=" mt-10 flex flex-col gap-1">
        {links.map((link, index) => (
          <Link
            href={link.href}
            className={
              "border rounded-lg px-5 py-2 flex items-center gap-3 " +
              (pathname == link.href
                ? "text-white border-zinc-700 bg-zinc-800"
                : "border-zinc-900 text-zinc-500 hover:text-white hover:border-zinc-700 hover:bg-zinc-800")
            }
          >
            <div className="text-2xl">{link.icon}</div>
            <div className="text-lg font-semibold">{link.name}</div>
          </Link>
        ))}
      </div>
    </div>
  );
}

export default DashboardMenu;
