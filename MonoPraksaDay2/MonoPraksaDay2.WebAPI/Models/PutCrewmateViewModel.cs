using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoPraksaDay2.WebAPI.Models
{
    public class PutCrewmateViewModel
    {
        public LastMissionViewModel LastMission { get; set; }
        public List<ExperienceViewModel> ExperienceList { get; set; }
        public PutCrewmateViewModel(LastMissionViewModel lastMission, List<ExperienceViewModel> expirienceList)
        {
            LastMission = lastMission;
            ExperienceList = expirienceList;
        }
    }
}