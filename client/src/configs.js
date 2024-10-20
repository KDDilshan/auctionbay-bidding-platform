import { Bounce } from "react-toastify";

export const toastConfig = {
  position: "top-right",
  autoClose: 5000,
  hideProgressBar: false,
  closeOnClick: true,
  pauseOnHover: true,
  draggable: true,
  progress: undefined,
  theme: "dark",
  transition: Bounce,
};

export const apiLink = "https://localhost:7218";

export function getToken() {
  var token = localStorage.getItem("token");
  if (!token) {
    return null;
  }
  return `Bearer ${token}`;
}

export function formatCurrency(val) {
  return (val / 100).toLocaleString("en-US", {
    style: "currency",
    currency: "USD",
  });
}
