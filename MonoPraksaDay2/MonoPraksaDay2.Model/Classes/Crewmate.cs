using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Model.Common;

namespace MonoPraksaDay2.Model
{
    public class Crewmate : ICrewmateViewModel
    {
        [Required]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
#nullable enable
        public ILastMissionViewModel? LastMission { get; set; } = default;
#nullable disable
        public List<IExperienceViewModel>? ExperienceList { get; set; } = default;
        public Crewmate(Guid id, string  firstName, string lastName, int age, LastMissionViewModel lastMission, List<ExperienceViewModel> experienceList)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            LastMission = lastMission;
            if(experienceList != null)
                ExperienceList = experienceList.Cast<IExperienceViewModel>().ToList();
            else
                ExperienceList = null;
        }

        public Crewmate(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Crewmate(Guid id, string firstName, string lastName, int age)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }

        public Crewmate(Guid id, LastMissionViewModel lastMission, List<ExperienceViewModel> experienceList)
        {
            this.Id = id;
            this.LastMission = lastMission;
            this.ExperienceList = experienceList.Cast<IExperienceViewModel>().ToList();
        }


        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Age})\nLastMission: {LastMission}\nExpirienceList: {ExperienceList}";
        }
    }
}