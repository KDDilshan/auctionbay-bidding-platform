"use client";
import React, { useState } from "react";
import { Button, ButtonGroup, Input } from "@nextui-org/react";
import { HiMiniEye, HiEyeSlash } from "react-icons/hi2";
import Link from "next/link";
import PasswordInput from "@/components/PasswordInput";
function page() {
  function login(e) {
    e.preventDefault();
    console.log("registered");
  }
  return (
    <div className="container-full flex justify-center items-center grow">
      <form
        onSubmit={login}
        className="max-w-sm w-full flex flex-col items-center gap-2"
      >
        <h1 className="font-bold text-xl">Welcome Back</h1>
        <p className="text-sm">
          Please enter your login credentials to continiue
        </p>
        <Input type="email" variant={"bordered"} label="Email address" />
        <PasswordInput lable={"Password"} />
        <Button type="submit" className="w-full">
          Sign in
        </Button>
        <p className="text-sm">
          <span className="text-gray-400">First time here?</span>{" "}
          <span className="text-white font-bold">
            <Link href={"/register"}>Sign up for free</Link>
          </span>
        </p>
      </form>
    </div>
  );
}

export default page;
