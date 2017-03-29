using MVC_HW3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Areas.Food.Controllers
{
    public class FoodHomeController : Controller
    {
        private TravelEntities db = new TravelEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.tRestaurant.ToList());
        }

        public ActionResult checkid(int id = 1)
        {

            return View(db.tMealClass.Where(MC => MC.fML_ID == id));
        }

        [ChildActionOnly]
        public ActionResult MealListSelect()
        {
            return PartialView(db.tMealList.ToList());
        }

        [ChildActionOnly]
        public ActionResult MealListSelecthorizontal()
        {
            return PartialView(db.tMealList.ToList());
        }

        public ActionResult GetImageByte(int id)
        {
            tRestaurant imgRestaurant = db.tRestaurant.Find(id);
            byte[] img = imgRestaurant.fRe_Image;
            return File(img, "image/jpeg");
        }


    }
}