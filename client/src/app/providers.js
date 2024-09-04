"use client";
import { NextUIProvider } from "@nextui-org/react";
import { useRouter } from "next/navigation";
import { createContext, useState } from "react";

export const UserContext = createContext({});

export function Providers({ children }) {
  const [userInfo, setUserInfo] = useState(null);
  const router = useRouter();
  return (
    <NextUIProvider navigate={router.push}>
      <UserContext.Provider value={{ userInfo, setUserInfo }}>
        {children}
      </UserContext.Provider>
    </NextUIProvider>
  );
}
