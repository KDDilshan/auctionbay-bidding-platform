"use client";
import ItemCard from "@/components/ItemCard";
import { apiLink } from "@/configs";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { useSearchParams } from "next/navigation";

function page() {
  const [auctions, setAuctions] = useState([]);
  const searchParams = useSearchParams();
  const search = searchParams.get("search");
  useEffect(() => {
    axios
      .post(apiLink + "/api/Auctions/search", {
        searchString: search ? search : "",
      })
      .then((res) => setAuctions(res.data))
      .catch((er) => console.log(er));
  }, [search]);
  return (
    <div className="container-full">
      <h1 className="text-2xl font-bold mt-5">Search Auctions</h1>
      {auctions.length == 0 && (
        <p className=" text-zinc-500"> results found.</p>
      )}

      <div className="pt-5 flex flex-wrap w-full justify-start gap-5">
        {auctions.map((item, index) => (
          <ItemCard key={item.id} item={item} />
        ))}
      </div>
    </div>
  );
}

export default page;
