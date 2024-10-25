"use client";
import MyTable from "@/components/Table";
import { apiLink, formatCurrency, getToken, toastConfig } from "@/configs";
import {
  Button,
  User,
  Card,
  CardBody,
  CardFooter,
  Image,
} from "@nextui-org/react";
import axios from "axios";
import { useSearchParams } from "next/navigation";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";

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

function page() {
  const searchParams = useSearchParams();
  const status = searchParams.get("status");
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
        return formatCurrency(cellValue);
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

    if (status) {
      if (status == "ok") toast.success("Payment successful", toastConfig);
      else toast.error("Payment failed", toastConfig);
    }
  }, []);
  return (
    <>
      <h1>Inventory</h1>
      <p>Manage your NFT collection and claim new NFTs.</p>
      <h2>Claims</h2>
      <p>Claim your NFTs here.</p>
      <MyTable
        columns={columns}
        rows={claims}
        renderCell={renderCell}
        emptyContent={"There is no Nfts to claim."}
        zeroPadding
      />
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
