using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using MonoPraksaDay2.Model;

namespace MonoPraksaDay2.WebAPI.Models
{
    public class GetCrewmateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
#nullable enable
        public LastMissionViewModel? LastMission { get; set; } = default;
        public List<ExperienceViewModel>? ExperienceList { get; set; }
        public GetCrewmateViewModel(string firstName, string lastName, int age, LastMissionViewModel? lastMissionViewModel, List<ExperienceViewModel>? experienceViewModels)
        {
            this.Age = age;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.LastMission = lastMissionViewModel;
            this.ExperienceList = experienceViewModels;
        }
    }
}