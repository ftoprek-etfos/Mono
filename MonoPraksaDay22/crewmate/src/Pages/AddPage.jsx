import CenterContainer from "../Components/CenterContainer";
import AddCrewmateForm from "../Components/AddCrewmateForm";
import Navbar from "../Components/Navbar";

export default function AddPage() {
    return (
        <CenterContainer>
            <Navbar/>
            <AddCrewmateForm />
        </CenterContainer>
    );
}