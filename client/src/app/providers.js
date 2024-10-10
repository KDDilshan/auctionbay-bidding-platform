"use client";
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
          })
        )
        .catch((er) => console.log(er))
        .finally(() => setLoading(false));
    } else setLoading(false);
  }, []);

  return (
    <NextUIProvider navigate={router.push}>
      <UserContext.Provider value={{ userInfo, setUserInfo }}>
        {loading ? (
          <div className="w-full h-screen flex justify-center items-center">
            {" "}
            <Spinner size="md" label="Loading..." />
          </div>
        ) : (
          children
        )}
      </UserContext.Provider>
    </NextUIProvider>
  );
}
