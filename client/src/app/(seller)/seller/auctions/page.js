"use client";
import React, { useEffect } from "react";
import axios from "axios";
import { apiLink, formatCurrency, getToken } from "@/configs";
import Link from "next/link";
import MyTable from "@/components/Table";
import { Button } from "@nextui-org/react";
import { useRouter } from "next/navigation";

const columns = [
  {
    key: "title",
    label: "NAME",
  },
  {
    key: "date",
    label: "DATE",
  },
  {
    key: "status",
    label: "STATUS",
  },
  {
    key: "currentBid",
    label: "CURRENT BID",
  },
  {
    key: "action",
    label: "ACTION",
  },
];
function page() {
  const [rows, setRows] = React.useState([]);
  const router = useRouter();
  const [isLoading, setIsLoading] = React.useState(false);
  const renderCell = React.useCallback((item, columnKey) => {
    const cellValue = item[columnKey];
    switch (columnKey) {
      case "action":
        return (
          <Button
            onClick={() => router.replace("/seller/auctions/" + item.id)}
            color="warning"
            variant="faded"
          >
            View
          </Button>
        );
      case "date":
        return new Date(item.endDate).toLocaleString();
      case "currentBid":
        return formatCurrency(item.currentBid);
      default:
        return cellValue;
    }
  });

  useEffect(() => {
    setIsLoading(true);
    axios
      .get(apiLink + "/api/Auctions/user", {
        headers: { Authorization: getToken() },
      })
      .then((res) => setRows(res.data))
      .catch((err) => console.log(err))
      .then(() => setIsLoading(false));
  }, []);

  return (
    <>
      <MyTable
        columns={columns}
        rows={rows}
        renderCell={renderCell}
        emptyContent="There is no Auctions to display"
        isLoading={isLoading}
      />
    </>
  );
}

export default page;
