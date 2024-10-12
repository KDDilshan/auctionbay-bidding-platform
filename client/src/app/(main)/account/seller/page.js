"use client";
import FormInputs from "@/components/FormInputs";
import Status from "@/components/Status";
import { apiLink, getToken, toastConfig } from "@/configs";
import { getLocalTimeZone, today } from "@internationalized/date";
import { Button, DatePicker, Input } from "@nextui-org/react";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";

function page() {
  const [date, setDate] = useState(
    today(getLocalTimeZone()).subtract({ days: 265 * 18 })
  );
  const [address, setAddress] = useState("");
  const [id, setId] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [status, setStatus] = useState(null);

  function handleSubmit(e) {
    const data = new FormData();
    data.set("DateOfBirth", date);
    data.set("Address", address);
    data.set("Id", id);
    e.preventDefault();
    setIsLoading(true);
    axios
      .post(apiLink + "/api/SellerRequests", data, {
        headers: {
          Authorization: getToken(), //jwt token
          "Content-Type": "multipart/form-data", // Important for file upload
        },
      })
      .then((res) => {
        toast.success("Success", toastConfig);
        setStatus("Pending");
      })
      .catch((er) => toast.error("Failed", toastConfig))
      .finally(() => setIsLoading(false));
  }

  useEffect(() => {
    axios
      .get(apiLink + "/api/SellerRequests/check", {
        headers: { Authorization: getToken() },
      })
      .then((res) => {
        setStatus(res.data.message);
      })
      .catch((er) => setStatus("None"));
  }, []);
  return (
    <>
      <h1>Marketplace Seller</h1>
      <p>
        Become a marketplace seller and start auctioning your NFTs to a global
        audience.
      </p>
      {status && status != "None" && <Status status={status} />}
      {(status == "None" || status == "Rejected") && (
        <>
          <h2>Seller Account Request</h2>
          <p>
            Submit your request to become a seller. Your account will be
            reviewed by an admin, and upon approval, your seller account will be
            created automatically.
          </p>
          <form onSubmit={handleSubmit}>
            <DatePicker
              label="Date of Birth"
              className="account-input"
              variant={"bordered"}
              isRequired
              showMonthAndYearPickers
              validationBehavior="native"
              maxValue={today(getLocalTimeZone()).subtract({ days: 265 * 18 })}
              value={date}
              onChange={(date) => setDate(date)}
            />
            <Input
              type="text"
              className="account-input"
              variant={"bordered"}
              label="Address"
              isRequired
              validationBehavior="native"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
            />
            <p className="pt-3 mb-1">Upload a clear, valid photo of your ID.</p>
            <FormInputs.ImagePicker setState={setId} value={id} />
            <Button
              type="submit"
              className="account-button"
              color="success"
              variant="faded"
              isLoading={isLoading}
            >
              Submit Request
            </Button>
          </form>
        </>
      )}
    </>
  );
}

export default page;
