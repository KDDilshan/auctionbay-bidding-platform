import { Image, Input } from "@nextui-org/react";
import React from "react";
import Countdown from "react-countdown";
import ItemCard from "./ItemCard";

function ShowCase() {
  
  return (
    <div className="container-full flex gap-3">
      <ItemCard />
      <ItemCard />
      <ItemCard />
      <ItemCard />
      <ItemCard />
    </div>
  );
}

export default ShowCase;
