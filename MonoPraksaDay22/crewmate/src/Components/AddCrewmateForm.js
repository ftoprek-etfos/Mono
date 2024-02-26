import Button from "./Button";

export default function AddCrewmateForm({addCrewmates, setContext}) {
    return (
        <div id="addCrewmateDiv">
                <h2>Add a new crewmate</h2>
                <form id="addCrewmateForm" onSubmit={null}>
                <label htmlFor="name">Name:</label>
                    <input type="text" id="name" name="name" required/>
                    <label htmlFor="age">Age:</label>
                    <input type="number" id="age" name="age" required/>
                    <label htmlFor="role">Role:</label>
                    <input type="text" id="role" name="role" required/>
                    <Button onClick={addCrewmates} text="Add crewmate"></Button>
                    <Button onClick={() => setContext("home")} text="Back"></Button>
                </form>
            </div>
    )
}