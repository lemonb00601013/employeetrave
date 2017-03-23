using MVC_HW3.Areas.EmployeeTrave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Areas.EmployeeTrave.Controllers
{
    public class TraveCaseController : Controller
    {
        TravelEntities2 db = new TravelEntities2();
        // GET: EmployeeTrave/TraveCase
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Home_Case()
        {
            
            return PartialView(db.tTravelCase.Where(a=>a.fTC_LorD==0&&a.tRegistrationOpen.Any()).ToList());
        }
        public ActionResult GetImage(int id)
        {
            tTravelCase photo = db.tTravelCase.Find(id);
            byte[] file = photo.fTC_Picture;

            return File(file, "image/jpeg");
        }
    }
}