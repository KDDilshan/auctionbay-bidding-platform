"use client";
import React, { useContext, useState } from "react";
import { Button } from "@nextui-org/react";
import Link from "next/link";
import FormInputs from "@/components/FormInputs";
import axios from "axios";
import { apiLink, toastConfig } from "@/configs";
import { toast } from "react-toastify";
import { useRouter } from "next/navigation";
import { UserContext } from "../../providers";
function page() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const { push } = useRouter();
  const { setUserInfo } = useContext(UserContext);

  function login(e) {
    e.preventDefault();
    setIsLoading(true);
    axios
      .post(apiLink + "/api/User/login", { email, password })
      .then((res) => {
        toast.success(res.data.message, toastConfig);
        setUserInfo({
          email: res.data.email,
          firstName: res.data.firstName,
          lastName: res.data.lastName,
        });
        localStorage.setItem("token", res.data.token);
        push("/");
      })
      .catch((er) => toast.error(er.response.data.message, toastConfig))
      .finally(() => setIsLoading(false));
  }

  return (
    <div className="container-full flex justify-center items-center grow">
      <form
        onSubmit={login}
        className="max-w-sm w-full flex flex-col items-center gap-2"
      >
        <h1 className="font-bold text-2xl">Welcome Back</h1>
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
        <Button type="submit" className="w-full" isLoading={isLoading}>
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
