import TableRow from './TableRow';

export default function Table({ crewmates, setCrewmates, editCrewmate, setStatus}) {

    function deleteCrewmate(id) {
        const response = window.confirm(`Are you sure you want to delete?`);
        if(!response)return;

        const crewmatesFiltered = crewmates.filter(crewmate => crewmate.id !== id);
        localStorage.setItem('crewmates', JSON.stringify(crewmatesFiltered));
        setCrewmates(crewmatesFiltered);
        setStatus("delete");
        setTimeout(() => {
            setStatus("");
          }, 2000);
    }

    return (
        crewmates.length > 0 ? (
        <table className="crewmateList">
        <thead>
            <tr>
                <th>Name</th>
                <th>Age</th>
                <th>Role</th>
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