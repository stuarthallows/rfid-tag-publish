import { useState, useEffect } from 'react';
import './App.css'
import { useScannedTag } from './useScannedTag'
import { formatDistanceToNow } from 'date-fns';

export default function App() {
  const tagId = "E28011000000111131222222";
  
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
      <div className="tagId">{tagId}</div>
      {tagId && <div className="tagAge">Updated {timeSinceLastUpdated}</div>}
    </div>
  )
}
