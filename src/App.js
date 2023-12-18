import React, { useEffect, useState } from 'react';

function App() {
  const [value, setValue] = useState('');

  useEffect(() => {
    fetch('api/TagScanned?name=Stuart')
      .then(response => response.text())
      .then(data => setValue(data))
      .catch(error => console.error('Error fetching data from api/TagScanned', error));
  }, []);

  return <div>{value} ;-)</div>;
}

export default App;
