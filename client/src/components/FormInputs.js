"use client";
import { Input } from "@nextui-org/react";
import validator from "validator";
import React, { useState } from "react";
import { HiEyeSlash, HiMiniEye } from "react-icons/hi2";

let regex =
  /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@.#$!%*?&])[A-Za-z\d@.#$!%*?&]{8,15}$/;

function Email({ lable, value, setState, className }) {
  return (
    <Input
      isRequired
      type="email"
      className={className ? className : ""}
      variant={"bordered"}
      label={lable}
      value={value}
      validationBehavior="native"
      validate={(t) =>
        t.length > 0 && !validator.isEmail(t) ? "Invalid email address" : null
      }
      onChange={(e) => setState(e.target.value)}
    />
  );
}

function Password({ lable, value, setState, validate, className }) {
  const [passVisible, setPassVisible] = useState(false);
  return (
    <Input
      type={passVisible ? "text" : "password"}
      className={className ? className : ""}
      variant={"bordered"}
      label={lable}
      isRequired
      value={value}
      onChange={(e) => setState(e.target.value)}
      validationBehavior="native"
      validate={(t) =>
        validate
          ? validate(t)
          : t.length > 0 && !regex.test(t)
          ? "Require minimum 8-15 characters, with at least one uppercase, lowercase, number, and special character."
          : true
      }
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
  );
}

const FormInputs = { Email, Password };
export default FormInputs;
