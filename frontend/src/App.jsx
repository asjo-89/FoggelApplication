import { useEffect, useState } from "react"
import CardStatistics from "./components/CardStatistics"
import Menu from "./components/menu"
import RegNewBirdForm from "./components/regNewBirdForm"
import { FaEarlybirds, FaRegSmileBeam, FaRegAngry  } from "react-icons/fa";
import { observationEndpoint } from "./API/apiUrl";


function App() {
  
  const [selectedBirdUrl, setSelectedBirdUrl] = useState("");
  const [birds, setBirds] = useState([]);
  const [successMessage, setSuccessMessage] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const deletedObservation = async (deletedId) => {
    console.log("Deleting observation with ID:", deletedId);
    setSuccessMessage("");
    setErrorMessage("");
    const response = await fetch(`${observationEndpoint}/${deletedId}`, {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json"
      }
    });

    const responseText = await response.text();
    const contentType = response.headers.get("content-type") || "";
    let data = null;

    if (responseText) {
      if (contentType.includes("application/json")) {
        data = JSON.parse(responseText);
      } else {
        data = { message: responseText };
      }
    }

    if (response.ok) {
      setBirds(prevBirds => prevBirds.filter(bird => bird.observationId !== deletedId));
      setSuccessMessage(data?.message || "Observationen har tagits bort.");
    } else {
      setErrorMessage(data?.message || "Ett fel uppstod vid borttagning av observationen.");
    }
  };

  
  const fetchObservations = async () => {
    try {
      const response = await fetch(observationEndpoint);
      const data = await response.json();
      setBirds(data ?? []);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    const loadObservations = async () => {
      console.log("Loading observations...");
      await fetchObservations();
    };

    loadObservations();
  }, []);

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
            {successMessage != "" && (
                <div className="form-message success">
                    <FaRegSmileBeam size="24px" />
                    <p>{successMessage}</p>
                </div>
              )}
              {errorMessage != "" && (
                <div className="form-message error">
                    <FaRegAngry size="24px" />
                    <p>{errorMessage}</p>
                </div>
              )}
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
              <CardStatistics birds={birds} deletedObservation={deletedObservation} />
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
