using System.Web;
using System.Web.Mvc;

namespace _5204_Passion_Project_n01442368_v2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
