using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TMS.Models;

namespace TMS.Controllers.DataControllers
{
    public class AgentVenueDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public IHttpActionResult ListAgentVenue(int id)
        {
            var agentVenues = db.AgentVenuesAssosiations.ToList();

            List<AgentVenueAssociationDto> agentVenueDtos = new List<AgentVenueAssociationDto>();

            agentVenues.ForEach(av =>
            {
                if (agentVenueDtos.Count(x => x.Id == av.Id) == 0)
                    if (av.AgentId.Equals(id))
                        agentVenueDtos.Add(new AgentVenueAssociationDto()
                        {
                            Id = av.Id,
                            AgentId = av.AgentId,
                            VenueId = av.VenueId,
                            AgentDetail = av.AgentDetail,
                            VenueDetail = av.VenueDetail
                        });
            });

            return Ok(agentVenueDtos);
        }

        [ResponseType(typeof(AgentVenuesAssosiation))]
        [HttpPost]
        public IHttpActionResult AddAgentVenue(AgentVenuesAssosiation agentVenue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AgentVenuesAssosiations.Add(agentVenue);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agentVenue.Id }, agentVenue); ;
        }

        [HttpGet]
        public IHttpActionResult ReleaseAgentProperty(int id)
        {
            AgentVenuesAssosiation agentVenue = db.AgentVenuesAssosiations.Find(id);
            if (agentVenue == null)
            {
                return NotFound();
            }

            db.AgentVenuesAssosiations.Remove(agentVenue);
            db.SaveChanges();

            return Ok(agentVenue);
        }

        [ResponseType(typeof(AgentVenuesAssosiation))]
        [HttpGet]
        public IHttpActionResult FindAgentVenue(int id)
        {
            AgentVenuesAssosiation agentVenue = db.AgentVenuesAssosiations.Find(id);
            if (agentVenue == null)
            {
                return NotFound();
            }

            AgentVenueAssociationDto agentVenueDto = new AgentVenueAssociationDto()
            {
                Id = agentVenue.Id,
                AgentId = agentVenue.AgentId,
                VenueId = agentVenue.VenueId,
                AgentDetail = agentVenue.AgentDetail,
                VenueDetail = agentVenue.VenueDetail
            };

            return Ok(agentVenueDto);
        }
    }
}
