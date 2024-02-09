using MonoPraksaDay2.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
