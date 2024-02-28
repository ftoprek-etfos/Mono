import Button from "./Button";

export default function AddCrewmateForm({addCrewmates, setContext}) {
    return (
        <div id="addCrewmateDiv">
                <h2>Add a new crewmate</h2>
                <form id="addCrewmateForm" onSubmit={null}>
                    <label htmlFor="firstName">First name:</label>
                    <input type="text" id="firstName" name="firstName" required/>
                    <label htmlFor="lastName">Last name:</label>
                    <input type="text" id="lastName" name="lastName" required/>
                    <label htmlFor="age">Age:</label>
                    <input type="number" id="age" name="age" required/>
                    <Button onClick={addCrewmates} text="Add crewmate"></Button>
                    <Button onClick={() => setContext("home")} text="Back"></Button>
                </form>
            </div>
    )
}