"use client";
import { IoMdEye } from "react-icons/io";
import React, { useEffect, useState } from "react";
import { Button, Chip, User, useDisclosure } from "@nextui-org/react";
import axios from "axios";
import { apiLink, getToken, toastConfig } from "@/configs";
import { toast } from "react-toastify";
import MyTable from "@/components/Table";
import UserBlockButton from "@/components/UserBlockButton";

const columns = [
  {
    key: "user",
    label: "USER",
  },
  {
    key: "role",
    label: "ROLE",
  },
  {
    key: "status",
    label: "STATUS",
  },
  {
    key: "action",
    label: "ACTION",
  },
];

function Page() {
  const [rows, setRows] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  const renderCell = React.useCallback((item, columnKey) => {
    const cellValue = item[columnKey];
    switch (columnKey) {
      case "user":
        return (
          <User
            avatarProps={{
              radius: "lg",
              src: "https://i.pravatar.cc/150?u=a042581f4e29026704d",
            }}
            description={item.email}
            name={item.firstName + " " + item.lastName}
          >
            {item.email}
          </User>
        );
      case "action":
        return <UserBlockButton user={item} setRows={setRows} />;
      case "status":
        return (
          <Chip color={item.status == "Active" ? "success" : "danger"}>
            {cellValue}
          </Chip>
        );
      default:
        return cellValue;
    }
  });

  useEffect(() => {
    axios
      .get(apiLink + "/api/User/all", {
        headers: { Authorization: getToken() },
      })
      .then((res) => setRows(res.data))
      .catch((er) => console.log(er))
      .then(() => setIsLoading(false));
  }, []);

  return (
    <>
      <MyTable
        columns={columns}
        rows={rows}
        renderCell={renderCell}
        isLoading={isLoading}
        emptyContent={"There is no users to display."}
      />
    </>
  );
}

export default Page;
