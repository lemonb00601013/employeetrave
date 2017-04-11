using MVC_HW3.Models;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Collections.Generic;
using System;
using System.Web;

namespace MVC_HW3.Areas.EmployeeTrave.Controllers
{
    public class TraveCaseController : Controller
    {
        TravelEntities db = new TravelEntities();
        // GET: EmployeeTrave/TraveCase
        public ActionResult Index()
        {
            return View();
        }
        
        //首頁大廣告
        public ActionResult Home_Case()
        {
            
            return PartialView(db.tTravelCase.Where(a=>a.fTC_LorD==0&&a.tRegistrationOpen.Any()).ToList());
        }
        //讀取二進為圖檔
        public ActionResult GetImage(int id)
        {
            tTravelCase photo = db.tTravelCase.Find(id);
            byte[] file = photo.fTC_Picture;

            return File(file, "image/jpeg");
        }
        
        public ActionResult NewCase(int? ID,int page=1 )
        {
            List<string> status= new List<string>();
                List<int> PerCount = new List<int>();
            //近期旅遊
            if (ID == null)
            {
               
                foreach (int A in db.tTravelCase.Where(a => a.fTC_LorD == 0 && a.tRegistrationOpen.Any()).OrderByDescending(a=>a.fTC_ID).Skip((page-1)*10).Take(10).Select(a=>a.fTC_ID))
                {
                    int Sum = 0;
                    if (db.tRegistrationOpen.Where(a => a.fTC_ID == A).OrderByDescending(a => a.fRO_CDate).First().fRO_CDate > DateTime.Now)
                    {
                        status.Add("報名開放中("+db.tRegistrationOpen.Where(a=>a.fTC_ID==A).AsEnumerable().Last().fRO_CDate.ToString()+"截止)");
                    }
                    else
                    {
                        status.Add("報名已截止");
                    }
                    
                    List<tRegistrationDetail> Case1 = db.tRegistrationDetail.Where(a => a.fTC_ID == A).ToList();
                    Sum += Case1.Where(a=>a.fSt_ID !=5).Count();
                    foreach (int rdID in Case1.Where(a => a.fSt_ID != 5).Select(a => a.fRD_ID))
                    {
                        Sum += db.tFamily.Where(a => a.fRD_ID == rdID && a.fFa_Car == true).Count();
                        Sum += db.tDependentsTravel.Where(a => a.fRD_ID == rdID && a.fDT_Car == true).Count();
                    }
                    PerCount.Add(Sum);
                }
                
               
                ViewBag.PerCount = PerCount;
                ViewBag.status = status;
                ViewBag.Class = "近期旅遊";

                return View(db.tTravelCase.Where(a => a.fTC_LorD == 0 && a.tRegistrationOpen.Any()).AsQueryable().OrderByDescending(a => a.fTC_ID).ToPagedList(1, 10));

            }
            else  //過往旅遊
            {
                foreach (int A in db.tTravelCase.Where(a => a.fTC_LorD >0).OrderByDescending(a => a.fTC_ID).Skip((page - 1) * 10).Take(10).Select(a => a.fTC_ID))
                {

                    int Sum = 0;
                    List<tRegistrationDetail> Case1 = db.tRegistrationDetail.Where(a => a.fTC_ID == A).ToList();
                    Sum += Case1.Count();
                    foreach (int rdID in Case1.Select(a => a.fRD_ID))
                    {
                        Sum += db.tFamily.Where(a => a.fRD_ID == rdID && a.fFa_Car == true).Count();
                        Sum += db.tDependentsTravel.Where(a => a.fRD_ID == rdID && a.fDT_Car == true).Count();
                    }
                    PerCount.Add(Sum);
                }
                ViewBag.PerCount = PerCount;
                ViewBag.Class = "過往旅遊";

                return View(db.tTravelCase.Where(a => a.fTC_LorD>0).AsQueryable().OrderByDescending(a => a.fTC_ID).ToPagedList(1, 10));
            }
            
        }

        //旅遊詳細
        public ActionResult CaseDetail(int id,bool had=false)
        {          
                ViewBag.had = had;
            tTravelCase Status = db.tTravelCase.Find(id);
            if (Status.fTC_LorD != 0 | !Status.tRegistrationOpen.Any())
            {                
                ViewBag.end = true;                
            }
            else {
                if (Status.tRegistrationOpen.OrderByDescending(a => a.fRO_ID).First().fRO_CDate < DateTime.Now)
                {
                    ViewBag.end = true;
                }
                else
                {ViewBag.end = false; }

                 }


            return View(db.tTravelCase.Find(id));
        }
 
