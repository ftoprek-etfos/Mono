using MonoPraksaDay2.Repository.Classes;
using MonoPraksaDay2.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Repository.Common;
using Model.Common;

namespace MonoPraksaDay2.Repository
{
    public class CrewmateRepository : IRepositoryCommon
    {
        static readonly string connString = "Host=localhost;Port=5432;Database=CrewmateDB;Username=postgres;Password=admin;";
        public async Task<CrewmateViewModel> GetCrewmateByIdAsync(Guid id)
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
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    connection.Close();
                    connection.Dispose();
                    return null;
                }
                CrewmateViewModel crewmateToReturn = null;
                while (await reader.ReadAsync())
                {
                    crewmateToReturn = new CrewmateViewModel(
                        (Guid)reader["id"],
                        (string)reader["FirstName"],
                        (string)reader["LastName"],
                        (int)reader["Age"],
                        reader["LastMissionId"] == DBNull.Value ? null : await Helper.GetLastMissionByIdAsync((Guid)reader["LastMissionId"], connString),
                        await Helper.GetExperienceListByIdAsync((Guid)reader["Id"], connString)
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

        public async Task<List<CrewmateViewModel>> GetCrewmatesAsync(string firstName = null, string lastName = null, int age = 0)
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
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }

                    List<CrewmateViewModel> crewList = new List<CrewmateViewModel>();

                    while (reader.Read())
                    {
                        LastMissionViewModel lastMission = null;
                        if (reader["Name"] != DBNull.Value && reader["Duration"] != DBNull.Value)
                        {
                            lastMission = new LastMissionViewModel((string)reader["Name"], (int)reader["Duration"]);
                        }

                            crewList.Add(new CrewmateViewModel(
                            (Guid)reader["Id"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (int)reader["Age"],
                            lastMission,
                            await Helper.GetExperienceListByIdAsync((Guid)reader["Id"], connString)
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

        public async Task<int> PutCrewmateAsync(Guid id, CrewmateViewModel crewmate)
        {
            CrewmateViewModel toEdit = await GetCrewmateByIdAsync(id);

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

                            await command.ExecuteNonQueryAsync();

                            command = new NpgsqlCommand();
                            command.Connection = connection;
                            command.CommandText = "UPDATE \"Crewmate\" SET \"LastMissionId\" = @lastMissionId WHERE \"Crewmate\".\"Id\" = @id";
                            command.Parameters.AddWithValue("id", id);
                            command.Parameters.AddWithValue("lastMissionId", lastMissionId);
                            await command.ExecuteNonQueryAsync();
                        }
                    }else
                    {
                        command.CommandText = "INSERT INTO \"LastMission\" (\"Id\",\"Name\",\"Duration\") VALUES (@id, @name, @duration)";
                        command.Parameters.AddWithValue("id", lastMissionId);
                        command.Parameters.AddWithValue("name", crewmate.LastMission.Name);
                        command.Parameters.AddWithValue("duration", crewmate.LastMission.Duration);

                        await command.ExecuteNonQueryAsync();

                        command = new NpgsqlCommand();
                        command.Connection = connection;
                        command.CommandText = "UPDATE \"Crewmate\" SET \"LastMissionId\" = @lastMissionId WHERE \"Crewmate\".\"Id\" = @id";
                        command.Parameters.AddWithValue("id", id);
                        command.Parameters.AddWithValue("lastMissionId", lastMissionId);
                        await command.ExecuteNonQueryAsync();
                    }

                    List<ExperienceViewModel> experienceList = await Helper.GetExperienceListByIdAsync(id, connString);
                    if (experienceList == null)
                    {
                        foreach (ExperienceViewModel experience in crewmate.ExperienceList)
                        {
                            await Helper.InsertExperienceAsync(connection, id, experience);
                        }
                        npgsqlTransaction.Commit();
                        connection.Close();
                        connection.Dispose();
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

                            await command.ExecuteNonQueryAsync();

                        }
                        else if (experience.Title != matchingExperience.Title && experience.Duration != matchingExperience.Duration)
                        {
                            await Helper.InsertExperienceAsync(connection, id, matchingExperience);
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

        public async Task<int> DeleteCrewmateAsync(Guid id)
        {
            CrewmateViewModel crewmateToDelete = await GetCrewmateByIdAsync(id);

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
                    await command.ExecuteNonQueryAsync();
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

        public async Task<int> PostCrewmateAsync(CrewmateViewModel crewmate)
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
                    command.Parameters.AddWithValue("lastMission", DBNull.Value);

                    connection.Open();

                    if (await command.ExecuteNonQueryAsync() <= 0)
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
