import { Image, Input } from "@nextui-org/react";
import React from "react";
import Countdown from "react-countdown";
import ItemCard from "./ItemCard";
import useEmblaCarousel from "embla-carousel-react";

function ShowCase() {
  const [emblaRef, emblaApi] = useEmblaCarousel({
    align: "start",
    
  });
  return (
    <section className="container-full">
      <div className="embla__viewport" ref={emblaRef}>
        <div className="embla__container gap-4">
          <ItemCard key={1} />
          <ItemCard key={2} />
          <ItemCard key={3} />
          <ItemCard key={4} />
          <ItemCard key={5} />
          <ItemCard key={6} />
          <ItemCard key={7} />
          <ItemCard key={8} />
          <ItemCard key={9} />
          <ItemCard key={10} />
        </div>
      </div>
    </section>
  );
}

export default ShowCase;
