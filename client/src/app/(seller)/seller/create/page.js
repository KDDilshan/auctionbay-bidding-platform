"use client";
import {
  Button,
  useDisclosure,
  Input,
  Select,
  SelectItem,
  Textarea,
  DateRangePicker,
} from "@nextui-org/react";
import React, { useEffect, useState } from "react";
import NftModal from "@/components/NftModal";
import axios from "axios";
import { apiLink, getToken, toastConfig } from "@/configs";
import { parseAbsoluteToLocal } from "@internationalized/date";
import { toast } from "react-toastify";

function page() {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [list, setList] = useState([]);
  const [title, setTitle] = useState("");
  const [startingPrice, setStartingPrice] = useState(0);
  const [description, setDescription] = useState("");
  const [nftId, setNftId] = useState(null);
  let [date, setDate] = React.useState({
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
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    axios
      .post(
        apiLink + "/api/Auctions",
        {
          title,
          description,
          price: startingPrice,
          nftId,
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

  return (
    <>
      <form
        onSubmit={handleSubmit}
        className="bg-zinc-900 p-5 rounded-xl flex flex-col gap-2"
      >
        <div className="flex gap-2">
          <Input
            type="text"
            className="w-1/2"
            variant={"bordered"}
            label="Title"
            isRequired
            validationBehavior="native"
            value={title}
            validate={(val) => parseFloat(val) > 0}
            onChange={(e) => setTitle(e.target.value)}
          />
          <DateRangePicker
            className="w-1/2"
            hideTimeZone
            granularity="second"
            label="Start and End Date"
            variant={"bordered"}
            value={date}
            onChange={(date) => {
              setDate(date);
              console.log(date.start.toDate(), date.end.toDate());
            }}
          />
        </div>
        <div className="flex gap-2 ">
          <Input
            type="number"
            className="w-1/2"
            variant={"bordered"}
            label="Starting Price"
            isRequired
            validationBehavior="native"
            value={startingPrice}
            onChange={(e) => setStartingPrice(e.target.value)}
          />
          <div className="w-1/2 flex gap-2">
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
              Add new NFT
            </Button>
          </div>
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
        <Button type="submit" size="lg" color="success" variant="faded">
          Create Auction
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

export default page;
