import {  useEffect, useRef, useState } from "react";
import { birdsEndpoint, observationEndpoint, imageEndpoint } from "../API/apiUrl";
import { FaRegSmileBeam, FaRegAngry  } from "react-icons/fa";



const RegNewBirdForm = () => {
    var dropdownBirdsRef = useRef(null);

    var [birdOpen, setBirdOpen] = useState(false);
    var [birds, setBirds] = useState([]);
    var [searchBirds, setSearchBirds] = useState("");
    var [searchResult, setSearchResult] = useState([]);

    var [selectedBird, setSelectedBird] = useState("");
    var [selectedYear, setSelectedYear] = useState("");
    var [selectedMonth, setSelectedMonth] = useState("");

    var [successMessage, setSuccessMessage] = useState("");
    var [errorMessage, setErrorMessage] = useState("");

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
        formModel.current.speciesId = bird.id;
        formModel.current.speciesName = bird.name;
        setSearchBirds(bird.name);
        setBirdOpen(false);
    };

    function handleDateChange(year, month) {
        if(year != null){
            formModel.current.year = Number(year);
            setSelectedYear(year);
        }
        else if(month != null) {          
            formModel.current.month = Number(month);
            setSelectedMonth(month);
        }
    };

    function handleBirdSearch(input) {
        setSearchBirds(input);
        if(!input) {
            setSearchResult([]);
            return;
        }
        const result = birds.filter(bird => 
            bird.name.toLowerCase().startsWith(input.toLowerCase())
        )
        setSearchResult(result);
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
                    setBirds(prev => [...prev, formModel.current]);
                }
                else {
                    setErrorMessage(data.message);
                }
            }
            catch(err) {
                setErrorMessage("Något gick fel. Försök igen.");
            };
        }
    };

  return (
    <form className="register-new-form" onSubmit={(e) => handleSave(e)}> 
        
        <div className="input-group">  
            <div className="dropdown-group" ref={dropdownBirdsRef}>     
                <input 
                    id="dropdown-input"
                    onClick={() => setBirdOpen(true)} 
                    onChange={(e) => handleBirdSearch(e.target.value)}
                    value={searchBirds} 
                    placeholder="Välj fågel..." 
                    className="input-field" /> 
                <div className={`dropdown ${birdOpen == true ? "open" : ""}`}>
                    {searchResult.length > 0 && (
                        <ul>
                            {searchResult.map(bird => 
                                <li key={bird.id} 
                                    className="suggestion-item" 
                                    onClick={() => handleBirdChange(bird)}>
                                        {bird.fileId && (
                                            <img style={{ width: "40px", height: "32px", objectFit: "cover" }} src={`${imageEndpoint}?fileId=${bird.fileId}`} alt={bird.name} />
                                        )}
                                    {bird.name}
                                </li>
                            )}
                        </ul>
                    )}
                </div>
            </div>
            {/* <button className="btn border add-bird-btn">
                <span>Lägg till ny fågel</span>
            </button> */}
        </div>
        <div className="input-group">
            <input onChange={(e) => {e.target.value != null && setSelectedYear(e.target.value)}}
                onBlur={(e) => handleDateChange(e.target.value, null)} 
                placeholder="ÅÅÅÅ" 
                type="number" 
                className="input-field" />
            <input onChange={(e) => {e.target.value != null && setSelectedMonth(e.target.value)}}
                onBlur={(e) => handleDateChange(null, e.target.value)} 
                placeholder="MM" 
                type="number" 
                className="input-field" />
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
  )
}

export default RegNewBirdForm




