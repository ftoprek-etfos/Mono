using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Model.Common;
using MonoPraksaDay2.Model;

namespace MonoPraksaDay2.WebAPI.Models
{
    public class GetCrewmateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
#nullable enable
        public ILastMissionViewModel? LastMission { get; set; } = default;
        public List<IExperienceViewModel>? ExperienceList { get; set; }
        public GetCrewmateViewModel(string firstName, string lastName, int age, ILastMissionViewModel? lastMissionViewModel, List<IExperienceViewModel>? experienceViewModels)
        {
            this.Age = age;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.LastMission = lastMissionViewModel;
            this.ExperienceList = experienceViewModels;
        }
    }
}