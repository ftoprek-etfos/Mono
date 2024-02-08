using MonoPraksaDay2.WebAPI.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoPraksaDay2.WebAPI.Help
{
    public static class Helper
    {

        public static void InsertExperience(NpgsqlConnection connection, Guid crewmateId, ExperienceViewModel experience)
        {
            Guid experienceId = Guid.NewGuid();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO \"Experience\" (\"Id\",\"Title\",\"Duration\",\"CrewmateId\") VALUES (@id, @name, @duration, @crewmateId)";

            command.Parameters.AddWithValue("id", experienceId);
            command.Parameters.AddWithValue("name", experience.Title);
            command.Parameters.AddWithValue("duration", experience.Duration);
            command.Parameters.AddWithValue("crewmateId", crewmateId);

            command.ExecuteNonQuery();
        }

        public static GetCrewmateViewModel GetCrewmateById(Guid? id, string connString)
        {
            if (id == null)
                return null;

            NpgsqlConnection connection = new NpgsqlConnection(connString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM \"Crewmate\" WHERE \"Crewmate\".\"Id\" = @id";
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            NpgsqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                connection.Close();
                return null;
            }
            GetCrewmateViewModel crewmateToReturn = null;
            while (reader.Read())
            {
                crewmateToReturn = new GetCrewmateViewModel(
                    (string)reader["FirstName"],
                    (string)reader["LastName"],
                    (int)reader["Age"],
                    reader["LastMissionId"] == DBNull.Value ? null : Helper.GetLastMissionById((Guid)reader["LastMissionId"], connString),
                    GetExperienceListById((Guid)reader["Id"], connString)
                    );
            }

            return crewmateToReturn;
        }

        public static List<ExperienceViewModel> GetExperienceListById(Guid id, string connString)
        {
            if (id == null)
                return null;

            NpgsqlConnection connection = new NpgsqlConnection(connString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM \"Experience\" WHERE \"CrewmateId\" = @id";
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            NpgsqlDataReader reader = command.ExecuteReader();

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
        public static LastMissionViewModel GetLastMissionById(Guid id, string connString)
        {
            if (id == null)
                return null;

            NpgsqlConnection connection = new NpgsqlConnection(connString);

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM \"LastMission\" WHERE \"LastMission\".\"Id\" = @id";
            command.Parameters.AddWithValue("id", id);

            connection.Open();
            NpgsqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                connection.Close();
                return null;
            }
            LastMissionViewModel lastMissionToReturn = null;
            while (reader.Read())
            {
                lastMissionToReturn = new LastMissionViewModel(
                    (string)reader["Name"],
                    (int)reader["Duration"]
                    );
            }
            return lastMissionToReturn;
        }

        public static List<GetCrewmateViewModel> GetFilteredList(List<CrewmateViewModel> crewList, string firstName, string lastName, int age)
        {
            List<CrewmateViewModel> toFilterList = crewList;

            toFilterList = toFilterList.Where(crewmate => ((string.IsNullOrEmpty(firstName) || crewmate.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase))
                && (string.IsNullOrEmpty(lastName) || crewmate.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)) && (age == 0 || crewmate.Age == age))).ToList();

            List<GetCrewmateViewModel> filteredCrewmateList = new List<GetCrewmateViewModel>();
            foreach (CrewmateViewModel crewMember in toFilterList)
            {
                filteredCrewmateList.Add(new GetCrewmateViewModel(crewMember.FirstName, crewMember.LastName, crewMember.Age, crewMember.LastMission, crewMember.ExperienceList));
            }

            return filteredCrewmateList;
        }
    }
}