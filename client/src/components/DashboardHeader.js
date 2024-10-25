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
import UserMenu from "./UserMenu";

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

      {userInfo && <UserMenu />}

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