        //報名頁面
        public ActionResult Sign_up(int id)
        {
        
            string Ep_Code=Request.Cookies["account"].Value;
            tEmployee Ep = db.tEmployee.Where(a => a.fEp_Code == Ep_Code).Single();

            if (db.tRegistrationDetail.Where(a => a.fEp_ID == Ep.fEp_ID && a.fSt_ID !=5).ToList().Select(a => a.tTravelCase.tDayTravel.Last().fDT_Date.Year).Where(a => a == DateTime.Now.Year).Any())
            {
                return RedirectToAction("CaseDetail", "TraveCase", new { area = "EmployeeTrave",id=id,had=true });
            }

            tTravelCase TC = db.tTravelCase.Find(id);

            int grantyear = 4500;
            int gotday = (((DateTime.Now) - (db.tEmployee.Where(a => a.fEp_ID == Ep.fEp_ID).Single().fEp_Seniority)).Days);
            int grant = gotday > 365 ? grantyear : (gotday / 30 * (grantyear / 12));

            List<tRegistrationDetail> Case1 = db.tRegistrationDetail.Where(a => a.fTC_ID == id).ToList();
            int Sum = 0;
            Sum += Case1.Where(a => a.fSt_ID != 5).Count();
            foreach (int rdID in Case1.Where(a => a.fSt_ID != 5).Select(a => a.fRD_ID))
            {
                Sum += db.tFamily.Where(a => a.fRD_ID == rdID && a.fFa_Car == true).Count();
                Sum += db.tDependentsTravel.Where(a => a.fRD_ID == rdID && a.fDT_Car == true).Count();
            }

            ViewBag.SumNow = Sum;
            ViewBag.RD_Grant = grant;              
            ViewBag.Ep_De = db.tDependents.Where(a => a.fEp_ID == Ep.fEp_ID).ToList();
            ViewBag.Employee = Ep;                   
            ViewBag.TravelCase = TC; 
            

            return View();
        }
        //新增眷屬partialview
        public ActionResult NewDep()
        {            
            return PartialView();
        }
        //新增親屬partialview
        public ActionResult NewFamily()
        {
            return PartialView();
        }
        //檢查報名人數
        public ActionResult PerCount(int id,int per)
        {
            List<tRegistrationDetail> Case1 = db.tRegistrationDetail.Where(a => a.fTC_ID == id).ToList();
            int Sum = 0;
            Sum += Case1.Where(a=>a.fSt_ID !=5).Count();
            foreach (int rdID in Case1.Where(a => a.fSt_ID != 5).Select(a => a.fRD_ID))
            {
                Sum += db.tFamily.Where(a => a.fRD_ID == rdID && a.fFa_Car == true).Count();
                Sum += db.tDependentsTravel.Where(a => a.fRD_ID == rdID && a.fDT_Car == true).Count();
            }
            int TC_Top = db.tTravelCase.Where(a => a.fTC_ID == id).Single().fTC_Top;
            if (TC_Top < (Sum + per))
            { return Content("名額剩餘"+(TC_Top-Sum)+"人,報名人數過多!!"); }
            return Content("OK");
        }
        //員工報名
        public ActionResult EpSign_up(tRegistrationDetail Ep)
        {           
            //Ep.fRD_Grant = grant;
            db.tRegistrationDetail.Add(Ep);
            db.SaveChanges();
            
            return Content(db.tRegistrationDetail.AsEnumerable().Last().fRD_ID.ToString());
        }
        //親友報名
        public ActionResult FaSign_up(tFamily Fa)
        {
           
         
            db.tFamily.Add(Fa);
            db.SaveChanges();

            return new EmptyResult();
        }
        //眷屬報名
        public ActionResult DeSign_up(tDependentsTravel De)
        {


            db.tDependentsTravel.Add(De);
            db.SaveChanges();

            return new EmptyResult();
        }
        //報名費小計partialview
        public ActionResult Sign_Over()
        {            
            return PartialView(db.tRegistrationDetail.AsEnumerable().Last());
        }
        //旅費管理
        public ActionResult ManagePage()
        {
            return View();

        }

