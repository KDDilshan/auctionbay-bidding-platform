"use client";
import React, { useEffect } from "react";
import {
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
} from "@nextui-org/react";
import axios from "axios";
import { apiLink, formatCurrency, getToken } from "@/configs";
import Link from "next/link";

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
  const renderCell = React.useCallback((item, columnKey) => {
    const cellValue = item[columnKey];
    switch (columnKey) {
      case "action":
        return (
          <Link
            href={`/seller/auctions/${item.id}`}
            color="warning"
            variant="faded"
          >
            View
          </Link>
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
    axios
      .get(apiLink + "/api/Auctions/user", {
        headers: { Authorization: getToken() },
      })
      .then((res) => setRows(res.data))
      .catch((err) => console.log(err));
  }, []);

  return (
    <>
      <Table>
        <TableHeader columns={columns}>
          {(column) => (
            <TableColumn key={column.key}>{column.label}</TableColumn>
          )}
        </TableHeader>
        <TableBody emptyContent="There is no Auctions to display" items={rows}>
          {(item) => (
            <TableRow key={item.title}>
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
