"use client";
import { apiLink, formatCurrency } from "@/configs";
import { useRouter } from "next/navigation";
import React from "react";
import Countdown from "react-countdown";

function ItemCard({ item }) {
  const router = useRouter();
  const renderer = ({ days, hours, minutes, seconds, completed }) => {
    if (completed) {
      // Render a completed state
      return <div>{item.status == "Open" ? "Closed" : item.status}</div>;
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
    <div
      onClick={() => router.replace("/auction/" + item.id)}
      className="bg-zinc-900 transition ease-in-out duration-300 hover:bg-zinc-800 relative hover:bottom-2 w-[280px] shrink-0 grow-0 rounded-xl overflow-hidden cursor-pointer"
    >
      <div className="w-full h-[170px] overflow-hidden">
        <img
          src={apiLink + "/wwwroot/uploads/" + item.image}
          className="object-cover w-full h-full hover:scale-105 transition ease-in-out duration-300"
          alt={item.title}
        />
      </div>
      <div className="p-4 flex flex-col gap-1">
        <h1 className=" font-bold">{item.title}</h1>
        <div className="flex  justify-between">
          <div>
            <div className="text-sm  text-zinc-400">Time Left</div>
            <Countdown
              renderer={renderer}
              date={new Date(item.endDate.slice(0, 23) + "Z").toLocaleString()}
            />
          </div>
          <div className="flex flex-col items-end">
            <div className="text-sm text-zinc-400">Current Bid</div>
            <div>{formatCurrency(item.currentBid)}</div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ItemCard;
