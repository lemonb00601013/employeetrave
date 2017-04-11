using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC_HW3.Models;
using MVC_HW3.ViewModel;

namespace MVC_HW3.Areas.Forum.Controllers
{
    public class ForumMyselfController : Controller
    {
        private TravelEntities db = new TravelEntities();

        // GET: Forum/ForumMyself

        
        public ActionResult Index(int? id = 0)
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "ForumHome");
            }

            ForumModel vm = new ForumModel();

            string epid = Request.Cookies["account"].Value;
            var EPid = db.tEmployee.Where(p => p.fEp_Code == epid).Single().fEp_ID;

            vm.Fclass = db.tForumClass.Where(p => p.fFC_Dad == null);
            if (id == 0)
            {
                vm.Ftitle = db.tForumTitle.Where(p=>p.fEp_ID == EPid);
            }
            else
                vm.Ftitle = db.tForumTitle.Where(p => p.tForumClass.fFC_Dad== id & p.fEp_ID == EPid);

            return View(vm);
        }
        

        public ActionResult EditDetail(int? id = 17)
        {
            ForumModel vm = new ForumModel();
            vm.Fclass = db.tForumClass.Where(p => p.fFC_Dad == null);

            vm.pushGood = db.tPushGood;
            vm.Fmessage = db.tForumMessage.Where(p => p.fMC_ID == id);
            vm.Ftitle = db.tForumTitle.Where(p => p.fMC_ID == id);
            vm.employees = db.tEmployee.Where(p => p.tForumMessage.First().fMC_ID == id);

            return View(vm);
        }

        //新增留言的部分
        [HttpGet]
        public ActionResult partial_mytwitter(int? id = 1)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult partial_mytwitter(tForumMessage message, int? id = 1)
        {
            var MC_ID = db.tForumMessage.Where(p => p.fFM_ID == id).Single().fMC_ID;

            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Detail", "ForumHome", new { id = MC_ID });
            }

            string epid = Request.Cookies["account"].Value;
            var EP_ID = db.tEmployee.Where(p => p.fEp_Code == epid).Single().fEp_ID;

            message.fEp_ID = EP_ID;
            message.fFM_Dad = id;
            message.fFM_Date = DateTime.Now;
            message.fMC_ID = MC_ID;

            db.tForumMessage.Add(message);
            db.SaveChanges();
            return PartialView();

            //return RedirectToAction("EditDetail", "ForumMyself", new { id = MC_ID });
        }




        //修改文章
        [HttpGet]
        public ActionResult EditMessage(int id = 10)
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "ForumHome");
            }

            ForumModel vm = new ForumModel();

            var MC_ID = db.tForumMessage.Where(p => p.fFM_ID == id).Single().fMC_ID;

            vm.Fmessage = db.tForumMessage.Where(p => p.fFM_ID == id);

            vm.Ftitle = db.tForumTitle.Where(p => p.fMC_ID == MC_ID);
            vm.employees = db.tEmployee.Where(p => p.tForumMessage.First().fMC_ID == MC_ID);

            return View(vm);
        }

        [HttpPost]
        public ActionResult EditMessage(tForumMessage message,int id = 10)
        {
            var MC_ID = db.tForumMessage.Where(p => p.fFM_ID == id).Single().fMC_ID;

            tForumMessage updateMessage = db.tForumMessage.Find(id);

            updateMessage.fFM_Content = message.fFM_Content;
            updateMessage.fFM_Date = DateTime.Now;

            db.Entry(updateMessage).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("EditDetail", "ForumMyself", new { id = MC_ID });
        }

        //刪除文章
        public ActionResult Delete(int? id)
        {
            var FTid = db.tForumTitle.Where(p => p.fMC_ID == id).Single().fFT_ID;
            var FMid= db.tForumMessage.Where(p => p.fMC_ID == id).Single().fFM_ID;

            tMessageCode deleCode = db.tMessageCode.Find(id);
            tForumTitle deleTitle = db.tForumTitle.Find(FTid);
            tForumMessage deleMessage = db.tForumMessage.Find(FMid);

            db.tMessageCode.Remove(deleCode);
            db.tForumTitle.Remove(deleTitle);
            db.tForumMessage.Remove(deleMessage);

            db.SaveChanges();


            return RedirectToAction("Index", "ForumMyself");
        }
    }


    
}