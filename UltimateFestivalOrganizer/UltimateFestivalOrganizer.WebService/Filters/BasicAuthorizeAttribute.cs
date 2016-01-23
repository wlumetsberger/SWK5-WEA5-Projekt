using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Threading;
using System.Text;
using System.Security.Principal;
using System.Net.Http;
using System.Net;
using UltimateFestivalOrganizer.BusinessLogik;


namespace UltimateFestivalOrganizer.WebService.Filters
{
    public class BasicAuthorizeAttribute : AuthorizationFilterAttribute
    {
        private IAuthenticationService authService;

        public BasicAuthorizeAttribute()
        {
            this.authService = ServiceFactory.GetAuthenticationService();
        }
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                Console.WriteLine("user is authenticated");
                return;
            }

            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                !String.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var credArray = GetCredentials(authHeader);
                    var userName = credArray[0];
                    var password = credArray[1];
                        if (authService.AuthenticateUser(userName, password))
                        {
                            var currentPrincipal = new GenericPrincipal(new GenericIdentity(userName), null);
                            Thread.CurrentPrincipal = currentPrincipal;
                            return;
                        }
               }
            }

            HandleUnauthorizedRequest(actionContext);
        }

        private string[] GetCredentials(System.Net.Http.Headers.AuthenticationHeaderValue authHeader)
        {
            var rawCred = authHeader.Parameter;
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var cred = encoding.GetString(Convert.FromBase64String(rawCred));
            var credArray = cred.Split(':');
            return credArray;
        }

        private void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
