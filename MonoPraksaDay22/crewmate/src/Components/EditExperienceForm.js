import Button from "./Button";
import CrewmateService from "../Services/CrewmateService";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

export default function EditExperienceForm({crewmateToEdit}) {
    const [experienceList, setExperienceList]  = useState({title: '', duration: 0});
    const [crewmate, setCrewmate] = useState({});
    const backUrl = "/edit/" + crewmateToEdit.id;
    const navigate = useNavigate();

    const handleInputChange = (event) =>
    {
        setExperienceList({...experienceList, [event.target.name]: event.target.value});
        setCrewmate({...crewmateToEdit, experienceList: [experienceList]})
    }

    const handleAddExperience = async () =>
    {
        if(document.forms.addExperienceForm.title.value === '' || document.forms.addExperienceForm.duration.value === '')
        {
            alert('Please fill all the fields!');
            return;
        }
        applyEditCrewmate();

        document.forms.addExperienceForm.title.value = '';
        document.forms.addExperienceForm.duration.value = '';
        navigate(backUrl);
    };

    const applyEditCrewmate = async () => {
        await CrewmateService.addExperience(crewmate);
    }

    return (
        <div id="editCrewmateDiv">
            <h2>Edit crewmate</h2>
                <form id="addExperienceForm" onSubmit={null} name="experience">
                    <label htmlFor="addExperience">Add experience:</label>
                    <br/>
                    <label htmlFor="experienceTitle">Title:</label>
                    <input type="text" id="editName" name="title" required onInput={handleInputChange}/>
                    <label htmlFor="experienceDuration">Duration:</label>
                    <input type="number" id="editName" name="duration" required onInput={handleInputChange}/>
                    <Button onClick={handleAddExperience} text="Add experience"/>
                    <Button href={backUrl} text="Back"/>
                </form>
        </div>
    );
}