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
  Dropdown,
  DropdownTrigger,
  DropdownMenu,
  DropdownItem,
  Input,
  Avatar,
} from "@nextui-org/react";
import { UserContext } from "@/app/providers";
import { usePathname, useRouter } from "next/navigation";

export default function Header() {
  const { userInfo, setUserInfo } = useContext(UserContext);
  const [search, setSearch] = React.useState("");
  const router = useRouter();
  const pathname = usePathname();
  const menuItems = [
    { name: "New", href: "/auction/category/new" },
    { name: "Cartoon", href: "/auction/category/cartoon" },
    { name: "Music", href: "/auction/category/music" },
    { name: "Art", href: "/auction/category/art" },
  ];

  const logout = () => {
    localStorage.removeItem("token");
    setUserInfo(null);
  };

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

      {userInfo && (
        <NavbarContent as="div" className="items-center" justify="end">
          <Dropdown placement="bottom-end">
            <DropdownTrigger>
              <Avatar
                isBordered
                as="button"
                className="transition-transform"
                color="warning"
                name={userInfo.firstName+" "+userInfo.lastName}
                size="sm"
                src="https://i.pravatar.cc/150?u=a042581f4e29026704d"
              />
            </DropdownTrigger>
            <DropdownMenu aria-label="Profile Actions" variant="flat">
              <DropdownItem
                key="profile"
                className="h-14 gap-2"
                onClick={() => router.replace("/account")}
              >
                <p className="font-semibold">Signed in as</p>
                <p className="font-semibold">{userInfo.email}</p>
              </DropdownItem>
              <DropdownItem
                onClick={() => router.replace("/account")}
                key="settings"
              >
                My Settings
              </DropdownItem>
              <DropdownItem
                onClick={() => router.replace("/account/inventory")}
                key="settings"
              >
                Inventory
              </DropdownItem>
              <DropdownItem
                onClick={() => router.replace("/account/transactions")}
                key="settings"
              >
                Transactions
              </DropdownItem>
              {userInfo.role == "Seller" && (
                <DropdownItem
                  key="configurations"
                  onClick={() => router.replace("/seller")}
                >
                  Seller Dashboard
                </DropdownItem>
              )}
              {userInfo.role == "Admin" && (
                <DropdownItem
                  key="configurations"
                  onClick={() => router.replace("/asmin")}
                >
                  Admin Dashboard
                </DropdownItem>
              )}
              <DropdownItem key="logout" color="danger" onClick={logout}>
                Log Out
              </DropdownItem>
            </DropdownMenu>
          </Dropdown>
        </NavbarContent>
      )}

      {!userInfo && (
        <NavbarContent justify="end">
          <NavbarItem className="hidden lg:flex">
            <Link href="login">Login</Link>
          </NavbarItem>
          <NavbarItem>
            <Button as={Link} color="warning" href="register" variant="flat">
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
