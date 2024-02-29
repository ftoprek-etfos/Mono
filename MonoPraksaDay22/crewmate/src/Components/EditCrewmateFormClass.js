import Button from "./Button";
import React from "react";
import ExperienceTable from "./ExperienceTable";
import CrewmateService from "../Services/CrewmateService";

let lastMission = {name: "", duration: ""}
let experienceList = [];
export default class EditCrewmateFormClass extends React.Component{

    handleInputChange = (e) => 
    {
        this.props.setCrewmateToEdit({...this.props.crewmateToEdit, [e.target.name]: e.target.value});
        if(e.target.name.split('.')[0] === "lastMission")
            lastMission = {...lastMission, [e.target.name.split('.')[1]]: e.target.value}
            this.props.setCrewmateToEdit({...this.props.crewmateToEdit, ["lastMission"]: lastMission});
    };

    handleSubmit = (e) => {
        if(!this.props.crewmateToEdit.experienceList)
        {
            alert('Please add at least one experience!');
            e.preventDefault();
            return;
        }
        this.applyEditCrewmate();
    }

    applyEditCrewmate = async () => {
        await CrewmateService.editCrewmate(this.props.crewmateToEdit);
        this.props.navigate('/')
    }

    render(){

        this.props.crewmateToEdit.lastMission ? 
        lastMission = {
            name: this.props.crewmateToEdit.lastMission.name,
            duration: this.props.crewmateToEdit.lastMission.duration
        } : lastMission = {name: "", duration: ""}
        experienceList = this.props.crewmateToEdit.experienceList;

        const addExperienceUrl = `/edit/${this.props.crewmateToEdit.id}/addExperience`;

        return (
        <div id="editCrewmateDiv">
          <h2>Edit crewmate</h2>
          <form id="editCrewmateForm" onSubmit={null}>
            <label htmlFor="firstName">First name:</label>
            <input type="text" id="editName" name="firstName" value={this.props.crewmateToEdit.firstName} disabled/>
            <label htmlFor="lastName">Last name:</label>
            <input type="text" id="editName" name="lastName" value={this.props.crewmateToEdit.lastName} disabled/>
            <label htmlFor="age">Age:</label>
            <input type="number" id="editAge" name="age" value={this.props.crewmateToEdit.age} disabled/>
            <label htmlFor="lastMission">Last mission:</label>
            <input type="text" id="editName" name="lastMission.name" value={lastMission.name} required onInput={this.handleInputChange}/>
            <label htmlFor="duration">Duration:</label>
            <input type="number" id="editName" name="lastMission.duration" value={lastMission.duration} required onInput={this.handleInputChange}/>
            <label htmlFor="ExperienceList">Experience list:</label>
            <ExperienceTable experienceList={this.props.crewmateToEdit.experienceList}/>
            <Button href={addExperienceUrl} text="Add new experience"/>
            <Button onClick={this.handleSubmit} text="Edit crewmate"/>
            <Button text="Back" href="/"/>
            <br/>
          </form>
        </div>
        );
    }
}
