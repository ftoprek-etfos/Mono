using MonoPraksaDay2.Model;
using System;
using System.Collections.Generic;
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

        public async Task<Crewmate> GetCrewmateByIdAsync(Guid id)
        {
            return await CrewmateRepository.GetCrewmateByIdAsync(id);
        }

        public Task<PagedList<Crewmate>> GetCrewmatesAsync(CrewmateFilter crewmateFilter, Paging paging, Sorting sorting)
        {
            return CrewmateRepository.GetCrewmatesAsync(crewmateFilter, paging, sorting);
        }

        public async Task<int> PutCrewmateAsync(Guid id, Crewmate crewmate)
        {
            return await CrewmateRepository.PutCrewmateAsync(id, crewmate);
        }

        public async Task<int> DeleteCrewmateAsync(Guid id)
        {
            return await CrewmateRepository.DeleteCrewmateAsync(id);
        }

        public Task<int> PostCrewmateAsync(Crewmate crewmate)
        {
            return CrewmateRepository.PostCrewmateAsync(crewmate);
        }

        public Task<List<LastMissionViewModel>> GetLastMissionListAsync()
        {
            return CrewmateRepository.GetLastMissionListAsync();
        }

    }
}
