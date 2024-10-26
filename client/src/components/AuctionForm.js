"use client";
import React, { useEffect, useState } from "react";
import {
  Button,
  Input,
  Select,
  SelectItem,
  Textarea,
  DateRangePicker,
  useDisclosure,
} from "@nextui-org/react";
import NftModal from "@/components/NftModal";
import axios from "axios";
import { apiLink, getToken } from "@/configs";
import { parseAbsoluteToLocal } from "@internationalized/date";

function AuctionForm({ handleSubmit, update }) {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [list, setList] = useState([]);
  const [title, setTitle] = useState("");
  const [startingPrice, setStartingPrice] = useState(0);
  const [description, setDescription] = useState("");
  const [nftId, setNftId] = useState(null);
  const [category, setCategory] = useState("art");
  const [date, setDate] = React.useState({
    start: parseAbsoluteToLocal(new Date().toISOString()),
    end: parseAbsoluteToLocal(new Date().toISOString()),
  });

  useEffect(() => {
    axios
      .get(
        apiLink + "/api/Nft/user",

        {
          headers: {
            Authorization: getToken(),
          },
        }
      )
      .then((res) => setList(res.data))
      .catch((er) => console.log(er));
    if (update) {
      setTitle(update.title);
      setStartingPrice(update.price);
      setDescription(update.description);
      setNftId(update.nftId);
      setDate({
        start: parseAbsoluteToLocal(
          new Date(update.startDate.slice(0, 23) + "Z").toISOString()
        ),
        end: parseAbsoluteToLocal(
          new Date(update.endDate.slice(0, 23) + "Z").toISOString()
        ),
      });
    }
  }, []);

  function Submit(e) {
    e.preventDefault();
    handleSubmit(title, description, startingPrice, nftId, date, category);
  }

  return (
    <>
      <form
        onSubmit={Submit}
        className="bg-zinc-900 p-5 rounded-xl flex flex-col gap-2"
      >
        <div className="flex gap-2 flex-col lg:flex-row">
          <Input
            type="text"
            className="w-1/2 max-lg:w-full"
            variant={"bordered"}
            label="Title"
            isRequired
            validationBehavior="native"
            value={title}
            validate={(val) => parseFloat(val) > 0}
            onChange={(e) => setTitle(e.target.value)}
          />
          <DateRangePicker
            className="w-1/2 max-lg:w-full"
            hideTimeZone
            granularity="second"
            label="Start and End Date"
            variant={"bordered"}
            value={date}
            onChange={(date) => {
              setDate(date);
            }}
          />
        </div>
        <div className="flex gap-2 flex-col lg:flex-row">
          <Input
            type="number"
            className={update ? "w-1/2 max-lg:w-full" : "w-1/3 max-lg:w-full"}
            variant={"bordered"}
            label="Starting Price"
            isRequired
            validationBehavior="native"
            value={startingPrice}
            onChange={(e) => setStartingPrice(e.target.value)}
          />
          <Select
            label="Select Category"
            className={update ? "w-1/2 max-lg:w-full" : "w-1/3 max-lg:w-full"}
            variant={"bordered"}
            isRequired
            validationBehavior="native"
            value={category}
            onChange={(e) => setCategory(e.target.value)}
          >
            <SelectItem key={"cartoon"}>Cartoon</SelectItem>
            <SelectItem key={"music"}>Music</SelectItem>
            <SelectItem key={"art"}>Art</SelectItem>
          </Select>
          {!update && (
            <div className="w-1/3 flex gap-2 max-lg:w-full">
              <Select
                label="Select Nft"
                className="w-3/4"
                variant={"bordered"}
                isRequired
                validationBehavior="native"
                value={nftId}
                onChange={(e) => setNftId(e.target.value)}
              >
                {list.map((item) => (
                  <SelectItem key={item.id}>{item.title}</SelectItem>
                ))}
              </Select>
              <Button
                onPress={onOpen}
                size="lg"
                className="w-1/4"
                color="primary"
                variant="faded"
              >
                Add NFT
              </Button>
            </div>
          )}
        </div>
        <Textarea
          label="Description"
          className="w-full"
          variant={"bordered"}
          isRequired
          validationBehavior="native"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
        />
        <Button
          isDisabled={
            update && (update.status === "Close" || update.status === "Sold")
          }
          type="submit"
          size="lg"
          color="success"
          variant="faded"
        >
          {update ? "Update Auction" : "Create Auction"}
        </Button>
      </form>
      <NftModal
        isOpen={isOpen}
        onClose={onClose}
        list={list}
        setList={setList}
        setNftId={setNftId}
      />
    </>
  );
}

export default AuctionForm;
