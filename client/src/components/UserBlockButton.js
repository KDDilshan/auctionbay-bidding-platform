"use client";
import { apiLink, getToken, toastConfig } from "@/configs";
import { Button } from "@nextui-org/react";
import axios from "axios";
import React, { useState } from "react";
import { toast } from "react-toastify";

function UserBlockButton({ user, setRows }) {
  const [isLoading, setIsLoading] = useState(false);
  const changeStatus = async () => {
    setIsLoading(true);
    axios
      .put(
        apiLink + "/api/User/Status",
        {
          userId: user.id,
          status: user.status == "Active" ? "Blocked" : "Active",
        },
        { headers: { Authorization: getToken() } }
      )
      .then(() => {
        setRows((prev) =>
          prev.map((item) =>
            item.id == user.id
              ? {
                  ...item,
                  status: user.status == "Active" ? "Blocked" : "Active",
                }
              : item
          )
        );
        toast.success("User status changed", toastConfig);
      })
      .catch((e) => {
        console.log(er);
        toast.error("Failed to update user", toastConfig);
      })
      .finally(() => setIsLoading(false));
  };
  return (
    <Button
      isLoading={isLoading}
      color={user.status == "Active" ? "warning" : "primary"}
      variant="faded"
      onClick={changeStatus}
    >
      {user.status == "Active" ? "Block" : "Unblock"}
    </Button>
  );
}

export default UserBlockButton;
