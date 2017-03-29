using MVC_HW3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MVC_HW3.Areas.Food.Controllers
{
    public class RestaurantController : Controller
    {
        private TravelEntities db = new TravelEntities();


        // GET: Restaurant
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult AddRestaurant()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddRestaurant(tRestaurant restaurant, HttpPostedFileBase RestaurantImg, List<string> chkfoodclass, string rd1, int fEp_ID = 2)
        {
            if (ModelState.IsValid)
            {
                if (RestaurantImg != null && RestaurantImg.ContentLength > 0)
                {   

                    //先在messageCode建立一個流水號
                    tMessageCode newCode = new tMessageCode();
                    db.tMessageCode.Add(newCode);
                    db.SaveChanges();

                    restaurant.fMC_ID = db.tMessageCode.ToList().Last().fMC_ID;
                    //將上傳的圖轉成二進位
                    var imgSize = RestaurantImg.ContentLength;
                    byte[] imgByte = new byte[imgSize];
                    RestaurantImg.InputStream.Read(imgByte, 0, imgSize);
                    restaurant.fRe_Image = imgByte;

                    //將tRe_site地址轉座標
                    var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}", Uri.EscapeDataString(restaurant.fRe_Addr));
                    var request = WebRequest.Create(requestUri);
                    var response = request.GetResponse();
                    var xdoc = XDocument.Load(response.GetResponseStream());

                    var result = xdoc.Element("GeocodeResponse").Element("result");
                    var locationElement = result.Element("geometry").Element("location");
                    var lat = locationElement.Element("lat");
                    var lng = locationElement.Element("lng");

                    double latitude = Double.Parse(lat.Value);
                    double lngitude = Double.Parse(lng.Value);
                    restaurant.fRe_site = String.Format("{0},{1}", Convert.ToString(latitude), Convert.ToString(lngitude));
                  
                    //後來要看使用者是誰 先給EPID預設值2
                    restaurant.fEp_ID = fEp_ID;

                    if (rd1 == "Yes") { restaurant.fRe_Delivery = true; } else { restaurant.fRe_Delivery = false; }

                    db.tRestaurant.Add(restaurant);
                    db.SaveChanges();

                    //meal篩選
                    tMealClass newMealC = new tMealClass();
            
                    int RestaurantLastID = db.tRestaurant.AsEnumerable().Last().fRe_ID;
                    foreach (string mealidnum in chkfoodclass)
                    {   
                        newMealC.fML_ID = Convert.ToInt32(mealidnum);
                        newMealC.fRe_ID = RestaurantLastID;
                        db.tMealClass.Add(newMealC);
                        
                    }

                        db.SaveChanges();
                    return RedirectToAction("Index", "FoodHome");
                }
                else
                {
                    ViewBag.message = "請選擇圖檔!!";
                }
            }
            ViewBag.datas = db.tRestaurant.ToList();
            return RedirectToAction("Index", "FoodHome");


        }

        public ActionResult RestaurantSelect(int[] id)
        {
            List<tRestaurant> newRestaurant = new List<tRestaurant>();
            List<tMealClass> newR = db.tMealClass.ToList();

            if (id == null)
            {
                return PartialView(db.tRestaurant.ToList());
            }

            foreach (int number in id)
            {
                List<int> mcnum = new List<int>();
                mcnum.AddRange(newR.Where(mc => mc.fML_ID == number).Select(mc => mc.fRe_ID));//找出符合MealListID的REID

                List<tMealClass> newM = new List<tMealClass>();

                foreach (int a in mcnum)
                {
                    newM.AddRange(newR.Where(mc => mc.fRe_ID == a));
                }
                newR = newM;
            }
            foreach (int newRnum in newR.Select(a => a.fRe_ID).Distinct())
            {
                newRestaurant.Add(db.tRestaurant.Where(r => r.fRe_ID == newRnum).Single());
            }

            return PartialView(newRestaurant);
        }
       
        public ActionResult RestaurantDetail(int?id)
        {
            ViewBag.imgId = id;
            return View(db.tRestaurant.Find(id));
        }

        private void distance(double lat,double lng) {
            //叡楊座標
            double centerLat = 25.066952;
            double centerLng = 121.525006;
            //相加相減後的XY值
            double distanceX = 0;
            double distanceY = 0;
            int distanceActually = 0;

            if ((lat > 0) && (centerLat > lat)) { distanceX = centerLat - lat; }
            else if ((lat > 0) && (centerLat < lat)) { distanceX = lat - centerLat; }
            else{ distanceX = centerLat + lat; }

            if ((lng > 0) && (centerLng > lng)) { distanceY = centerLng - lng; }
            else if ((lng > 0) && (centerLng < lng)) { distanceY = lng - centerLng; }
            else { distanceY = centerLat + lng; }

            distanceActually = ((int)Math.Sqrt((distanceX*distanceX) + (distanceY*distanceY)));
            
        }

    }
}