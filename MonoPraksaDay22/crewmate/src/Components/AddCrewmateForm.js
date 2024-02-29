import CrewmateService from "../Services/CrewmateService";
import Button from "./Button";
import { useState } from 'react';
import { Navigate, useNavigate } from 'react-router-dom';

export default function AddCrewmateForm() {
    const [crewmate, setCrewmate] = useState({});
    const navigate = useNavigate();

    function handleInputChange(e) {
        setCrewmate({...crewmate, [e.target.name]: e.target.value});
    }

    async function submitNewCrewmate() {
        if(crewmate.firstName === '' || crewmate.lastName === '' || crewmate.age === '')
        {
            alert('Please fill all the fields!');
            return;
        }
        await CrewmateService.addCrewmate(crewmate).then(() => {
            navigate("/");
        });
    }

    return (
        <div id="addCrewmateDiv">
                <h2>Add a new crewmate</h2>
                <form id="addCrewmateForm" onSubmit={null}>
                    <label htmlFor="firstName">First name:</label>
                    <input type="text" id="firstName" name="firstName" required onInput={handleInputChange}/>
                    <label htmlFor="lastName">Last name:</label>
                    <input type="text" id="lastName" name="lastName" required onInput={handleInputChange}/>
                    <label htmlFor="age">Age:</label>
                    <input type="number" id="age" name="age" required onInput={handleInputChange}/>
                    <Button onClick={submitNewCrewmate} text="Add crewmate"></Button>
                    <Button text="Back" href="/"></Button>
                </form>
            </div>
    )
}