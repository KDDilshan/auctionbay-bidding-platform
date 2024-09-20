"use client";
import { Image, Input } from "@nextui-org/react";
import validator from "validator";
import React, { useState } from "react";
import { HiEyeSlash, HiMiniEye } from "react-icons/hi2";
import { MdAddPhotoAlternate } from "react-icons/md";
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

function ImagePicker({ value, setState, className }) {
  return (
    <div className="w-32 aspect-square rounded-xl relative flex flex-col items-center justify-center border-zinc-700 border-2 hover:border-zinc-500 text-zinc-500 over overflow-hidden">
      <input
        type="file"
        id="id"
        name="id"
        accept="image/*"
        className="w-full h-full opacity-0 absolute top-0 left-0 cursor-pointer bg-white"
        required
        onChange={(e) => setState(e.target.files[0])}
      />
      {value?.name ? (
        <img
          className="h-full w-full object-cover"
          src={URL.createObjectURL(value)}
        />
      ) : (
        <MdAddPhotoAlternate className="text-2xl" />
      )}
    </div>
  );
}

const FormInputs = { Email, Password, ImagePicker };
export default FormInputs;
