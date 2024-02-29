import CrewmateService from "../Services/CrewmateService";
import Filter from "../Components/Filter";
import { Button as ReactButton }  from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import Table from '../Components/Table';
import PageSize from "../Components/PageSize";
import CenterContainer from "../Components/CenterContainer";
import { useState, useEffect } from "react";
import Navbar from "../Components/Navbar";

export default function ListPage() {
    const [crewmates, setCrewmates] = useState([]);
    const [showFilters, setShowFilters] = useState(false);
    const [sorting, setSorting] = useState({orderBy: "Age", sortOrder: "ASC"});
    const [filters, setFilters] = useState({firstName: "", lastName: "", age: "", lastMissionId: ""});
    const [paging, setPaging] = useState({pageNumber: 1, totalPages: 1, pageSize: 3});
    const [isLoading, setIsLoading] = useState(true);
    const [missionList, setMissionList] = useState([]);

    function nextPage() {
      if(paging.totalPages === paging.pageNumber) return;
      setPaging({...paging, pageNumber: paging.pageNumber + 1});
    }
    const handlePageSizeChange = (event) => {
      setPaging({...paging, pageSize: event.target.value, pageNumber: 1});
    };
  
    function previousPage(){
      if(paging.pageNumber === 1) return;
      setPaging({...paging, pageNumber: paging.pageNumber - 1});
    }

    const fetchData = async () => {
      try{
        await CrewmateService.getAllCrewmates(filters, paging, sorting).then((response) => {
            setCrewmates(response.data.list);
            setPaging({...paging, totalPages: response.data.pageCount});
        });
      }catch(e){
        setCrewmates([]);
      }finally{
        setIsLoading(false);
      }
    };

    const fetchMissions = async () => {
        try{
          await CrewmateService.getMissions().then((response) => {
              setMissionList(response.data);
          });
        }catch(e){
          setMissionList([]);
        }
      };

    useEffect(() => {
        fetchMissions();
        const delaySearch = setTimeout(() => {
          fetchData();
        }, 500);
        return () => clearTimeout(delaySearch);
      }, [paging.pageNumber, paging.pageSize, sorting, filters]);


    return (
        <CenterContainer>
            <Navbar/>
            <h2>Crewmate list</h2>
            <ReactButton variant={!showFilters ? "outline-info" : "outline-secondary"} onClick={() => setShowFilters(!showFilters)}>{!showFilters ? "Show filters" : "Hide filters"}</ReactButton>
            <Filter missionList={missionList} showFilters={showFilters} filters={filters} setFilters={setFilters} sorting={sorting} setSorting={setSorting}/>
            <Table isLoading={isLoading} crewmates={crewmates} fetchData={fetchData}/>
            {paging.totalPages !== paging.pageNumber && <ReactButton variant="info" onClick={nextPage}>Next</ReactButton>}
            {paging.pageNumber > 1 && <ReactButton variant="secondary" onClick={previousPage}>Previous</ReactButton>}
            <PageSize paging={paging} handlePageSizeChange={handlePageSizeChange}/> 
        </CenterContainer>
    );
}