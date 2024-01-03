import React from 'react';
import { useSignalRClient } from './useSignalRClient';

export function App() {
  var tagId = useSignalRClient();

  return <div> Tag Id: {tagId}</div>;
}