        public ActionResult Manage(int? Status)
        {           
            switch (Status)
            {                
                case 1:
                    return PartialView(db.tTravelCase.Where(a=>a.fTC_LorD>0).OrderByDescending(a=>a.fTC_ID).ToList());
                case 2:
                    return PartialView(db.tTravelCase.Where(a=>a.fTC_LorD<0).OrderByDescending(a=>a.fTC_ID).ToList());
                case 3:
                    return PartialView(db.tTravelCase.Where(a => a.fTC_LorD == 0 &&a.tRegistrationOpen.Any()).OrderByDescending(a => a.fTC_ID).ToList());
                case 4:
                    return PartialView(db.tTravelCase.Where(a => a.fTC_LorD == 0 && !a.tRegistrationOpen.Any()).OrderByDescending(a => a.fTC_ID).ToList());
                default:
                    return PartialView(db.tTravelCase.OrderByDescending(a => a.fTC_ID).ToList());
            }           
        }
        public ActionResult TravelEdit(int id)
        {
            ViewBag.datas = db.tCityCountry.Where(a => a.fCC_Dad == 1);

            List<tRegistrationDetail> Case1 = db.tRegistrationDetail.Where(a => a.fTC_ID == id).ToList();
            int Sum = 0;
            int another = 0;
            Sum += Case1.Where(a => a.fSt_ID != 5).Count();
            foreach (int rdID in Case1.Where(a => a.fSt_ID != 5).Select(a => a.fRD_ID))
            {
                Sum += db.tFamily.Where(a => a.fRD_ID == rdID && a.fFa_Car == true).Count();
                another += db.tFamily.Where(a => a.fRD_ID == rdID && a.fFa_Car == false).Count();
                Sum += db.tDependentsTravel.Where(a => a.fRD_ID == rdID && a.fDT_Car == true).Count();
                another += db.tDependentsTravel.Where(a => a.fRD_ID == rdID && a.fDT_Car == false).Count();
            }

            ViewBag.YCount = Sum;
            ViewBag.NCount = another;



            return View(db.tTravelCase.Find(id));
        }

        public ActionResult TravelEditEnd(tTravelCase TCback, HttpPostedFileBase Image)
        {
            int TC_ID = TCback.fTC_ID;
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    //將上傳的圖轉成二進位
                    var imgSize = Image.ContentLength;
                    byte[] imgByte = new byte[imgSize];
                    Image.InputStream.Read(imgByte, 0, imgSize);
                    TCback.fTC_Picture = imgByte;
                }
                
                db.Entry(TCback).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
         
            return RedirectToAction("TravelEdit", "TraveCase", new { area = "EmployeeTrave", id =TC_ID });
        }





        public ActionResult DeleteCase(int id)
        {
            foreach (tRegistrationDetail ep in db.tRegistrationDetail.Where(a => a.fTC_ID == id))
            {
                int RD_ID = ep.fRD_ID;
                foreach (tFamily fa in db.tFamily.Where(a => a.fRD_ID == RD_ID))
                {
                    db.tFamily.Remove(fa);
                }
                foreach (tDependentsTravel dt in db.tDependentsTravel.Where(a => a.fRD_ID == RD_ID))
                {
                    db.tDependentsTravel.Remove(dt);
                }
                db.SaveChanges();
                db.tRegistrationDetail.Remove(ep);
            }
            foreach (tDayTravel DT in db.tDayTravel.Where(a=>a.fTC_ID==id))
            {
                db.tDayTravel.Remove(DT);
            }
            foreach (tRegistrationOpen RO in db.tRegistrationOpen.Where(a => a.fTC_ID == id))
            {
                db.tRegistrationOpen.Remove(RO);
            }
            foreach (tPenalty Pe in db.tPenalty.Where(a => a.fTC_ID == id))
            {
                db.tPenalty.Remove(Pe);
            }
            db.SaveChanges();

            tTravelCase delete= db.tTravelCase.Find(id);
            db.tTravelCase.Remove(delete);
            db.SaveChanges();
            return new EmptyResult();

        }
        public ActionResult CreateCase()
        {
            tTravelCase newtrave = new tTravelCase();
            newtrave.fTC_Title = "新方案";
            newtrave.fTC_Top = 0;
            newtrave.fTC_Gate = 0;
            newtrave.fTC_Cost = 0;
            newtrave.fTC_TDate = "(無行程)";
            newtrave.fTC_Content = "";
            newtrave.fTC_Notes = "";
            newtrave.fTC_LorD = 0;
            newtrave.fCC_ID = 2;
            newtrave.fTC_PerL = 2;
            newtrave.fTC_Car = 0;
            newtrave.fTC_Eat = 0;
            newtrave.fTC_hotel = 0;
            db.tTravelCase.Add(newtrave);
            db.SaveChanges();
            return new EmptyResult();

        }
        //開放報名
        public ActionResult Opentravel(int id)
        {
            
            return PartialView(db.tRegistrationOpen.Where(a=>a.fTC_ID==id).OrderByDescending(a=>a.fRO_CDate).ToList());
        }

      
        public ActionResult OpentravelC(tRegistrationOpen Oback)
        {
            int TC_ID = Oback.fTC_ID;
            if (ModelState.IsValid)
            {
                Oback.fRO_ODate = DateTime.Now;
                db.tRegistrationOpen.Add(Oback);
                db.SaveChanges();             

            }

            return RedirectToAction("TravelEdit", "TraveCase", new { area = "EmployeeTrave", id = TC_ID });
        }
        public ActionResult OpentravelP()
        {
            return PartialView();
        }
        public ActionResult OpentravelD(int id)
        {
            tRegistrationOpen End = db.tRegistrationOpen.Find(id);
            End.fRO_CDate = DateTime.Now;
            db.Entry(End).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            
            return new EmptyResult();

        }


