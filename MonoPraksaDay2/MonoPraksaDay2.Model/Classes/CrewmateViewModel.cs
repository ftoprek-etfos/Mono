using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Common;

namespace MonoPraksaDay2.Model
{
    public class CrewmateViewModel : ICrewmateViewModel
    {
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

        public CrewmateViewModel(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public CrewmateViewModel(Guid id, LastMissionViewModel lastMission, List<ExperienceViewModel> experienceList)
        {
            this.Id = id;
            this.LastMission = lastMission;
            this.ExperienceList = experienceList;
        }


        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Age})\nLastMission: {LastMission}\nExpirienceList: {ExperienceList}";
        }
    }
}