using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonoPraksaDay2.Model;

namespace Service.Common
{
    public interface IServiceCommon
    {
        public Task<CrewmateViewModel> GetCrewmateByIdAsync(Guid id);
        public Task<List<CrewmateViewModel>> GetCrewmatesAsync(string firstName = null, string lastName = null, int age = 0);
        public Task<int> PutCrewmateAsync(Guid id, CrewmateViewModel crewmate);
        public Task<int> DeleteCrewmateAsync(Guid id);
        public Task<int> PostCrewmateAsync(CrewmateViewModel crewmate);
    }
}
