import Button from "./Button";
import { useNavigate } from 'react-router-dom';

export default function TableRow({crewmate, deleteCrewmate}) {

    const navigate = useNavigate();

    const handleEditClick = () => {
      const id = crewmate.id;
  
      navigate(`/edit/${id}`);
    };

    return (
        <tr>
            <td>{crewmate.firstName}</td>
            <td>{crewmate.lastName}</td>
            <td>{crewmate.age}</td>
            <td>
                <Button isBig={false} text="Edit" onClick={handleEditClick} />
                <Button isBig={false} onClick={() =>deleteCrewmate(crewmate.id)} text="Delete" />
            </td>
        </tr>
    );
}