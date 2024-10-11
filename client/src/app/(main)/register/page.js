"use client";
import React, { useState } from "react";
import { Button, Input } from "@nextui-org/react";
import Link from "next/link";
import FormInputs from "@/components/FormInputs";
import axios from "axios";
import { toast } from "react-toastify";
import { apiLink, toastConfig } from "@/configs";
import { useRouter } from "next/navigation";
function page() {
  const [firstName, setfirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [rePassword, setRePassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const { push } = useRouter();
  function register(e) {
    e.preventDefault();
    setIsLoading(true);
    axios
      .post(apiLink + "/api/User/register", {
        firstName,
        lastName,
        email,
        password,
      })
      .then((res) => {
        toast.success("Account created successfully", toastConfig);
        push("/login");
      })
      .catch((er) => {
        toast.error(er.response?.data[0].description, toastConfig);
      })
      .finally(() => setIsLoading(false));
  }
  return (
    <div className="container-full flex justify-center items-center grow">
      <form
        onSubmit={register}
        className="max-w-sm flex flex-col items-center gap-2"
      >
        <h1 className=" font-bold text-2xl">Create an account</h1>
        <p className="text-sm">Enter your email below to create your account</p>
        <div className="flex gap-1">
          <Input
            type="text"
            variant={"bordered"}
            label="First Name"
            isRequired
            validationBehavior="native"
            value={firstName}
            onChange={(e) => setfirstName(e.target.value)}
          />
          <Input
            type="text"
            variant={"bordered"}
            label="Last Name"
            isRequired
            validationBehavior="native"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
        </div>
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
        <FormInputs.Password
          lable={"Confirm Password"}
          value={rePassword}
          setState={setRePassword}
          validate={(t) => (password != t ? "Password does not match" : true)}
        />
        <Button type="submit" className="w-full" isLoading={isLoading}>
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
