using System.Web;
using System.Web.Mvc;
using UltimateFestivalOrganizer.WebService.Filters;

namespace UltimateFestivalOrganizer.WebService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           // filters.Add(new BasicAuthorizeAttribute());
        }
    }
}
