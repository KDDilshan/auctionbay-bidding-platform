"use client";
import SummaryBoxes from "@/components/SummaryBoxes";
import { apiLink, getToken } from "@/configs";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { RiAuctionFill, RiPuzzleFill, RiUserFill } from "react-icons/ri";
function page() {
  const [list, setList] = useState([]);
  useEffect(() => {
    axios
      .get(apiLink + "/api/Summary/admin", {
        headers: { Authorization: getToken() },
      })
      .then((res) => {
        setList([
          {
            name: "Total Auctions",
            detail: res.data.totalAuctions,
            icon: <RiAuctionFill />,
          },
          {
            name: "Total Nfts",
            detail: res.data.totalNfts,
            icon: <RiPuzzleFill />,
          },
          {
            name: "Total Users",
            detail: res.data.totalUsers,
            icon: <RiUserFill />,
          },
        ]);
      })
      .catch((er) => {
        console.log(er);
      })
      .finally(() => {});
  }, []);
  return (
    <>
      <SummaryBoxes list={list} />
    </>
  );
}

export default page;
