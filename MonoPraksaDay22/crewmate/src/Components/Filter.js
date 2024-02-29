
export default function Filter({showFilters, setFilters, setSorting, sorting, filters, missionList}) {
    
    const handleSortTypeChange = (event) => {
        setSorting({...sorting, orderBy: event.target.value});
      };
      const handleSortOrderChange = (event) => {
        setSorting({...sorting, sortOrder: event.target.value});
      };
      const handleFilterChange = (event) => {
        setFilters({...filters, [event.target.name]: event.target.value});
      };
    return (
        <>
            {showFilters && <div style={{display: 'flex', flexDirection: 'column'}}>
                <label for="filterBy">Filter by:</label>
                <lable for="firstName">First name:</lable>
                <input type="text" id="firstName" name="firstName" placeholder="John..." onChange={handleFilterChange}/>
                <lable for="lastName">Last name:</lable>
                <input type="text" id="lastName" name="lastName" placeholder="Doe..." onChange={handleFilterChange}/>
                <lable for="age">Age:</lable>
                <input type="number" id="age" name="age" placeholder="25..." onChange={handleFilterChange}/>
                <select name="lastMissionId" id="lastMissionId" onChange={handleFilterChange}>
                  <option value="">All missions</option>
                  {missionList.map((mission) => <option key={mission.id} value={mission.id}>{mission.name}</option>)}
                </select>
                <br/>
            </div>}
            {<div style={{display: 'flex', gap: '10px'}}>
            <label for="sortBy">Sort by:</label>
                <select name="sortBy" id="sortBy" onChange={handleSortTypeChange}>
                <option value="Age">Age</option>
                <option value="FirstName">First name</option>
                <option value="LastName">Last name</option>
                </select>
                <label for="sortOrder">Sort order:</label>
                <select name="sortOrder" id="sortOrder" onChange={handleSortOrderChange}>
                <option value="ASC">↑ASC</option>
                <option value="DESC">↓DESC</option>
                </select>
            </div>}
        </>
    );
}