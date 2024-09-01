import { Input } from '@nextui-org/react';
import React, { useState } from 'react'
import { HiEyeSlash, HiMiniEye } from 'react-icons/hi2';

function PasswordInput({lable}) {
    const [passVisible, setPassVisible] = useState(false);
  return (
    <Input
      type={passVisible ? "text" : "password"}
      variant={"bordered"}
      label={lable}
      isRequired
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

export default PasswordInput
