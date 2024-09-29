"use client";
import { apiLink, getToken } from "@/configs";
import { NextUIProvider } from "@nextui-org/react";
import { useRouter } from "next/navigation";
import { createContext, useEffect, useState } from "react";

export const UserContext = createContext({});

export function Providers({ children }) {
  const [userInfo, setUserInfo] = useState(null);
  const router = useRouter();

  useEffect(() => {
    var token = getToken();
    if (token) {
      axios
        .get(apiLink + "/api/User", {
          headers: { Authorization: token },
        })
        .then((res) =>
          setUserInfo({
            email: res.data.email,
            firstName: res.data.firstName,
            lastName: res.data.lastName,
          })
        )
        .catch((er) => console.log(er));
    }
  }, []);

  return (
    <NextUIProvider navigate={router.push}>
      <UserContext.Provider value={{ userInfo, setUserInfo }}>
        {children}
      </UserContext.Provider>
    </NextUIProvider>
  );
}
