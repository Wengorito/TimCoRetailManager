using System.Web;
using System.Web.Mvc;

namespace TRMWebUI_NET4._7._2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
