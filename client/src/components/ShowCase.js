"use client";
import React, { useEffect, useState } from "react";
import ItemCard from "./ItemCard";
import useEmblaCarousel from "embla-carousel-react";
import {
  NextButton,
  PrevButton,
  usePrevNextButtons,
} from "./EmblaCarouselArrowButtons";
import axios from "axios";
import { apiLink, getToken } from "@/configs";

function ShowCase({ title, category }) {
  const [items, setItems] = useState([]);
  const [emblaRef, emblaApi] = useEmblaCarousel({
    align: "start",
  });
  const {
    prevBtnDisabled,
    nextBtnDisabled,
    onPrevButtonClick,
    onNextButtonClick,
  } = usePrevNextButtons(emblaApi);
  useEffect(() => {
    axios
      .get(apiLink + "/api/Auctions/category/" + category)
      .then((res) => setItems(res.data).slice(0, 10))
      .catch((er) => console.log(er));
  }, []);
  return items.length > 0 ? (
    <section className="container-w flex flex-col gap-1">
      <h1 className="max-lg:px-6 lg:px-8 text-2xl font-bold">{title}</h1>
      <div className="w-full lg:px-8 flex relative group/show">
        <PrevButton onClick={onPrevButtonClick} disabled={prevBtnDisabled} />
        <div className="embla__viewport max-lg:px-6" ref={emblaRef}>
          <div className="embla__container gap-4 py-2">
            {items.map((item, index) => (
              <ItemCard key={item.id} item={item} />
            ))}
          </div>
        </div>
        <NextButton onClick={onNextButtonClick} disabled={nextBtnDisabled} />
      </div>
    </section>
  ) : null;
}

export default ShowCase;
