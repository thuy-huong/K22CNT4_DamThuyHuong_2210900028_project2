using System.Web;
using System.Web.Mvc;

namespace K22CNT4_DamThuyHuong_2210900028_Project2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
