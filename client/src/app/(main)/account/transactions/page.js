"use client";
import React, { useEffect, useState } from "react";
import axios from "axios";
import { apiLink, getToken } from "@/configs";
import MyTable from "@/components/Table";

const columns = [
  {
    key: "auction",
    label: "AUCTION",
  },
  {
    key: "nft",
    label: "NFT",
  },
  {
    key: "date",
    label: "DATE",
  },
  {
    key: "amount",
    label: "AMOUNT",
  },
];

function page() {
  const [transactions, setTransactions] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const renderCell = React.useCallback((item, columnKey) => {
    const cellValue = item[columnKey];
    switch (columnKey) {
      case "date":
        return new Date(cellValue).toLocaleString();
      case "amount":
        return "USD " + cellValue / 100 + "." + (cellValue % 100);
      default:
        return cellValue;
    }
  });

  useEffect(() => {
    setIsLoading(true);
    axios
      .get(apiLink + "/api/Payment/transactions", {
        headers: { Authorization: getToken() },
      })
      .then((res) => setTransactions(res.data))
      .catch((err) => console.log("Failed to fetch transactions"))
      .finally(() => setIsLoading(false));
  }, []);
  return (
    <>
      <h1>Transactions</h1>
      <p className="mb-2">
        Track all your past transactions, including payment details.
      </p>
      <MyTable
        columns={columns}
        rows={transactions}
        renderCell={renderCell}
        emptyContent={"There is no transactions to display."}
        zeroPadding
        isLoading={isLoading}
      />
    </>
  );
}

export default page;
