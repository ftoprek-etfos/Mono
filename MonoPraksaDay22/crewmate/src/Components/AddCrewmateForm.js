import Button from "./Button";
import { useState } from 'react';

export default function AddCrewmateForm({addCrewmates, setContext}) {
    const [crewmate, setCrewmate] = useState({});

    function handleInputChange(e) {
        setCrewmate({...crewmate, [e.target.name]: e.target.value});
    }

    function submitNewCrewmate() {
        if(crewmate.firstName === '' || crewmate.lastName === '' || crewmate.age === '')
        {
            alert('Please fill all the fields!');
            return;
        }
        addCrewmates(crewmate);
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
                    <Button onClick={() => setContext("home")} text="Back"></Button>
                </form>
            </div>
    )
}