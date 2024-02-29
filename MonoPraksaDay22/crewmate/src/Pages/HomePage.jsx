import 'bootstrap/dist/css/bootstrap.min.css';
import Button from '../Components/Button';
import CenterContainer from "../Components/CenterContainer";
import Navbar from '../Components/Navbar';

export default function HomePage()
{
    return (
        <CenterContainer>
            <Navbar/>
            <h2>
                Welcome to the Crewmate Database
            </h2>
            <img src="https://cdn.pixabay.com/photo/2013/07/13/11/29/space-158243_1280.png" alt="space" width="50%"/><br/>
        </CenterContainer>
    );
}