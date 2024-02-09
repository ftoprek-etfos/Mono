using MonoPraksaDay2.Repository;
using MonoPraksaDay2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CrewmateService
    {

        public CrewmateViewModel GetCrewmateById(Guid id)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return crewmateRepository.GetCrewmateById(id);
        }

        public List<CrewmateViewModel> GetCrewmates(string firstName = null, string lastName = null, int age = 0)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return crewmateRepository.GetCrewmates(firstName, lastName, age);
        }

        public int PutCrewmate(Guid id, CrewmateViewModel crewmate)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return crewmateRepository.PutCrewmate(id, crewmate);
        }

        public int DeleteCrewmate(Guid id)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return crewmateRepository.DeleteCrewmate(id);
        }

        public int PostCrewmate(CrewmateViewModel crewmate)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return crewmateRepository.PostCrewmate(crewmate);
        }
    }
}
