"use client";
import React, { useContext, useEffect } from "react";
import { useState } from "react";
import { CiCircleCheck } from "react-icons/ci";
import { GrSave } from "react-icons/gr";
import { User, Link, Button } from "@nextui-org/react";
import { Input } from "@nextui-org/input";
import axios from "axios";
import { apiLink, getToken, toastConfig } from "@/configs";
import Loading from "@/components/Loading";
import Countdown from "react-countdown";
import { toast } from "react-toastify";
import { UserContext } from "@/app/providers";

function page({ params }) {
  const [loading, setLoading] = useState(true);
  const [btnLoading, setBtnLoading] = useState(false);
  const [bidValue, setBidValue] = useState(0);
  const [nft, setNft] = useState(null);
  const { userInfo } = useContext(UserContext);
  useEffect(() => {
    axios
      .get(apiLink + "/api/auctions/" + params.id)
      .then((res) => {
        setNft(res.data);
        setLoading(false);
      })
      .catch((er) => console.log(er));
  }, []);

  const placeBid = async (e) => {
    e.preventDefault();
    setBtnLoading(true);
    const data = {
      price: bidValue,
    };
    await axios
      .post(apiLink + "/api/Auctions/" + params.id + "/bid", data, {
        headers: {
          Authorization: getToken(),
        },
      })
      .then((res) => {
        setNft({
          ...nft,
          currentBid: bidValue * 100,
          numberOfBids: nft.numberOfBids + 1,
        });
        toast.success("Bid Placed", toastConfig);
      })
      .catch((er) => {
        if (er.response.status === 400)
          toast.error(er.response.data, toastConfig);
        else toast.error("Something went wrong", toastConfig);
      })
      .finally(() => setBtnLoading(false));
  };

  const renderer = ({ days, hours, minutes, seconds, completed }) => {
    if (completed) {
      // Render a completed state
      return <div>{nft.status == "Open" ? "Closed" : nft.status}</div>;
    } else {
      // Render a countdown
      return (
        <div className="flex gap-4 text-xs ">
          {/* Days */}
          <div className="flex flex-col items-center">
            <div className="flex space-x-1">
              <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                {formatstart(days)}
              </span>
              <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                {formatend(days)}
              </span>
            </div>
            <span className="mt-2">Days</span>
          </div>

          {/* Hours */}
          <div className="flex flex-col items-center">
            <div className="flex space-x-1">
              <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                {formatstart(hours)}
              </span>
              <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                {formatend(hours)}
              </span>
            </div>
            <span className="mt-2">Hours</span>
          </div>

          {/* Minutes */}
          <div className="flex flex-col items-center">
            <div className="flex space-x-1">
              <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                {formatstart(minutes)}
              </span>
              <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                {formatend(minutes)}
              </span>
            </div>
            <span className="mt-2">Minutes</span>
          </div>

          {/* Seconds */}
          <div className="flex flex-col items-center">
            <div className="flex space-x-1">
              <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                {formatstart(seconds)}
              </span>
              <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                {formatend(seconds)}
              </span>
            </div>
            <span className="mt-2">Seconds</span>
          </div>
        </div>
      );
    }
  };
  function formatstart(time) {
    var start = time / 10;
    return parseInt(start);
  }
  function formatend(time) {
    var end = time % 10;
    return end;
  }

  if (loading) return <Loading />;

  return (
    <div className="container py-12 mx-auto item-center">
      <div className="text-center ">
        <h2 className="text-4xl font-bold">Bid Your Bid Value</h2>
      </div>
      <div className="flex flex-col max-w-5xl p-6 mx-auto text-white rounded-lg shadow-lg md:flex-row">
        <div className="w-full p-4  h-[500px] mb-4 rounded-lg bg-slate-800 md:w-1/2 md:mb-0">
          <img
            src={apiLink + "/wwwroot/uploads/" + nft.image}
            className="object-cover w-full h-full rounded-md"
            alt=""
          />
        </div>

        <div className="flex flex-col justify-center w-full md:w-1/2 md:pl-6">
          <div className="flex justify-between mt-2">
            <p className="flex gap-1 text-sm text-blue-600 ">
              {nft.category}
              <CiCircleCheck className="text-sm text-white bg-green-500 rounded-full mt-0.5" />
            </p>
            <div className="text-lg cursor-pointer">
              <GrSave color="gray" size={24} />
            </div>
          </div>
          <div>
            <h2 className="mb-1 text-3xl font-semibold">{nft.title}</h2>
            <p className="mb-4 text-gray-400">{nft.description}</p>
            <User
              name={nft.owner}
              description={
                <Link
                  href="https://twitter.com/jrgarciadev"
                  size="sm"
                  isExternal
                >
                  @{nft.email}
                </Link>
              }
              avatarProps={{
                src: "https://avatars.githubusercontent.com/u/30373425?v=4",
              }}
            />
          </div>
          <hr className="mt-6 border-t-2 border-slate-500"></hr>
          {/* Countdown Section */}
          <div>
            <p className="p-2 mt-4 -ml-2 text-sm text-gray-400">
              Auction Ending In:
            </p>
            <Countdown
              renderer={renderer}
              date={new Date(nft.endDate.slice(0, 23) + "Z").toLocaleString()}
            />
          </div>
          <hr className="mt-6 border-t-2 border-slate-500"></hr>
          {/* Bid Form */}
          <div className="p-6 -mt-2">
            <p className="mb-1 text-lg text-left ">
              Current Bid : $
              {nft.currentBid / 100 + "." + (nft.currentBid % 100)}
            </p>
            {userInfo ? (
              nft.status == "Open" && (
                <form onSubmit={placeBid} className="flex flex-col">
                  {/* INput for bid value */}
                  <Input
                    type="number"
                    placeholder="0.00"
                    labelPlacement="outside"
                    isRequired
                    validate={(val) => parseFloat(val) > nft.currentBid}
                    className="mb-2"
                    value={bidValue}
                    onChange={(e) => setBidValue(e.target.value)}
                    startContent={
                      <div className="flex items-center pointer-events-none">
                        <span className="text-default-400 text-small">$</span>
                      </div>
                    }
                  />
                  {/* Place Bid button */}
                  <Button
                    isLoading={btnLoading}
                    type="submit"
                    className="w-full p-2 px-4 text-white bg-blue-700 rounded-xl hover:bg-blue-900"
                  >
                    Place Bid
                  </Button>
                </form>
              )
            ) : (
              <div>Log into your account for place a bid</div>
            )}
          </div>
          <div>
            <p className="-mt-4 text-gray-400">
              Total Bids: {nft.numberOfBids}
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default page;
