import { useState, useEffect } from 'react';
import './App.css'
import { useScannedTag } from './useScannedTag'
import { formatDistanceToNow } from 'date-fns';

export default function App() {
  const tagId = useScannedTag();
  
  const [tagIdUpdatedAt, setTagIdUpdatedAt] = useState(Date.now());
  useEffect(() => {
    setTagIdUpdatedAt(Date.now());
  }, [tagId]);

  const [timeSinceLastUpdated, setTimeSinceLastUpdated] = useState("");
  useEffect(() => {
    const interval = setInterval(() => {
      const distance = formatDistanceToNow(tagIdUpdatedAt, { addSuffix: true, includeSeconds: true });
      setTimeSinceLastUpdated(distance);
    }, 1_000);
  
    return () => {
      clearInterval(interval);
    };
  }, [tagIdUpdatedAt]);

  return (
    <div>
      <h1>Tag Id: {tagId}</h1>
      {tagId && <h2>Updated {timeSinceLastUpdated}</h2>}
    </div>
  )
}
