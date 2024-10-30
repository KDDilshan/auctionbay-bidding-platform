"use client";
import React from "react";
import axios from "axios";
import { apiLink, getToken, toastConfig } from "@/configs";
import { toast } from "react-toastify";
import AuctionForm from "@/components/AuctionForm";
import { useRouter } from "next/navigation";

function page() {
  const router = useRouter();
  const handleSubmit = (
    title,
    description,
    startingPrice,
    nftId,
    date,
    category
  ) => {
    axios
      .post(
        apiLink + "/api/Auctions",
        {
          title,
          description,
          price: startingPrice,
          nftId,
          startDate: new Date(date.start.toDate()).toISOString(),
          endDate: new Date(date.end.toDate()).toISOString(),
          category,
        },
        {
          headers: {
            Authorization: getToken(),
          },
        }
      )
      .then((res) => {
        toast.success("Auction Created", toastConfig);
        router.replace("/seller/auctions");
      })
      .catch((er) => {
        toast.error("Failed Operation", toastConfig);
        console.log(er);
      });
  };

  return (
    <>
      <AuctionForm handleSubmit={handleSubmit} />
    </>
  );
}

export default page;
