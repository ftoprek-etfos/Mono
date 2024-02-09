using MonoPraksaDay2.Model;

namespace MonoPraksaDay2.WebAPI.Models
{
    public class PostCrewmateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
#nullable enable
        public LastMissionViewModel? LastMission { get; set; } = default;
        public PostCrewmateViewModel(string firstName, string lastName, int age, LastMissionViewModel? lastMission)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.LastMission = lastMission;
        }
    }
}