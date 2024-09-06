"use client";
import React, { useCallback, useEffect, useState } from "react";
import { FaChevronLeft, FaChevronRight } from "react-icons/fa6";
export const usePrevNextButtons = (emblaApi) => {
  const [prevBtnDisabled, setPrevBtnDisabled] = useState(true);
  const [nextBtnDisabled, setNextBtnDisabled] = useState(true);

  const onPrevButtonClick = useCallback(() => {
    if (!emblaApi) return;
    emblaApi.scrollPrev();
  }, [emblaApi]);

  const onNextButtonClick = useCallback(() => {
    if (!emblaApi) return;
    emblaApi.scrollNext();
  }, [emblaApi]);

  const onSelect = useCallback((emblaApi) => {
    setPrevBtnDisabled(!emblaApi.canScrollPrev());
    setNextBtnDisabled(!emblaApi.canScrollNext());
  }, []);

  useEffect(() => {
    if (!emblaApi) return;

    onSelect(emblaApi);
    emblaApi.on("reInit", onSelect).on("select", onSelect);
  }, [emblaApi, onSelect]);

  return {
    prevBtnDisabled,
    nextBtnDisabled,
    onPrevButtonClick,
    onNextButtonClick,
  };
};

export const PrevButton = (props) => {
  const { onClick, disabled } = props;

  return (
    <div className="absolute top-0 left-0 w-8 h-full hidden lg:flex">
      <Btn onClick={onClick} disabled={disabled}>
        <FaChevronLeft />
      </Btn>
    </div>
  );
};

export const NextButton = (props) => {
  const { onClick, disabled } = props;

  return (
    <div className="absolute top-0 right-0 w-8 h-full hidden lg:flex">
      <Btn onClick={onClick} disabled={disabled}>
        <FaChevronRight />
      </Btn>
    </div>
  );
};

const Btn = ({ children, onClick, disabled }) => {
  return (
    <>
      {!disabled && (
        <button
          className="h-full w-full flex items-center justify-center invisible group-hover:visible hover:bg-zinc-800 rounded-xl text-2xl"
          type="button"
          onClick={onClick}
          disabled={disabled}
        >
          {children}
        </button>
      )}
    </>
  );
};
