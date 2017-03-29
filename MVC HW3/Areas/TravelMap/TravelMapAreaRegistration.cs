using System.Web.Mvc;

namespace MVC_HW3.Areas.TravelMap
{
    public class TravelMapAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TravelMap";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TravelMap_default",
                "TravelMap/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}