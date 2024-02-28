import TableRowExperience from './TableRowExperience';
import axios from 'axios';
export default function ExperienceTable({experienceList}) {

    function deleteCrewmate(id) {
        const response = window.confirm(`Are you sure you want to delete?`);
        if(!response)return;

    }

    return (
        experienceList ? (
        <table className="crewmateList">
        <thead>
            <tr>
                <th>Title</th>
                <th>Duration</th>
            </tr>
        </thead>
        <tbody>
        { experienceList.map(experience => <TableRowExperience key={experience.duration} experience={experience}/>)}
        </tbody>
        </table>) : (
            <tr>
                <td>No experiences found!</td>
            </tr>
        )
    );
}