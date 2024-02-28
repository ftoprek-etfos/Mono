import Button from './/Button';
import StatusText from './StatusText';
import Table from './Table';
import { useEffect, useState } from 'react';
import AddCrewmateForm from './AddCrewmateForm';
import EditCrewmateFormClass from './EditCrewmateFormClass';
import axios from 'axios';

export default function MainContainer() {
  const [crewmates, setCrewmates] = useState([]);
  const [isShowing, setShowing] = useState(false);
  const [context, setContext] = useState("home");
  const [crewmateToEdit, setCrewmateToEdit] = useState({});
  const [status, setStatus] = useState("");

  useEffect(() => {
    axios.get('https://localhost:44334/api/Crew').then((response) => {
      setCrewmates(response.data);
    });
  }, [status]);

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
    if(window.document.forms.addCrewmateForm.firstName.value === '' || window.document.forms.addCrewmateForm.lastName.value === '' || window.document.forms.addCrewmateForm.age.value === '')
    {
        alert('Please fill all the fields!');
        return;
    }
    const crewmate = {
        firstName: window.document.forms.addCrewmateForm.firstName.value,
        lastName: window.document.forms.addCrewmateForm.lastName.value,
        age: window.document.forms.addCrewmateForm.age.value
      }
    axios.post('https://localhost:44334/api/Crew', crewmate).then((response) => {
    });
    setContext("home");
    setStatus("add");
    setTimeout(() => {
        setStatus("");
      }, 2000);
  }

  async function editCrewmate(id) {
        await axios.get(`https://localhost:44334/api/Crew/${id}`).then((response) => {
            setCrewmateToEdit(response.data);
        });
        setContext("edit");
  }

  function applyEditCrewmate() {
        let crewmateList = crewmates.filter(crewmate => crewmate.id !== crewmateToEdit.id);
        setCrewmates([...crewmateList, crewmateToEdit]);

        const putCrewmateInfo = {
          lastMission: {
            name: crewmateToEdit.lastMission.name,
            duration: crewmateToEdit.lastMission.duration
          },
          experienceList: crewmateToEdit.experienceList.map((experience) => {
            const { id, ...restOfExperience } = experience;
            return restOfExperience;
          })
        };
        axios.put(`https://localhost:44334/api/Crew/${crewmateToEdit.id}`, putCrewmateInfo);
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