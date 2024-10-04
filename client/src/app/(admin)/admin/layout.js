"use client";
import { UserContext } from "@/app/providers";
import { useContext } from "react";
import { Avatar, Dropdown, DropdownItem, DropdownMenu, DropdownTrigger, Input, Link } from "@nextui-org/react";
import { MdDashboard } from "react-icons/md";
import { FiSearch } from "react-icons/fi";
import { usePathname } from "next/navigation";

export default function RootLayout({ children }) {
  const { userInfo } = useContext(UserContext);
  const pathname = usePathname();
  const links = [
    { name: "Dashboard", icon: <MdDashboard />, href: "/admin" },
    { name: "Seller Requests", icon: <MdDashboard />, href: "/admin/sellerreq" },
    { name: "Users", icon: <MdDashboard />, href: "/admin/users" },
  ];
  return (
    <div className="container-w flex h-screen grow fixed top-0">
      <div className="w-[20%] bg-zinc-900 p-5 h-full">
        <h1 className="text-3xl text-center">NFTFY</h1>
        <div className=" mt-12 flex flex-col gap-1">
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
      <div className="w-[80%] h-full">
        <div className="py-5 px-10  bg-zinc-900 flex justify-between items-center">
          <Input
            classNames={{
              base: "max-w-full sm:max-w-[25rem] h-10",
              mainWrapper: "h-full",
              input: "text-small",
              inputWrapper:
                "h-full font-normal text-default-500 bg-default-400/20 dark:bg-default-500/20",
            }}
            placeholder="Type to search..."
            size="sm"
            startContent={<FiSearch />}
            type="search"
          />
          <Dropdown placement="bottom-end">
            <DropdownTrigger>
              <Avatar
                isBordered
                as="button"
                className="transition-transform"
                color="secondary"
                name="Jason Hughes"
                size="sm"
                src="https://i.pravatar.cc/150?u=a042581f4e29026704d"
              />
            </DropdownTrigger>
            <DropdownMenu aria-label="Profile Actions" variant="flat">
              <DropdownItem key="profile" className="h-14 gap-2">
                <p className="font-semibold">Signed in as</p>
                <p className="font-semibold">zoey@example.com</p>
              </DropdownItem>
              <DropdownItem key="settings">My Settings</DropdownItem>
              <DropdownItem key="team_settings">Team Settings</DropdownItem>
              <DropdownItem key="analytics">Analytics</DropdownItem>
              <DropdownItem key="system">System</DropdownItem>
              <DropdownItem key="configurations">Configurations</DropdownItem>
              <DropdownItem key="help_and_feedback">
                Help & Feedback
              </DropdownItem>
              <DropdownItem key="logout" color="danger">
                Log Out
              </DropdownItem>
            </DropdownMenu>
          </Dropdown>
        </div>
        <div className="w-full h-full bg-zinc-950 overflow-y-auto overflow-x-hidden p-5">
          {" "}
          {children}
        </div>
      </div>
    </div>
  );
}
