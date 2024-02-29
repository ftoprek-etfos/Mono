import axios from "axios";
async function login(username, password) {
  return await axios.post("https://localhost:44323/Api/User/login", {
    username: username,
    password: password,
    grant_type: "password"
  },  {headers: {'content-type': 'application/x-www-form-urlencoded'}});
}

async function getPlayers() {
    const token = localStorage.getItem("AuthToken");
  return await axios.get("https://localhost:44323/api/TeamLeader/699fb301-440d-4c36-bc78-e1200e8157c5",
  {headers: {'Authorization': `Bearer ${token}`}});
}

async function registerUser(data) {
  return await axios.post("https://localhost:44323/Api/User/Register", data);
}
const FootballService = {
    login,
    getPlayers,
    registerUser,
    };

export default FootballService;