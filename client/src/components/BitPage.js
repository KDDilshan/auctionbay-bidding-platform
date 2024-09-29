import React from "react";
import { useState } from "react";
import { Button } from "@nextui-org/react";
import Image from "next/image";
import { CiCircleCheck } from "react-icons/ci";
import { AiFillHeart, AiOutlineHeart } from "react-icons/ai";
import { User, Link } from "@nextui-org/react";
import Countdown from "react-countdown";

function BitPage() {
  const [nft, setNft] = useState({
    name: "Robot Nurse",
    category: "Art",
    description: "This is a unique piece of digital art. It is a robot nurse.",
    image: "/Hero Image.webp",
    currentBid: 2.5,
  });

  const [bidValue, setBidValue] = useState("");
  const [currentBid, setCurrentBid] = useState(nft.currentBid);

  const handleBid = () => {
    const bid = parseFloat(bidValue);
    if (bid > currentBid) {
      setCurrentBid(bid);
      alert("Bid placed successfully!");
    } else {
      alert("Bid must be higher than the current bid.");
    }
    setBidValue("");
  };

  // like counter
  const [likes, setLikes] = useState(0);
  const [isLiked, setIsLiked] = useState(false);

  const handleLike = () => {
    setIsLiked(!isLiked);
    setLikes(isLiked ? likes : likes + 1);
  };

  const renderer = ({ days, hours, minutes, seconds, completed }) => {
    if (completed) {
      // Render a completed state
      return <div>SOLD</div>;
    } else {
      // Render a countdown
      return (
        <span>
          {formatTime(days) +
            " : " +
            formatTime(hours) +
            " : " +
            formatTime(minutes) +
            " : " +
            formatTime(seconds)}
        </span>
      );
    }
  };
  function formatTime(time) {
    return time < 10 ? `0${time}` : time;
  }

  return (
    <div className="container py-12 mx-auto item-center">
      <div className="my-5 text-center">
        <h2 className="my-6 text-4xl font-bold">Bid Your Bid Value </h2>
      </div>
      <div className="flex flex-col max-w-4xl p-6 mx-auto text-white bg-gray-800 rounded-lg shadow-lg md:flex-row">
        <div className="w-full mb-4 md:w-1/2 md:mb-0">
          <Image
            src={nft.image}
            alt={nft.name}
            width={2000}
            height={2000}
            className="object-cover w-full h-full rounded-md"
          />
        </div>

        <div className="flex flex-col justify-center w-full md:w-1/2 md:pl-6">
          <div className="flex justify-between -mt-1">
            <p className="flex gap-1 text-sm text-blue-600 ">
              {nft.category}
              <CiCircleCheck className="text-sm text-white bg-green-500 rounded-full mt-0.5" />
            </p>
            <div className="">
              <Button
                onClick={handleLike}
                className="transition l-2 text-white-500 txt-sm hover:text-red-700 "
                size="sm"
                color="white"
                variant="bordered"
              >
                {isLiked ? (
                  <AiFillHeart className="text-sm" />
                ) : (
                  <AiOutlineHeart className="text-sm" />
                )}
                {likes}
              </Button>
            </div>
          </div>
          <div>
            <h2 className="mb-2 text-3xl font-semibold">{nft.name}</h2>
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

          {/* Bid Form */}
          <div className="p-6 border rounded-2xl">
            <p className="text-sm ">
              Current Bid: <span className="font-semibold">{currentBid} $</span>
            </p>
            <div className="flex gap-3">
              <input
                type="number"
                placeholder="Enter your bid $"
                value={bidValue}
                onChange={(e) => setBidValue(e.target.value)}
                className="p-2 mt-3 mb-4 text-sm text-white bg-gray-700 rounded-md w-36"
              />
              <div className="mx-4 border-l border-gray-300"></div>

              {/* Countdown Section */}
              <div>
                <div className="text-sm text-zinc-400">Time Left</div>
                <Countdown renderer={renderer} date={Date.now() + 100000000} />
              </div>
            </div>

            {/* Place Bid Button */}
            <div className="flex justify-center mt-4">
              <Button
                onClick={handleBid}
                className="w-full px-4 py-2 transition bg-blue-500 rounded hover:bg-blue-600 md:w-1/2"
                color="primary"
                size="full"
              >
                Place Bid
              </Button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default BitPage;
