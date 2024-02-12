using MonoPraksaDay2.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IRepositoryCommon
    {
        public Task<CrewmateViewModel> GetCrewmateByIdAsync(Guid id);
        public Task<List<CrewmateViewModel>> GetCrewmatesAsync(string firstName = null, string lastName = null, int age = 0);
        public Task<int> PutCrewmateAsync(Guid id, CrewmateViewModel crewmate);
        public Task<int> DeleteCrewmateAsync(Guid id);
        public Task<int> PostCrewmateAsync(CrewmateViewModel crewmate);

    }
}
