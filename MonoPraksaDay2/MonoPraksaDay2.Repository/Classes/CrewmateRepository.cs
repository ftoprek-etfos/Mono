using MonoPraksaDay2.Repository.Classes;
using MonoPraksaDay2.WebAPI.Models;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay2.Repository
{
    public class CrewmateRepository : ICommon
    {
        static string connString = "Host=localhost;Port=5432;Database=CrewmateDB;Username=postgres;Password=admin;";
        public CrewmateViewModel GetCrewmateById(Guid id)
        {
            if (id == null)
                return null;

            NpgsqlConnection connection = new NpgsqlConnection(connString);

            try
            {
                NpgsqlCommand command = new NpgsqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM \"Crewmate\" WHERE \"Crewmate\".\"Id\" = @id";
                command.Parameters.AddWithValue("id", id);

                connection.Open();
                NpgsqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    connection.Close();
                    connection.Dispose();
                    return null;
                }
                CrewmateViewModel crewmateToReturn = null;
                while (reader.Read())
                {
                    crewmateToReturn = new CrewmateViewModel(
                        (Guid)reader["id"],
                        (string)reader["FirstName"],
                        (string)reader["LastName"],
                        (int)reader["Age"],
                        reader["LastMissionId"] == DBNull.Value ? null : Helper.GetLastMissionById((Guid)reader["LastMissionId"], connString),
                        Helper.GetExperienceListById((Guid)reader["Id"], connString)
                        );
                }
                connection.Close();
                connection.Dispose();

                return crewmateToReturn;
            }
            catch (Exception ex)
            {
                connection.Close();
                connection.Dispose();
                return null;
            }            
        }

        public List<CrewmateViewModel> GetCrewmates(string firstName = null, string lastName = null, int age = 0)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connString);

            try
            {
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
                        return null;
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

                    return crewList;
                }
            }catch(Exception e)
            {
                connection.Close();
                connection.Dispose();
                return null;
            }
        }

        public int PutCrewmate(Guid id, CrewmateViewModel crewmate)
        {
            CrewmateViewModel toEdit = GetCrewmateById(id);

            if (toEdit == null)
                return 0;

            NpgsqlConnection connection = new NpgsqlConnection(connString);
            NpgsqlTransaction npgsqlTransaction = null;
            try
            {
                using (connection)
                {
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    
                    connection.Open();
                    npgsqlTransaction = connection.BeginTransaction();

                    Guid lastMissionId = Guid.NewGuid();

                    if (toEdit.LastMission != null)
                    {
                        if (toEdit.LastMission.Name != crewmate.LastMission.Name)
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
                    if (experienceList == null)
                    {
                        foreach (ExperienceViewModel experience in crewmate.ExperienceList)
                        {
                            Helper.InsertExperience(connection, id, experience);
                        }

                        connection.Close();
                        return 1;
                    }

                    foreach (ExperienceViewModel experience in experienceList)
                    {
                        var matchingExperience = crewmate.ExperienceList.FirstOrDefault(exp => exp.Title == experience.Title);

                        if (experience.Title == matchingExperience.Title && experience.Duration != matchingExperience.Duration)
                        {
                            command = new NpgsqlCommand();
                            command.Connection = connection;
                            command.CommandText = "UPDATE \"Experience\" SET \"Duration\" = @duration WHERE \"Experience\".\"Id\" = @id";

                            command.Parameters.AddWithValue("id", experience.Id);
                            command.Parameters.AddWithValue("duration", matchingExperience.Duration);

                            command.ExecuteNonQuery();

                        }
                        else if (experience.Title != matchingExperience.Title && experience.Duration != matchingExperience.Duration)
                        {
                            Helper.InsertExperience(connection, id, matchingExperience);
                        }
                    }
                    npgsqlTransaction.Commit();
                    connection.Close();

                    return 1;
                }

            }
            catch (Exception ex)
            {
                npgsqlTransaction.Rollback();
                connection.Close();
                connection.Dispose();
                return -1;
            }
        }

        public int DeleteCrewmate(Guid id)
        {
            CrewmateViewModel crewmateToDelete = GetCrewmateById(id);

            if (crewmateToDelete == null)
                return 0;

            NpgsqlConnection connection = new NpgsqlConnection(connString);

            try
            {
                using (connection)
                {
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.Connection = connection;
                    command.CommandText = "DELETE FROM \"Crewmate\" WHERE \"Crewmate\".\"Id\" = @id";
                    command.Parameters.AddWithValue("id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }catch(Exception ex)
            {
                connection.Close();
                connection.Dispose();
                return -1;
            }

            return 1;
        }

        public int PostCrewmate(CrewmateViewModel crewmate)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connString);
            try
            {
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

                    if (command.ExecuteNonQuery() <= 0)
                    {
                        connection.Close();
                        return 0;
                    }

                    connection.Close();
                    return 1;
                }
            }
            catch(Exception ex)
            {
                connection.Close();
                connection.Dispose();
                return -1;
            }

        }
    }
}
