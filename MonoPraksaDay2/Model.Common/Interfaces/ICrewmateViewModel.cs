using System;
using System.Collections.Generic;

namespace Model.Common
{
    public interface ICrewmateViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public ILastMissionViewModel LastMission { get; set; }
        public List<IExperienceViewModel> ExperienceList { get; set; }
    }
}
