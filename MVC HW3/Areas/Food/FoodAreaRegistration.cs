﻿using System.Web.Mvc;

namespace MVC_HW3.Areas.Food
{
    public class FoodAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Food";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Food_default",
                "Food/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}