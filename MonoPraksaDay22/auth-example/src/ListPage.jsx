import 'bootstrap/dist/css/bootstrap.min.css';
import { Table, Container } from "react-bootstrap";
import FootballService from './FootballService';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

export default function LoginPage() {

  const [playerList, setPlayerList] = useState([{}]);
  const navigate = useNavigate();

  useEffect(() => {
    async function fetchData() {
        try{
            const response = await FootballService.getPlayers();
            setPlayerList(response.data.List);
        }catch(error){
            console.error(error);
            localStorage.removeItem("AuthToken");
            navigate("/login");
        }
    }
    fetchData();
  }, []);



  return (
    <Container>
         <Table striped bordered hover responsive>
      <thead>
        <tr>
          <th>ID</th>
          <th>First Name</th>
          <th>Last Name</th>
          <th>Username</th>
          <th>Height</th>
          <th>Weight</th>
          <th>Date of Birth</th>
          <th>County</th>
          <th>Description</th>
        </tr>
      </thead>
      <tbody>
        {playerList.map(player => (
          <tr key={player.Id}>
            <td>{player.Id}</td>
            <td>{player.FirstName}</td>
            <td>{player.LastName}</td>
            <td>{player.Username}</td>
            <td>{player.Height}</td>
            <td>{player.Weight}</td>
            <td>{new Date(player.DateOfBirth).toLocaleDateString()}</td>
            <td>{player.County ? player.County.CountyName : ''}</td>
            <td>{player.Description}</td>
          </tr>
        ))}
      </tbody>
    </Table>
  </Container>
  );
}
