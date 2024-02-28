using MonoPraksaDay2.Model;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Common;
using Model.Common;
using MonoPraksaDay2.Common;
using System.Data;

namespace MonoPraksaDay2.Repository
{
    public class CrewmateRepository : IRepositoryCommon
    {
        static readonly string connString = "Host=localhost;Port=5432;Database=CrewmateDB;Username=postgres;Password=postgres;";
        public async Task<Crewmate> GetCrewmateByIdAsync(Guid id)
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
                Crewmate crewmateToReturn = null;
                while (await reader.ReadAsync())
                {
                    crewmateToReturn = new Crewmate(
                        (Guid)reader["id"],
                        (string)reader["FirstName"],
                        (string)reader["LastName"],
                        (int)reader["Age"],
                        reader["LastMissionId"] == DBNull.Value ? null : await GetLastMissionByIdAsync((Guid)reader["LastMissionId"], connString),
                        await GetExperienceListByIdAsync((Guid)reader["Id"], connString)
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

        public async Task<List<Crewmate>> GetCrewmatesAsync(CrewmateFilter crewmateFilter, Paging paging, Sorting sorting)
        {
            NpgsqlConnection connection = new NpgsqlConnection(connString);

            try
            {
                using (connection)
                {
                    NpgsqlCommand command = new NpgsqlCommand();
                    command.CommandType = CommandType.Text;
                    command.Connection = connection;
                    connection.Open();
                    StringBuilder sqlQueryBuilder = new StringBuilder("SELECT * FROM \"Crewmate\" LEFT JOIN \"LastMission\" ON \"LastMission\".\"Id\" = \"Crewmate\".\"LastMissionId\" WHERE 1=1");

                   if(crewmateFilter != null)
                    {
                        await ApplyFilter(sqlQueryBuilder, command, crewmateFilter);
                    }

                    sqlQueryBuilder.Append($" ORDER BY \"{sorting.ReturnOrderBy()}\" {(sorting.SortOrder == "ASC" ? sorting.SortOrder : "DESC")} OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY");

                    command.Parameters.AddWithValue("offset", paging.ReturnOffset());
                    command.Parameters.AddWithValue("pageSize", paging.PageSize);

                    command.CommandText = sqlQueryBuilder.ToString();
                    await command.PrepareAsync();

                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                    if (!reader.HasRows)
                    {
                        connection.Close();
                        return null;
                    }

                    List<Crewmate> crewList = new List<Crewmate>();

                    while (reader.Read())
                    {
                        LastMissionViewModel lastMission = null;
                        if (reader["Name"] != DBNull.Value && reader["Duration"] != DBNull.Value)
                        {
                            lastMission = new LastMissionViewModel((Guid)reader["LastMissionId"], (string)reader["Name"], (int)reader["Duration"]);
                        }

                            crewList.Add(new Crewmate(
                            (Guid)reader["Id"],
                            (string)reader["FirstName"],
                            (string)reader["LastName"],
                            (int)reader["Age"],
                            lastMission,
                            await GetExperienceListByIdAsync((Guid)reader["Id"], connString)
                        ));
                    }
                    connection.Close();
                    return crewList;
                }
            }catch(Exception e)
            {
                connection.Close();
                connection.Dispose();
                return null;
            }
        }

        public async Task<int> PutCrewmateAsync(Guid id, Crewmate crewmate)
        {
            Crewmate toEdit = await GetCrewmateByIdAsync(id);

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
                    }


                    List<ExperienceViewModel> experienceList = await GetExperienceListByIdAsync(id, connString);
                    if (experienceList == null)
                    {
                        foreach (ExperienceViewModel experience in crewmate.ExperienceList)
                        {
                            await InsertExperienceAsync(connection, id, experience);
                        }
                        npgsqlTransaction.Commit();
                        connection.Close();
                        connection.Dispose();
                        return 1;
                    }

                    foreach (ExperienceViewModel experience in crewmate.ExperienceList)
                    {
                        var matchingExperience = experienceList.FirstOrDefault(exp => exp.Title == experience.Title);

                        if (matchingExperience != null && experience.Title == matchingExperience.Title && experience.Duration != matchingExperience.Duration)
                        {
                            command = new NpgsqlCommand();
                            command.Connection = connection;
                            command.CommandText = "UPDATE \"Experience\" SET \"Duration\" = @duration WHERE \"Experience\".\"Id\" = @id";

                            command.Parameters.AddWithValue("id", experience.Id);
                            command.Parameters.AddWithValue("duration", experience.Duration);

                            await command.ExecuteNonQueryAsync();

                        }
                        else if (matchingExperience == null)
                        {
                            await InsertExperienceAsync(connection, id, experience);
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
            Crewmate crewmateToDelete = await GetCrewmateByIdAsync(id);

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

        public async Task<int> PostCrewmateAsync(Crewmate crewmate)
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

        async Task<List<ExperienceViewModel>> GetExperienceListByIdAsync(Guid id, string connString)
        {
            if (id == null)
                return null;

            NpgsqlConnection connection = new NpgsqlConnection(connString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM \"Experience\" WHERE \"CrewmateId\" = @id";
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                connection.Close();
                return null;
            }

            List<ExperienceViewModel> experienceListToReturn = new List<ExperienceViewModel>();
            while (reader.Read())
            {
                experienceListToReturn.Add(new ExperienceViewModel(
                    (Guid)reader["Id"],
                    (string)reader["Title"],
                    (int)reader["Duration"]
                    ));
            }

            return experienceListToReturn;
        }

        async Task<LastMissionViewModel> GetLastMissionByIdAsync(Guid id, string connString)
        {
            if (id == null)
                return null;

            NpgsqlConnection connection = new NpgsqlConnection(connString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM \"LastMission\" WHERE \"LastMission\".\"Id\" = @id";
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            NpgsqlDataReader reader = await command.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                connection.Close();
                return null;
            }
            LastMissionViewModel lastMissionToReturn = null;
            while (await reader.ReadAsync())
            {
                lastMissionToReturn = new LastMissionViewModel(
                    (Guid)reader["Id"],
                    (string)reader["Name"],
                    (int)reader["Duration"]
                    );
            }
            return lastMissionToReturn;
        }

        async Task InsertExperienceAsync(NpgsqlConnection connection, Guid crewmateId, IExperienceViewModel experience)
        {
            Guid experienceId = Guid.NewGuid();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO \"Experience\" (\"Id\",\"Title\",\"Duration\",\"CrewmateId\") VALUES (@id, @name, @duration, @crewmateId)";

            command.Parameters.AddWithValue("id", experienceId);
            command.Parameters.AddWithValue("name", experience.Title);
            command.Parameters.AddWithValue("duration", experience.Duration);
            command.Parameters.AddWithValue("crewmateId", crewmateId);

            await command.ExecuteNonQueryAsync();
        }

        async Task ApplyFilter(StringBuilder sqlQueryBuilder, NpgsqlCommand command, CrewmateFilter crewmateFilter)
        {
            if (!string.IsNullOrEmpty(crewmateFilter.FirstName))
            {
                sqlQueryBuilder.Append(" AND \"FirstName\" LIKE @firstName");
                command.Parameters.AddWithValue("firstName", NpgsqlDbType.Varchar, crewmateFilter.FirstName);
            }

            if (!string.IsNullOrEmpty(crewmateFilter.LastName))
            {
                sqlQueryBuilder.Append(" AND \"LastName\" LIKE @lastName");
                command.Parameters.AddWithValue("lastName", crewmateFilter.LastName);
            }

            if (crewmateFilter.Age != null)
            {
                sqlQueryBuilder.Append(" AND \"Age\" = @age");
                command.Parameters.AddWithValue("age", crewmateFilter.Age);
            }

            if (crewmateFilter.LastMissionId != null)
            {
                sqlQueryBuilder.Append(" AND \"LastMissionId\" = @lastMissionId");
                command.Parameters.AddWithValue("lastMissionId", crewmateFilter.LastMissionId);
            }
        }
    }
}
