"use client";
import React, { useEffect, useState } from "react";
import axios from "axios";
import { apiLink, getToken, toastConfig } from "@/configs";
import { toast } from "react-toastify";
import AuctionForm from "@/components/AuctionForm";
import Loading from "@/components/Loading";

function page({ params }) {
  const [update, setUpdate] = useState(null);
  const handleSubmit = (title, description, startingPrice, nftId, date) => {
    axios
      .put(
        apiLink + "/api/Auctions/" + params.id,
        {
          title,
          description,
          price: startingPrice,
          startDate: new Date(date.start.toDate()).toISOString(),
          endDate: new Date(date.end.toDate()).toISOString(),
        },
        {
          headers: {
            Authorization: getToken(),
          },
        }
      )
      .then((res) => {
        toast.success("Auction Created", toastConfig);
      })
      .catch((er) => {
        toast.error("Failed Operation", toastConfig);
        console.log(er);
      });
  };

  useEffect(() => {
    axios
      .get(
        apiLink + "/api/Auctions/" + params.id + "/user",

        {
          headers: {
            Authorization: getToken(),
          },
        }
      )
      .then((res) => {
        setUpdate(res.data);
      })
      .catch((er) => console.log(er));
  }, []);

  if (!update) return <Loading />;

  return (
    <>
      <h1 className="text-xl mb-2">{update.title}</h1>
      <div>
        <div className="bg-zinc-900 p-5 rounded-xl flex gap-2 mb-2">
          <img
            src={apiLink + "/wwwroot/uploads/" + update.img}
            alt={update.title}
            className="w-40 h-40 rounded-xl"
          />
        </div>
      </div>
      <AuctionForm handleSubmit={handleSubmit} update={update} />
    </>
  );
}

export default page;
