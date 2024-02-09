using MonoPraksaDay2.WebAPI.Models;
using Npgsql;
using System;
using System.Collections.Generic;

namespace MonoPraksaDay2.Repository
{
    internal interface IRepositoryCommon
    {
        public CrewmateViewModel GetCrewmateById(Guid id);
        public List<CrewmateViewModel> GetCrewmates(string firstName = null, string lastName = null, int age = 0);
        public int PutCrewmate(Guid id, CrewmateViewModel crewmate);
        public int DeleteCrewmate(Guid id);
        public int PostCrewmate(CrewmateViewModel crewmate);

    }
}
