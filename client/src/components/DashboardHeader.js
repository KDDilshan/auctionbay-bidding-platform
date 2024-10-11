"use client";
import React, { useContext } from "react";
import { FiSearch } from "react-icons/fi";
import {
  Navbar,
  NavbarMenuToggle,
  NavbarMenu,
  NavbarMenuItem,
  NavbarContent,
  Link,
  Dropdown,
  DropdownTrigger,
  DropdownMenu,
  DropdownItem,
  Input,
  Avatar,
} from "@nextui-org/react";
import { UserContext } from "@/app/providers";
import { usePathname, useRouter } from "next/navigation";

export default function DashboardHeader({ links }) {
  const { userInfo, setUserInfo } = useContext(UserContext);
  const router = useRouter();
  const pathname = usePathname();
  const logout = () => {
    localStorage.removeItem("token");
    setUserInfo(null);
  };

  return (
    <Navbar
      disableAnimation
      maxWidth="full"
      classNames={{ base: "w-full bg-zinc-900", wrapper: "px-10 py-5" }}
    >
      <NavbarContent className="lg:hidden" justify="start">
        <NavbarMenuToggle />
      </NavbarContent>

      <NavbarContent justify="start">
        <Input
          classNames={{
            base: "w-full sm:max-w-[25rem] h-10",
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
      </NavbarContent>

      <NavbarContent as="div" className="items-center" justify="end">
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
            <DropdownItem
              key="profile"
              className="h-14 gap-2"
              onClick={() => router.push("/account")}
            >
              <p className="font-semibold">Signed in as</p>
              <p className="font-semibold">{userInfo.email}</p>
            </DropdownItem>
            <DropdownItem key="settings">My Settings</DropdownItem>
            <DropdownItem key="configurations">Seller Dashboard</DropdownItem>
            <DropdownItem key="logout" color="danger" onClick={logout}>
              Log Out
            </DropdownItem>
          </DropdownMenu>
        </Dropdown>
      </NavbarContent>

      <NavbarMenu>
        {links.map((item, index) => (
          <NavbarMenuItem key={`${item}-${index}`}>
            <Link
              className="w-full"
              color={pathname == item.href ? "warning" : "foreground"}
              href={item.href}
              size="lg"
            >
              {item.name}
            </Link>
          </NavbarMenuItem>
        ))}
      </NavbarMenu>
    </Navbar>
  );
}
