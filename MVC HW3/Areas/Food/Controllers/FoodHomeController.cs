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
            int max = (int)db.tRestaurant.Select(p => p.fR_Price).Max();
            int min = (int)db.tRestaurant.Select(p => p.fR_Price).Min();
            List<SelectListItem> dropdownlistPrice = new List<SelectListItem>();
            
            if (min < 100) { min = 100; }
            else if (min % 100 == 0) {}
            else if (min > 100) { min = min + (100 - (min % 100)); }

            if (max % 100 == 0) { }
            else { max = max + (100 - (max % 100));}


            int midprice = 100;
            if (max / min <= 10) { midprice = 100; }
            else if ((max / min > 10) && (max / min <= 15)) { midprice = 150; }
            else { midprice = 200;}

            int count = max / min;

            min = 0;
            dropdownlistPrice.Add(new SelectListItem()
            {
                Text = "無限制",
                Value = min.ToString() + "~" + (max.ToString())
            });
            for (int i = 0; i <= count - 1; i++)
            {
                dropdownlistPrice.Add(new SelectListItem()
                {
                    Text =min.ToString()+"$~"+(min+midprice).ToString()+"$",
                    Value = min.ToString() + "~" + (min + midprice).ToString()
                });
                min = min + midprice;
            }

            ViewBag.dropdownPrice = dropdownlistPrice;
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