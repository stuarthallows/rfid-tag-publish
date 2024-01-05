import { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

export const useScannedTag = () => {
  const [tagId, setTagId] = useState("");
  const apiBaseUrl = window.location.origin;
  
  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(apiBaseUrl + '/api')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    connection.on("nextTag", (data) => {
      setTagId(data);
    });

    connection
      .start()
      .then(() => console.log("Hub connection started"))
      .catch((err) => console.error("Error while starting connection: " + err));

    return () => {
      connection.off("nextTag");
      connection.stop();
      console.log("Hub connection closed");
    };
  }, [apiBaseUrl]);

  return tagId;
};
