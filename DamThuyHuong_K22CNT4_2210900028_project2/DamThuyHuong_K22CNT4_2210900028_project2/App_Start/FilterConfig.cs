using System.Web;
using System.Web.Mvc;

namespace DamThuyHuong_K22CNT4_2210900028_project2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
