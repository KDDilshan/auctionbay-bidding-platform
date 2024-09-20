"use client";
import React, { useEffect, useState } from "react";

function Status({ status }) {
  const [color, setColor] = useState("zinc");
  const [text, setText] = useState(
    "Your request has been submitted successfully and is currently under review. You will be notified once the review is complete."
  );
  useEffect(() => {
    if (status) {
      if (status === "Approved") {
        setColor("green");
        setText(
          "Congratulations! Your request has been approved. You can now visit your Seller Account to start managing your listings."
        );
      } else if (status === "Rejected") {
        setColor("red");
        setText(
          "Unfortunately, your request has been declined. Please try again or make any necessary changes before resubmitting."
        );
      } else if (status === "Pending") {
        setColor("zinc");
        setText(
          "Your request has been submitted successfully and is currently under review. You will be notified once the review is complete."
        );
      }
    }
  }, [status]);
  return (
    <div
      className={`p-5 border-l-4 bg-zinc-800 mt-2 border-${color}-500 text-${color}-500 text-md`}
    >
      {text}
    </div>
  );
}

export default Status;
