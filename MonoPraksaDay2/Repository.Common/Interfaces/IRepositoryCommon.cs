using MonoPraksaDay2.Common;
using MonoPraksaDay2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IRepositoryCommon
    {
        public Task<Crewmate> GetCrewmateByIdAsync(Guid id);
        public Task<PagedList<Crewmate>> GetCrewmatesAsync(CrewmateFilter crewmateFilter, Paging paging, Sorting sorting);
        public Task<int> PutCrewmateAsync(Guid id, Crewmate crewmate);
        public Task<int> DeleteCrewmateAsync(Guid id);
        public Task<int> PostCrewmateAsync(Crewmate crewmate);

    }
}
