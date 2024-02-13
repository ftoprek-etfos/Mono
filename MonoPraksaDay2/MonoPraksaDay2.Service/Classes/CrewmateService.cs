using MonoPraksaDay2.Repository;
using MonoPraksaDay2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Common;
using Repository.Common;
using MonoPraksaDay2.Common;

namespace Service
{
    public class CrewmateService : IServiceCommon
    {
        protected IRepositoryCommon CrewmateRepository { get; set; }

        public CrewmateService(IRepositoryCommon repository)
        {
            CrewmateRepository = repository;
        }

        public async Task<CrewmateViewModel> GetCrewmateByIdAsync(Guid id)
        {
            return await CrewmateRepository.GetCrewmateByIdAsync(id);
        }

        public Task<List<CrewmateViewModel>> GetCrewmatesAsync(CrewmateFilter crewmateFilter, Paging paging, Sorting sorting)
        {
            return CrewmateRepository.GetCrewmatesAsync(crewmateFilter, paging, sorting);
        }

        public async Task<int> PutCrewmateAsync(Guid id, CrewmateViewModel crewmate)
        {
            return await CrewmateRepository.PutCrewmateAsync(id, crewmate);
        }

        public async Task<int> DeleteCrewmateAsync(Guid id)
        {
            return await CrewmateRepository.DeleteCrewmateAsync(id);
        }

        public Task<int> PostCrewmateAsync(CrewmateViewModel crewmate)
        {
            return CrewmateRepository.PostCrewmateAsync(crewmate);
        }
    }
}
