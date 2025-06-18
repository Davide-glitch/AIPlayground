import axios from "axios";

export const defaultHeaders = {
  "Content-Type": "application/json",
};

export const AIPlaygroundApiClient = axios.create({
  baseURL: "http://localhost:5147/api/",
  headers: defaultHeaders,
});
