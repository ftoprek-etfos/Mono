import axios from "axios";

const applyFilters = (filters, paging, sorting) => {
    let url = `https://localhost:44334/api/Crew?pageNumber=${paging.pageNumber}&pageSize=${paging.pageSize}&orderBy=${sorting.orderBy}&sortOrder=${sorting.sortOrder}`;
    if(filters.firstName !== "") url += `&firstName=${filters.firstName}`;
    if(filters.lastName !== "") url += `&lastName=${filters.lastName}`;
    if(filters.age !== "") url += `&age=${filters.age}`;
    if(filters.lastMissionId !== "") url += `&lastMissionId=${filters.lastMissionId}`;
    return url;
  };

const getAllCrewmates = async (filters, paging, sorting) => {
    return await axios.get(applyFilters(filters, paging, sorting));
    };

const getCrewmateById = async (id) => {
    return await axios.get(`https://localhost:44334/api/Crew/${id}`);
    };

const addCrewmate = async (crewmate) => {
    return await axios.post('https://localhost:44334/api/Crew', crewmate);
    };

const addExperience = async (crewmate) => {
    const putCrewmateInfo = {
        experienceList: crewmate.experienceList
    };
    return await axios.put(`https://localhost:44334/api/Crew/${crewmate.id}`, putCrewmateInfo);
    };

const editCrewmate = async (crewmate) => {
    const putCrewmateInfo = {
        lastMission: {
            name: crewmate.lastMission.name,
            duration: crewmate.lastMission.duration
        },
        experienceList: crewmate.experienceList.map((experience) => {
            const { id, ...restOfExperience } = experience;
            return restOfExperience;
        })
    };
    return await axios.put(`https://localhost:44334/api/Crew/${crewmate.id}`, putCrewmateInfo);
    };

const deleteCrewmate = async (id) => {
    return await axios.delete(`https://localhost:44334/api/Crew/${id}`);
    };

const getMissions = async () => {
    return await axios.get('https://localhost:44334/api/Crew/Missions');
    };

const CrewmateService = {
    getAllCrewmates,
    getCrewmateById,
    addCrewmate,
    editCrewmate,
    addExperience,
    getMissions,
    deleteCrewmate
};

export default CrewmateService;