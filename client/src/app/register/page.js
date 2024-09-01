"use client";
import React, { useState } from "react";
import { Button, ButtonGroup, Input } from "@nextui-org/react";
import { HiMiniEye, HiEyeSlash } from "react-icons/hi2";
import PasswordInput from "@/components/PasswordInput";
import Link from "next/link";
function page() {
  function register(e) {
    e.preventDefault();
    console.log("registered");
  }
  return (
    <div className="container-full flex justify-center items-center grow">
      <form
        onSubmit={register}
        className="max-w-sm flex flex-col items-center gap-2"
      >
        <h1 className=" font-bold text-xl">Create an account</h1>
        <p className="text-sm">Enter your email below to create your account</p>
        <div className="flex gap-1">
          <Input
            className="w-[50%]"
            type="text"
            variant={"bordered"}
            label="First Name"
            isRequired
          />
          <Input
            className="w-[50%]"
            type="text"
            variant={"bordered"}
            label="Last Name"
            isRequired
          />
        </div>
        <Input
          isRequired
          type="email"
          variant={"bordered"}
          label="Email address"
        />
        <PasswordInput lable={"Password"} />
        <PasswordInput lable={"Confirm Password"} />
        <Button type="submit" className="w-full">
          Sign up
        </Button>
        <p className="text-sm">
          <span className="text-gray-400">Already have an account?</span>{" "}
          <span className="text-white font-bold">
            <Link href={"/login"}>Sign in here</Link>
          </span>
        </p>
      </form>
    </div>
  );
}

export default page;
