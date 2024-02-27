import Button from './/Button';
import StatusText from './StatusText';
import Table from './Table';
import { useState } from 'react';
import AddCrewmateForm from './AddCrewmateForm';
import EditCrewmateFormClass from './EditCrewmateFormClass';
import {v4 as uuidv4} from 'uuid';

export default function MainContainer() {
  const [crewmates, setCrewmates] = useState(localStorage.getItem('crewmates') ? JSON.parse(localStorage.getItem('crewmates')) : []);
  const [isShowing, setShowing] = useState(false);
  const [context, setContext] = useState("home");
  const [crewmateToEdit, setCrewmateToEdit] = useState({});
  const [status, setStatus] = useState("");

  const renderContent = () => {
    switch (context) {
      case 'add':
        return (
            <AddCrewmateForm addCrewmates={addCrewmates} setContext={setContext}/>
        );
      case 'home':
        return (
            <>
                <Button text="Crewmate logbook"/>
                <StatusText status={status}/>
                {isShowing && <Table crewmates={crewmates} setCrewmates={setCrewmates} editCrewmate={editCrewmate} setStatus={setStatus}/>}
                <Button text="Show crewmates" onClick={() => setShowing(true)}/>
                <Button text="Add crewmates" onClick={() => setContext("add")}/>
            </>
        );
      case 'edit':
        return (
            <EditCrewmateFormClass setContext={setContext} crewmateToEdit={crewmateToEdit} applyEditCrewmate={applyEditCrewmate} setCrewmateToEdit={setCrewmateToEdit}/>
            );
      default:
        return null;
    }
  };

  function addCrewmates() {
    if(window.document.forms.addCrewmateForm.name.value === '' || window.document.forms.addCrewmateForm.age.value === '' || window.document.forms.addCrewmateForm.role.value === '')
    {
        alert('Please fill all the fields!');
        return;
    }
    const crewmate = {
        id: uuidv4(),
        name: window.document.forms.addCrewmateForm.name.value,
        age: window.document.forms.addCrewmateForm.age.value,
        role: window.document.forms.addCrewmateForm.role.value
    }

    window.localStorage.setItem('crewmates', JSON.stringify([...crewmates, crewmate]));
    setCrewmates([...crewmates, crewmate]);
    setContext("home");
    setStatus("add");
    setTimeout(() => {
        setStatus("");
      }, 2000);
  }

  function editCrewmate(id) {
        const crewmateToEdit = crewmates.find(crewmate => crewmate.id === id);
        setCrewmateToEdit(crewmateToEdit);
        setContext("edit");
  }

  function applyEditCrewmate() {
        let crewmateList = crewmates.filter(crewmate => crewmate.id !== crewmateToEdit.id);
        crewmateToEdit.name = window.document.forms.editCrewmateForm.name.value;
        crewmateToEdit.age = window.document.forms.editCrewmateForm.age.value;
        crewmateToEdit.role = window.document.forms.editCrewmateForm.role.value;
        setCrewmates([...crewmateList, crewmateToEdit]);
        window.localStorage.setItem('crewmates', JSON.stringify([...crewmateList, crewmateToEdit]));
        setContext("home");
        setStatus("edit");
        setTimeout(() => {
            setStatus("");
          }, 2000);
    
  }

  return (
    renderContent()
  );
}