"use client";
import { UserContext } from "@/app/providers";
import {
  Avatar,
  Dropdown,
  DropdownItem,
  DropdownMenu,
  DropdownTrigger,
  NavbarContent,
} from "@nextui-org/react";
import React, { useContext } from "react";
import { useRouter } from "next/navigation";

function UserMenu() {
  const { userInfo, setUserInfo } = useContext(UserContext);
  const router = useRouter();
  const logout = () => {
    localStorage.removeItem("token");
    setUserInfo(null);
  };

  return (
    <NavbarContent as="div" className="items-center" justify="end">
      <Dropdown placement="bottom-end">
        <DropdownTrigger>
          <Avatar
            isBordered
            as="button"
            className="transition-transform"
            color="warning"
            name={userInfo.firstName + " " + userInfo.lastName}
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
              onClick={() => router.replace("/admin")}
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
  );
}

export default UserMenu;
