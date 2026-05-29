import { useState } from "react"
import CardStatistics from "./components/CardStatistics"
import Menu from "./components/menu"
import RegNewBirdForm from "./components/regNewBirdForm"
import { FaEarlybirds } from "react-icons/fa";
import { observationEndpoint } from "./API/apiUrl";


function App() {
  
  const [selectedBirdUrl, setSelectedBirdUrl] = useState("");
  const [birds, setBirds] = useState([]);

  const fetchObservations = async () => {
      try {
          const response = await fetch(observationEndpoint);
          const data = await response.json();
          setBirds(data ?? []);
        } catch (err) {
            console.error(err);
        }
    };
    
    fetchObservations();

  return (
    <>
        <header>
          <div className="nav-container">
            <img className="header-bg" src="/images/birds-flying-bg-crop.png" alt="Leafs." />
            <figure>
              <img src="/images/foggel-logo.png" alt="Fôggel Logo." className="logo" />
            </figure>
          </div>
        </header>

      <div className="wrapper">  
        <main className="main-container">
          <aside className="menu-section border">
            <Menu />
          </aside>

          <section className="content-section">
            <section className="register-new-section">
              <RegNewBirdForm birdUrl={setSelectedBirdUrl} onAddedBird={fetchObservations}/>
                <div className="bird-preview">
                  {selectedBirdUrl.startsWith("http") ? 
                    <img src={ selectedBirdUrl } alt="Fågel" /> :
                    <FaEarlybirds size={100} style={{ position: "absolute", top: "50%", left: "50%", transform: "translate(-50%, -50%)", zIndex: 1 }} color={"var(--shadow-dark-green)"} />
                  }
                </div>
            </section>
            
            <section>
              <CardStatistics birds={birds} />
            </section>
          </section>
        </main>

        <footer>
          <div className="footer-container">
            &copy; 2026 BirdView. All rights reserved.
            <a href="https://www.textstudio.com/">Font generator</a>
          </div>
        </footer>
      </div>
    </>
  )
}

export default App
