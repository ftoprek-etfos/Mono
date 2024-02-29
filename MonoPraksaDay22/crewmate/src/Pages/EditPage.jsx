import CenterContainer from "../Components/CenterContainer"
import CrewmateService from "../Services/CrewmateService";
import { useState, useEffect } from "react";
import { useNavigate, useParams } from 'react-router-dom';
import EditCrewmateFormClass from "../Components/EditCrewmateFormClass";
import Navbar from "../Components/Navbar";

export default function EditPage() {

    const [crewmateToEdit, setCrewmateToEdit] = useState({});
    const params = useParams();
    const navigate = useNavigate();

    const fetchData = async () => {
        try{
            await CrewmateService.getCrewmateById(params.id).then((response) => {
                setCrewmateToEdit(response.data);
            });
        }catch(e){
            setCrewmateToEdit(null);
        }
    };

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <CenterContainer>
            <Navbar/>
            <EditCrewmateFormClass crewmateToEdit={crewmateToEdit} setCrewmateToEdit={setCrewmateToEdit} navigate={navigate}/>
        </CenterContainer>
    );
}