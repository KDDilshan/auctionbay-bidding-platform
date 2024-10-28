"use client";
import React, { useContext } from "react";
import { FiSearch } from "react-icons/fi";
import {
  Navbar,
  NavbarBrand,
  NavbarMenuToggle,
  NavbarMenu,
  NavbarMenuItem,
  NavbarContent,
  NavbarItem,
  Link,
  Button,
  Input,
} from "@nextui-org/react";
import { UserContext } from "@/app/providers";
import { usePathname, useRouter } from "next/navigation";
import UserMenu from "./UserMenu";

export default function Header() {
  const { userInfo } = useContext(UserContext);
  const [search, setSearch] = React.useState("");
  const router = useRouter();
  const pathname = usePathname();
  const menuItems = [
    { name: "New", href: "/auction/category/new" },
    { name: "Cartoon", href: "/auction/category/cartoon" },
    { name: "Music", href: "/auction/category/music" },
    { name: "Art", href: "/auction/category/art" },
  ];

  const handleKeyDown = (e) => {
    if (e.key === "Enter") {
      const quary = new URLSearchParams();
      if (search.trim()) {
        quary.append("search", search.trim());
      }
      router.replace(`/auction?${quary.toString()}`);
    }
  };

  return (
    <Navbar
      disableAnimation
      isBordered
      maxWidth="full"
      classNames={{ base: "w-full", wrapper: "container-full" }}
    >
      <NavbarContent className="sm:hidden" justify="start">
        <NavbarMenuToggle />
      </NavbarContent>

      <NavbarContent className="sm:hidden pr-3" justify="center">
        <NavbarBrand>
          <Link href="/" className="font-bold text-inherit">
            NFTFY
          </Link>
        </NavbarBrand>
      </NavbarContent>

      <NavbarContent className="hidden sm:flex gap-4" justify="center">
        <NavbarBrand>
          <Link href="/" className="font-bold text-2xl text-inherit">
            NFTFY
          </Link>
        </NavbarBrand>
        {menuItems.map((item, index) => (
          <NavbarItem>
            <Link
              color={item.href == pathname ? "warning" : "foreground"}
              href={item.href}
            >
              {item.name}
            </Link>
          </NavbarItem>
        ))}
      </NavbarContent>

      <NavbarContent justify="end" className="hidden sm:flex">
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
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          onKeyDown={handleKeyDown}
        />
      </NavbarContent>

      {userInfo && <UserMenu />}

      {!userInfo && (
        <NavbarContent justify="end">
          <NavbarItem className="hidden lg:flex">
            <Link href="login">Login</Link>
          </NavbarItem>
          <NavbarItem>
            <Button
              onClick={() => router.replace("/register")}
              color="warning"
              variant="flat"
            >
              Sign Up
            </Button>
          </NavbarItem>
        </NavbarContent>
      )}

      <NavbarMenu>
        {menuItems.map((item, index) => (
          <NavbarMenuItem key={`${item.name}-${index}`}>
            <Link
              className="w-full"
              color={item.href == pathname ? "warning" : "foreground"}
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
