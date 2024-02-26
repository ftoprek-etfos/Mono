import Button from "./Button";



export default function EditCrewmateForm({crewmateToEdit, applyEditCrewmate, setContext, setCrewmateToEdit}) {

    function handleInputChange(e)
    {
        setCrewmateToEdit({...crewmateToEdit, [e.target.name]: e.target.value});
    }

    return (
        <div id="editCrewmateDiv">
            <h2>Edit crewmate</h2>
            <form id="editCrewmateForm" onSubmit={null}>
                <label htmlFor="name">Name:</label>
                <input type="text" id="editName" name="name" value={crewmateToEdit.name} required onInput={handleInputChange}/>
                <label htmlFor="age">Age:</label>
                <input type="number" id="editAge" name="age" value={crewmateToEdit.age} required onInput={handleInputChange}/>
                <label htmlFor="role">Role:</label>
                <input type="text" id="editRole" name="role" value={crewmateToEdit.role} required onInput={handleInputChange}/>
                <Button onClick={applyEditCrewmate} text="Edit crewmate"/>
                <Button onClick={() => setContext("home")} text="Back"></Button>
                <br/>
            </form>
        </div>
    )
}