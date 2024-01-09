import { useState, useEffect } from 'react';
import { formatDistanceToNow } from 'date-fns';
import { useScannedTag } from './useScannedTag';
import { TagId } from './TagId';
import './App.css';

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

  if(!tagId) {
    return <div className="tagId">No tag scanned</div>;
  }

  return (
    <div>
      <TagId value={tagId}/>
      {tagId && <div className="tagAge">Updated {timeSinceLastUpdated}</div>}
    </div>
  )
}
