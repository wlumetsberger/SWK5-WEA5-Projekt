using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UltimateFestivalOrganizer.WebService.Filters;

namespace UltimateFestivalOrganizer.WebService.Controllers
{
    
    public class UserController : ApiController
    {
        /// <summary>
        /// GET api/user/authenticate
        /// </summary>
        /// <returns>Boolean if BasicAuthentication was successful, if not Route will throw 401</returns>
        [BasicAuthorizeAttribute]
        [Route("api/user/authenticate")]
        public Boolean Get()
        {
            return true;
        }


    }
}
