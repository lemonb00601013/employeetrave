using MVC_HW3.Models;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Collections.Generic;
using System;

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

        public ActionResult NewCase(int? ID,int page=1 )
        {
            List<string> status= new List<string>();
                List<int> PerCount = new List<int>();
            int Sum = 0;
            if (ID == null)
            {
               
                foreach (int A in db.tTravelCase.Where(a => a.fTC_LorD == 0 && a.tRegistrationOpen.Any()).OrderBy(a=>a.fTC_ID).Skip((page-1)*10).Take(10).Select(a=>a.fTC_ID))
                {
                    if (db.tRegistrationOpen.Where(a => a.fTC_ID == A).OrderByDescending(a => a.fRO_CDate).First().fRO_CDate > DateTime.Now)
                    {
                        status.Add("報名開放中("+db.tRegistrationOpen.Where(a=>a.fTC_ID==A).AsEnumerable().Last().fRO_CDate.ToString()+"截止)");
                    }
                    else
                    {
                        status.Add("報名已截止");
                    }
                    
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
                ViewBag.status = status;
                ViewBag.Class = "近期旅遊";

                return View(db.tTravelCase.Where(a => a.fTC_LorD == 0 && a.tRegistrationOpen.Any()).AsQueryable().OrderByDescending(a => a.fTC_ID).ToPagedList(1, 10));

            }
            else
            {
                foreach (int A in db.tTravelCase.Where(a => a.fTC_LorD >0).OrderBy(a => a.fTC_ID).Skip((page - 1) * 10).Take(10).Select(a => a.fTC_ID))
                {
                  
                    
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
        public ActionResult CaseDetail(int id)
        {
            return View(db.tTravelCase.Find(id));
        }
       
        public ActionResult Sign_up(int id)
        {
            string Ep_Code=Request.Cookies["account"].Value;
            tEmployee Ep = db.tEmployee.Where(a => a.fEp_Code == Ep_Code).Single();
            ViewBag.TC_ID = id;
            ViewBag.Ep_Code = Ep_Code;
            ViewBag.Ep_Name =Ep.fEp_Name;
            ViewBag.Ep_TEL = Ep.fEp_Phone;
            return View();
        }
        
    }
}