using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UltimateFestivalOrganizer.BusinessLogik;
using UltimateFestivalOrganizer.DAL.Common.Domain;
using UltimateFestivalOrganizer.WebService.Filters;

namespace UltimateFestivalOrganizer.WebService.Controllers
{

    public class ArtistController : ApiController
    {
        private IQueryService service;

        public ArtistController()
        {
            service = ServiceFactory.GetQueryService();
        }

        /// <summary>
        /// // GET api/artist/getAll
        /// </summary>
        /// <returns> JSON Array with Artists</returns>
        [Route("api/artist/getAll")]
        public IList<Artist> GetAll()
        {
            return service.QueryArtists();

        }
        /// <summary>
        /// // GET api/artist/getArtistByMail
        /// </summary>
        /// <returns> JSON Array with Artists</returns>
        [Route("api/artist/getArtistByMail")]
        public Artist GetArtist(string id)
        {
            return service.QueryArtistById(id);
        }
    }
}
