"use client";
import React from "react";
import { useState } from "react";
import Image from "next/image";
import { CiCircleCheck } from "react-icons/ci";
import { GrSave } from "react-icons/gr";
import { User, Link } from "@nextui-org/react";
import { Input } from "@nextui-org/input";

function BitPage() {
  const [nft, setNft] = useState({
    name: "Robot Nurse",
    category: "Art",
    description:
      "This is a unique piece of digital art. It is a robot nurse.It is a robot nurseThis is a unique piece of digital art. It is a This is a uniquThis is a unique piece of digital art. It is a robot nurse.It is e piece of digital art. It is a robot nurse.It is robot nurse.It is .It is a robot nurse. ",
    image: "/Hero Image.webp",
    currentBid: 2.5,
  });

  return (
    <div className="container py-12 mx-auto item-center">
      <div className="text-center ">
        <h2 className="text-4xl font-bold">Bid Your Bid Value </h2>
      </div>
      <div className="flex flex-col max-w-5xl p-6 mx-auto text-white rounded-lg shadow-lg md:flex-row">
        <div className="w-full p-4  h-[500px] mb-4 rounded-lg bg-slate-800 md:w-1/2 md:mb-0">
          <Image
            src={nft.image}
            alt={nft.name}
            width={500}
            height={500}
            className="object-cover w-full h-full rounded-md"
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
            <h2 className="mb-1 text-3xl font-semibold">{nft.name}</h2>
            <p className="mb-4 text-gray-400">{nft.description}</p>
            <User
              name="Junior Garcia"
              description={
                <Link
                  href="https://twitter.com/jrgarciadev"
                  size="sm"
                  isExternal
                >
                  @jrgarciadev
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
            <div className="flex gap-4 text-xs ">
              {/* Days */}
              <div className="flex flex-col items-center">
                <div className="flex space-x-1">
                  <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                    0
                  </span>
                  <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                    2
                  </span>
                </div>
                <span className="mt-2">Days</span>
              </div>

              {/* Hours */}
              <div className="flex flex-col items-center">
                <div className="flex space-x-1">
                  <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                    0
                  </span>
                  <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                    5
                  </span>
                </div>
                <span className="mt-2">Hours</span>
              </div>

              {/* Minutes */}
              <div className="flex flex-col items-center">
                <div className="flex space-x-1">
                  <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                    3
                  </span>
                  <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                    0
                  </span>
                </div>
                <span className="mt-2">Minutes</span>
              </div>

              {/* Seconds */}
              <div className="flex flex-col items-center">
                <div className="flex space-x-1">
                  <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                    1
                  </span>
                  <span className="p-2 py-3 font-semibold rounded-md bg-slate-700">
                    5
                  </span>
                </div>
                <span className="mt-2">Seconds</span>
              </div>
            </div>
          </div>
          <hr className="mt-6 border-t-2 border-slate-500"></hr>
          {/* Bid Form */}
          <div className="p-6 -mt-2">
            <div className="flex flex-col">
              <p className="mb-1 text-lg text-left ">Current Bid : 5$</p>
              {/* INput for bid value */}
              <Input
                type="number"
                placeholder="0.00"
                labelPlacement="outside"
                className="mb-2"
                startContent={
                  <div className="flex items-center pointer-events-none">
                    <span className="text-default-400 text-small">$</span>
                  </div>
                }
              />
              {/* Place Bid button */}
              <button className="w-full p-2 px-4 text-white bg-blue-700 rounded-xl hover:bg-blue-900">
                Place Bid
              </button>
            </div>
          </div>
          <div>
            <p className="-mt-4 text-gray-400">Total Bids: 5</p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default BitPage;
