using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonoPraksaDay2.Common;
using MonoPraksaDay2.Model;

namespace Service.Common
{
    public interface IServiceCommon
    {
        public Task<Crewmate> GetCrewmateByIdAsync(Guid id);
        public Task<PagedList<Crewmate>> GetCrewmatesAsync(CrewmateFilter crewmateFilter, Paging paging, Sorting sorting);
        public Task<int> PutCrewmateAsync(Guid id, Crewmate crewmate);
        public Task<int> DeleteCrewmateAsync(Guid id);
        public Task<int> PostCrewmateAsync(Crewmate crewmate);
    }
}
