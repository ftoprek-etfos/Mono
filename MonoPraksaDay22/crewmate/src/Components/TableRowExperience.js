import Button from "./Button";

export default function TableRow({experience}) {
    return (
        <tr>
            <td>{experience.title}</td>
            <td>{experience.duration}</td>
        </tr>
    );
}