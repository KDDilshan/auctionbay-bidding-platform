"use client";
import { apiLink, getToken } from "@/configs";
import {
  Button,
  Table,
  TableBody,
  TableCell,
  TableColumn,
  TableHeader,
  TableRow,
  User,
  Card,
  CardBody,
  CardFooter,
  Image,
} from "@nextui-org/react";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { IoMdEye } from "react-icons/io";

const columns = [
  {
    key: "nft",
    label: "NFT",
  },
  {
    key: "auction",
    label: "AUCTION",
  },
  {
    key: "amount",
    label: "AMOUNT",
  },
  {
    key: "action",
    label: "ACTION",
  },
];
const list = [
  {
    title: "Orange",
    img: "https://localhost:7218/wwwroot/uploads/9a6aefea-ec49-48db-ae62-88c93ac9fbb2.png",
    price: "$5.50",
  },
  {
    title: "Tangerine",
    img: "https://localhost:7218/wwwroot/uploads/9a6aefea-ec49-48db-ae62-88c93ac9fbb2.png",
    price: "$3.00",
  },
  {
    title: "Raspberry",
    img: "https://localhost:7218/wwwroot/uploads/9a6aefea-ec49-48db-ae62-88c93ac9fbb2.png",
    price: "$10.00",
  },
  {
    title: "Lemon",
    img: "https://localhost:7218/wwwroot/uploads/9a6aefea-ec49-48db-ae62-88c93ac9fbb2.png",
    price: "$5.30",
  },
  {
    title: "Avocado",
    img: "https://localhost:7218/wwwroot/uploads/9a6aefea-ec49-48db-ae62-88c93ac9fbb2.png",
    price: "$15.70",
  },
  {
    title: "Lemon 2",
    img: "https://localhost:7218/wwwroot/uploads/9a6aefea-ec49-48db-ae62-88c93ac9fbb2.png",
    price: "$8.00",
  },
  {
    title: "Banana",
    img: "https://localhost:7218/wwwroot/uploads/9a6aefea-ec49-48db-ae62-88c93ac9fbb2.png",
    price: "$7.50",
  },
  {
    title: "Watermelon",
    img: "https://localhost:7218/wwwroot/uploads/9a6aefea-ec49-48db-ae62-88c93ac9fbb2.png",
    price: "$12.20",
  },
];
function page() {
  const [claims, setClaims] = useState([]);
  const [collection, setCollection] = useState([]);
  const renderCell = React.useCallback((item, columnKey) => {
    const cellValue = item[columnKey];
    switch (columnKey) {
      case "nft":
        return (
          <User
            avatarProps={{
              radius: "sm",
              src: apiLink + "/wwwroot/uploads/" + item.img,
            }}
            name={item.name}
          ></User>
        );
      case "action":
        return (
          <Button
            color="warning"
            variant="faded"
            aria-label="Take a photo"
            onClick={() => makeClaim(item)}
          >
            Claim
          </Button>
        );
      case "amount":
        return "USD " + cellValue / 100 + "." + (cellValue % 100);
      default:
        return cellValue;
    }
  });

  const makeClaim = async (item) => {
    axios
      .post(
        apiLink + "/api/Payment/" + item.id,
        {},
        {
          headers: { Authorization: getToken() },
        }
      )
      .then((res) => window.location.replace(res.data.url))
      .catch((err) => console.log(err));
  };

  useEffect(() => {
    axios
      .get(apiLink + "/api/Auctions/claims", {
        headers: { Authorization: getToken() },
      })
      .then((res) => setClaims(res.data))
      .catch((err) => console.log(err));

    axios
      .get(apiLink + "/api/Nft/user", {
        headers: { Authorization: getToken() },
      })
      .then((res) => setCollection(res.data))
      .catch((err) => console.log(err));
  }, []);
  return (
    <>
      <h1>Inventory</h1>
      <p>Manage your NFT collection and claim new NFTs.</p>
      <h2>Claims</h2>
      <p>Claim your NFTs here.</p>
      <Table
        aria-label="Example table with dynamic content"
        classNames={{ wrapper: "p-0" }}
      >
        <TableHeader columns={columns}>
          {(column) => (
            <TableColumn key={column.key}>{column.label}</TableColumn>
          )}
        </TableHeader>
        <TableBody emptyContent={"There is no Nfts to claim."} items={claims}>
          {(item) => (
            <TableRow key={item.key}>
              {(columnKey) => (
                <TableCell>{renderCell(item, columnKey)}</TableCell>
              )}
            </TableRow>
          )}
        </TableBody>
      </Table>
      <h2>Collection</h2>
      <p>Your NFT Collection.</p>
      <div className="mt-2 gap-2 grid grid-cols-2 sm:grid-cols-6">
        {collection.map((item, index) => (
          <Card
            classNames={{ base: "bg-zinc-700" }}
            shadow="sm"
            key={index}
            isPressable
            onPress={() => console.log("item pressed")}
          >
            <CardBody className="overflow-visible p-0">
              <Image
                shadow="sm"
                radius="lg"
                width="100%"
                alt={item.title}
                className="w-full object-cover h-[140px]"
                src={apiLink + "/wwwroot/uploads/" + item.image}
              />
            </CardBody>
            <CardFooter className="text-small justify-between">
              <b>{item.title}</b>
            </CardFooter>
          </Card>
        ))}
      </div>
    </>
  );
}

export default page;
