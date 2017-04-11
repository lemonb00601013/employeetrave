
using MVC_HW3.Models;
using MVC_HW3.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MVC_HW3.Areas.Forum.Controllers
{
    public class ForumHomeController : Controller
    {
        private TravelEntities db = new TravelEntities();

        // GET: Home
        //討論版首頁，可以看到熱門文章與入口
        public ActionResult FirstPage(int? id = 0)
        {
            ForumModel vm = new ForumModel();

            vm.Fclass = db.tForumClass.Where(p=>p.fFC_Dad==null);
           
            return View(vm);
        }

        public ActionResult FPclicktitle(int? id = 0)
        {
            ForumModel vm = new ForumModel();

            vm.Fclass = db.tForumClass;
            vm.Ftitle = db.tForumTitle.AsEnumerable().Where(a => a.fFT_Date > DateTime.Now.AddMonths(-3)).OrderByDescending(a => a.tMessageCode.tForumMessage.First().tPushGood.Count()).Take(5);
            
            return PartialView(vm);
        }
        public ActionResult FPpushgood(int? id = 0)
        {
            ForumModel vm = new ForumModel();

            vm.Fclass = db.tForumClass;
            vm.Ftitle = db.tForumTitle.AsEnumerable().Where(a => a.fFT_Date > DateTime.Now.AddMonths(-3)).OrderByDescending(a => a.fFT_Popul).Take(5);

            return PartialView(vm);
          
        }



        //文章標題首頁
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
            //接著透過cookie找出會員編號，確認讀讚人數
            if (Request.Cookies["account"] != null)
            {
                string epid = Request.Cookies["account"].Value;
                var EP_ID = db.tEmployee.Where(p => p.fEp_Code == epid).Single().fEp_ID;
                
                ViewBag.EPIP = EP_ID;
            }

            var ftID = db.tForumTitle.Where(p => p.fMC_ID == id).First().fFT_ID;
            var addPOP = db.tForumTitle.Where(p => p.fMC_ID == id).First().fFT_Popul;

            tForumTitle updatePopul = db.tForumTitle.Find(ftID);
            updatePopul.fFT_Popul = updatePopul.fFT_Popul + 1;
            db.Entry(updatePopul).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            

            //讀取Detail
            ForumModel vm = new ForumModel();
            vm.Fclass = db.tForumClass.Where(p => p.fFC_Dad == null);

            vm.pushGood = db.tPushGood;
            vm.Fmessage = db.tForumMessage.Where(p => p.fMC_ID == id);
            vm.Ftitle = db.tForumTitle.Where(p => p.fMC_ID == id);
            vm.employees = db.tEmployee.Where(p => p.tForumMessage.First().fMC_ID == id);
            
            return View(vm);
        }

        
        //點讚去
        public ActionResult pushGoodall(tPushGood updatePG, int? id = 1)
        {
            //當然你沒有帳號，會重登一次頁面，之後會要修改成登入頁面
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Detail", "ForumHome", new { id = id });
            }

            //傳值得ID便是文章編號，透過他可找出文章代碼，接著透過cookie找出會員編號，再匯入點讚裡面
            string epid = Request.Cookies["account"].Value;
            var EP_ID = db.tEmployee.Where(p => p.fEp_Code == epid).Single().fEp_ID;
            var MC_ID = db.tForumMessage.Where(p => p.fFM_ID == id).Single().fMC_ID;

            updatePG.fEp_ID = EP_ID;
            updatePG.fFM_ID = id;

            db.tPushGood.Add(updatePG);
            db.SaveChanges();

            
            ForumModel vm = new ForumModel();
            vm.Fmessage = db.tForumMessage.Where(p => p.fFM_ID == id);
            vm.pushGood = db.tPushGood;

            return PartialView(vm);
        }

        //收回點讚
        public ActionResult recoveryGood(tPushGood recoveryPG, int? id = 1)
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Detail", "ForumHome", new { id = id });
            }

            //傳值得ID便是文章編號，以及cookie的員工編號，便可找出他點讚的編號，刪掉就是取消
            string epid = Request.Cookies["account"].Value;
            var EP_ID = db.tEmployee.Where(p => p.fEp_Code == epid).Single().fEp_ID;

            var PGid = db.tPushGood.Where(p => p.fFM_ID == id && p.fEp_ID == EP_ID).Single().fPG_ID;
            
            tPushGood delePG = db.tPushGood.Find(PGid);

            db.tPushGood.Remove(delePG);
            db.SaveChanges();

            ForumModel vm = new ForumModel();
            vm.Fmessage = db.tForumMessage.Where(p => p.fFM_ID == id);
            vm.pushGood = db.tPushGood;

            return PartialView(vm);
        }



        //顯示留言的部分
        public ActionResult twitter_showall(int? id = 1)
        {
            ForumModel vm = new ForumModel();
            vm.Fmessage = db.tForumMessage.Where(p => p.fFM_Dad == id).OrderByDescending(p => p.fFM_Date);
            return PartialView(vm);
        }
        public ActionResult twitter_all(int? id = 1)
        {
            ForumModel vm = new ForumModel();
            vm.Fmessage = db.tForumMessage.Where(p => p.fFM_Dad == id);
            return PartialView(vm);
        }
        public ActionResult twitter_short(int? id = 1)
        {
            ForumModel vm = new ForumModel();
            vm.Fmessage = db.tForumMessage.Where(p => p.fFM_Dad == id).OrderByDescending(p=>p.fFM_Date);
            return PartialView(vm);
        }

        //新增留言的部分
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

            //return PartialView();
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

            var result = db.tForumClass.Where(p => p.fFC_Dad != null).OrderBy(p => p.fFC_ID);
            var items = new List<GroupedSelectListItem>();
            foreach (var select in result)
            {
                items.Add(new GroupedSelectListItem()
                {
                    Value = select.fFC_ID.ToString(),
                    Text = string.Format("{0}",
                        select.fFC_Class),
                    GroupKey = select.tForumClass2.fFC_ID.ToString(),
                    GroupName = select.tForumClass2.fFC_Class
                });
            }

            ViewBag.Items = items;

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
        public ActionResult Message(int id = 12)
        {
            if (Request.Cookies["account"] == null)
            {
                return RedirectToAction("Index", "ForumHome");
            }

            ViewBag.ftitle = db.tForumTitle.Where(p => p.fMC_ID == id).FirstOrDefault().fFT_Title;
            ViewBag.femp = db.tForumTitle.Where(p => p.fMC_ID == id).FirstOrDefault().tEmployee.fEp_Nickname;
            ViewBag.fdate = db.tForumTitle.Where(p => p.fMC_ID == id).FirstOrDefault().fFT_Date;
            ViewBag.fcont = db.tForumMessage.Where(p => p.fMC_ID == id).FirstOrDefault().fFM_Content;

            return View();
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

        //Action=>取得Class圖片
        public ActionResult GetImageClass(int id)
        {
            tForumClass classPicture = db.tForumClass.Find(id);
            byte[] img = classPicture.fFC_Picture;
            return File(img, "image/jpeg");
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