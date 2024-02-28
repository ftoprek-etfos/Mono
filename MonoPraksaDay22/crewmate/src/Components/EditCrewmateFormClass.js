import Button from "./Button";
import React from "react";
import ExperienceTable from "./ExperienceTable";

let lastMission = {name: "", duration: ""}
let experienceList = [];
export default class EditCrewmateFormClass extends React.Component{
    
    handleInputChange = (e) => 
    {
        this.props.setCrewmateToEdit({...this.props.crewmateToEdit, [e.target.name]: e.target.value});
        console.log(e.target.name.split('.')[1]);
        if(e.target.name.split('.')[0] === "lastMission")
            lastMission = {...lastMission, [e.target.name.split('.')[1]]: e.target.value}
            this.props.setCrewmateToEdit({...this.props.crewmateToEdit, ["lastMission"]: lastMission});
    };

    handleAddExperience = (e) =>
    {
        this.props.setCrewmateToEdit({...this.props.crewmateToEdit, ["experienceList"]: [...this.props.crewmateToEdit.experienceList, {title: document.forms.addExperienceForm.title.value, duration: document.forms.addExperienceForm.duration.value}]});
        document.forms.addExperienceForm.title.value = '';
        document.forms.addExperienceForm.duration.value = '';
        this.setState({currScreen: "edit"});
    };
    state = {
        currScreen: "edit"
    }
    render(){
        lastMission = {
            name: this.props.crewmateToEdit.lastMission.name,
            duration: this.props.crewmateToEdit.lastMission.duration
        };
        experienceList = this.props.crewmateToEdit.experienceList;
        switch (this.state.currScreen) {
            case 'edit':
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
                  <label htmlFor="duration">duration:</label>
                  <input type="number" id="editName" name="lastMission.duration" value={lastMission.duration} required onInput={this.handleInputChange}/>
                  <label htmlFor="ExperienceList">Experience list:</label>
                  <ExperienceTable experienceList={this.props.crewmateToEdit.experienceList}/>
                  <Button onClick={() => this.setState({currScreen: "addExperience"})} text="Add new experience"/>
                  <Button onClick={this.props.applyEditCrewmate} text="Edit crewmate"/>
                  <Button onClick={() => this.props.setContext("home")} text="Back"/>
                  <br/>
                </form>
                </div>
              );
            case 'addExperience':    
              return (
                <div id="editCrewmateDiv">
                    <h2>Edit crewmate</h2>
                      <form id="addExperienceForm" onSubmit={null} name="experience">
                      <label htmlFor="addExperience">Add experience:</label>
                      <br/>
                      <label htmlFor="experienceTitle">Title:</label>
                      <input type="text" id="editName" name="title" required/>
                      <label htmlFor="experienceDuration">Duration:</label>
                      <input type="number" id="editName" name="duration" required/>
                      <Button onClick={this.handleAddExperience} text="Add experience"/>
                      <Button onClick={() => this.setState({currScreen: "edit"})} text="Back"/>
                  </form>
                </div>
              );
            default:
              return null;
          }
    }
}
