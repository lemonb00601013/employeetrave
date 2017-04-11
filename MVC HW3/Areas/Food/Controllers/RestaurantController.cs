using MVC_HW3.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                        db.SaveChanges();
                    }

                    
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
      
        public ActionResult RestaurantSelect(int[] id,string distancemap,string price)
        {
                List<tRestaurant> newRestaurant = new List<tRestaurant>();
                List<tMealClass> newR = db.tMealClass.ToList();

                int maxPrice = (int)db.tRestaurant.Select(p => p.fR_Price).Max();
                //id 距離 價錢皆無
                if ((id == null) && (distancemap == "" || distancemap == null)&&(price== null||price==("0~"+maxPrice.ToString())))
                {

                    return PartialView(db.tRestaurant.ToList());
                }
                if ((id == null) && (distancemap == "" || distancemap == null) && (price != null))
                {
                newRestaurant = db.tRestaurant.ToList();
                List<tRestaurant> allneedtoRemoverestaurant = new List<tRestaurant>();
                string[] splitPrice = price.Split('~');

                foreach (tRestaurant r in newRestaurant)
                {
                    if (Convert.ToInt32(r.fR_Price) < Convert.ToInt32(splitPrice[0]) || (Convert.ToInt32(r.fR_Price) > Convert.ToInt32(splitPrice[1])))
                    {
                        allneedtoRemoverestaurant.Add(r);
                    }
                }
                foreach (tRestaurant rr in allneedtoRemoverestaurant)
                {
                    newRestaurant.Remove(rr);
                }
                return PartialView(newRestaurant);
                }

            //id無 距離有
            if ((id == null) && (distancemap != "" || distancemap != null))
                {
                    if (id == null) { newRestaurant = db.tRestaurant.ToList(); }

                    List<tRestaurant> allneedtoRemoverestaurant = new List<tRestaurant>();
                    foreach (string site in newRestaurant.Select(r => r.fRe_site))
                    {
                        List<string[]> xy = new List<string[]>();
                        xy.Add(site.Split(','));
                        double aa = Convert.ToDouble(xy[0][0].ToString());
                        double bb = Convert.ToDouble(xy[0][1].ToString());
                        if (distance(aa, bb, distancemap)) { }
                        else
                        {
                            tRestaurant r = new tRestaurant();
                            r = newRestaurant.Where(d => d.fRe_site == site).Single();
                            allneedtoRemoverestaurant.Add(r);
                        }
                    }

                foreach (tRestaurant rr in allneedtoRemoverestaurant)
                {
                    newRestaurant.Remove(rr);
                }

                allneedtoRemoverestaurant = new List<tRestaurant>();

                string[] splitPrice = price.Split('~');

                foreach (tRestaurant r in newRestaurant)
                {
                    if (Convert.ToInt32(r.fR_Price) < Convert.ToInt32(splitPrice[0]) || (Convert.ToInt32(r.fR_Price) > Convert.ToInt32(splitPrice[1])))
                    {
                        allneedtoRemoverestaurant.Add(r);
                    }
                }
                foreach (tRestaurant rr in allneedtoRemoverestaurant)
                {
                    newRestaurant.Remove(rr);
                }

                return PartialView(newRestaurant);
                }
                //id有 距離無
                else if (id != null && distancemap == "" || distancemap == null)
                {

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

                List<tRestaurant> allneedtoRemoverestaurant = new List<tRestaurant>();
                string[] splitPrice = price.Split('~');

                foreach (tRestaurant r in newRestaurant)
                {
                    if (Convert.ToInt32(r.fR_Price) < Convert.ToInt32(splitPrice[0]) || (Convert.ToInt32(r.fR_Price) > Convert.ToInt32(splitPrice[1])))
                    {
                        allneedtoRemoverestaurant.Add(r);
                    }
                }
                foreach (tRestaurant rr in allneedtoRemoverestaurant)
                {
                    newRestaurant.Remove(rr);
                }


                return PartialView(newRestaurant);
                }
                //id有 距離有
                else if (Convert.ToDouble(distancemap) > 0)
                {
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

                    List<tRestaurant> allneedtoRemoverestaurant = new List<tRestaurant>();
                    foreach (string site in newRestaurant.Select(r => r.fRe_site))
                    {
                        List<string[]> xy = new List<string[]>();
                        xy.Add(site.Split(','));
                        double aa = Convert.ToDouble(xy[0][0].ToString());
                        double bb = Convert.ToDouble(xy[0][1].ToString());
                        if (distance(aa, bb, distancemap)) { }
                        else
                        {
                            tRestaurant r = new tRestaurant();
                            r = newRestaurant.Where(d => d.fRe_site == site).Single();
                        allneedtoRemoverestaurant.Add(r);
                        }
                    }
                    foreach (tRestaurant rr in allneedtoRemoverestaurant)
                    {
                        newRestaurant.Remove(rr);
                    }
                allneedtoRemoverestaurant = new List<tRestaurant>();
                string[] splitPrice = price.Split('~');
                foreach (tRestaurant r in newRestaurant)
                {
                    if (Convert.ToInt32(r.fR_Price) < Convert.ToInt32(splitPrice[0]) || (Convert.ToInt32(r.fR_Price) > Convert.ToInt32(splitPrice[1])))
                    {
                        allneedtoRemoverestaurant.Add(r);
                    }
                }
                foreach (tRestaurant rr in allneedtoRemoverestaurant)
                {
                    newRestaurant.Remove(rr);
                }
                return PartialView(newRestaurant);
                }
                return PartialView(db.tRestaurant.ToList());
            
           
        }
       
        public ActionResult RestaurantDetail(int?id)
        {
            ViewBag.imgId = id;
            return PartialView(db.tRestaurant.Find(id));
        }



        private const double EARTH_RADIUS = 6378.137;
        private double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        //算距離 如果座標大於所選範圍回傳false
        private bool distance(double lat, double lng,string distancemap)
        {
            //叡楊座標
            double centerLat = 25.066952;
            double centerLng = 121.525006;
            double radLat1 = rad(centerLat);
            double radLat2 = rad(lat);
            double a = radLat1 - radLat2;
            double b = rad(centerLng) - rad(lng);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            
            if (s > Convert.ToDouble(distancemap)/1000) { return false; }
            else{ return true; }
        }
        //判斷是不是福委
        [HttpGet]
        public ActionResult EditorRestaurant()
        {
            string account = Request.Cookies["account"].Value;
            int id = db.tEmployee.Where(e => e.fEp_Code == account).Single().fEp_ID;            
            if (db.tEmployee.Find(id).fId_ID == 3) { return View(db.tRestaurant.ToList()); }
            else { return View(db.tRestaurant.Where(r => r.fEp_ID == id)); }
        }

        //GET修改
        public ActionResult editordetailA(int? id)
        {            
            return PartialView(db.tRestaurant.Find(id));
        }

        
        //POST修改
        public ActionResult editordetailB(string rd2, HttpPostedFileBase updateImg,tRestaurant introduction)
        {   
            tRestaurant newRestaurant =db.tRestaurant.Find(Convert.ToInt32(Request.Form["RestaurantID"]));
            
            newRestaurant.fRe_Name = Request.Form["RestaurantName"];
            newRestaurant.fR_Price = Convert.ToDecimal(Request.Form["RestaurantPrice"]);
            newRestaurant.fRe_Tel = Request.Form["RestaurantTel"];
            if (rd2 == "Yes") { newRestaurant.fRe_Delivery = true; }
            else { newRestaurant.fRe_Delivery = false;}
            newRestaurant.fRe_Content = Request.Form["RestaurantContent"];
            newRestaurant.fRe_Addr = Request.Form["RestaurantAddr"];
            newRestaurant.fRe_introduction = introduction.fRe_introduction;
            //修改相片
            if (updateImg != null && updateImg.ContentLength > 0)
            {
                var imgSize = updateImg.ContentLength;
                byte[] imgByte = new byte[imgSize];
                updateImg.InputStream.Read(imgByte, 0, imgSize);
                newRestaurant.fRe_Image = imgByte;
            }
            string account = Request.Cookies["account"].Value;
            int id = db.tEmployee.Where(e => e.fEp_Code == account).Single().fEp_ID;
            newRestaurant.fEp_ID = id;

            //修改座標
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}", Uri.EscapeDataString(Request.Form["RestaurantAddr"]));
            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var result = xdoc.Element("GeocodeResponse").Element("result");
            var locationElement = result.Element("geometry").Element("location");
            var lat = locationElement.Element("lat");
            var lng = locationElement.Element("lng");

            double latitude = Double.Parse(lat.Value);
            double lngitude = Double.Parse(lng.Value);
            newRestaurant.fRe_site = String.Format("{0},{1}", Convert.ToString(latitude), Convert.ToString(lngitude));


            db.Entry(newRestaurant).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("editorRestaurant");
        }
        //刪除
        public ActionResult Delete(int id)
        {
            tMealClass removeMealClass = db.tMealClass.Where(m => m.fRe_ID == id).Single();
            db.tMealClass.Remove(removeMealClass);
            db.SaveChanges();

            db.tRestaurant.Remove(db.tRestaurant.Find(id));
            db.SaveChanges();
            //轉到Index Action顯示刪除完的結果
            return new EmptyResult();
        }


        [HttpPost]
        public ActionResult UpLoadToDB(HttpPostedFileBase upload, string CKEditorFuncNum)
        {
            //只針對「~/userfiles/images」資料夾
            if (Directory.Exists(Server.MapPath("~/userfiles/images")) == false)
            {
                Directory.CreateDirectory(Server.MapPath("~/userfiles/images"));
            }

            if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/userfiles/images"), upload.FileName)))
            {
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/userfiles/images"), upload.FileName));
            }

            upload.SaveAs(Path.Combine(Server.MapPath("~/userfiles/images"), upload.FileName));

            //組回傳字串，假設不會有錯誤訊息
            string fileName = upload.FileName;
            string sPath = @"""/userfiles/images/" + fileName + @"""";

            string startTag = @"<script type = ""text/javascript"">";
            string endTag = "</script>";
            string contentBefore = @"window.parent.CKEDITOR.tools.callFunction( """;
            string contentAfter = @""", " + sPath + @", """" );";

            string result = startTag + contentBefore + CKEditorFuncNum + contentAfter + endTag;

            return Content(result, "text/html");
        }

    }
}