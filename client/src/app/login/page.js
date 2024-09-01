"use client";
import React, { useState } from "react";
import { Button, ButtonGroup, Input } from "@nextui-org/react";
import { HiMiniEye, HiEyeSlash } from "react-icons/hi2";
import Link from "next/link";
function page() {
  const [passVisible, setPassVisible] = useState(false);
  return (
    <div className="container-full flex justify-center items-center grow">
      <div className="max-w-xs flex flex-col items-center gap-2">
        <h1 className="font-bold text-xl">Welcome Back</h1>
        <p className="text-sm">
          Please enter your login credentials to continiue
        </p>
        <Input type="email" variant={"bordered"} label="Email address" />
        <Input
          type={passVisible ? "text" : "password"}
          variant={"bordered"}
          label="Password"
          endContent={
            <button
              className="focus:outline-none"
              type="button"
              onClick={() => setPassVisible(!passVisible)}
              aria-label="toggle password visibility"
            >
              {passVisible ? (
                <HiEyeSlash className="text-2xl text-default-400 pointer-events-none" />
              ) : (
                <HiMiniEye className="text-2xl text-default-400 pointer-events-none" />
              )}
            </button>
          }
        />
        <Button className="w-full">Sign in</Button>
        <p className="text-sm">
          <span className="text-gray-400">First time here?</span>{" "}
          <span className="text-white font-bold">
            <Link href={"/register"}>Sign up for free</Link>
          </span>
        </p>
      </div>
    </div>
  );
}

export default page;
