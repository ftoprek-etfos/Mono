import { Container, Row, Col, Form, Card, Button } from "react-bootstrap";
import { useState } from "react";
import FootballService from "./FootballService";
import { useNavigate } from "react-router-dom";

export default function RegisterPage() {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        FirstName: '',
        LastName: '',
        Username: '',
        Password: '',
        VerifyPassword: '',
        Email: '',
        SportCategory: '699fb301-440d-4c36-bc78-e1200e8157c5',
        TeamId: null,
        Height: '',
        Weight: '',
        PreferredPositionId: null,
        DateOfBirth: '',
        CountyId: 'e7a10345-ad7e-4d85-bdc8-ccdf98b08a76',
        Description: '',
      });

    function handleInputChange(e) {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    }
    async function onRegister(e) {
        e.preventDefault();
        await FootballService.registerUser(formData);
        navigate("/login");
    }
    return (
        <Container>
          <Row className="vh-100 d-flex justify-content-center align-items-center">
            <Col md={8} lg={6} xs={12}>
              <div className="border border-3 border-primary"></div>
              <Card className="shadow">
                <Card.Body>
                  <div className="mb-3 mt-md-4">
                    <p className=" mb-5">Create your account!</p>
                    <div className="mb-3">
                      <Form onSubmit={onRegister}>
                        <Form.Group className="mb-3" controlId="formBasicFirstName">
                          <Form.Label className="text-center">First Name</Form.Label>
                          <Form.Control
                            type="text"
                            name="FirstName"
                            placeholder="Enter First Name"
                            value={formData.FirstName}
                            onChange={handleInputChange}
                          />
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formBasicLastName">
                          <Form.Label className="text-center">Last Name</Form.Label>
                          <Form.Control
                            type="text"
                            name="LastName"
                            placeholder="Enter Last Name"
                            value={formData.LastName}
                            onChange={handleInputChange}
                          />
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formBasicEmail">
                          <Form.Label className="text-center">Email:</Form.Label>
                          <Form.Control
                            type="text"
                            name="Email"
                            placeholder="Enter Email"
                            value={formData.Email}
                            onChange={handleInputChange}
                          />
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formBasicWeight">
                          <Form.Label className="text-center">Weight</Form.Label>
                          <Form.Control
                            type="number"
                            name="Weight"
                            placeholder="Enter Weight"
                            value={formData.Weight}
                            onChange={handleInputChange}
                          />
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formBasicHeight">
                          <Form.Label className="text-center">Height</Form.Label>
                          <Form.Control
                            type="number"
                            name="Height"
                            placeholder="Enter Height"
                            value={formData.Height}
                            onChange={handleInputChange}
                          />
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formBasicDateOfBirth">
                            <Form.Label className="text-center">Date of Birth</Form.Label>
                            <Form.Control
                                type="date"
                                name="DateOfBirth"
                                placeholder="Enter Date of Birth"
                                value={formData.DateOfBirth}
                                onChange={handleInputChange}
                            />
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formBasicDescription">
                            <Form.Label className="text-center">Date of Birth</Form.Label>
                            <Form.Control
                                type="text"
                                name="Description"
                                placeholder="Enter description"
                                value={formData.Description}
                                onChange={handleInputChange}
                            />
                        </Form.Group>
    
    
                        <Form.Group className="mb-3" controlId="formBasicEmail">
                          <Form.Label className="text-center">Username</Form.Label>
                          <Form.Control
                            type="text"
                            name="Username"
                            placeholder="Enter username"
                            value={formData.Username}
                            onChange={handleInputChange}
                          />
                        </Form.Group>
    
                        <Form.Group className="mb-3" controlId="formBasicPassword">
                          <Form.Label>Password</Form.Label>
                          <Form.Control
                            type="password"
                            name="Password"
                            placeholder="Password"
                            value={formData.Password}
                            onChange={handleInputChange}
                          />
                        </Form.Group>
    
                        <Form.Group className="mb-3" controlId="formBasicVerifyPassword">
                          <Form.Label>Verify Password</Form.Label>
                          <Form.Control
                            type="password"
                            name="VerifyPassword"
                            placeholder="Verify Password"
                            value={formData.VerifyPassword}
                            onChange={handleInputChange}
                          />
                        </Form.Group>
    
                        <div className="d-grid">
                          <Button variant="primary" type="submit">
                            Register
                          </Button>
                        </div>
                      </Form>
                      <div className="mt-3">
                        <p className="mb-0  text-center">
                          Already have an account?{' '}
                          <a href="/login" className="text-primary fw-bold">
                            Sign In
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
    };