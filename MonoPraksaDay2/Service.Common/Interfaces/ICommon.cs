using System;
using System.Collections.Generic;
using MonoPraksaDay2.Model;

namespace Service.Interfaces
{
    internal interface ICommon
    {
        public CrewmateViewModel GetCrewmateById(Guid id);
        public List<CrewmateViewModel> GetCrewmates(string firstName = null, string lastName = null, int age = 0);
        public int PutCrewmate(Guid id, CrewmateViewModel crewmate);
        public int DeleteCrewmate(Guid id);
        public int PostCrewmate(CrewmateViewModel crewmate);
    }
}
