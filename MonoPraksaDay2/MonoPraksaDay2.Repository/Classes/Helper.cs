using Model.Common;
using MonoPraksaDay2.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPraksaDay2.Repository.Classes
{
    public static class Helper
    {

        public async static Task<List<ExperienceViewModel>> GetExperienceListByIdAsync(Guid id, string connString)
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

        public async static Task<LastMissionViewModel> GetLastMissionByIdAsync(Guid id, string connString)
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
                    (string)reader["Name"],
                    (int)reader["Duration"]
                    );
            }
            return lastMissionToReturn;
        }

        public async static Task InsertExperienceAsync(NpgsqlConnection connection, Guid crewmateId, IExperienceViewModel experience)
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
    }
}
