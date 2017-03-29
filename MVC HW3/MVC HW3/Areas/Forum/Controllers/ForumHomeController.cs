
using MVC_HW3.Models;
using MVC_HW3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC_HW3.Areas.Forum.Controllers
{
    public class ForumHomeController : Controller
    {
        private TravelEntities db = new TravelEntities();

        // GET: Home
        //管理版首頁，可以看到文章標題
        public ActionResult Index(int? id = 0)
        {
            ForumModel vm = new ForumModel();

            vm.Fclass = db.tForumClass.Where(p => p.fFC_Dad == null);

            if (id == 0)
            {
                vm.Ftitle = db.tForumTitle.ToList();
            }
            else
            {
                vm.Ftitle = db.tForumTitle.Where(p => p.tForumClass.fFC_Dad == id);
            }
            
            return View(vm);
        }

        //文章內部得細項
        public ActionResult Detail(int? id = 10)
        {
            ForumModel vm = new ForumModel();
            vm.Fclass = db.tForumClass.Where(p => p.fFC_Dad == null);

            vm.Fmessage = db.tForumMessage.Where(p => p.fMC_ID == id);
            vm.Ftitle = db.tForumTitle.Where(p => p.fMC_ID == id);
            vm.employees = db.tEmployee.Where(p => p.tForumMessage.First().fMC_ID == id);
            
            return View(vm);
        }

        //推文留言的部分
        [HttpGet]
        public ActionResult partial_twitter(int? id = 1)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult partial_twitter(tForumMessage message, int? id = 1)
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

            return RedirectToAction("Detail", "ForumHome", new { id = MC_ID });
        }

        //新建立一個文章
        [HttpGet]
        public ActionResult Create()
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "ForumHome");
            }

            //var main = db.tForumClass.Where(p => p.fFC_Dad == null);
            //var sub = db.tForumClass.Where(p => p.fFC_Dad == main.FirstOrDefault().fFC_ID);

            //ViewBag.data = db.tForumClass.Where(p => p.fFC_Dad == null);
            ViewBag.datas = db.tForumClass.Where(p => p.fFC_Dad != null);

            
            return View();
        }

        [HttpPost]
        public ActionResult Create(tMessageCode mescode, tForumTitle title, tForumMessage message)
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "ForumHome");
            }

            string epid = Request.Cookies["account"].Value;
            var EP_ID = db.tEmployee.Where(p => p.fEp_Code == epid).Single().fEp_ID;

            db.tMessageCode.Add(mescode);
            db.SaveChanges();

            title.fMC_ID = db.tMessageCode.AsEnumerable().Last().fMC_ID;
            title.fEp_ID = EP_ID;
            title.fFT_Date = DateTime.Now;

            message.fEp_ID = EP_ID;
            message.fFM_Date = DateTime.Now;
            message.fMC_ID = db.tMessageCode.AsEnumerable().Last().fMC_ID;

            db.tForumTitle.Add(title);
            db.tForumMessage.Add(message);

            db.SaveChanges();


            return RedirectToAction("Index", "ForumHome");
        }

        //對一個文章進行回文
        [HttpGet]
        public ActionResult Message(int id = 10)
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "ForumHome");
            }

            ForumModel vm = new ForumModel();

            vm.Fmessage = db.tForumMessage.Where(p => p.fMC_ID == id);
            vm.Ftitle = db.tForumTitle.Where(p => p.fMC_ID == id);
            vm.employees = db.tEmployee.Where(p => p.tForumMessage.First().fMC_ID == id);

            ViewBag.getid = id;

            return View(vm);
        }

        [HttpPost]
        public ActionResult Message(tForumMessage message, int id = 10)
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "ForumHome");
            }

            string epid = Request.Cookies["account"].Value;
            var EP_ID = db.tEmployee.Where(p => p.fEp_Code == epid).Single().fEp_ID;

            message.fEp_ID = EP_ID;
            message.fFM_Date = DateTime.Now;
            message.fMC_ID = id;

            db.tForumMessage.Add(message);
            db.SaveChanges();

            return RedirectToAction("Detail", "ForumHome",new { id=id});
        }

        

        //Action=>取得會員個人照片
        public ActionResult GetImageByte(int id)
        {
            tEmployee empPicture = db.tEmployee.Find(id);
            byte[] img = empPicture.fEp_Picture;
            return File(img,"image/jpeg");
        }

    }
}