'use client';
import React from "react";
import { FaChevronLeft, FaChevronRight } from "react-icons/fa6";
import ItemCard from "./ItemCard";
import useEmblaCarousel from "embla-carousel-react";
import { NextButton, PrevButton, usePrevNextButtons } from "./EmblaCarouselArrowButtons";

function ShowCase() {
  const [emblaRef, emblaApi] = useEmblaCarousel({
    align: "start",
  });
  const {
    prevBtnDisabled,
    nextBtnDisabled,
    onPrevButtonClick,
    onNextButtonClick,
  } = usePrevNextButtons(emblaApi);
  return (
    <section className="container-w lg:px-8 flex relative group">
      <PrevButton onClick={onPrevButtonClick} disabled={prevBtnDisabled} />
      <div className="embla__viewport max-lg:px-6" ref={emblaRef}>
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
      <NextButton onClick={onNextButtonClick} disabled={nextBtnDisabled} />
    </section>
  );
}

export default ShowCase;
