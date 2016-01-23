using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public static class ServiceFactory
    {
        private static IAdministrationServices administrationService;
        private static IAuthenticationService authenticationService;
        private static IQueryService queryService;

        public static IAdministrationServices GetAdministrationService()
        {
            if(administrationService == null)
            {
                administrationService = new AdministrationServices();
            }
            return administrationService;
        }

        public static IAuthenticationService GetAuthenticationService()
        {
            if(authenticationService == null)
            {
                authenticationService = new AuthenticationService();
            }
            return authenticationService;
        }

        public static IQueryService GetQueryService()
        {
            if(queryService == null)
            {
                queryService = new QueryService();
            }
            return queryService;
        }



    }
}
