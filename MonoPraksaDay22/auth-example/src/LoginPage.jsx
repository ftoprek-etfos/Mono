import 'bootstrap/dist/css/bootstrap.min.css';
import { Col, Button, Row, Container, Card, Form } from "react-bootstrap";
import FootballService from './FootballService';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';


export default function LoginPage() {

  const [user, setUser] = useState({username: '', password: ''});
  const navigate = useNavigate();

  function handleInputChange (e) {
    setUser({
      ...user,
      [e.target.name]: e.target.value
    })
    console.log(e.target.name);
  }
  
  async function onSubmit (e) {
    e.preventDefault();
    console.log(user);
    await FootballService.login(user.username, user.password)
    .then(response => {
      localStorage.setItem("AuthToken", response.data.access_token);
      navigate("/");
    })
  }
  
  return (
    <Container>
    <Row className="vh-100 d-flex justify-content-center align-items-center">
      <Col md={8} lg={6} xs={12}>
        <div className="border border-3 border-primary"></div>
        <Card className="shadow">
          <Card.Body>
            <div className="mb-3 mt-md-4">
              <p className=" mb-5">Please enter your login and password!</p>
              <div className="mb-3">
                <Form onSubmit={onSubmit}>
                  <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label className="text-center">
                      Username
                    </Form.Label>
                    <Form.Control type="username" name="username" placeholder="Enter username" onInput={handleInputChange}/>
                  </Form.Group>

                  <Form.Group
                    className="mb-3"
                    controlId="formBasicPassword"
                  >
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" name="password" placeholder="Password" onInput={handleInputChange}/>
                  </Form.Group>
                  <div className="d-grid">
                    <Button variant="primary" type="submit">
                      Login
                    </Button>
                  </div>
                </Form>
                <div className="mt-3">
                  <p className="mb-0  text-center">
                    Don't have an account?{" "}
                    <a href="/register" className="text-primary fw-bold">
                      Sign Up
                    </a>
                  </p>
                </div>
              </div>
            </div>
          </Card.Body>
        </Card>
      </Col>
    </Row>
  </Container>
  );
}