        public ActionResult Penalty(int id)
        {
           
            return PartialView(db.tPenalty.Where(a => a.fTC_ID == id).OrderByDescending(a=>a.fPe_Day).ToList());
        }
        public ActionResult PenaltyP()
        {

            return PartialView();
        }
        public ActionResult PenaltyD(int id)
        {
            db.tPenalty.Remove(db.tPenalty.Find(id));
            db.SaveChanges();
            return new EmptyResult();

        }
        public ActionResult PenaltyC(tPenalty Pback)
        {
            int TC_ID = Pback.fTC_ID;
            if (ModelState.IsValid)
            {

                db.tPenalty.Add(Pback);
                db.SaveChanges();

            }

            return RedirectToAction("TravelEdit", "TraveCase", new { area = "EmployeeTrave", id = TC_ID });
        }
        public ActionResult DayTravel(int id)
        {

            return PartialView(db.tDayTravel.Where(a => a.fTC_ID == id).OrderBy(a=>a.fDT_Date).ToList());
        }
        public ActionResult DayTravelF(int id)
        {

            return PartialView(db.tDayTravel.Find(id));
        }
        public ActionResult DayTravelC(tDayTravel Dback)
        {
            int TC_ID = Dback.fTC_ID;
            if (ModelState.IsValid)
            {

                db.tDayTravel.Add(Dback);
                db.SaveChanges();
                TC_DateNew(TC_ID);
            }

            return RedirectToAction("TravelEdit", "TraveCase", new { area = "EmployeeTrave", id = TC_ID });
        }
        public ActionResult DayTravelE(tDayTravel Dback)
        {
            int TC_ID = Dback.fTC_ID;
            if (ModelState.IsValid)
            {
                db.Entry(Dback).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TC_DateNew(TC_ID);

            }

            return RedirectToAction("TravelEdit", "TraveCase", new { area = "EmployeeTrave", id = TC_ID });
        }
        public ActionResult DayTravelP()
        {

            return PartialView();
        }
        private void TC_DateNew(int TC_ID)
        {
            List<tDayTravel> DList = db.tDayTravel.Where(a => a.fTC_ID == TC_ID).OrderBy(a => a.fDT_Date).ToList();
            if (DList.Count == 0)
            {
                db.tTravelCase.Find(TC_ID).fTC_TDate = "(無行程)";
            }
            else
            {
                if (DList.Count == 1)
                {
                    db.tTravelCase.Find(TC_ID).fTC_TDate = DList[0].fDT_Date.ToShortDateString();
                }
                else { db.tTravelCase.Find(TC_ID).fTC_TDate = DList.First().fDT_Date.ToShortDateString() + "-" + DList.Last().fDT_Date.ToShortDateString(); }


            }
            db.SaveChanges();
        }
        public ActionResult DayTravelD(int id)
        {
            int TC_ID = db.tDayTravel.Find(id).fTC_ID;
            db.tDayTravel.Remove(db.tDayTravel.Find(id));
            db.SaveChanges();
            TC_DateNew(TC_ID);
            return new EmptyResult();

        }
        //流團
        public ActionResult GameOver(int id)
        {
            tTravelCase Over = db.tTravelCase.Find(id);
            Over.fTC_LorD = -1;
            db.Entry(Over).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();            
            return new EmptyResult();

        }
        //出團
        public ActionResult GOGO(int id)
        {
            tTravelCase GO = db.tTravelCase.Find(id);
            GO.fTC_LorD =db.tTravelCase.Where(a=>a.fTC_LorD>0).Count()+1;
            db.Entry(GO).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return new EmptyResult();

        }

        //旅費統計
        public ActionResult TotalMoney(int id)
        {            
            return View(db.tTravelCase.Find(id));
        }
        //報名狀況
        public ActionResult SignStatus(int id)
        {

            return View(db.tTravelCase.Find(id));
        }
        public ActionResult SignSave(tTravelCase TraveCase)
        {
            int TC_ID = TraveCase.fTC_ID;

            return RedirectToAction("SignStatus", "TraveCase", new { area = "EmployeeTrave", id = TC_ID });
        }
    }
}