using System.Web.Mvc;

namespace MVC_HW3.Areas.EmployeeTrave
{
    public class EmployeeTraveAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EmployeeTrave";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EmployeeTrave_default",
                "EmployeeTrave/{controller}/{action}/{id}",
                new { Controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}