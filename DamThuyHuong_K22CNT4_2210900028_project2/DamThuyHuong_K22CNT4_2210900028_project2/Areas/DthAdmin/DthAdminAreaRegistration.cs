using System.Web.Mvc;

namespace DamThuyHuong_K22CNT4_2210900028_project2.Areas.DthAdmin
{
    public class DthAdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DthAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DthAdmin_default",
                "DthAdmin/{controller}/{action}/{id}",
                new { action = "DthIndex",Controller="DthDashboard", id = UrlParameter.Optional }
            );
        }
    }
}