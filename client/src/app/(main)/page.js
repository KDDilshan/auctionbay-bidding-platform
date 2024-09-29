"use client";

import BitPage from "@/components/BitPage";
import Categories from "@/components/Categories";
import Creators from "@/components/Creators";
import Hero from "@/components/Hero";
import ShowCase from "@/components/ShowCase";

export default function Home() {
  return (
    <>
    <main className="flex flex-col gap-5">
      <Hero/>
      <Categories/>
      <Creators />
      <BitPage/>
      <ShowCase />
      <ShowCase />
    </main>
    </>
  );
}
