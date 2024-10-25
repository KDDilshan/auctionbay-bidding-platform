"use client";
import { IoMdEye } from "react-icons/io";
import React, { useEffect, useState } from "react";
import {
  Button,
  User,
  useDisclosure,
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
} from "@nextui-org/react";
import axios from "axios";
import { apiLink, getToken, toastConfig } from "@/configs";
import Image from "next/image";
import { toast } from "react-toastify";
import MyTable from "@/components/Table";

const columns = [
  {
    key: "name",
    label: "NAME",
  },
  {
    key: "date",
    label: "DATE",
  },
  {
    key: "status",
    label: "STATUS",
  },
  {
    key: "action",
    label: "ACTION",
  },
];

function Page() {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const [rows, setRows] = React.useState([]);
  const [image, setImage] = React.useState(null);
  const [selectedItem, setSelectedItem] = React.useState(null);
  const [rejectLoading, setRejectLoading] = useState(false);
  const [acceptLoading, setAcceptLoading] = useState(false);
  const [isLoading, setIsLoading] = useState(true);

  const renderCell = React.useCallback((item, columnKey) => {
    const cellValue = item[columnKey];
    switch (columnKey) {
      case "name":
        return (
          <User
            avatarProps={{
              radius: "lg",
              src: "https://i.pravatar.cc/150?u=a042581f4e29026704d",
            }}
            description={item.email}
            name={cellValue}
          >
            {item.email}
          </User>
        );
      case "action":
        return (
          <Button
            isIconOnly
            color="warning"
            variant="faded"
            aria-label="Take a photo"
            onClick={() => getImage(item)}
          >
            <IoMdEye />
          </Button>
        );
      case "date":
        return new Date(cellValue).toLocaleString();
      default:
        return cellValue;
    }
  });

  useEffect(() => {
    axios
      .get(apiLink + "/api/SellerRequests", {
        headers: { Authorization: getToken() },
      })
      .then((res) => setRows(res.data))
      .catch((er) => console.log(er))
      .then(() => setIsLoading(false));
  }, []);

  const getImage = async (item) => {
    axios
      .get(apiLink + "/api/SellerRequests/" + item.id + "/image", {
        headers: { Authorization: getToken() },
        responseType: "blob",
      })
      .then((res) => {
        setSelectedItem(item);
        setImage(URL.createObjectURL(res.data));
        onOpen();
      })
      .catch((er) => console.log(er));
  };

  const updateStatus = async (status) => {
    axios
      .put(
        apiLink + "/api/SellerRequests/" + selectedItem.id,
        { status },
        {
          headers: {
            Authorization: getToken(),
          },
        }
      )
      .then((res) => {
        setRows(
          rows.map((r) => (r.id === selectedItem.id ? { ...r, status } : r))
        );
        if (status === "Approved")
          toast.success("Request Accepted successfully", toastConfig);
        else toast.error("Request Rejected successfully", toastConfig);
        onClose();
      })
      .catch((er) => console.log(er))
      .finally(() => {
        setAcceptLoading(false);
        setRejectLoading(false);
      });
  };
  return (
    <>
      <MyTable
        columns={columns}
        rows={rows}
        renderCell={renderCell}
        isLoading={isLoading}
        emptyContent={"There is no Seller Requests to display."}
      />
      <Modal size={"xl"} isOpen={isOpen} onClose={onClose}>
        <ModalContent>
          {(onClose) => (
            <>
              <ModalHeader className="flex flex-col gap-1">
                Seller Request
              </ModalHeader>
              <ModalBody>
                <div className="flex">
                  <Image src={image} alt="image" width={250} height={200} />
                  <div className="px-4 flex flex-col gap-2">
                    <div>
                      <div className=" text-zinc-500 text-sm">Name</div>
                      <div className=" text-zinc-300">{selectedItem.name}</div>
                    </div>
                    <div>
                      <div className=" text-zinc-500 text-sm">Email</div>
                      <div className=" text-zinc-300">{selectedItem.email}</div>
                    </div>
                    <div>
                      <div className=" text-zinc-500 text-sm">
                        Date of birth
                      </div>
                      <div className=" text-zinc-300">
                        {new Date(selectedItem.dob).toDateString()}
                      </div>
                    </div>
                    <div>
                      <div className=" text-zinc-500 text-sm">Address</div>
                      <div className=" text-zinc-300">
                        {selectedItem.address}
                      </div>
                    </div>
                  </div>
                </div>
              </ModalBody>
              <ModalFooter>
                <Button
                  color="danger"
                  variant="light"
                  onPress={() => {
                    updateStatus("Rejected");
                    setRejectLoading(true);
                  }}
                  isLoading={rejectLoading}
                >
                  Reject
                </Button>
                <Button
                  color="primary"
                  onPress={() => {
                    updateStatus("Approved");
                    setAcceptLoading(true);
                  }}
                  isLoading={acceptLoading}
                >
                  Accept
                </Button>
              </ModalFooter>
            </>
          )}
        </ModalContent>
      </Modal>
    </>
  );
}

export default Page;
