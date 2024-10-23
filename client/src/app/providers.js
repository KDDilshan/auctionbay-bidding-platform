"use client";
import Loading from "@/components/Loading";
import { apiLink, getToken } from "@/configs";
import { NextUIProvider, Spinner } from "@nextui-org/react";
import axios from "axios";
import { useRouter } from "next/navigation";
import { createContext, useEffect, useState } from "react";

export const UserContext = createContext({});

export function Providers({ children }) {
  const [userInfo, setUserInfo] = useState(null);
  const [loading, setLoading] = useState(true);
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
            status: res.data.status,
            role: res.data.role,
          })
        )
        .catch((er) => console.log(er))
        .finally(() => setLoading(false));
    } else setLoading(false);
  }, []);

  return (
    <NextUIProvider navigate={router.push}>
      <UserContext.Provider value={{ userInfo, setUserInfo }}>
        {loading ? <Loading /> : children}
      </UserContext.Provider>
    </NextUIProvider>
  );
}
