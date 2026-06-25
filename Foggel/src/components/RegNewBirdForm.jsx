import {  useEffect, useRef, useState } from "react";
import { birdsEndpoint, observationEndpoint, imageEndpoint } from "../API/apiUrl";
import { FaRegSmileBeam, FaRegAngry  } from "react-icons/fa";



const RegNewBirdForm = ({ birdUrl, onAddedBird }) => {
    var dropdownBirdsRef = useRef(null);

    var [birdOpen, setBirdOpen] = useState(false);
    var [birds, setBirds] = useState([]);
    var [searchBirds, setSearchBirds] = useState("");
    var [searchResult, setSearchResult] = useState([]);

    var [selectedYear, setSelectedYear] = useState("");
    var [selectedMonth, setSelectedMonth] = useState("");

    var [successMessage, setSuccessMessage] = useState("");
    var [errorMessage, setErrorMessage] = useState("");

    var [selectedIndex, setSelectedIndex] = useState(-1);

  const months = [
      "Januari", "Februari", "Mars", "April", "Maj", "Juni",
      "Juli", "Augusti", "September", "Oktober", "November", "December"
    ];

    var formModel = useRef({
        speciesId: null,
        speciesName: null,
        year: null,
        month: null
    });
    
    useEffect(() => {    
        function handleClickOutside(e) {
            if(!dropdownBirdsRef.current.contains(e.target)) {
                setBirdOpen(false);
            }
        };     

        document.addEventListener('mousedown', handleClickOutside);        
        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        }        
    }, []);  

    useEffect(() => { 
        const fetchBirds = async () => {
            fetch(birdsEndpoint)
            .then(response => response.json())
            .then(data => {
                setBirds(data.model);
            })
            .catch(err => console.error(err))            
        }
        fetchBirds();
    }, []);

    function handleBirdChange(bird) {
        console.log("Selected bird:", bird);
        formModel.current.speciesId = bird.id;
        formModel.current.speciesName = bird.name;
        formModel.current.fileId = bird.fileId;
        setSearchBirds(bird.name);
        setBirdOpen(false);
        setSearchResult([]);
        setSelectedIndex(-1);
        birdUrl(bird.fileId ? `${imageEndpoint}?fileId=${bird.fileId}` : "#");
    };

    function handleDateChange(year, month) {
        console.log(year, month);
        if(year != null){
            formModel.current.year = Number(year);
            setSelectedYear(year);
        }
        else if(month != null) {          
            formModel.current.month = months.indexOf(month) + 1;
            setSelectedMonth(month);
        }
    };

    function handleBirdSearch(input) {
        console.log("Handle bird search input:", input);
        setSearchBirds(input);
        setBirdOpen(true);
        if(!input || input === "") {
            console.log("Empty input, clearing search results.");
            // setSearchResult([]);
            setSelectedIndex(-1);
            return;
        }
        const result = birds.filter(bird => 
            bird.name.toLowerCase().startsWith(input.toLowerCase())
        )
        setSearchResult(result);
        setSelectedIndex(result.length > 0 ? 0 : -1);
    }
    
    async function handleSave(e) {
        e.preventDefault();
        setErrorMessage("");
        setSuccessMessage("");
        let validForm = false;

        if(formModel.current.speciesId == null
            || formModel.current.speciesName == null
            || formModel.current.year == null
            || formModel.current.month == null) 
        {
            setErrorMessage("Alla fält måste vara ifyllda!")
            return;
        }
        if(formModel.current.year != null){
            if(formModel.current.year > new Date().getFullYear() || formModel.current.year < 1900) {
                setErrorMessage("År måste vara efter 1900 och nuvarande år.");
                setSelectedYear("");
                return;
            }
        }
        if(formModel.current.month != null) {
            if(formModel.current.month < 1 || formModel.current.month > 12) {
                setErrorMessage("Månad måste vara mellan 1 och 12.");
                setSelectedMonth("");
                return;
            }            
        }
        validForm = true;
        if(validForm) {
            console.log(formModel.current);
            try {
                const response = await fetch(observationEndpoint, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'                },
                    body: JSON.stringify(formModel.current)
                });
                
                const data = await response.json();
                if(response.ok) {
                    setSuccessMessage(data.message);
                    onAddedBird();
                    setSearchBirds("");
                    setSelectedIndex(-1);
                }
                else {
                    setErrorMessage(data.message);
                    setSearchBirds("");
                    setSelectedIndex(-1);
                    return;
                }
            }
            catch(error) {
                setErrorMessage("Något gick fel. Försök igen.");
                console.error("Error saving observation:", error);
                setSearchBirds("");
                setSelectedIndex(-1);
            };
        }
    };

    function handleKeyDown(e) {
        if(e.key === "Enter" && birdOpen && searchResult.length > 0 && selectedIndex >= 0) {
            console.log("Enter pressed");
            e.preventDefault();
            handleBirdChange(searchResult[selectedIndex]);
        }
        else if(e.key === "Escape" && birdOpen) {
            console.log("Escape pressed");
            e.preventDefault();
            setBirdOpen(false);
        }
        else if(e.key === "ArrowDown" && birdOpen && searchResult.length > 0) {
            console.log("ArrowDown pressed");
            e.preventDefault();
            setSelectedIndex(prev => {
                const nextIndex = prev + 1;
                return nextIndex >= searchResult.length ? 0 : nextIndex;
            });
        }
        else if(e.key === "ArrowUp" && birdOpen && searchResult.length > 0) {
            console.log("ArrowUp pressed");
            e.preventDefault();
            setSelectedIndex(prev => {
                const nextIndex = prev - 1;
                return nextIndex < 0 ? searchResult.length - 1 : nextIndex;
            });
        }
    }

  return (
    <>
        <form className="register-new-form" onSubmit={(e) => handleSave(e)}>

          <div className="input-group">
              <div className="dropdown-group" ref={dropdownBirdsRef}>
                <input
                    id="dropdown-input"
                    onKeyDown={handleKeyDown}
                    onFocus={() => { setBirdOpen(true); setSearchResult(birds); }}
                    onClick={() => { setBirdOpen(true); setSearchResult(birds); }}
                    onChange={(e) => handleBirdSearch(e.target.value)}
                    value={searchBirds}
                    placeholder="Välj en fågel från listan..."
                    className="input-field" />
                <ul className={`dropdown ${birdOpen == true ? "open" : ""}`}>
                    {searchResult.map((bird, index) => (
                        <li key={bird.id}
                            className={`suggestion-item ${index === selectedIndex ? "selected" : ""}`}
                            onClick={() => handleBirdChange(bird)}>
                            {bird.name}
                        </li>
                    ))}
                </ul>
              </div>
          </div>
          <div className="input-group">
            <select className="input-field">
                <option className="suggestion-item" disabled>Välj plats...</option>
                <option className="suggestion-item">Trädgård</option>
                <option className="suggestion-item">Grövelsjön</option>
            </select>
          </div>
          <div className="input-group">
                  <select className="input-field" onChange={(e) => handleDateChange(e.target.value, null)} value={selectedYear}>
                    <option value="">Välj år...</option>
                    { 
                        Array.from(
                            {length: 50},
                            (_, index) => new Date().getFullYear() - index
                        ).map(year => (
                            <option key={year} value={year}>{year}</option>
                        ))
                    }
                </select>

                <select className="input-field" onChange={(e) => handleDateChange(null, e.target.value)} value={selectedMonth}>
                    <option value="">Välj månad...</option>                    
                    {
                        months.map(month => 
                        <option key={month} value={month}>{month}</option>)
                    }
                </select>
          </div>
          <div className="form-bottom">
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
              <button type="submit" className="btn border add-bird-btn save-btn">
                  <span>
                      Spara
                  </span>
              </button>
          </div>
      </form>
      
    </>
  )
}

export default RegNewBirdForm




