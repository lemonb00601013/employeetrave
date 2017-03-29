using MVC_HW3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MVC_HW3.Controllers
{
    public class HomeController : Controller
    {
        TravelEntities db = new TravelEntities();
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
        [ChildActionOnly]
        public ActionResult Home_Bulletin()
        {
            return PartialView(db.tBulletin.OrderByDescending(a=>a.fBu_Date).Take(5).ToList());
        }
      
        [HttpPost]
        public ActionResult trylog(string EmployeeID, string password, string remember)
        {
            var ep = db.tEmployee.Where(a => a.fEp_Code == EmployeeID && a.fEp_Password == password);
            if (ep.Any())
            {
                Response.Cookies["account"].Value = EmployeeID;
                if (ep.Single().fId_ID == 3)
                {
                    Response.Cookies["identity"].Value = "Manager";
                }
                if (remember == "yes")
                {
                    Response.Cookies["account"].Expires = DateTime.MaxValue;
                }
            }

            return new EmptyResult();
        }

        public ActionResult logout()
        {
            Response.Cookies["account"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Manergers"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Connection()
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.datas = db.tConnectionClass.Where(a => a.fCC_Class != "訊息").ToList();

            return View();
        }

        [HttpPost]
        public ActionResult Connection(tConnection Connect, HttpPostedFileBase Image)
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    //將上傳的圖轉成二進位
                    var imgSize = Image.ContentLength;
                    byte[] imgByte = new byte[imgSize];
                    Image.InputStream.Read(imgByte, 0, imgSize);
                    Connect.fCo_Image = imgByte;
                }
                string epID = Request.Cookies["account"].Value;
                Connect.fEp_ID = db.tEmployee.Where(a => a.fEp_Code == epID).Single().fEp_ID;
                Connect.fCo_Date = DateTime.Now;
                db.tConnection.Add(Connect);
                db.SaveChanges();
                ViewBag.message = "訊息已送出,感謝您的聯絡!!";

            }
            
            ViewBag.datas = db.tConnectionClass.Where(a => a.fCC_Class != "訊息").ToList();
            return View();
        }
        [HttpGet]
        public ActionResult Bulletin(int? page,int? ID)
        {
            if (ID == null)
            {
                ViewBag.Class = "最新消息";
                return View(db.tBulletin.AsQueryable().OrderByDescending(a=>a.fBu_Date).ToPagedList(page ?? 1,10));
            }
            else
            {
                ViewBag.Class =db.tBulletinClass.Where(a=>a.fBC_ID==ID).Single().fBC_Class;
                return View(db.tBulletin.Where(a => a.fBC_ID==ID).AsQueryable().OrderByDescending(a => a.fBu_Date).ToPagedList(page ?? 1, 10));
            }
            

        }
        public ActionResult BulletinDetail(int ID)
        {            
            return View(db.tBulletin.Find(ID));
        }

        public ActionResult GetImageFile(string fileName)
        {
            string Furl = Url.Content("~/Images/" + fileName + ".png");
            return File(Furl, "image/png");
        }

        //[ChildActionOnly]
        public ActionResult Marquee(int No)
        {
            if (No == 5)
            {
                No = 0;
            }
            
            return PartialView(db.tBulletin.OrderByDescending(a => a.fBu_Date).Skip(No).First());
        }
    }
}