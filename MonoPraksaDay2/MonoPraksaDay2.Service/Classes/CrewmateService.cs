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

        public async Task<CrewmateViewModel> GetCrewmateByIdAsync(Guid id)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return await crewmateRepository.GetCrewmateByIdAsync(id);
        }

        public Task<List<CrewmateViewModel>> GetCrewmatesAsync(string firstName = null, string lastName = null, int age = 0)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return crewmateRepository.GetCrewmatesAsync(firstName, lastName, age);
        }

        public async Task<int> PutCrewmateAsync(Guid id, CrewmateViewModel crewmate)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return await crewmateRepository.PutCrewmateAsync(id, crewmate);
        }

        public async Task<int> DeleteCrewmateAsync(Guid id)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return await crewmateRepository.DeleteCrewmateAsync(id);
        }

        public Task<int> PostCrewmateAsync(CrewmateViewModel crewmate)
        {
            CrewmateRepository crewmateRepository = new CrewmateRepository();
            return crewmateRepository.PostCrewmateAsync(crewmate);
        }
    }
}
