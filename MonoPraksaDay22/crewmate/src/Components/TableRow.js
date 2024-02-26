import Button from "./Button";

export default function TableRow({crewmate, editCrewmate, deleteCrewmate}) {
    return (
        <tr>
            <td>{crewmate.name}</td>
            <td>{crewmate.age}</td>
            <td>{crewmate.role}</td>
            <td>
                <Button isBig={false} onClick={() =>editCrewmate(crewmate.id)} text="Edit" />
                <Button isBig={false} onClick={() =>deleteCrewmate(crewmate.id)} text="Delete" />
            </td>
        </tr>
    );
}