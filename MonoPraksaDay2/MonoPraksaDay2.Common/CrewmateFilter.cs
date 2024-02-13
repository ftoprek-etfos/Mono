using System;

namespace MonoPraksaDay2.Common
{
    public class CrewmateFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public Guid? LastMissionId { get; set; }

        public CrewmateFilter(string firstName, string lastName, int? age, Guid? lastMissionId)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            LastMissionId = lastMissionId;
        }
    }
}
