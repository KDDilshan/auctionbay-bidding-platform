"use client";
import { Image } from "@nextui-org/react";
import React from "react";
import Countdown from "react-countdown";

function ItemCard() {
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
    <div className="bg-zinc-900 transition ease-in-out duration-300 hover:bg-zinc-800 relative hover:bottom-2 w-[280px] shrink-0 grow-0 rounded-xl overflow-hidden">
      <div className="w-full h-[170px] overflow-hidden">
        <Image
          radius="none"
          removeWrapper
          isZoomed
          classNames={{
            img: "object-cover w-full h-full",
          }}
          loading="lazy"
          src="https://i.seadn.io/gcs/files/cf409610bec578435f454b9cf78da72f.png?auto=format&dpr=1&h=500"
        />
      </div>
      <div className="p-4 flex flex-col gap-1">
        <h1 className=" font-bold">Blast Royale: Corpos</h1>
        <div className="text-sm text-zinc-300 flex justify-between">
          <p>USDT 300.00</p>
          <p>12 Bids</p>
        </div>
        <div className="flex  justify-between">
          <div>
            <div className="text-sm  text-zinc-400">Time Left</div>
            <Countdown renderer={renderer} date={Date.now() + 100000000} />
          </div>
          <div className="flex flex-col items-end">
            <div className="text-sm text-zinc-400">Current Bid</div>
            <div>USD 3000.00</div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ItemCard;
