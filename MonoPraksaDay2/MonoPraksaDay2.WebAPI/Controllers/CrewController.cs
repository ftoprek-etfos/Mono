using MonoPraksaDay2.WebAPI.Help;
using MonoPraksaDay2.WebAPI.Models;
using Npgsql;
using NpgsqlTypes;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace MonoPraksaDay2.WebAPI.Controllers
{
    public class CrewController : ApiController
    {
        // GET: api/Crew
        [HttpGet]
        public HttpResponseMessage GetCrewList(string firstName = null, string lastName = null, int age = 0)
        {
            CrewmateService crewmateService = new CrewmateService();
            List<CrewmateViewModel> crewmateList = crewmateService.GetCrewmates(firstName, lastName, age);

            if(crewmateList == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No crewmates found!");

            List<GetCrewmateViewModel> getCrewmateList = new List<GetCrewmateViewModel>();
            foreach(CrewmateViewModel crewmate in crewmateList)
            {
                getCrewmateList.Add(new GetCrewmateViewModel(
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
        public HttpResponseMessage GetCrewmate(Guid id)
        {
            CrewmateService crewmateService = new CrewmateService();

            CrewmateViewModel crewmate = crewmateService.GetCrewmateById(id);

            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Crewmate not found under id {id}!");

            GetCrewmateViewModel getCrewmate = new GetCrewmateViewModel(crewmate.FirstName, crewmate.LastName, crewmate.Age, crewmate.LastMission, crewmate.ExperienceList);

            return Request.CreateResponse(HttpStatusCode.OK, getCrewmate);
        }


        // POST: api/Crew
        [HttpPost]
        public HttpResponseMessage PostCrewmate(PostCrewmateViewModel crewmate)
        {
            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide data for a crewmate");

            CrewmateViewModel crewmateToCreate = new CrewmateViewModel(crewmate.FirstName, crewmate.LastName, crewmate.Age);

            CrewmateService crewmateService = new CrewmateService();

            int result = crewmateService.PostCrewmate(crewmateToCreate);

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
        public HttpResponseMessage PutEditCrewmate(Guid id, [FromBody] PutCrewmateViewModel crewmate)
        {
            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide an edit to the crewmate");

            CrewmateViewModel toEditCrewmate = new CrewmateViewModel(id, null, null, 0, crewmate.LastMission, crewmate.ExperienceList);

            CrewmateService crewmateService = new CrewmateService();

            int result = crewmateService.PutCrewmate(id, toEditCrewmate);

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
        public HttpResponseMessage DeleteCrewmate(Guid id)
        {
            CrewmateService crewmateService = new CrewmateService();

            int result = crewmateService.DeleteCrewmate(id);

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