"use client";
import React, { useState } from "react";
import { Button } from "@nextui-org/react";
import Link from "next/link";
import FormInputs from "@/components/FormInputs";
function page() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
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
        <FormInputs.Email
          lable={"Email address"}
          value={email}
          setState={setEmail}
        />
        <FormInputs.Password
          lable={"Password"}
          value={password}
          setState={setPassword}
        />
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
