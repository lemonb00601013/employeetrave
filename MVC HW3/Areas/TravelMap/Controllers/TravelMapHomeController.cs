using MVC_HW3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MVC_HW3.Areas.TravelMap.Controllers
{
    public class TravelMapHomeController : Controller
    {
        private TravelEntities db = new TravelEntities();
        // GET: Home
        public ActionResult Index(int CountryId = 0,string searchCity="")
        {
            if(searchCity != "")
            {
                var city = db.tCityCountry.Where(c => c.fCC_Place == searchCity).ToList();
                return View(city);
            }
            else
            {
                var city = db.tCityCountry.Where(c => c.fCC_Dad == CountryId).ToList();
                return View(city);
            }
            
        }


        public ActionResult country()
        {
            var country = db.tCityCountry.Where(c => c.fCC_Dad == null).ToList();

            return PartialView(country);
        }

        public ActionResult GetImageByte(int id)
        {
            tCityCountry _citycountry = db.tCityCountry.Find(id);
            byte[] img = _citycountry.fCC_Img;
            try
            {
                return File(img, "image/jpeg");
            }
            catch
            {
                return View();
            }
            
        }

        //新增旅遊城市
        [HttpGet]
        public ActionResult CreateCity()
        {
            return PartialView();
        }
        
        [HttpPost]
        public ActionResult CreateCity(tCityCountry _city, HttpPostedFileBase fImage,string fCountry,string fCity)
        {
            if (ModelState.IsValid)
            {
                if (fImage != null && fImage.ContentLength > 0)
                {
                    //檢查國家是否重複新增
                    //若有則直接新增城市
                    //若無則先新增國家在新增城市
                    var _country = from c in db.tCityCountry
                                   where (c.fCC_Place == fCountry)
                                   select c;

                    if (_country.Count() == 0)
                    {
                        _city.fCC_Dad = null;
                        _city.fCC_Place = fCountry;

                        db.tCityCountry.Add(_city);
                        db.SaveChanges();

                        tCityCountry tcc = new tCityCountry();
                        tcc.fCC_Dad = db.tCityCountry.AsEnumerable().Last().fCC_ID;
                        tcc.fCC_Place = fCity;

                        //將上傳的圖轉成二進位
                        var imgSize = fImage.ContentLength;
                        byte[] imgByte = new byte[imgSize];
                        fImage.InputStream.Read(imgByte, 0, imgSize);
                        tcc.fCC_Img = imgByte;

                        db.tCityCountry.Add(tcc);
                        db.SaveChanges();
                    }
                    else
                    {
                        _city.fCC_Dad = _country.Single().fCC_ID;
                        _city.fCC_Place = fCity;

                        //將上傳的圖轉成二進位
                        var imgSize = fImage.ContentLength;
                        byte[] imgByte = new byte[imgSize];
                        fImage.InputStream.Read(imgByte, 0, imgSize);
                        _city.fCC_Img = imgByte;

                        db.tCityCountry.Add(_city);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "TravelMapHome");
                }
                else
                {
                    ViewBag.message = "請選擇圖檔!!";
                }
            }
            return View();
        }

        //顯示城市資料
        public ActionResult ShowAll()
        {
            return View(db.tCityCountry.Where(a => a.fCC_Dad != null).ToList());
        }

        public ActionResult Delete(int id = 0)
        {
            db.tCityCountry.Remove(db.tCityCountry.Find(id));
            db.SaveChanges();

            return RedirectToAction("ShowAll");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            return View(db.tCityCountry.Where(a=>a.fCC_ID==id).ToList());
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase fImage)
        {
            tCityCountry _ct = db.tCityCountry.Find(Convert.ToInt32(Request.Form["cityID"]));
            _ct.fCC_Place = Request.Form["CityName"];

            //將上傳的圖轉成二進位
            var imgSize = fImage.ContentLength;
            byte[] imgByte = new byte[imgSize];
            fImage.InputStream.Read(imgByte, 0, imgSize);
            _ct.fCC_Img = imgByte;


            db.Entry(_ct).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ShowAll");
        }
    }
}