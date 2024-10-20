"use client";
import Categories from "@/components/Categories";
import Creators from "@/components/Creators";
import Hero from "@/components/Hero";
import ShowCase from "@/components/ShowCase";

export default function Home() {
  return (
    <>
      <main className="flex flex-col gap-5">
        <Hero />
        <Categories />
        <Creators />
        <ShowCase title={"Top Collector Buys Today"} category={"new"} />
        <ShowCase title={"Cartoon Collection"} category={"cartoon"} />
        <ShowCase title={"Art Collection"} category={"art"} />
        <ShowCase title={"Music Collection"} category={"music"} />
      </main>
    </>
  );
}
