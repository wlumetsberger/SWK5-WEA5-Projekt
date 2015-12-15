using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateFestivalOrganizer.BusinessLogik
{
    public static class ServiceFactory
    {
        private static IAdministrationServices service;

        public static IAdministrationServices GetAdministrationService()
        {
            if(service == null)
            {
                service = new AdministrationServices();
            }
            return service;
        }

    }
}
