"use client";
import { UserContext } from "@/app/providers";
import FormInputs from "@/components/FormInputs";
import { apiLink, getToken, toastConfig } from "@/configs";
import { Button, Input } from "@nextui-org/react";
import axios from "axios";
import React, { useContext, useEffect, useState } from "react";
import { toast } from "react-toastify";

function page() {
  const { userInfo, setUserInfo } = useContext(UserContext);

  const [FirstName, setFirstName] = useState("");
  const [LastName, setLastName] = useState("");
  const [pd, setPd] = useState(false);

  const [email, setEmail] = useState("");
  const [ePassword, setEPassword] = useState("");
  const [emailLoading, setEmailLoading] = useState(false);

  const [dPassword, setDPassword] = useState("");

  useEffect(() => {
    if (userInfo) {
      setEmail(userInfo.email);
      setFirstName(userInfo.firstName);
      setLastName(userInfo.lastName);
    }
  }, []);

  const savePersonalDetails = (e) => {
    e.preventDefault();
    setPd(true);
    axios
      .put(
        apiLink + "/api/User/UpdateUserNames",
        { FirstName, LastName },
        { headers: { Authorization: getToken() } }
      )
      .then((res) => {
        toast.success("Personal Details Updated", toastConfig),
          setUserInfo({
            ...userInfo,
            firstName: FirstName,
            lastName: LastName,
          });
      })
      .catch((er) =>
        toast.error("Failed to update personal details", toastConfig)
      )
      .finally(() => setPd(false));
  };

  const changeEmail = (e) => {
    e.preventDefault();
    setEmailLoading(true);
    axios
      .put(
        apiLink + "/api/User/UpdateUserEmail",
        { Email: email, Password: ePassword },
        { headers: { Authorization: getToken() } }
      )
      .then((res) => {
        toast.success("Email Updated", toastConfig),
          setUserInfo({
            ...userInfo,
            email: email,
          });
      })
      .catch((er) => toast.error("Failed to update email", toastConfig))
      .finally(() => setEmailLoading(false));
  };
  return (
    <>
      <h1>Account Settings</h1>
      <p>Manage your account’s details.</p>
      <h2>Personal Details</h2>
      <p>
        Manage your name and contact info. These personal details are private
        and will not be displayed to other users.{" "}
      </p>
      <form onSubmit={savePersonalDetails}>
        <div className="account-input-container">
          <Input
            type="text"
            className="account-input"
            variant={"bordered"}
            label="First Name"
            isRequired
            validationBehavior="native"
            value={FirstName}
            onChange={(e) => setFirstName(e.target.value)}
          />
          <Input
            type="text"
            className="account-input"
            variant={"bordered"}
            label="Last Name"
            isRequired
            validationBehavior="native"
            value={LastName}
            onChange={(e) => setLastName(e.target.value)}
          />
        </div>
        <Button
          type="submit"
          className="account-button"
          color="success"
          variant="faded"
          isLoading={pd}
        >
          Save Changes
        </Button>
      </form>
      <h2>Change Email Address</h2>
      <p>
        You will receive a verification email at your new address to confirm the
        change. Please note that all account notifications, including bidding
        alerts and transaction confirmations, will be sent to your new email
        address once the change is complete.
      </p>
      <form onSubmit={changeEmail}>
        <div className="account-input-container">
          <FormInputs.Email
            className="account-input"
            lable="New Eamil"
            value={email}
            setState={setEmail}
          />
          <FormInputs.Password
            className="account-input"
            lable="Current Password"
            value={ePassword}
            setState={setEPassword}
          />
        </div>
        <Button
          type="submit"
          className="account-button"
          color="success"
          variant="faded"
          isLoading={emailLoading}
        >
          Save Changes
        </Button>
      </form>
      <h2>Delete Account</h2>
      <p>
        Click REQUEST ACCOUNT DELETE to initiate the process of permanently
        deleting your account, including all personal information, transaction
        history, bids, NFT collections, and any remaining balances in your
        account wallet. Once your account is deleted, all associated data,
        including your wallet balance, will be permanently removed and cannot be
        recovered.
      </p>
      <form>
        <FormInputs.Password
          className="account-input"
          lable="Current Password"
          value={dPassword}
          setState={setDPassword}
        />
        <Button
          type="submit"
          className="account-button"
          color="danger"
          variant="faded"
        >
          REQUEST ACCOUNT DELETE
        </Button>
      </form>
    </>
  );
}

export default page;
