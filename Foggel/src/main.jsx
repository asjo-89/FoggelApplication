import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './fonts.css'
import './variables.css'
import './main.css'
import './buttons.css'
import './tables.css'
import App from './App.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>
)
