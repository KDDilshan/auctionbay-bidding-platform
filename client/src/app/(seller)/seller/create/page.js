"use client";
import {
  Button,
  useDisclosure,
  DatePicker,
  Input,
  Select,
  SelectItem,
  Textarea,
} from "@nextui-org/react";
import React from "react";
import { now, getLocalTimeZone } from "@internationalized/date";
import NftModal from "@/components/NftModal";

function page() {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const animals = [
    { key: "cat", label: "Cat" },
    { key: "dog", label: "Dog" },
    { key: "elephant", label: "Elephant" },
    { key: "lion", label: "Lion" },
    { key: "tiger", label: "Tiger" },
    { key: "giraffe", label: "Giraffe" },
    { key: "dolphin", label: "Dolphin" },
    { key: "penguin", label: "Penguin" },
    { key: "zebra", label: "Zebra" },
    { key: "shark", label: "Shark" },
    { key: "whale", label: "Whale" },
    { key: "otter", label: "Otter" },
    { key: "crocodile", label: "Crocodile" },
  ];
  return (
    <>
      <div className="bg-zinc-900 p-5 rounded-xl flex flex-col gap-2">
        <div className="flex gap-2">
          <Input
            type="text"
            className="w-1/2"
            variant={"bordered"}
            label="Title"
            isRequired
            validationBehavior="native"
          />
          <DatePicker
            label="Start Date"
            className="w-1/4"
            variant={"bordered"}
            isRequired
            hideTimeZone
            showMonthAndYearPickers
            validationBehavior="native"
            defaultValue={now(getLocalTimeZone())}
          />
          <DatePicker
            label="End Date"
            className="w-1/4"
            variant={"bordered"}
            isRequired
            hideTimeZone
            showMonthAndYearPickers
            validationBehavior="native"
            defaultValue={now(getLocalTimeZone())}
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
          />
          <div className="w-1/2 flex gap-2 items-center">
            <Select
              label="Select Nft"
              className="w-3/4"
              variant={"bordered"}
              isRequired
              validationBehavior="native"
            >
              {animals.map((animal) => (
                <SelectItem key={animal.key}>{animal.label}</SelectItem>
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
        />
        <Button size="lg" color="success" variant="faded">
          Create Auction
        </Button>
      </div>
      <NftModal isOpen={isOpen} onClose={onClose} />
    </>
  );
}

export default page;
