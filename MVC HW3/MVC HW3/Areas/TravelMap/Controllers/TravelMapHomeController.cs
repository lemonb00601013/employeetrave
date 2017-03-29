using MVC_HW3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Areas.TravelMap.Controllers
{
    public class TravelMapHomeController : Controller
    {
        private TravelEntities db = new TravelEntities();
        // GET: Home
        public ActionResult Index(int CountryId = 0)
        {
            var city = db.tCityCountry.Where(c => c.fCC_Dad == CountryId).ToList();
            return View(city);
        }


        public ActionResult country()
        {
            var country = db.tCityCountry.Where(c => c.fCC_Dad == null).ToList();

            return PartialView(country);
        }

    }
}