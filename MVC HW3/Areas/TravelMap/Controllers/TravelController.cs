using MVC_HW3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MVC_HW3.Areas.TravelMap
{
    public class TravelController : Controller
    {
        private TravelEntities db = new TravelEntities();
        // GET: Travel
        public ActionResult Index(int ClassId = 0)
        {
            return View(db.tTravelDetail.Where(t => t.fTC_ID == ClassId).ToList());
        }

        public ActionResult GetImageByte(int id)
        {
            tTravelDetail _TravelDetail = db.tTravelDetail.Find(id);
            byte[] img = _TravelDetail.fTD_Image;
            return File(img, "image/jpeg");
        }

        public ActionResult TravelClass()
        {
            return PartialView(db.tTravelClass.ToList());
        }


        [HttpGet]
        public ActionResult CreateDetail()
        {
            ViewBag.tcity = db.tCityCountry.Where(c => c.fCC_Dad != null).ToList();
            ViewBag.tclass = db.tTravelClass.ToList();
            return PartialView();
        }


        [HttpPost]
        public ActionResult CreateDetail(tTravelDetail _TravelDetail, HttpPostedFileBase fImage, string fTD_site)
        {
            if (ModelState.IsValid)
            {
                if (fImage != null && fImage.ContentLength > 0)
                {
                    //將上傳的圖轉成二進位
                    var imgSize = fImage.ContentLength;
                    byte[] imgByte = new byte[imgSize];
                    fImage.InputStream.Read(imgByte, 0, imgSize);
                    _TravelDetail.fTD_Image = imgByte;

                    //將tTD_side地址轉座標
                    var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}", Uri.EscapeDataString(fTD_site));
                    var request = WebRequest.Create(requestUri);
                    var response = request.GetResponse();
                    var xdoc = XDocument.Load(response.GetResponseStream());

                    var result = xdoc.Element("GeocodeResponse").Element("result");
                    var locationElement = result.Element("geometry").Element("location");
                    var lat = locationElement.Element("lat");
                    var lng = locationElement.Element("lng");

                    double latitude = Double.Parse(lat.Value);
                    double lngitude = Double.Parse(lng.Value);
                    _TravelDetail.fTD_site = String.Format("{0},{1}", Convert.ToString(latitude), Convert.ToString(lngitude));
                    db.tTravelDetail.Add(_TravelDetail);
                    db.SaveChanges();

                    return RedirectToAction("Index", "TravelMapHome");
                }
                else
                {
                    ViewBag.message = "請選擇圖檔!!";
                }


            }

            return View();
        }

        
    }
}