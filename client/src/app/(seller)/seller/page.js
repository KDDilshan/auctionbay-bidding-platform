"use client";
import SummaryBoxes from "@/components/SummaryBoxes";
import { apiLink, formatCurrency, getToken } from "@/configs";
import axios from "axios";
import React, { useEffect, useState } from "react";
import {
  RiAuctionFill,
  RiMoneyDollarCircleFill,
  RiPuzzleFill,
} from "react-icons/ri";

function page() {
  const [list, setList] = useState([]);
  useEffect(() => {
    axios
      .get(apiLink + "/api/Summary/seller", {
        headers: { Authorization: getToken() },
      })
      .then((res) => {
        setList([
          {
            name: "Total Earnings",
            detail: formatCurrency(res.data.totalEarnings),
            icon: <RiMoneyDollarCircleFill />,
          },
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
