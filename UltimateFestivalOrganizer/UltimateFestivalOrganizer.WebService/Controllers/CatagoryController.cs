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
    public class CatagoryController : ApiController
    {
        private IQueryService service;

        public CatagoryController()
        {
            service = ServiceFactory.GetQueryService();
        }
        /// <summary>
        /// GET /api/catagory/getAll
        /// </summary>
        /// <returns>JSON Array with Catagories</returns>
        [Route("api/catagory/getAll")]
        public IList<Catagory> GetAll()
        {
            return service.QueryCatagories();

        }
    }
}
