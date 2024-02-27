import Button from "./Button";
import React from "react";


export default class EditCrewmateFormClass extends React.Component{
    handleInputChange = (e) => 
    {
        this.props.setCrewmateToEdit({...this.props.crewmateToEdit, [e.target.name]: e.target.value});
    };

    render(){
    return (
        <div id="editCrewmateDiv">
            <h2>Edit crewmate</h2>
            <form id="editCrewmateForm" onSubmit={null}>
                <label htmlFor="name">Name:</label>
                <input type="text" id="editName" name="name" value={this.props.crewmateToEdit.name} required onInput={this.handleInputChange}/>
                <label htmlFor="age">Age:</label>
                <input type="number" id="editAge" name="age" value={this.props.crewmateToEdit.age} required onInput={this.handleInputChange}/>
                <label htmlFor="role">Role:</label>
                <input type="text" id="editRole" name="role" value={this.props.crewmateToEdit.role} required onInput={this.handleInputChange}/>
                <Button onClick={this.props.applyEditCrewmate} text="Edit crewmate"/>
                <Button onClick={() => this.props.setContext("home")} text="Back"/>
                <br/>
            </form>
        </div>
    )
    }
}
