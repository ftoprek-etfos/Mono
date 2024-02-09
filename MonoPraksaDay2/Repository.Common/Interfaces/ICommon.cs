using MonoPraksaDay2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface ICommon
    {
        public CrewmateViewModel GetCrewmateById(Guid id);
        public List<CrewmateViewModel> GetCrewmates(string firstName = null, string lastName = null, int age = 0);
        public int PutCrewmate(Guid id, CrewmateViewModel crewmate);
        public int DeleteCrewmate(Guid id);
        public int PostCrewmate(CrewmateViewModel crewmate);

    }
}
