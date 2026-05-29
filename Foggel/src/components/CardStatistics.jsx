import React, { useState } from 'react'
import { FaCheck } from "react-icons/fa";

const CardStatistics = ({ birds }) => {
    const birdFilterId = "bird-species-filter";
    const yearFilterId = "year-filter";
    const monthFilterId = "month-filter";

  const [yearFilter, setYearFilter] = useState(0);
  const [monthFilter, setMonthFilter] = useState("");
  const [birdFilter, setBirdFilter] = useState("");

  
  const months = [
      "Januari", "Februari", "Mars", "April", "Maj", "Juni",
      "Juli", "Augusti", "September", "Oktober", "November", "December"
    ];
    
    
    const observationsList = Array.isArray(birds) && birds.length > 0
    ? birds.filter(o => {
        const matchesYear = yearFilter === 0 || o.observationYear === yearFilter;
        const matchesMonth = monthFilter === "" || o.monthName === monthFilter;
        const matchesBird = birdFilter === "" || (o.speciesName || "").toLowerCase().includes(birdFilter.toLowerCase());
        
        return matchesYear && matchesMonth && matchesBird;
    })
    : [];

    const notFound = observationsList.length === 0;
  return (
    <div className="card">
        <div className="card-top">
            <h2>Senaste fågelskådningar</h2>
            <div className="filter-group"> 
                <input id={birdFilterId} 
                    className="input-field" 
                    // onChange={handleFilter}
                    onChange={(e => setBirdFilter(e.target.value ?? ""))}
                    value={birdFilter}
                    placeholder="Ange fågelart..." />
                
                <select className="input-field" id={yearFilterId}
                    onChange={(e => setYearFilter(parseInt(e.target.value) || 0))}
                    value={yearFilter}
                >
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

                <select className="input-field" id={monthFilterId}
                    onChange={(e => setMonthFilter(e.target.value ?? ""))}
                    value={monthFilter}
                >
                    <option value="">Välj månad...</option>                    
                    {
                        months.map(month => 
                        <option key={month} value={month}>{month}</option>)
                    }
                </select>
            </div>
            
        </div>
        
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
                {
                    notFound 
                    ? <tr><td colSpan="4">Inga observationer hittades.</td></tr> 
                    : observationsList.map((obs, index) => (
                        <tr key={obs.id ?? index}>
                            <td>{obs.observationYear}</td>
                            <td>{obs.monthName}</td>
                            <td>{obs.speciesName}</td>
                            <td>{new Date(obs.createdDate).toLocaleDateString("sv-SE")}</td>
                        </tr>
                    ))
                }
            </tbody>
        </table>
    </div>
  )
}

export default CardStatistics