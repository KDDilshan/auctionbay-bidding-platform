"use client";
import React, { useEffect, useState } from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableColumn,
  TableHeader,
  TableRow,
} from "@nextui-org/react";
import axios from "axios";
import { apiLink, getToken } from "@/configs";

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
    axios
      .get(apiLink + "/api/Payment/transactions", {
        headers: { Authorization: getToken() },
      })
      .then((res) => setTransactions(res.data))
      .catch((err) => console.log("Failed to fetch transactions"));
  }, []);
  return (
    <>
      <h1>Transactions</h1>
      <p className="mb-2">
        Track all your past transactions, including payment details.
      </p>
      <Table aria-label="Transactions" classNames={{ wrapper: "p-0" }}>
        <TableHeader columns={columns}>
          {(column) => (
            <TableColumn key={column.key}>{column.label}</TableColumn>
          )}
        </TableHeader>
        <TableBody
          emptyContent={"There is no transactions to display."}
          items={transactions}
        >
          {(item) => (
            <TableRow key={item.key}>
              {(columnKey) => (
                <TableCell>{renderCell(item, columnKey)}</TableCell>
              )}
            </TableRow>
          )}
        </TableBody>
      </Table>
    </>
  );
}

export default page;
