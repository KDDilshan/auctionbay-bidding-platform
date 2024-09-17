import { Button } from "@nextui-org/react";
import React from "react";
import Image from "next/image";

function Hero() {
  return (
    <div className="relative flex items-center justify-between px-6 py-5 container-w md:px-8 lg:px-32">         
      <div>
        <h1 className="font-extrabold leading-tight text-white text-7xl">
          Discover & Collect <br />
          <span className="text-yellow-500"> Super Rare </span>
          Digital <br /> Artworks
        </h1>

        <p className="mt-4 text-lg text-white">
          Buy, sell, and discover exclusive digital assets in the world's most
          vibrant NFT marketplace.
          <br />
          Join a global community of collectors and creators,
          <br />
          and unlock endless opportunities to own and trade rare digital art
          pieces.
        </p>

        <div className="mt-6 space-x-4">
          <Button
            radius="full"
            size="md"
            className="text-white shadow-lg bg-gradient-to-tr from-blue-300 to-blue-950"
          >
            Let's Explore
          </Button>
          <Button
            radius="full"
            size="md"
            color="primary"
            variant="bordered"
            className="text-white"
          >
            Sell NFT
          </Button>
        </div>
      </div>

      <div className="max-md:hidden">
        <Image
          src="/Hero Image.webp"
          alt="Hero Image"
          width={600}
          height={500}
          className="rounded-[40px]"
        />
      </div>
    </div>
  );
}

export default Hero;
