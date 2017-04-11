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

        //新增推薦
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
                    tMessageCode _MCode = new tMessageCode();
                    db.tMessageCode.Add(_MCode);
                    db.SaveChanges();

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
                    _TravelDetail.fMC_ID = db.tMessageCode.AsEnumerable().Last().fMC_ID;
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


        //聊天室系統
        public ActionResult Comment(tForumMessage _Comment,int id,string _content ="")
        {
            if (_content != "")
            {
                string epCode = Request.Cookies["account"].Value;
                int epID = db.tEmployee.Where(a => a.fEp_Code == epCode).Single().fEp_ID;
                _Comment.fFM_Date = DateTime.Now;
                _Comment.fFM_Content = _content;
                _Comment.fMC_ID = id;
                _Comment.fEp_ID = epID;
                db.tForumMessage.Add(_Comment);
                db.SaveChanges();
            }
            return PartialView(db.tForumMessage.Where(t => t.fMC_ID == id).OrderBy(t => t.fFM_Date).ToList());
        }


        //評分系統
        public ActionResult GetScore(tDetailScore _DScore,int id,string _starScore="")
        {
            if (_starScore != "")
            {
                string epCode = Request.Cookies["account"].Value;
                int epID = db.tEmployee.Where(q => q.fEp_Code == epCode).Single().fEp_ID;

                //檢查此客戶過去是否已經有評分
                var userScore = from s in db.tDetailScore
                                where (s.fTD_ID == id && s.fEP_ID == epID)
                                select s;
                int uss = userScore.Count();
                //沒有評過分則新增
                if (uss == 0)
                {
                    _DScore.fTD_ID = id;
                    _DScore.fEP_ID = epID;
                    _DScore.fDS_Score = _starScore;
                    db.tDetailScore.Add(_DScore);
                    db.SaveChanges();
                }
                //若有評過分則修改之前的評分
                else
                {
                    _DScore = db.tDetailScore.Find(userScore.Single().fDS_ID);
                    _DScore.fDS_Score = _starScore;
                    db.Entry(_DScore).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            
            //計算推薦地方的平均分數
            db.tDetailScore.Where(s => s.fTD_ID == id);
            var a = from s in db.tDetailScore
                    where (s.fTD_ID == id)
                    select s.fDS_Score;

            double ScoreCount = a.Count();
            double sum = 0;

            foreach (var scoreall in a)
            {
                sum += Convert.ToDouble(scoreall);
                
            }
            if(ScoreCount == 0)
            {
                ViewBag.ScoreAvg = "0";
            }
            else
            {
                ViewBag.ScoreAvg = (sum / ScoreCount).ToString("#0.0");
            }

            //將平均分數記錄到資料庫中，方便做排序
            tTravelDetail TD = db.tTravelDetail.Find(id);
            TD.fTD_Score = ViewBag.ScoreAvg;
            db.Entry(TD).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return PartialView();
        }


        //依照評分系統排序
        public ActionResult ScoreSorting(int ClassID = 0)
        {
            return PartialView(db.tTravelDetail.Where(t => t.fTC_ID == ClassID));
        }

        //依照新增時間排序
        public ActionResult TimeSorting(int ClassID = 0)
        {
            return PartialView(db.tTravelDetail.Where(t => t.fTC_ID == ClassID));
        }
    }
}