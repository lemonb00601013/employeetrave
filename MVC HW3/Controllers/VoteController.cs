using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_HW3.Models;
using MVC_HW3.ViewModel;
using System.IO;

namespace MVC_HW3.Controllers
{
    public class VoteController : Controller
    {
        TravelEntities db = new TravelEntities();
        // GET: Vote
        public ActionResult Index()
        {
            return View(db.tVoteProject.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( tVoteProject tVoteProjects, HttpPostedFileBase fVP_Imagefile)
        {
            if (ModelState.IsValid)
            {
                if (fVP_Imagefile != null && fVP_Imagefile.ContentLength > 0)
            {
                //檔案上傳
                string strPath = Request.PhysicalApplicationPath + @"fVP_Imagefile\" + fVP_Imagefile.FileName;
                fVP_Imagefile.SaveAs(strPath);
                tVoteProjects.fVP_Imagefile = fVP_Imagefile.FileName;

                //將上傳的圖轉成二進位
                var imgSize = fVP_Imagefile.ContentLength;
                byte[] imgByte = new byte[imgSize];
                fVP_Imagefile.InputStream.Read(imgByte, 0, imgSize);
                tVoteProjects.fVP_Image= imgByte;

                db.tVoteProject.Add(tVoteProjects);
                db.SaveChanges();

                return RedirectToAction("Index", "Vote");
            }
            else
                {
                ViewBag.message = "請選擇圖檔!!";
                }
               
            }
            ViewBag.datas = db.tVoteProject.ToList();
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tVoteProject tVoteProjects = db.tVoteProject.Find(id);
            if (tVoteProjects == null)
            {
                return HttpNotFound();
            }
            return View(tVoteProjects);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fVP_ID,fVP_Name,fVP_Image,fVP_Description,fVP_StartDate,fVP_EndDate,")] tVoteItemOptionViewModel tVoteItemOptionViewModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tVoteItemOptionViewModels).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tVoteItemOptionViewModels);
        }

        //回傳二進位圖
        public ActionResult GetImageByte(int id)
        {
            tVoteProject tVoteProjects = db.tVoteProject.Find(id);
            byte[] img = tVoteProjects.fVP_Image;
            return File(img, "image/jpeg");
        }

        public ActionResult Details(int? id)
        {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                tVoteProject tVoteProjects = db.tVoteProject.Find(id);
                if (tVoteProjects == null)
                {
                    return HttpNotFound();
                }
                return View(tVoteProjects);


        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tVoteProject tVoteProjects = db.tVoteProject.Find(id);
            if (tVoteProjects == null)
            {
                return HttpNotFound();
            }
            return View(tVoteProjects);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tVoteProject tVoteProjects = db.tVoteProject.Find(id);
            db.tVoteProject.Remove(tVoteProjects);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //public ActionResult ItemType(int? id)
        //{
            

        //    //ViewBag.id = id;
        //    //ViewBag.datas = db.tVoteType.ToList();
        //    //tVoteType tVoteTypes = new tVoteType();
        //    return View(db.tVoteType.ToList());
        //}

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateItems(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItems(tVoteItem tVoteItems, HttpPostedFileBase fVI_Imagefile)
        {

            if (ModelState.IsValid)
            {
                if (fVI_Imagefile != null && fVI_Imagefile.ContentLength > 0)
                {
                    //檔案上傳
                    string strPath = Request.PhysicalApplicationPath + @"fVI_Imagefile\" + fVI_Imagefile.FileName;
                    fVI_Imagefile.SaveAs(strPath);
                    tVoteItems.fVI_Imagefile = fVI_Imagefile.FileName;

                    //將上傳的圖轉成二進位
                    var imgSize = fVI_Imagefile.ContentLength;
                    byte[] imgByte = new byte[imgSize];
                    fVI_Imagefile.InputStream.Read(imgByte, 0, imgSize);
                    tVoteItems.fVI_Image = imgByte;

                    db.tVoteItem.Add(tVoteItems);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Vote");
                }
                else
                {
                    ViewBag.message = "請選擇圖檔!!";
                }

            }
            ViewBag.datas = db.tVoteProject.ToList();
            return View();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}