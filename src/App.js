import React from 'react';
import { useSignalRClient } from './SignalRClient';

function App() {
  var tagId = useSignalRClient();

  return <div> Tag Id: {tagId}</div>;
}

export default App;
