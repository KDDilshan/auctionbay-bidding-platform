"use client";
import { UserContext } from "@/app/providers";
import { useContext } from "react";


export default function RootLayout({ children }) {
  const { userInfo } = useContext(UserContext);
  return <>{children}</>;
}
