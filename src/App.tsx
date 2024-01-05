import './App.css'
import { useScannedTag } from './useScannedTag'

export default function App() {
  const tagId = useScannedTag();

  return (
    <div>
      <h1>Tag ID: {tagId}</h1>
    </div>
  )
}
