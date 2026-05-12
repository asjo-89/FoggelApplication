import React, { useEffect, useState } from 'react'
import { observationEndpoint } from '../API/apiUrl'
import { FaCheck } from "react-icons/fa";

const CardStatistics = () => {
    const birdFilterId = "bird-species-filter";
    const monthFilterId = "month-filter";

  const [observations, setObservations] = useState([]);
  const [filteredObservations, setFilteredObservations] = useState([]);
  const [speciesFilter, setSpeciesFilter] = useState("");
  const [monthFilter, setMonthFilter] = useState("");
  const [dropdownOpen, setDropdownOpen] = useState(false);

  useEffect(() => {
    const fetchObservations = async () => {
        try {
            const response = await fetch(observationEndpoint);
            const data = await response.json();
            setObservations(data ?? []);
        } catch (err) {
            console.error(err);
        }
    };

    fetchObservations();
  }, []);

  function handleFilter(e) {
    const eventId = e.target.id;

    if(eventId === birdFilterId) {
        const name = e.target.value.toLowerCase();
        setFilteredObservations(
            observations.filter(o => 
                o.name.toLowerCase() === name
            )
        );
    }
    else if(eventId === monthFilterId) {
        const month = Number(e.target.value);
        setFilteredObservations(
            observations.filter(o => 
                o.month === month
            )
        );
    }
  }

  return (
    <div className="card">
        <div className="card-top">
            <h2>Senaste fågelskådningar</h2>
            <div className="filter-group"> 
                <input id={birdFilterId} 
                    className="input-field" 
                    onChange={handleFilter}
                    placeholder="Ange fågelart..." /> 
                <input id={monthFilterId} 
                    className="input-field" 
                    onClick={() => setDropdownOpen(true)} 
                    onChange={handleFilter}
                    placeholder="Välj månad..." />  
                <div className={`dropdown ${dropdownOpen == true ? "open" : ""}`}>
                    <ul>
                        {/* <li className="suggestion-item" >
                        </li> */}
                    </ul>
                </div>
                {/* <button onClick={updateObservationListWithFilters}>
                    Sök
                </button>                */}
            </div>
            {/* <div className="checkbox-container">
                <label htmlFor="year-checkbox" className="checkbox-group">
                    <input id="year-checkbox" type="checkbox" />
                    <div className="checkbox">
                        <FaCheck className="check-icon" color="#144100e8" />
                    </div>
                    <p>År</p>
                </label>
                <label htmlFor="month-checkbox" className="checkbox-group">
                    <input id="month-checkbox" type="checkbox" />
                    <div className="checkbox">
                        <FaCheck className="check-icon" color="#144100e8" />
                    </div>
                    <p>Månad</p>
                </label>
                <label htmlFor="species-name-checkbox" className="checkbox-group">
                    <input id="species-name-checkbox" type="checkbox" />
                    <div className="checkbox">
                        <FaCheck className="check-icon" color="#144100e8" />
                    </div>
                    <p>Fågelart</p>
                </label>
                <label htmlFor="created-date-checkbox" className="checkbox-group">
                    <input id="created-date-checkbox" type="checkbox" />
                    <div className="checkbox">
                        <FaCheck className="check-icon" color="#144100e8" />
                    </div>
                    <p>Skapad</p>
                </label>
            </div> */}
        </div>
        {/* <figure className="image-frame">
            <img src="/images/magpie.jpg" alt="Skata." />
            <figcaption>Skata</figcaption>
        </figure>
        <figure className="image-frame">
            <img src="/images/gråsiska.jpg" alt="Gråsiska." />
            <figcaption>Gråsiska</figcaption>
        </figure> */}
        <table className="observations-table">
            <thead>
                <tr>
                    <th>År</th>
                    <th>Månad</th>
                    <th>Fågelart</th>
                    <th>Skapad</th>
                </tr>
            </thead>
            <tbody>
                {Array.isArray(observations) && observations.map((obs, index) => (
                    <tr key={obs.id ?? index}>
                        <td>{obs.observationYear}</td>
                        <td>{obs.monthName}</td>
                        <td>{obs.speciesName}</td>
                        <td>{new Date(obs.createdDate).toLocaleDateString("sv-SE")}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    </div>
  )
}

export default CardStatistics