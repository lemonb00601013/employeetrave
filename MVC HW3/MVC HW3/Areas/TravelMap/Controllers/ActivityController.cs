using MVC_HW3.Models;
using MVC_HW3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_HW3.Areas.TravelMap.Controllers
{
    public class ActivityController : Controller
    {

        TravelEntities db = new TravelEntities();
        // GET: Activity

        [HttpGet]
        public ActionResult Index()
        {
            ActivityModel vm = new ActivityModel();
            vm.ActList = db.tActivityList.ToList();
            ViewBag.city = db.tCityCountry.Where(c => c.fCC_Dad == 1).ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(tCityCountry _city,tSampleProposal _proposal,List<string> ActCheckbox)
        {
            _proposal.fCC_ID = _city.fCC_ID;
            _proposal.fSP_Date = DateTime.Now;
            db.tSampleProposal.Add(_proposal);
            db.SaveChanges();
            int a = db.tSampleProposal.AsEnumerable().Last().fSP_ID;
            
            tActivitySelect _actselect = new tActivitySelect();
            foreach (string _actlist in ActCheckbox)
            {
                _actselect.fSP_ID= a;
                _actselect.fAL_ID =Convert.ToInt32(_actlist);
                db.tActivitySelect.Add(_actselect);
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Activity");
        }

    }
}