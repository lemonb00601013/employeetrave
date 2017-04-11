using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVC_HW3.Models;
using MVC_HW3.ViewModel;

namespace MVC_HW3.Areas.Forum.Controllers
{
    public class ForumAdminController : Controller
    {
        TravelEntities db = new TravelEntities();

        // GET: Forum/ForumAdmin
        public ActionResult Index(int? id = 0)
        {
            if (Request.Cookies["account"] == null /*&& Request.Cookies["Mangers"]==null*/)
            {
                return RedirectToAction("Index", "ForumHome");
            }

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

        [HttpGet]
        public ActionResult AddClass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddClass(tForumClass Fclass, HttpPostedFileBase ClassImage)
        {
            if (ModelState.IsValid)
            {
                if (ClassImage != null && ClassImage.ContentLength > 0)
                {
                    
                    var imgSize = ClassImage.ContentLength;
                    byte[] imgByte = new byte[imgSize];
                    ClassImage.InputStream.Read(imgByte, 0, imgSize);
                    Fclass.fFC_Picture = imgByte;

                    db.tForumClass.Add(Fclass);
                    db.SaveChanges();

                    return RedirectToAction("Index", "ForumHome");
                }
                else
                {
                    ViewBag.message = "請選擇圖檔!!";
                }
            }
            return View();
        }



        public ActionResult AdminDetail()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}