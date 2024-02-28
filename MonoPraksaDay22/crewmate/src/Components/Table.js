import TableRow from './TableRow';
import axios from 'axios';
export default function Table({ crewmates, setCrewmates, editCrewmate, setStatus}) {

    function deleteCrewmate(id) {
        const response = window.confirm(`Are you sure you want to delete?`);
        if(!response)return;

        axios.delete(`https://localhost:44334/api/Crew/${id}`).then(() => {
            setCrewmates(crewmates.filter(crewmate => crewmate.id !== id));
        });
        setStatus("delete");
        setTimeout(() => {
            setStatus("");
          }, 2000);
    }
    console.log(crewmates)

    return (
        crewmates.length > 0 ? (
        <table className="crewmateList">
        <thead>
            <tr>
                <th>First name</th>
                <th>Last name</th>
                <th>Age</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
        { crewmates.map(crewmate => <TableRow key={crewmate.id} crewmate={crewmate} editCrewmate={editCrewmate} deleteCrewmate={deleteCrewmate} />)}
        </tbody>
        </table>) : (
            <tr>
                <td>No crewmates found!</td>
            </tr>
        )
    );
}