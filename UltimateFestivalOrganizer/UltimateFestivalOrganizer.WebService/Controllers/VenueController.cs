using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.DAL.Common.Domain;

namespace UltimateFestivalOrganizer.WebService.Controllers
{
    public class VenueController : ApiController
    {
        private IQueryService service;

        public VenueController()
        {
            service = ServiceFactory.GetQueryService();
        }

        [Route("api/venue/getAll")]
        public IList<Venue> GetAll()
        {
            return service.QueryVenues();

        }
    }
}
