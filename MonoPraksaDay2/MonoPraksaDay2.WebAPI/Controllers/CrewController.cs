using MonoPraksaDay2.WebAPI.Help;
using MonoPraksaDay2.WebAPI.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MonoPraksaDay2.WebAPI.Controllers
{
    public class CrewController : ApiController
    {
        static string connString = "Host=localhost;Port=5432;Database=CrewmateDB;Username=postgres;Password=admin;";

        // GET: api/Crew
        [HttpGet]
        public HttpResponseMessage GetCrewList(string firstName = null, string lastName = null, int age = 0)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connString);
            using (connection)
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM \"Crewmate\" LEFT JOIN \"LastMission\" ON \"LastMission\".\"Id\" = \"Crewmate\".\"LastMissionId\" WHERE " +
                                                 "(@firstName IS NULL OR \"FirstName\" LIKE @firstName) AND " +
                                                 "(@lastName IS NULL OR \"LastName\" LIKE @lastName) AND " +
                                                 "(@age = 0 OR \"Age\" = @age)"; 
                command.Parameters.AddWithValue("firstName", NpgsqlDbType.Varchar, (object)firstName ?? DBNull.Value);
                command.Parameters.AddWithValue("lastName", NpgsqlDbType.Varchar, (object)lastName ?? DBNull.Value);
                command.Parameters.AddWithValue("age", NpgsqlDbType.Integer, age);
                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No crewmates found!");
                }

                List<CrewmateViewModel> crewList = new List<CrewmateViewModel>();

                while (reader.Read())
                {
                    LastMissionViewModel lastMission = new LastMissionViewModel((string)reader["Name"], (int)reader["Duration"]);

                    crewList.Add(new CrewmateViewModel(
                        (Guid)reader["Id"], 
                        (string)reader["FirstName"], 
                        (string)reader["LastName"], 
                        (int)reader["Age"],
                        lastMission,
                        Helper.GetExperienceListById((Guid)reader["Id"], connString)
                    ));
                }

                List<GetCrewmateViewModel> filteredCrewmateList = Helper.GetFilteredList(crewList, firstName, lastName, age);

                return Request.CreateResponse(HttpStatusCode.OK, filteredCrewmateList);
            }
        }

        // GET: api/Crew/1
        [HttpGet]
        public HttpResponseMessage GetCrewmate(Guid id)
        {
            GetCrewmateViewModel getCrewmate = Helper.GetCrewmateById(id, connString);

            if (getCrewmate == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Crewmate not found under id {id}!");

            return Request.CreateResponse(HttpStatusCode.OK, getCrewmate);
        }


        // POST: api/Crew
        [HttpPost]
        public HttpResponseMessage PostCrewmate(PostCrewmateViewModel crewmate)
        {
            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide data for a crewmate");

            NpgsqlConnection connection = new NpgsqlConnection(connString);
            using (connection)
            {
                Guid id = Guid.NewGuid();

                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO \"Crewmate\" (\"Id\",\"FirstName\",\"LastName\",\"Age\",\"LastMissionId\") VALUES (@id, @fname, @lname, @age, @lastMission)";
                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("fname", crewmate.FirstName);
                command.Parameters.AddWithValue("lname", crewmate.LastName);
                command.Parameters.AddWithValue("age", crewmate.Age);
                command.Parameters.AddWithValue("lastMission", crewmate.LastMission.Id);

                connection.Open();

                if(command.ExecuteNonQuery() <= 0)
                {
                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Failed to add a new crewmate {crewmate.FirstName}");
                }

                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Added a new crewmate {crewmate.FirstName}");
        }
        
        // PUT: api/Crew/id
        [HttpPut]
        public HttpResponseMessage PutEditCrewmate(Guid id, [FromBody] PutCrewmateViewModel crewmate)
        {
            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide an edit to the crewmate");

            GetCrewmateViewModel toEdit = Helper.GetCrewmateById(id,connString);
            
            if (toEdit == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Crewmate not found.");

            NpgsqlConnection connection = new NpgsqlConnection(connString);
            using (connection)
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;

                connection.Open();

                Guid lastMissionId = Guid.NewGuid();

                if(toEdit.LastMission != null)
                { 
                    if(toEdit.LastMission.Name != crewmate.LastMission.Name)
                    {
                        command.CommandText = "INSERT INTO \"LastMission\" (\"Id\",\"Name\",\"Duration\") VALUES (@id, @name, @duration)";
                        command.Parameters.AddWithValue("id", lastMissionId);
                        command.Parameters.AddWithValue("name", crewmate.LastMission.Name);
                        command.Parameters.AddWithValue("duration", crewmate.LastMission.Duration);

                        command.ExecuteNonQuery();

                        command = new NpgsqlCommand();
                        command.Connection = connection;
                        command.CommandText = "UPDATE \"Crewmate\" SET \"LastMissionId\" = @lastMissionId WHERE \"Crewmate\".\"Id\" = @id";
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("lastMissionId", lastMissionId);
                        command.ExecuteNonQuery();
                    }
                }

                List<ExperienceViewModel> experienceList = Helper.GetExperienceListById(id, connString);
                if(experienceList == null)
                {
                    foreach(ExperienceViewModel experience in crewmate.ExperienceList)
                    {
                        Helper.InsertExperience(connection, id, experience);
                    }

                    connection.Close();
                    return Request.CreateResponse(HttpStatusCode.OK, $"Crewmate {toEdit.FirstName} edited successfully");
                }

                foreach (ExperienceViewModel experience in experienceList)
                {
                    var matchingExperience = crewmate.ExperienceList.FirstOrDefault(exp => exp.Title == experience.Title);

                    if(experience.Title == matchingExperience.Title && experience.Duration != matchingExperience.Duration)
                    {
                        command = new NpgsqlCommand();
                        command.Connection = connection;
                        command.CommandText = "UPDATE \"Experience\" SET \"Duration\" = @duration WHERE \"Experience\".\"Id\" = @id";

                        command.Parameters.AddWithValue("id", experience.Id);
                        command.Parameters.AddWithValue("duration", experience.Duration);

                        command.ExecuteNonQuery();

                    }else if(experience.Title != matchingExperience.Title && experience.Duration != matchingExperience.Duration)
                    {
                        Helper.InsertExperience(connection, id, experience);
                    }
                }
                connection.Close();
            }
            return Request.CreateResponse(HttpStatusCode.OK, $"Crewmate {toEdit.FirstName} edited successfully");
        }

        // DELETE: api/Crew/id
        [HttpDelete]
        public HttpResponseMessage DeleteCrewmate(Guid? id)
        {
            GetCrewmateViewModel crewmateToDelete = Helper.GetCrewmateById(id, connString);

            if(crewmateToDelete == null )
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Crewmate not found under id {id}");

            NpgsqlConnection connection = new NpgsqlConnection(connString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM \"Crewmate\" WHERE \"Crewmate\".\"Id\" = @id";
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            connection.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, $"Crewmate deleted successfully");
        }
    }
}