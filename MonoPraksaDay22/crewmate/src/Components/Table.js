import CrewmateService from '../Services/CrewmateService';
import TableRow from './TableRow';
import axios from 'axios';
export default function Table({ crewmates, fetchData, isLoading}) {

    async function deleteCrewmate(id) {
        const response = window.confirm(`Are you sure you want to delete?`);
        if(!response)return;

        await CrewmateService.deleteCrewmate(id).then(() => {
            fetchData();
        });
    }
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
        { crewmates.map(crewmate => <TableRow key={crewmate.id} crewmate={crewmate} deleteCrewmate={deleteCrewmate} />)}
        </tbody>
        </table>) : (
            <tr>
                {isLoading ? <td>Loading...</td> : <td>No crewmates found!</td>}
            </tr>
        )
    );
}