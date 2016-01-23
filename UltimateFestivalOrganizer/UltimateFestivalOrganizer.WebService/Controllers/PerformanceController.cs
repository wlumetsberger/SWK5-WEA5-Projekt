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
        /// <summary>
        /// GEt api/performance/getAll
        /// </summary>
        /// <returns>returns JSON array with all Performances</returns>
        [Route("api/performance/getAll")]
        public IList<Performance> GetAll()
        {
            return service.QueryPerformances();
        }
        /// <summary>
        /// GET api/performance/getByDay
        /// </summary>
        /// <param name="day"></param>
        /// <returns>JSON Array with Performances found for day</returns>
        [Route("api/performance/getByDay")]
        public IList<Performance> GetPerformance(DateTime day)
        {
            return service.QueryPerfomancesByDay(day);
        }
        /// <summary>
        /// GET api/performance/getByCatagory
        /// </summary>
        /// <param name="catagory"></param>
        /// <returns>JSON Array with Performances found for catagory</returns>
        [Route("api/performance/getByCatagory")]
        public IList<Performance> GetPerformanceByCatagory(int catagoryId)
        {
            return service.QueryPerfomancesByCatagory(catagoryId);
        }
        /// <summary>
        /// GET api/performance/getByVenue
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns>JSON Array with Performances found for venue</returns>
        [Route("api/performance/getByVenue")]
        public IList<Performance> GetPerformancesByVenue(int venueId)
        {
            return service.QueryPerfomancesByVenue(venueId);
        }
        /// <summary>
        /// GET api/performance/getByArtist
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns>JSON-Array of Performances found for artist</returns>
        [Route("api/performance/getByArtist")]
        public IList<Performance> GetPerformancesByArtist(int artistId)
        {
            return service.QueryPerfomancesByArtist(artistId);
        }

        /// <summary>
        /// POST api/performance/postpone
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Boolean if Postpone was possible or not</returns>
        [BasicAuthorize]
        [HttpPost]
        [Route("api/performance/postpone")]
        public Boolean  DoPostpone([FromBody]PostponePerformance value)
        {
            return service.PostponePerformance(value.Id, value.StagingTime, value.VenueId);
     
        }
        /// <summary>
        /// POST api/performance/postponeCheck
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Boolean if Postpone of Performance would be possible</returns>
        [BasicAuthorize]
        [HttpPost]
        [Route("api/performance/postponeCheck")]
        public Boolean CheckPostpone([FromBody] PostponePerformance value)
        {
            return service.CheckPostponeIsPossible(value.Id, value.StagingTime, value.VenueId);
        }
        /// <summary>
        /// POST api/performance/cancel
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Boolean if cancle was Possible</returns>
        [BasicAuthorize]
        [HttpPost]
        [Route("api/performance/cancel")]
        public Boolean CanclePerformance([FromBody] PostponePerformance value)
        {
            return service.CanclePerformance(value.Id);
        }
    }
}
