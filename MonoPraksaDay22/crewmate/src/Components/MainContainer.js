import Button from './/Button';
import StatusText from './StatusText';
import Table from './Table';
import { useEffect, useState } from 'react';
import AddCrewmateForm from './AddCrewmateForm';
import EditCrewmateFormClass from './EditCrewmateFormClass';
import axios from 'axios';
import { Button as ReactButton }  from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

export default function MainContainer() {
  const [crewmates, setCrewmates] = useState([]);
  const [isShowing, setShowing] = useState(false);
  const [context, setContext] = useState("home");
  const [crewmateToEdit, setCrewmateToEdit] = useState({});
  const [status, setStatus] = useState("");
  const [sorting, setSorting] = useState({orderBy: "Age", sortOrder: "ASC"});
  const [filters, setFilters] = useState({firstName: "", lastName: "", age: ""});
  const [pageNumber, setPageNumber] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [showFIlters, setShowFilters] = useState(false);

  useEffect(() => {
    let url = `https://localhost:44334/api/Crew?pageNumber=${pageNumber}&orderBy=${sorting.orderBy}&sortOrder=${sorting.sortOrder}`;
    if(filters.firstName !== "") url += `&firstName=${filters.firstName}`;
    if(filters.lastName !== "") url += `&lastName=${filters.lastName}`;
    if(filters.age !== "") url += `&age=${filters.age}`;
    const fetchData = async () => {
      try{
        await axios.get(url).then((response) => {
          setCrewmates(response.data.list);
          setTotalPages(response.data.pageCount);
        });
      }catch(e){
        console.warn(e)
        setCrewmates([]);
      }
    };
    const delaySearch = setTimeout(() => {
      fetchData();
    }, 500);
    return () => clearTimeout(delaySearch);
  }, [status, pageNumber, sorting, filters]);

  function nextPage() {
    if(totalPages === pageNumber) return;
    setPageNumber(pageNumber + 1);
  }

  function previousPage(){
    if(pageNumber === 1) return;
    setPageNumber(pageNumber - 1);
  }
  const handleSortTypeChange = (event) => {
    setSorting({...sorting, orderBy: event.target.value});
  };
  const handleSortOrderChange = (event) => {
    setSorting({...sorting, sortOrder: event.target.value});
  };
  const handleFilterChange = (event) => {
    setFilters({...filters, [event.target.name]: event.target.value});
  };
  const renderContent = () => {
    switch (context) {
      case 'add':
        return (
            <AddCrewmateForm addCrewmates={addCrewmates} setContext={setContext}/>
        );
      case 'home':
        return (
            <>
                <Button text="Crewmate logbook"/>
                <StatusText status={status}/>
                {isShowing && <ReactButton variant={!showFIlters ? "outline-info" : "outline-secondary"} onClick={() => setShowFilters(!showFIlters)}>{!showFIlters ? "Show filters" : "Hide filters"}</ReactButton>}
                {isShowing && showFIlters && <div style={{display: 'flex', flexDirection: 'column'}}>
                  <label for="filterBy">Filter by:</label>
                  <lable for="firstName">First name:</lable>
                  <input type="text" id="firstName" name="firstName" placeholder="John..." onChange={handleFilterChange}/>
                  <lable for="lastName">Last name:</lable>
                  <input type="text" id="lastName" name="lastName" placeholder="Doe..." onChange={handleFilterChange}/>
                  <lable for="age">Age:</lable>
                  <input type="number" id="age" name="age" placeholder="25..." onChange={handleFilterChange}/>
                  <br/>
                </div>}
                {isShowing && <div style={{display: 'flex', gap: '10px'}}>
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
                {isShowing && <Table crewmates={crewmates} setCrewmates={setCrewmates} editCrewmate={editCrewmate} setStatus={setStatus}/>}
                {isShowing &&  totalPages !== pageNumber && <ReactButton variant="info" onClick={nextPage}>Next</ReactButton>}
                {pageNumber > 1 && <ReactButton variant="secondary" onClick={previousPage}>Previous</ReactButton>}
                <Button text="Show crewmates" onClick={() => setShowing(true)}/>
                <Button text="Add crewmates" onClick={() => setContext("add")}/>
            </>
        );
      case 'edit':
        return (
            <EditCrewmateFormClass setContext={setContext} crewmateToEdit={crewmateToEdit} applyEditCrewmate={applyEditCrewmate} setCrewmateToEdit={setCrewmateToEdit}/>
            );
      default:
        return null;
    }
  };

  function addCrewmates() {
    if(window.document.forms.addCrewmateForm.firstName.value === '' || window.document.forms.addCrewmateForm.lastName.value === '' || window.document.forms.addCrewmateForm.age.value === '')
    {
        alert('Please fill all the fields!');
        return;
    }
    const crewmate = {
        firstName: window.document.forms.addCrewmateForm.firstName.value,
        lastName: window.document.forms.addCrewmateForm.lastName.value,
        age: window.document.forms.addCrewmateForm.age.value
      }
    axios.post('https://localhost:44334/api/Crew', crewmate);
    setContext("home");
    setStatus("add");
    setTimeout(() => {
        setStatus("");
      }, 2000);
  }

  async function editCrewmate(id) {
        await axios.get(`https://localhost:44334/api/Crew/${id}`).then((response) => {
            setCrewmateToEdit(response.data);
        });
        setContext("edit");
  }

  function applyEditCrewmate() {
        let crewmateList = crewmates.filter(crewmate => crewmate.id !== crewmateToEdit.id);
        setCrewmates([...crewmateList, crewmateToEdit]);

        const putCrewmateInfo = {
          lastMission: {
            name: crewmateToEdit.lastMission.name,
            duration: crewmateToEdit.lastMission.duration
          },
          experienceList: crewmateToEdit.experienceList.map((experience) => {
            const { id, ...restOfExperience } = experience;
            return restOfExperience;
          })
        };
        axios.put(`https://localhost:44334/api/Crew/${crewmateToEdit.id}`, putCrewmateInfo);
        setContext("home");
        setStatus("edit");
        setTimeout(() => {
            setStatus("");
          }, 2000);
    
  }

  return (
    renderContent()
  );
}