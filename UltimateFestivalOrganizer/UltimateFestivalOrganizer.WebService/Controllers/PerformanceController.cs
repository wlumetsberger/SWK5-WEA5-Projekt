using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.DAL.Common.Domain;
using UltimateFestivalOrganizer.WebService.Filters;
using UltimateFestivalOrganizer.WebService.Models;

namespace UltimateFestivalOrganizer.WebService.Controllers
{
    public class PerformanceController : ApiController
    {
        private IQueryService service;

        public PerformanceController()
        {
            service = ServiceFactory.GetQueryService();
        }
        [Route("api/performance/getAll")]
        public IList<Performance> GetAll()
        {
            return service.QueryPerformances();
        }
        [Route("api/performance/getByDay")]
        public IList<Performance> GetPerformance(DateTime day)
        {
            return service.QueryPerfomancesByDay(day);
        }
        [Route("api/performance/getByCatagory")]
        public IList<Performance> GetPerformanceByCatagory(int catagoryId)
        {
            return service.QueryPerfomancesByCatagory(catagoryId);
        }
        [Route("api/performance/getByVenue")]
        public IList<Performance> GetPerformancesByVenue(int venueId)
        {
            return service.QueryPerfomancesByVenue(venueId);
        }
        [Route("api/performance/getByArtist")]
        public IList<Performance> GetPerformancesByArtist(int artistId)
        {
            return service.QueryPerfomancesByArtist(artistId);
        }

        // POST api/values
        [BasicAuthorize]
        [HttpPost]
        [Route("api/performance/postpone")]
        public Boolean  DoPostpone([FromBody]PostponePerformance value)
        {
            return service.PostponePerformance(value.Id, value.StagingTime, value.VenueId);
     
        }

        [BasicAuthorize]
        [HttpPost]
        [Route("api/performance/postponeCheck")]
        public Boolean CheckPostpone([FromBody] PostponePerformance value)
        {
            return service.CheckPostponeIsPossible(value.Id, value.StagingTime, value.VenueId);
        }

        [BasicAuthorize]
        [HttpPost]
        [Route("api/performance/cancel")]
        public Boolean CanclePerformance([FromBody] PostponePerformance value)
        {
            return service.CanclePerformance(value.Id);
        }
    }
}
