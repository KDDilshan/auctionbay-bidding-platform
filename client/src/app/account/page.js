"use client";
import FormInputs from "@/components/FormInputs";
import { Button, Input } from "@nextui-org/react";
import React, { useState } from "react";

function page() {
const[email, setEmail] = useState("");
  return (
    <div className="account">
      <h1>Account Settings</h1>
      <p>Manage your account’s details.</p>
      <h2>Personal Details</h2>
      <p>
        Manage your name and contact info. These personal details are private
        and will not be displayed to other users.{" "}
      </p>
      <form action="#">
        <div className="account-input-container">
          <Input
            type="text"
            className="account-input"
            variant={"bordered"}
            label="First Name"
            isRequired
            validationBehavior="native"
          />
          <Input
            type="text"
            className="account-input"
            variant={"bordered"}
            label="Last Name"
            isRequired
            validationBehavior="native"
          />
        </div>
        <Button
          type="submit"
          className="account-button"
          color="success"
          variant="faded"
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
      <form action="#">
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
            value={email}
            setState={setEmail}
          />
        </div>
        <Button
          type="submit"
          className="account-button"
          color="success"
          variant="faded"
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
      <FormInputs.Password
        className="account-input"
        lable="Current Password"
        value={email}
        setState={setEmail}
      />
      <Button
        type="submit"
        className="account-button"
        color="danger"
        variant="faded"
      >
        REQUEST ACCOUNT DELETE
      </Button>
    </div>
  );
}

export default page;
