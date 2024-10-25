"use client";
import {
  Modal,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalFooter,
  Button,
  Input,
  Textarea,
} from "@nextui-org/react";
import React, { useState } from "react";
import FormInputs from "./FormInputs";
import { apiLink, getToken, toastConfig } from "@/configs";
import { toast } from "react-toastify";
import axios from "axios";

function NftModal({ isOpen, onClose, list, setList, setNftId }) {
  const [image, setImage] = useState(null);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  function handleSubmit() {
    const data = new FormData();
    data.set("Title", name);
    data.set("Description", description);
    data.set("Image", image);
    setIsLoading(true);
    axios
      .post(apiLink + "/api/Nft", data, {
        headers: {
          Authorization: getToken(), //jwt token
          "Content-Type": "multipart/form-data", // Important for file upload
        },
      })
      .then((res) => {
        toast.success("Success", toastConfig);
        setList([...list, res.data]);
        setNftId(res.data.id);
        onClose();
      })
      .catch((er) => toast.error("Failed", toastConfig))
      .finally(() => setIsLoading(false));
  }

  return (
    <Modal isOpen={isOpen} onClose={onClose}>
      <ModalContent>
        {(onClose) => (
          <>
            <ModalHeader className="flex flex-col gap-1">Add Nft</ModalHeader>
            <ModalBody>
              <Input
                type="text"
                variant={"bordered"}
                label="Name"
                isRequired
                validationBehavior="native"
                value={name}
                onChange={(e) => setName(e.target.value)}
              />
              <Textarea
                label="Description"
                className="w-full"
                variant={"bordered"}
                isRequired
                validationBehavior="native"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
              />
              <FormInputs.ImagePicker value={image} setState={setImage} />
            </ModalBody>
            <ModalFooter>
              <Button color="danger" variant="light" onPress={onClose}>
                Close
              </Button>
              <Button
                isLoading={isLoading}
                color="primary"
                onPress={handleSubmit}
              >
                Add Nft
              </Button>
            </ModalFooter>
          </>
        )}
      </ModalContent>
    </Modal>
  );
}

export default NftModal;
