using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonoPraksaDay2.Common;
using MonoPraksaDay2.Model;

namespace Service.Common
{
    public interface IServiceCommon
    {
        public Task<CrewmateViewModel> GetCrewmateByIdAsync(Guid id);
        public Task<List<CrewmateViewModel>> GetCrewmatesAsync(CrewmateFilter crewmateFilter, Paging paging, Sorting sorting);
        public Task<int> PutCrewmateAsync(Guid id, CrewmateViewModel crewmate);
        public Task<int> DeleteCrewmateAsync(Guid id);
        public Task<int> PostCrewmateAsync(CrewmateViewModel crewmate);
    }
}
