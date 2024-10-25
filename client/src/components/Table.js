"use client";
import React from "react";
import {
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  Spinner,
} from "@nextui-org/react";
function MyTable({
  columns,
  rows,
  renderCell,
  emptyContent,
  isLoading,
  zeroPadding,
}) {
  return (
    <Table classNames={zeroPadding ? { wrapper: "p-0" } : {}}>
      <TableHeader columns={columns}>
        {(column) => <TableColumn key={column.key}>{column.label}</TableColumn>}
      </TableHeader>
      <TableBody
        emptyContent={emptyContent}
        isLoading={isLoading}
        loadingContent={<Spinner color="default" label="Loading..." />}
        items={rows}
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
  );
}

export default MyTable;
