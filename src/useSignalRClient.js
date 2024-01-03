import React, { useEffect } from "react";
import * as signalR from "@microsoft/signalr";

export const useSignalRClient = () => {
  var [tagId, setTagId] = React.useState("");

  // const apiBaseUrl = window.location.origin;
  // apiBaseUrl + '/api'

  var connectionUrl = process.env.REACT_APP_SIGNALR_HUB_URL;

  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(connectionUrl)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    connection.on("nextTag", (data) => {
      console.log("On:", data);
      setTagId(data);
    });

    connection
      .start()
      .then(() => console.log("Connection started"))
      .catch((err) => console.error("Error while starting connection: " + err));

    return () => {
      console.log("Connection closed");
      connection.off("nextTag");
      connection.stop();
    };
  }, [connectionUrl]);

  return tagId;
};
