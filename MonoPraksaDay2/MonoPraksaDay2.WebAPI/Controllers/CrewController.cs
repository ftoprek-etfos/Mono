using Microsoft.Ajax.Utilities;
using MonoPraksaDay2.WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.UI;

namespace MonoPraksaDay2.WebAPI.Controllers
{
    public class CrewController : ApiController
    {
        static List<CrewmateViewModel> crewList = new List<CrewmateViewModel>();

        // GET: api/Crew
        [HttpGet]
        public HttpResponseMessage GetCrewList(string firstName = null, string lastName = null, int age = 0)
        {
            if (crewList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Crewmate list is empty!");

            List<CrewmateViewModel> toFilterList = crewList;

            if (!string.IsNullOrEmpty(firstName))
            {
                toFilterList.Where(crewmate => crewmate.FirstName == firstName).ToList();
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                toFilterList.Where(crewmate => crewmate.LastName == lastName).ToList();
            }

            if (age > 0)
            {
                toFilterList = toFilterList.Where(crewmate => crewmate.Age == age).ToList();
            }

            List<GetCrewmateViewModel> getFilteredCrewmateList = new List<GetCrewmateViewModel>();
            foreach (CrewmateViewModel crewMember in toFilterList)
            {
                getFilteredCrewmateList.Add(new GetCrewmateViewModel(crewMember.FirstName, crewMember.LastName, crewMember.Age));
            }

            if(Request.Headers.Contains("Admin"))
                return Request.CreateResponse(HttpStatusCode.OK, crewList);
            else
                return Request.CreateResponse(HttpStatusCode.OK, getFilteredCrewmateList);

        }

        // GET: api/Crew/1
        [HttpGet]
        public HttpResponseMessage GetCrewmate(int id)
        {
            if (crewList.Count == 0)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Crewmate list is empty!");
            if(crewList.Where(crewMember => crewMember.Id == id).FirstOrDefault() == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Crewmate not found under id {id}");

            CrewmateViewModel userToFilter = crewList.Where(crewMember => crewMember.Id == id).FirstOrDefault();
            GetCrewmateViewModel filteredUser = new GetCrewmateViewModel(userToFilter.FirstName, userToFilter.LastName, userToFilter.Age);

            return Request.CreateResponse(HttpStatusCode.OK, filteredUser);
        }

        // POST: api/Crew
        [HttpPost]
        public HttpResponseMessage PostCrewmate(PostCrewmateViewModel crewmate)
        {
            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide data for a crewmate");

            crewList.Add(new CrewmateViewModel(crewList.Count > 0 ? crewList.Max(crewMember => crewMember.Id) + 1 : 0, 
                crewmate.FirstName, crewmate.LastName, crewmate.Age));
            return Request.CreateResponse(HttpStatusCode.OK, $"Added a new crewmate {crewmate.FirstName}");
        }

        // PUT: api/Crew/id
        [HttpPut]
        public HttpResponseMessage PutEditCrewmate(int id, [FromBody] PutCrewmateViewModel crewmate)
        {
            if (crewmate == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide an edit to the crewmate");

            int index = 0;

            if (crewList.Where(crewMember => crewMember.Id == id).FirstOrDefault() != null)
            {
                index = crewList.FindIndex(crewMember => crewMember.Id == id);
                crewList[index].LastMission = crewmate.LastMission;
                foreach (ExperienceViewModel experience in crewmate.ExperienceList)
                {
                    if (crewList[index].ExperienceList == null)
                        crewList[index].ExperienceList = new List<ExperienceViewModel>();
                    crewList[index].ExperienceList.Add(experience);
                }

            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide an edit to the crewmate");
            return Request.CreateResponse(HttpStatusCode.OK, $"Crewmate {crewList[index].FirstName} edited successfully");
        }


        // DELETE: api/Crew/id
        [HttpDelete]
        public HttpResponseMessage DeleteCrewmate(int id)
        {
            if (id > crewList.Max(crewmate => crewmate.Id))
                return Request.CreateResponse(HttpStatusCode.NotFound, $"Crewmate not found under id {id}");
            if (crewList.Where(crewMember => crewMember.Id == id).FirstOrDefault() != null)
            {
                int index = crewList.FindIndex(crewMember => crewMember.Id == id);
                crewList.RemoveAt(index);
            }
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"Please provide a crewmate to delete");
            return Request.CreateResponse(HttpStatusCode.OK, $"Crewmate deleted successfully");
        }

    }
}