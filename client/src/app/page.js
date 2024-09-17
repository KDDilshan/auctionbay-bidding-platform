"use client";

import Categories from "@/components/Categories";
import Creators from "@/components/Creators";
import Hero from "@/components/Hero";
import ShowCase from "@/components/ShowCase";
import CreatorCard from "@/components/CreatorCard";

export default function Home() {
  return (
    <>
    <main className="flex flex-col gap-5">
      <Hero/>
      <Categories/>
      <Creators />
      <ShowCase />
      <ShowCase />
    </main>
    </>
  );
}
