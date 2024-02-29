
/*import { useEffect, useState } from 'react';
import AddCrewmateForm from './AddCrewmateForm';
import EditCrewmateFormClass from './EditCrewmateFormClass';
import axios from 'axios';


 function MainContainer() {
  const [context, setContext] = useState("home");

  const renderContent = () => {
    switch (context) {
      case 'add':
        return (
            
        );
      case 'edit':
        return (
            );
      default:
        return null;
    }
  };

  function addCrewmates(crewmate) {
    
    axios.post('https://localhost:44334/api/Crew', crewmate);
    setContext("home");
    setStatus("add");
    setTimeout(() => {
        setStatus("");
      }, 2000);
  }





  return (
    renderContent()
  );
}*/