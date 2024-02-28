using MonoPraksaDay2.Model;
using MonoPraksaDay2.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Service.Common;
using MonoPraksaDay2.Common;

namespace MonoPraksaDay2.WebAPI.Controllers
{
    public class CrewController : ApiController
    {
        protected IServiceCommon CrewmateService { get; set; }

        public CrewController(IServiceCommon crewService)
        {
            CrewmateService = crewService;
        }
        // GET: api/Crew    
        [HttpGet]
        public async Task<HttpResponseMessage> GetCrewListAsync(string firstName = null, string lastName = null, int? age = null, Guid? lastMissionId = null,
            int pageSize = 3, int pageNumber = 1, string orderBy = "Age", string sortOrder = "ASC")
        {
            CrewmateFilter crewmateFilter = new CrewmateFilter(firstName, lastName, age, lastMissionId);
            Paging paging = new Paging(pageNumber, pageSize);
            Sorting sorting = new Sorting(orderBy, sortOrder);

            PagedList<Crewmate> crewmateList = await CrewmateService.GetCrewmatesAsync(crewmateFilter, paging, sorting);

            if(crewmateList == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No crewmates found!");

            PagedList<GetCrewmateViewModel> getCrewmateList = new PagedList<GetCrewmateViewModel>();
            getCrewmateList.List = new List<GetCrewmateViewModel>();
            getCrewmateList.TotalCount = crewmateList.TotalCount;
            getCrewmateList.PageSize = crewmateList.PageSize;
            getCrewmateList.PageCount = crewmateList.PageCount;
            foreach (Crewmate crewmate in crewmateList.List)
            {
                getCrewmateList.List.Add(new GetCrewmateViewModel(
                    crewmate.Id,
                    crewmate.FirstName,
                    crewmate.LastName,
                    crewmate.Age,
                    crewmate.LastMission,
                    crewmate.ExperienceList
                    ));
            }

            return Request.CreateResponse(HttpStatusCode.OK, getCrewmateList);
        }

        // GET: api/Crew/1
        [HttpGet]
        public async Task<HttpResponseMessage> GetCrewmateAsync(Guid id)
        {
            Crewmate crewmate = await CrewmateService.GetCrewmateByIdAsync(id);

            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Crewmate not found under id {id}!");

            GetCrewmateViewModel getCrewmate = new GetCrewmateViewModel(crewmate.Id, crewmate.FirstName, crewmate.LastName, crewmate.Age, crewmate.LastMission, crewmate.ExperienceList);

            return Request.CreateResponse(HttpStatusCode.OK, getCrewmate);
        }


        // POST: api/Crew
        [HttpPost]
        public async Task<HttpResponseMessage> PostCrewmateAsync(PostCrewmateViewModel crewmate)
        {
            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide data for a crewmate");

            Crewmate crewmateToCreate = new Crewmate(crewmate.FirstName, crewmate.LastName, crewmate.Age);

            int result = await CrewmateService.PostCrewmateAsync(crewmateToCreate);

            switch (result)
            {
                case 0:
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Crewmate was not created!");
                case 1:
                    return Request.CreateResponse(HttpStatusCode.OK, $"Added a new crewmate {crewmate.FirstName}");
                default:
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error creating a crewmate");
            }

        }
        
        // PUT: api/Crew/id
        [HttpPut]
        public async Task<HttpResponseMessage> PutEditCrewmateAsync(Guid id, [FromBody] PutCrewmateViewModel crewmate)
        {
            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide an edit to the crewmate");

            Crewmate toEditCrewmate = new Crewmate(id, crewmate.LastMission, crewmate.ExperienceList);

            int result = await CrewmateService.PutCrewmateAsync(id, toEditCrewmate);

            switch (result)
            {
                case 0: 
                    return Request.CreateResponse(HttpStatusCode.BadRequest, $"Crewmate to edit not found");
                case 1:
                    return Request.CreateResponse(HttpStatusCode.OK, $"Crewmate edited successfully");
                default:
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error editing a crewmate");
            }
        }

        // DELETE: api/Crew/id
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteCrewmateAsync(Guid id)
        {
            int result = await CrewmateService.DeleteCrewmateAsync(id);

            switch (result)
            {
                case 0:
                    return Request.CreateResponse(HttpStatusCode.NotFound, $"Crewmate not found");
                case 1:
                    return Request.CreateResponse(HttpStatusCode.OK, $"Crewmate deleted successfully");
                default:
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Error deleting a crewmate");
            }
        }
    }
}