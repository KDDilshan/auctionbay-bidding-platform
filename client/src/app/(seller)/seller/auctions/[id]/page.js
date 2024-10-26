"use client";
import React, { useEffect, useState } from "react";
import axios from "axios";
import { apiLink, getToken, toastConfig } from "@/configs";
import { toast } from "react-toastify";
import AuctionForm from "@/components/AuctionForm";
import Loading from "@/components/Loading";
import MyChart from "@/components/MyChart";

function page({ params }) {
  const [update, setUpdate] = useState(null);
  const [bids, setBids] = useState([]);
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

    axios
      .get(
        apiLink + "/api/Auctions/" + params.id + "/bids",

        {
          headers: {
            Authorization: getToken(),
          },
        }
      )
      .then((res) =>
        setBids(
          res.data.map((bid) => {
            return {
              date: new Date(bid.bidDate.slice(0, 23) + "Z").toLocaleString(),
              price: bid.bidPrice / 100,
            };
          })
        )
      )
      .catch((er) => console.log(er));
  }, []);

  if (!update) return <Loading />;

  return (
    <>
      <h1 className="text-2xl font-semibold mb-2">{update.title}</h1>
      <div className="flex flex-col md:flex-row items-start">
        <div className="bg-zinc-900 p-5 rounded-xl flex gap-2 mb-2 w-full md:w-1/3">
          <img
            src={apiLink + "/wwwroot/uploads/" + update.img}
            alt={update.title}
            className="w-40 h-40 rounded-xl"
          />
          <div>
            <h1 className="text-xl">{update.nftName}</h1>
            <p>{update.nftDes}</p>
          </div>
        </div>
        <div>
          <MyChart list={bids} />
        </div>
      </div>
      <AuctionForm handleSubmit={handleSubmit} update={update} />
    </>
  );
}

export default page;
