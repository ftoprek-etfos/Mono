import CenterContainer from "../Components/CenterContainer";
import EditExperienceForm from "../Components/EditExperienceForm";
import CrewmateService from "../Services/CrewmateService";
import { useState, useEffect } from "react";
import { useParams } from 'react-router-dom';

export default function EditExperiencePage() {
    const [crewmateToEdit, setCrewmateToEdit] = useState({});
    const params = useParams();

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
            <EditExperienceForm crewmateToEdit={crewmateToEdit}/>
        </CenterContainer>
    );
}