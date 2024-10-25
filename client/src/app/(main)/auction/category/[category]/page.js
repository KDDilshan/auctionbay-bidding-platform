"use client";
import ItemCard from "@/components/ItemCard";
import Loading from "@/components/Loading";
import { apiLink } from "@/configs";
import axios from "axios";
import React, { useEffect, useState } from "react";

function page({ params }) {
  const [items, setItems] = useState([]);
  const [loading, setLoading] = useState(false);
  useEffect(() => {
    axios
      .get(apiLink + "/api/Auctions/category/" + params.category)
      .then((res) => setItems(res.data))
      .catch((er) => console.log(er))
      .finally(() => setLoading(false));
  }, [params.category]);

  if (loading) return <Loading />;

  return (
    <div className="container-full">
      <h1 className="text-2xl font-bold mt-5">
        {params.category.charAt(0).toUpperCase() + params.category.slice(1)}{" "}
        Category
      </h1>
      {items.length == 0 && (
        <p className=" text-zinc-500">
          There is no auctions found from this category.
        </p>
      )}
      <div className="pt-5">
        {items.map((item, index) => (
          <ItemCard key={item.id} item={item} />
        ))}
      </div>
    </div>
  );
}

export default page;
