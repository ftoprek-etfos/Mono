import Button from "./Button";
import CenterContainer from "./CenterContainer";

export default function Navbar() {
    return (
        <div style={{display: "flex", justifyContent: "space-around", width: "100%", alignItems: "center", gap: "10px", fontSize: "10px", borderRadius: "2%"}}>
            <Button text="Crewmate logbook" href="/"/>
            <Button text="Show crewmates" href="/list"/>
            <Button text="Add crewmates" href="/add"/>
        </div>
    );
}