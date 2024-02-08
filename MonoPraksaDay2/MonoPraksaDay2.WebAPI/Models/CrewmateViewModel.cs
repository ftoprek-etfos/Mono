using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonoPraksaDay2.WebAPI.Models
{
    public class CrewmateViewModel
    {
        private Guid guid;
        private string v1;
        private string v2;
        private int v3;
        private object value;

        [Required]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
#nullable enable
        public LastMissionViewModel? LastMission { get; set; } = default;
#nullable disable
        public List<ExperienceViewModel> ExperienceList { get; set; }

        public CrewmateViewModel(Guid id, string  firstName, string lastName, int age, LastMissionViewModel lastMission, List<ExperienceViewModel> experienceList)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            LastMission = lastMission;
            ExperienceList = experienceList;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Age})\nLastMission: {LastMission}\nExpirienceList: {ExperienceList}";
        }
    }
}